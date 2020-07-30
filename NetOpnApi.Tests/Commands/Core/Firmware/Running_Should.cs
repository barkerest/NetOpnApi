using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class Running_Should : BaseCommandFactTest<NetOpnApi.Commands.Core.Firmware.Running>
    {
        public Running_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            Assert.Equal("ready", Command.Response.Status, ignoreCase: true);
        }
    }
}
