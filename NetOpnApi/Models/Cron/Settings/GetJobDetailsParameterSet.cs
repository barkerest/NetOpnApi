using System;
using System.Collections.Generic;

namespace NetOpnApi.Models.Cron.Settings
{
    public class GetJobDetailsParameterSet : IParameterSet
    {
        /// <summary>
        /// The ID of the job to retrieve (or null for default job settings).
        /// </summary>
        public Guid? JobId { get; set; }

        IReadOnlyList<string> IParameterSet.GetUrlParameters()
            => JobId is null ? null : new[] {JobId.ToString()};

        IReadOnlyList<KeyValuePair<string, string>> IParameterSet.GetQueryParameters() => null;

        object IParameterSet.GetRequestPayload() => null;

        Type IParameterSet.GetRequestPayloadDataType() => null;
    }
}
