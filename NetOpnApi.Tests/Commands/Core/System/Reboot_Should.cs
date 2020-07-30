using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.System
{
    public class Reboot_Should : BaseCommandFactTest<NetOpnApi.Commands.Core.System.Reboot>
    {
        public Reboot_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override SpecialTest IsSpecialTest { get; } = SpecialTest.CoreSystemReboot;

        protected override void CheckResponse()
        {
            Assert.Equal("ok", Command.Response.Status, ignoreCase: true);
        }
    }
}
