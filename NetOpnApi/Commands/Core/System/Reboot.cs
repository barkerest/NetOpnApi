using NetOpnApi.Models;

namespace NetOpnApi.Commands.Core.System
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
    }
}
