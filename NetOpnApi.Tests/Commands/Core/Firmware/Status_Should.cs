using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class Status_Should : BaseCommandFactTest<NetOpnApi.Commands.Core.Firmware.Status>
    {
        public Status_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            Assert.False(string.IsNullOrWhiteSpace(Command.Response.OsVersion));
            this.LogInformation(Command.Response.OsVersion);
            this.LogInformation(Command.Response.StatusMessage);
        }
    }
}
