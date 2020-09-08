using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetOpnApiBuilder.Enums;

namespace NetOpnApiBuilder.Models
{
    public class ApiExporter
    {
        private readonly BuilderDb _db;
        private readonly ILogger   _logger;
        private readonly object    _threadLock   = new object();
        private          bool      _initializing = false;

        public ApiExporter(BuilderDb db, ILogger<ApiExporter> logger)
        {
            _db     = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private bool _initialized = false;


        public bool                         Initialized              => _initialized;
        public IReadOnlyList<ApiCommand>    Commands                 { get; private set; }
        public IReadOnlyList<ApiObjectType> Types                    { get; private set; }
        public IReadOnlyList<ApiCommand>    CommandsMissingData      { get; private set; }
        public IReadOnlyList<ApiCommand>    CommandsMissingTypes     { get; private set; }
        public IReadOnlyList<ApiCommand>    CommandsWithInvalidTypes { get; private set; }
        public IReadOnlyList<ApiObjectType> TypesWithMissingTypes    { get; private set; }
        public IReadOnlyList<ApiObjectType> TypesWithInvalidTypes    { get; private set; }

        public bool Ready =>
            Initialized
            && Commands.Any()
            && !CommandsMissingData.Any()
            && !CommandsMissingTypes.Any()
            && !CommandsWithInvalidTypes.Any()
            && !TypesWithMissingTypes.Any()
            && !TypesWithInvalidTypes.Any();

        public async Task InitAsync()
        {
            // ensure initialization only happens once.
            waitForInitialization:
            while (_initializing) Thread.Sleep(1);

            lock (_threadLock)
            {
                if (_initialized) return;
                if (_initializing) goto waitForInitialization;
                _initializing = true;
            }

            try
            {
                Commands = await _db.ApiCommands
                                    .Include(x => x.Controller)
                                    .ThenInclude(x => x.Module)
                                    .ThenInclude(x => x.Source)
                                    .Where(x => x.Skip == false && x.Controller.Skip == false && x.Controller.Module.Skip == false)
                                    .ToListAsync();

                _logger.LogInformation($"Found {Commands.Count} commands ready for export.");

                CommandsMissingData = Commands
                                      .Where(x => x.HasMissingData)
                                      .ToArray();

                if (CommandsMissingData.Any())
                {
                    _logger.LogInformation($"Found {CommandsMissingData.Count} commands missing configuration data.");
                }

                CommandsMissingTypes = Commands
                                       .Where(
                                           x =>
                                               ((x.ResponseBodyDataType & ApiDataType.Object) == ApiDataType.Object && x.ResponseBodyObjectTypeID is null)
                                               || ((x.UsePost && (x.PostBodyDataType & ApiDataType.Object) == ApiDataType.Object && x.PostBodyObjectTypeID is null))
                                       )
                                       .ToArray();

                if (CommandsMissingTypes.Any())
                {
                    _logger.LogInformation($"Found {CommandsMissingTypes} commands missing object type values.");
                }

                var allTypes = await _db.ApiObjectTypes
                                        .Include(x => x.Properties)
                                        .ToListAsync();

                _logger.LogDebug($"Found {allTypes.Count} defined types.");

                CommandsWithInvalidTypes = Commands
                                           .Where(
                                               x =>
                                                   (x.ResponseBodyObjectTypeID != null && !allTypes.Any(y => y.ID == x.ResponseBodyObjectTypeID))
                                                   || (x.UsePost && x.PostBodyObjectTypeID != null && !allTypes.Any(y => y.ID == x.PostBodyObjectTypeID))
                                           )
                                           .ToArray();

                if (CommandsWithInvalidTypes.Any())
                {
                    _logger.LogWarning($"Found {CommandsWithInvalidTypes.Count} commands with invalid object type values.");
                }

                int[] GetUsedTypes(IEnumerable<ApiObjectType> types)
                    => Commands
                       // used as a return value.
                       .Where(x => x.ResponseBodyObjectTypeID.HasValue)
                       .Select(x => x.ResponseBodyObjectTypeID.Value)
                       .Union(
                           Commands
                               // used as a post body.
                               .Where(x => x.UsePost && x.PostBodyObjectTypeID.HasValue)
                               .Select(x => x.PostBodyObjectTypeID.Value)
                       )
                       .Union(
                           types
                               .SelectMany(x => x.Properties)
                               // used as a property value from a different type.
                               .Where(x => x.DataTypeObjectTypeID.HasValue && x.DataTypeObjectTypeID != x.ObjectTypeID)
                               .Select(x => x.DataTypeObjectTypeID.Value)
                       )
                       .Distinct()
                       .OrderBy(x => x)
                       .ToArray();

                var usedTypes = GetUsedTypes(allTypes);

                // remove unused types.
                var removed = allTypes.RemoveAll(x => !usedTypes.Contains(x.ID));
                while (removed > 0)
                {
                    usedTypes = GetUsedTypes(allTypes);
                    removed   = allTypes.RemoveAll(x => !usedTypes.Contains(x.ID));
                }

                Types = allTypes.ToArray();
                _logger.LogInformation($"Found {Types.Count} types to export.");

                TypesWithMissingTypes = Types
                                        .Where(
                                            x => x.Properties
                                                  .Any(y => (y.DataType & ApiDataType.Object) == ApiDataType.Object && y.DataTypeObjectTypeID is null)
                                        )
                                        .ToArray();

                if (TypesWithMissingTypes.Any())
                {
                    _logger.LogInformation($"Found {TypesWithMissingTypes.Count} types missing object type values.");
                }

                TypesWithInvalidTypes = Types
                                        .Where(
                                            x => x.Properties
                                                  .Any(y => !Types.Any(z => z.ID == y.DataTypeObjectTypeID))
                                        )
                                        .ToArray();

                if (TypesWithInvalidTypes.Any())
                {
                    _logger.LogWarning($"Found {TypesWithInvalidTypes.Count} types with invalid object type values.");
                }

                _initialized = true;
            }
            finally
            {
                _initializing = false;
            }
        }
        
        
    }
}
