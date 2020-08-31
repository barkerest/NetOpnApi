using System;
using System.Collections.Generic;
using NetOpnApi.Models;
using NetOpnApi.Models.Cron.Settings;

namespace NetOpnApi.Commands.Cron.Settings
{
    public class ToggleJob : BaseCommand, ICommandWithResponseAndParameterSet<ResultOnly>
    {
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public ResultOnly Response { get; set; }

        public Guid JobId { get; set; }

        public bool? Enabled { get; set; }

        IReadOnlyList<string> ICommandWithParameterSet.GetUrlParameters()
            => Enabled.HasValue
                   ? new[] {JobId.ToString(), Enabled.Value ? "1" : "0"}
                   : new[] {JobId.ToString()};

        IReadOnlyList<KeyValuePair<string, string>> ICommandWithParameterSet.GetQueryParameters() => null;

        object ICommandWithParameterSet.GetRequestPayload() => null;

        Type ICommandWithParameterSet.GetRequestPayloadDataType() => null;
    }
}
