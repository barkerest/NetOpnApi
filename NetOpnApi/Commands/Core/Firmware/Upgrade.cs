using NetOpnApi.Models;
using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Perform an upgrade.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/upgrade
    /// </remarks>
    public class Upgrade : BaseCommand, ICommandWithResponseAndParameterSet<StatusWithUuid, UpgradeParameterSet>
    {
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public StatusWithUuid Response { get; set; }

        /// <inheritdoc />
        public UpgradeParameterSet ParameterSet { get; } = new UpgradeParameterSet(){UpgradeType = UpgradeType.All};
    }
}
