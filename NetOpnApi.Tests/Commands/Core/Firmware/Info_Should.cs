using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class Info_Should : BaseCommandFactTest<NetOpnApi.Commands.Core.Firmware.Info>
    {
        public Info_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            Assert.False(string.IsNullOrWhiteSpace(Command.Response.ProductName));
            this.LogInformation($"{Command.Response.ProductName} v{Command.Response.ProductVersion}");
            Assert.NotEmpty(Command.Response.Packages);
            Assert.NotEmpty(Command.Response.Plugins);
        }
    }
}
