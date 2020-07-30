using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class GetFirmwareOptions_Should : BaseCommandFactTest<NetOpnApi.Commands.Core.Firmware.GetFirmwareOptions>
    {
        public GetFirmwareOptions_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            
            // Should be at least one subscription.
            Assert.NotEmpty(Command.Response.Subscriptions);
            
            // should have at least 1 flavor.
            Assert.NotEmpty(Command.Response.Flavors);
            
            // should have at least 1 release type.
            Assert.NotEmpty(Command.Response.ReleaseTypes);
            
            // should have at least 1 mirror.
            Assert.NotEmpty(Command.Response.Mirrors);
        }
    }
}
