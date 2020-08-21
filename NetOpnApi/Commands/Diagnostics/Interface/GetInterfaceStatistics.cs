using System.Collections.Generic;
using NetOpnApi.Models.Diagnostics.Interface;

namespace NetOpnApi.Commands.Diagnostics.Interface
{
    public class GetInterfaceStatistics : BaseCommand, ICommandWithResponse<Dictionary<string, InterfaceStatistics>>
    {
        /// <inheritdoc />
        public override string ResponseRootElementName { get; } = "statistics";

        /// <inheritdoc />
        public Dictionary<string, InterfaceStatistics> Response { get; set; }
    }
}
