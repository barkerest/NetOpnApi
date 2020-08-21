using NetOpnApi.Commands.Diagnostics.Interface;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Diagnostics.Interface
{
    public class GetARP_Should : BaseCommandFactTest<GetARP>
    {
        public GetARP_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            Assert.NotEmpty(Command.Response);
        }
    }
}
