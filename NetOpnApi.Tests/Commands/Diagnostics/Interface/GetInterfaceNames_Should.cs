using NetOpnApi.Commands.Diagnostics.Interface;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Diagnostics.Interface
{
    public class GetInterfaceNames_Should : BaseCommandFactTest<GetInterfaceNames>
    {
        public GetInterfaceNames_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            Assert.True(Command.Response.Count >= 2);
            Assert.True(Command.Response.ContainsValue("lan"));
            Assert.True(Command.Response.ContainsValue("wan"));
        }
    }
}
