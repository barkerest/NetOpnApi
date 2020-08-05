using NetOpnApi.Models;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Initiate a health audit of the firmware.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/health
    /// </remarks>
    public class HealthAudit : BaseCommand, ICommandWithResponse<StatusWithUuid>
    {
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public StatusWithUuid Response { get; set; }

        public HealthAudit()
            : base("health")
        {
            
        }
    }
}
