using NetOpnApi.Models;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Initiate a security audit of the firmware.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/audit
    /// </remarks>
    public class SecurityAudit : BaseCommand, ICommandWithResponse<StatusWithUuid>
    {
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public StatusWithUuid Response { get; set; }

        public SecurityAudit()
            : base("audit")
        {
            
        }
    }
}
