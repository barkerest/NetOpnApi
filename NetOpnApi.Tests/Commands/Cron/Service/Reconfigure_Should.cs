using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Cron.Service
{
    public class Reconfigure_Should : BaseCommandFactTest<NetOpnApi.Commands.Cron.Service.Reconfigure>
    {
        public Reconfigure_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            Assert.Equal("ok", Command.Response.Status);
        }
    }
}
