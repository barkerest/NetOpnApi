using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Commands.Diagnostics.Dns;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Diagnostics.Dns
{
    public class ReverseLookup_Should : BaseCommandTheoryTest<ReverseLookup, (string address, string name), ReverseLookup_Should.Params>
    {
        public class Params : IEnumerable<(string address, string name)>
        {
            private static readonly IEnumerable<(string address, string name)> List = new[]
            {
                ("127.0.0.1", "localhost"),
                ("198.41.0.4", "a.root-servers.net"),
                ("10.0.0.1", "10.0.0.1"),
                ("::1", "localhost"),
                ("2001:500:200::b", "b.root-servers.net"),
            };

            public IEnumerator<(string address, string name)> GetEnumerator() => List.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();
        }

        public ReverseLookup_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void SetParameters((string address, string name) args)
        {
            Command.IpAddress = args.address;
        }

        protected override void CheckResponse((string address, string name) args)
        {
            Assert.NotNull(Command.Response);
            Assert.True(Command.Response.ContainsKey(args.address));
            Assert.Equal(args.name, Command.Response[args.address]);
        }
    }
}
