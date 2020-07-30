using System;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.System
{
    public class Halt_Should : BaseCommandFactTest<NetOpnApi.Commands.Core.System.Halt>
    {
        public Halt_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override SpecialTest IsSpecialTest { get; } = SpecialTest.CoreSystemHalt;

        protected override void CheckResponse()
        {
            Assert.Equal("ok", Command.Response.Status, ignoreCase: true);
        }
    }
}
