using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class HealthAudit_Should : BaseUpgradeStatusFactTest<NetOpnApi.Commands.Core.Firmware.HealthAudit>
    {
        public HealthAudit_Should(ITestOutputHelper output)
            : base(output)
        {
        }
        
        protected override SpecialTest IsSpecialTest { get; } = SpecialTest.CoreFirmwareHealth;
        
    }
}
