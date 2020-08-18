using System;
using System.Collections.Generic;

namespace NetOpnApi.Models.Diagnostics.Dns
{
    public class ReverseLookupParameterSet : IParameterSet
    {
        public string IpAddress { get; set; }


        IReadOnlyList<string> IParameterSet.GetUrlParameters() => null;

        IReadOnlyList<KeyValuePair<string, string>> IParameterSet.GetQueryParameters() =>
            new[]
            {
                new KeyValuePair<string, string>("address", IpAddress),
            };

        object IParameterSet.GetRequestPayload() => null;

        Type IParameterSet.GetRequestPayloadDataType() => null;
    }
}
