using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace NetOpnApi.Commands
{
    /// <summary>
    /// Base command type for built-in commands.
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        private static string BaseTypeNamespace { get; }

        static BaseCommand()
        {
            var nm   = typeof(BaseCommand).FullName ?? throw new Exception("Failed to determine namespace for base command type.");
            var last = nm.LastIndexOf('.');
            BaseTypeNamespace = nm.Substring(0, last + 1);
        }

        /// <inheritdoc />
        public string Module { get; }

        /// <inheritdoc />
        public string Controller { get; }

        /// <inheritdoc />
        public string Command { get; }

        /// <inheritdoc />
        public IDeviceConfig Config { get; set; }

        private ILogger _logger;

        /// <inheritdoc />
        public ILogger Logger
        {
            get => _logger ??= new NullLogger<BaseCommand>();
            set => _logger = value;
        }

        /// <summary>
        /// Set to true to use POST otherwise GET will be used.
        /// </summary>
        public virtual bool UsePost { get; } = false;

        /// <summary>
        /// Get the name of the root element that contains the response in the JSON object. 
        /// </summary>
        public virtual string ResponseRootElementName { get; } = null;

        /// <summary>
        /// Get the type of root element that contains the response in the JSON object.
        /// </summary>
        public virtual JsonValueKind ResponseRootElementValueKind { get; } = JsonValueKind.Object;
        
        protected BaseCommand(string commandName = null)
        {
            var typeName = GetType().FullName ?? throw new Exception("Cannot determine type name.");
            if (!typeName.StartsWith(BaseTypeNamespace)) throw new Exception($"Commands must be in the {BaseTypeNamespace} namespace.");
            typeName = typeName.Substring(BaseTypeNamespace.Length);
            var parts = typeName.Split('.');
            if (parts.Length != 3) throw new Exception($"Command names must include three parts (Module.Controller.Command).");
            Module     = parts[0].ToLower();
            Controller = parts[1].ToLower();
            Command    = commandName ?? parts[2].ToLower();
        }
    }
}
