using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class SecurityAudit_Should : BaseUpgradeStatusFactTest<NetOpnApi.Commands.Core.Firmware.SecurityAudit>
    {
        protected override SpecialTest IsSpecialTest { get; } = SpecialTest.CoreFirmwareAudit;

        public SecurityAudit_Should(ITestOutputHelper output)
            : base(output)
        {
        }
        
    }
}
