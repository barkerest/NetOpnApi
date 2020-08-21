using NetOpnApi.Commands.Diagnostics.Interface;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Diagnostics.Interface
{
    public class FlushARP_Should : BaseCommandFactTest<FlushARP>
    {
        public FlushARP_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            Assert.StartsWith("text/html", Command.Response.ContentType);
        }
    }
}
