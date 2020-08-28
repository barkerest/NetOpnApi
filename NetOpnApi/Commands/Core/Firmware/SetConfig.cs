using NetOpnApi.Models;
using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Set the firmware configuration.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/setfirmwareconfig
    /// </remarks>
    public class SetConfig : BaseCommand, ICommandWithResponseAndParameterSet<StatusOnly, SetFirmwareConfigParameterSet>
    {
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public StatusOnly Response { get; set; }

        /// <inheritdoc />
        public SetFirmwareConfigParameterSet ParameterSet { get; } = new SetFirmwareConfigParameterSet();

        public SetConfig()
            : base("setfirmwareconfig")
        {
            
        }
    }
}
