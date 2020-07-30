using NetOpnApi.Models;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Determine if the firmware is running.
    /// </summary>
    public class Running : BaseCommand, ICommandWithResponse<StatusMessage>
    {
        /// <inheritdoc />
        public StatusMessage Response { get; set; }
    }
}
