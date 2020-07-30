using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Menu
{
    public class Tree_Should : BaseCommandFactTest<NetOpnApi.Commands.Core.Menu.Tree>
    {
        public Tree_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotEmpty(Command.Response);
            var lobby = Command.Response.FirstOrDefault(x => x.ID.Equals("Lobby", StringComparison.OrdinalIgnoreCase));
            Assert.NotNull(lobby);
            Assert.NotEmpty(lobby.Children);
            var dash = lobby.Children.FirstOrDefault(x => x.ID.Equals("Dashboard", StringComparison.OrdinalIgnoreCase));
            Assert.NotNull(dash);
        }
    }
}
