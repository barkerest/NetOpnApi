using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class GetVersionStatus_Should : BaseCommandFactTest<NetOpnApi.Commands.Core.Firmware.GetVersionStatus>
    {
        public GetVersionStatus_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override SpecialTest IsSpecialTest { get; } = SpecialTest.CoreFirmwareStatus;

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            Assert.False(string.IsNullOrWhiteSpace(Command.Response.OsVersion));
            this.LogInformation(Command.Response.OsVersion);
            this.LogInformation(Command.Response.StatusMessage);
        }
    }
}
