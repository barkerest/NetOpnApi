using System;
using System.Collections.Generic;

namespace NetOpnApi.Commands.Diagnostics.Dns
{
    public class ReverseLookup : BaseCommand, ICommandWithResponseAndParameterSet<Dictionary<string,string>>
    {
        /// <inheritdoc />
        public Dictionary<string, string> Response     { get; set; }

        /// <summary>
        /// The IP address to lookup.
        /// </summary>
        public string IpAddress { get; set; }
        
        IReadOnlyList<string> ICommandWithParameterSet.GetUrlParameters() => null;

        IReadOnlyList<KeyValuePair<string, string>> ICommandWithParameterSet.GetQueryParameters() =>
            new[]
            {
                new KeyValuePair<string, string>("address", IpAddress),
            };

        object ICommandWithParameterSet.GetRequestPayload() => null;

        Type ICommandWithParameterSet.GetRequestPayloadDataType() => null;
        
        public ReverseLookup()
            : base("reverse_lookup")
        {
            
        }
    }
}
