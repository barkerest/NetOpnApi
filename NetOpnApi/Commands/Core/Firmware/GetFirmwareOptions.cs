using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get the options available for firmware configuration.
    /// </summary>
    public class GetFirmwareOptions : BaseCommand, ICommandWithResponse<Options>
    {
        /// <inheritdoc />
        public Options Response { get; set; }
    }
}
