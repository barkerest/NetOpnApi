using NetOpnApi.Commands.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class GetPackageDetails_Should : BaseCommandFactTest<GetPackageDetails>
    {
        public GetPackageDetails_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void SetParameters()
        {
            Command.PackageName = "ca_root_nss";
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            Assert.False(string.IsNullOrEmpty(Command.Response.Details));
        }
    }
}
