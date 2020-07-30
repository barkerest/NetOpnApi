using NetOpnApi.Models;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Power off the device.
    /// </summary>
    /// <remarks>
    /// WARNING: Powering off the device will cause it to no longer be available.
    /// </remarks>
    public class Poweroff : BaseCommand, ICommandWithResponse<StatusMessage>
    {
        /// <inheritdoc />
        public StatusMessage Response { get; set; }

        /// <inheritdoc />
        public override bool UsePost { get; } = true;
    }
}
