using NetOpnApi.Commands.Diagnostics.Interface;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Diagnostics.Interface
{
    public class GetBpfStatistics_Should : BaseCommandFactTest<GetBpfStatistics>
    {
        public GetBpfStatistics_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            Assert.NotNull(Command.Response.Entries);
            Assert.NotEmpty(Command.Response.Entries);
        }
    }
}
