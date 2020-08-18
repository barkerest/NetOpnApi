using System.Collections.Generic;
using NetOpnApi.Models.Diagnostics.Dns;

namespace NetOpnApi.Commands.Diagnostics.Dns
{
    public class ReverseLookup : BaseCommand, ICommandWithResponseAndParameterSet<Dictionary<string,string>, ReverseLookupParameterSet>
    {
        /// <inheritdoc />
        public Dictionary<string, string> Response     { get; set; }

        /// <inheritdoc />
        public ReverseLookupParameterSet  ParameterSet { get; } = new ReverseLookupParameterSet();

        public ReverseLookup()
            : base("reverse_lookup")
        {
            
        }
    }
}
