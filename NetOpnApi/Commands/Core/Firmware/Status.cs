using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get status information for the firmware.
    /// </summary>
    public class Status : BaseCommand, ICommandWithResponse<StatusInformation>
    {
        /// <inheritdoc />
        public StatusInformation Response { get; set; }
    }
}
