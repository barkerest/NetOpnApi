using NetOpnApi.Models;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Reboot the device.
    /// </summary>
    /// <remarks>
    /// WARNING: Rebooting the device will make it unavailable for a short while.
    ///          There is a chance the device will not boot back up if there are hardware issues.
    /// </remarks>
    public class Reboot : BaseCommand, ICommandWithResponse<StatusMessage>
    {
        /// <inheritdoc />
        public StatusMessage Response { get; set; }

        /// <inheritdoc />
        public override bool UsePost { get; } = true;
    }
}
