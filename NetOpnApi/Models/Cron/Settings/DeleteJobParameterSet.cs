using System;
using System.Collections.Generic;

namespace NetOpnApi.Models.Cron.Settings
{
    public class DeleteJobParameterSet : IParameterSet
    {
        public Guid JobId { get; set; }

        IReadOnlyList<string> IParameterSet.GetUrlParameters()
            => new[]
            {
                JobId.ToString()
            };

        IReadOnlyList<KeyValuePair<string, string>> IParameterSet.GetQueryParameters() => null;

        object IParameterSet.GetRequestPayload() => null;

        Type IParameterSet.GetRequestPayloadDataType() => null;
    }
}
