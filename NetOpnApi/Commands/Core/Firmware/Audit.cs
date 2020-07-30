using NetOpnApi.Models;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Initiate a security audit of the firmware.
    /// </summary>
    public class Audit : BaseCommand, ICommandWithResponse<StatusWithUuid>
    {
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public StatusWithUuid Response { get; set; }
    }
}
