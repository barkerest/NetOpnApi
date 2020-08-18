using NetOpnApi.Commands.Diagnostics.Activity;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Diagnostics.Activity
{
    public class GetTopActivity_Should : BaseCommandFactTest<GetTopActivity>
    {
        public GetTopActivity_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            Assert.NotNull(Command.Response.Headers);
            Assert.NotNull(Command.Response.Details);
            Assert.NotEmpty(Command.Response.Headers);
            Assert.NotEmpty(Command.Response.Details);
        }
    }
}
