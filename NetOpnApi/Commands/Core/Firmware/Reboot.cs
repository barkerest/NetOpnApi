using NetOpnApi.Models;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Reboot the device.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/reboot
    /// 
    /// WARNING: Rebooting the device will make it unavailable for a short while.
    ///          There is a chance the device will not boot back up if there are hardware issues.
    /// </remarks>
    public class Reboot : BaseCommand, ICommandWithResponse<StatusWithUuid>
    {
        /// <inheritdoc />
        public StatusWithUuid Response { get; set; }

        /// <inheritdoc />
        public override bool UsePost { get; } = true;
    }
}
