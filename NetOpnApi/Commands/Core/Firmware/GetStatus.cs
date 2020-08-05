using NetOpnApi.Models;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get the current firmware state (eg - ready, busy).
    /// </summary>
    /// <remarks>
    /// GET: /api/core/firmware/running
    /// </remarks>
    public class GetStatus : BaseCommand, ICommandWithResponse<StatusMessage>
    {
        /// <inheritdoc />
        public StatusMessage Response { get; set; }

        public GetStatus()
            : base("running")
        {
            
        }
    }
}
