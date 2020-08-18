using System;
using System.Collections.Generic;

namespace NetOpnApi.Models.Cron.Settings
{
    public class ToggleJobParameterSet : IParameterSet
    {
        public Guid JobId { get; set; }

        public bool? Enabled { get; set; }

        IReadOnlyList<string> IParameterSet.GetUrlParameters()
            => Enabled.HasValue
                   ? new[] {JobId.ToString(), Enabled.Value ? "1" : "0"}
                   : new[] {JobId.ToString()};

        IReadOnlyList<KeyValuePair<string, string>> IParameterSet.GetQueryParameters() => null;

        object IParameterSet.GetRequestPayload() => null;

        Type IParameterSet.GetRequestPayloadDataType() => null;
    }
}
