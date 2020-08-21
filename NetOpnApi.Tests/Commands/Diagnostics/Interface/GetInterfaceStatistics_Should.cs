using System.Linq;
using NetOpnApi.Commands.Diagnostics.Interface;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Diagnostics.Interface
{
    public class GetInterfaceStatistics_Should : BaseCommandFactTest<GetInterfaceStatistics>
    {
        public GetInterfaceStatistics_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            Assert.NotEmpty(Command.Response);
            Assert.Contains(Command.Response, x => x.Value.Name == "lo0");
        }
    }
}
