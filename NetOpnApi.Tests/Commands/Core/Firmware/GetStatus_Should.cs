using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class GetStatus_Should : BaseCommandFactTest<NetOpnApi.Commands.Core.Firmware.GetStatus>
    {
        public GetStatus_Should(ITestOutputHelper output)
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
