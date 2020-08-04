using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get the current upgrade status.
    /// </summary>
    public class UpgradeStatus : BaseCommand, ICommandWithResponse<UpgradeStatusMessage>
    {
        /// <inheritdoc />
        public UpgradeStatusMessage Response { get; set; }
    }
}
