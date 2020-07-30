using NetOpnApi.Models;
using NetOpnApi.Models.Core.System.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Set the firmware configuration.
    /// </summary>
    public class SetFirmwareConfig : BaseCommand, ICommandWithResponseAndParameterSet<StatusMessage, SetConfigParameterSet>
    {
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public StatusMessage Response { get; set; }

        /// <inheritdoc />
        public SetConfigParameterSet ParameterSet { get; } = new SetConfigParameterSet();
    }
}
