using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class Reboot_Should : BaseCommandFactTest<NetOpnApi.Commands.Core.Firmware.Reboot>
    {
        public Reboot_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override SpecialTest IsSpecialTest { get; } = SpecialTest.CoreFirmwareReboot;
        
        protected override void CheckResponse()
        {
            Assert.Equal("ok", Command.Response.Status, ignoreCase: true);
        }
    }
}
