using NetOpnApi.Models;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get the current firmware state (eg - ready, busy).
    /// </summary>
    /// <remarks>
    /// GET: /api/core/firmware/running
    /// </remarks>
    public class GetStatus : BaseCommand, ICommandWithResponse<StatusOnly>
    {
        /// <inheritdoc />
        public StatusOnly Response { get; set; }

        public GetStatus()
            : base("running")
        {
            
        }
    }
}
