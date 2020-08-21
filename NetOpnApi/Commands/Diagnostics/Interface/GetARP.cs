using System.Text.Json;
using NetOpnApi.Models.Diagnostics.Interface;

namespace NetOpnApi.Commands.Diagnostics.Interface
{
    /// <summary>
    /// Get ARP entries from the device.
    /// </summary>
    /// <remarks>
    /// GET: /api/diagnostics/interface/getarp
    /// </remarks>
    public class GetARP : BaseCommand, ICommandWithResponse<ArpEntry[]>
    {
        /// <inheritdoc />
        public override JsonValueKind ResponseRootElementValueKind { get; } = JsonValueKind.Array;
        

        /// <inheritdoc />
        public ArpEntry[] Response { get; set; }
    }
}
