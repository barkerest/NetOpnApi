using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get the firmware configuration.
    /// </summary>
    public class GetFirmwareConfig : BaseCommand, ICommandWithResponse<Config>
    {
        /// <inheritdoc />
        public Config Response { get; set; }
    }
}
