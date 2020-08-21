using System.Collections.Generic;

namespace NetOpnApi.Commands.Diagnostics.Interface
{
    public class GetInterfaceNames : BaseCommand, ICommandWithResponse<Dictionary<string,string>>
    {
        /// <inheritdoc />
        public Dictionary<string, string> Response { get; set; }
    }
}
