using System;
using System.Collections.Generic;

namespace NetOpnApi.Models.Core.Firmware
{
    public class GetChangeLogParameterSet : IParameterSet
    {
        public bool Update { get; set; } = false;

        public string Version { get; set; } = "";
        
        IReadOnlyList<string> IParameterSet.GetUrlParameters()
        {
            return new[] {Update ? "update" : Version};
        }

        IReadOnlyList<KeyValuePair<string, string>> IParameterSet.GetQueryParameters() => null;

        object IParameterSet.GetRequestPayload() => null;

        Type IParameterSet.GetRequestPayloadDataType() => null;
    }
}
