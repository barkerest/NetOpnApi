using System;
using System.Collections.Generic;
using NetOpnApi.Models;
using NetOpnApi.Models.Cron.Settings;

namespace NetOpnApi.Commands.Cron.Settings
{
    /// <summary>
    /// Delete a job.
    /// </summary>
    /// <remarks>
    /// POST: /api/cron/settings/deljob/$jobid
    /// WARNING: In 20.7, API does not fail gracefully if the ID is invalid.
    /// </remarks>
    public class DeleteJob : BaseCommand, ICommandWithResponseAndParameterSet<ResultOnly>
    {
        /// <inheritdoc />
        public ResultOnly         Response     { get; set; }

        
        public Guid JobId { get; set; }

        IReadOnlyList<string> ICommandWithParameterSet.GetUrlParameters()
            => new[]
            {
                JobId.ToString()
            };

        IReadOnlyList<KeyValuePair<string, string>> ICommandWithParameterSet.GetQueryParameters() => null;

        object ICommandWithParameterSet.GetRequestPayload() => null;

        Type ICommandWithParameterSet.GetRequestPayloadDataType() => null;
        
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        public DeleteJob()
            : base("deljob")
        {
            
        }
    }
}
