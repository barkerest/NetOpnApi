using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class Poweroff_Should : BaseCommandFactTest<NetOpnApi.Commands.Core.Firmware.Poweroff>
    {
        public Poweroff_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override SpecialTest IsSpecialTest { get; } = SpecialTest.CoreFirmwarePoweroff;
        
        protected override void CheckResponse()
        {
            Assert.Equal("ok", Command.Response.Status, ignoreCase: true);
        }
    }
}
