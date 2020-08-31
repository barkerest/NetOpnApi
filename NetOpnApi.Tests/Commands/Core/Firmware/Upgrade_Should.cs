using NetOpnApi.Models.Core.Firmware;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class Upgrade_Should : BaseUpgradeStatusFactTest<NetOpnApi.Commands.Core.Firmware.Upgrade>
    {
        public Upgrade_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override SpecialTest IsSpecialTest { get; } = SpecialTest.CoreFirmwareUpgrade;

        protected override void SetParameters()
        {
            // only upgrade the package repository, should be quite a bit faster.
            Command.UpgradeType = UpgradeType.PackageRepository;
        }
    }
}
