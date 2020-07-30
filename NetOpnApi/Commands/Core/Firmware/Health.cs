using NetOpnApi.Models;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Initiate a health check of the firmware.
    /// </summary>
    public class Health : BaseCommand, ICommandWithResponse<StatusWithUuid>
    {
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public StatusWithUuid Response { get; set; }
    }
}
