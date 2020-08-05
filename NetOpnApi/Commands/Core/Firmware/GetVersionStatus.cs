using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get version status information for the firmware.
    /// </summary>
    /// <remarks>
    /// GET: /api/core/firmware/status
    /// </remarks>
    public class GetVersionStatus : BaseCommand, ICommandWithResponse<VersionStatus>
    {
        /// <inheritdoc />
        public VersionStatus Response { get; set; }

        public GetVersionStatus()
            : base("status")
        {
            
        }
    }
}
