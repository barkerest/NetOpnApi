using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Menu
{
    public class Search_Should : BaseCommandTheoryTest<
        NetOpnApi.Commands.Core.Menu.Search,
        Search_Should.Param,
        Search_Should.ParamList>
    {
        public class Param
        {
            public Param(string term, string expectedItemId, string expectedItemBreadcrumb, int expectedDepth, bool expectLicense)
            {
                Term                   = term;
                ExpectedItemId         = expectedItemId;
                ExpectedItemBreadcrumb = expectedItemBreadcrumb;
                ExpectedDepth          = expectedDepth;
                ExpectLicense          = expectLicense;
            }

            public string Term                   { get; }
            public string ExpectedItemId         { get; }
            public string ExpectedItemBreadcrumb { get; }
            public int    ExpectedDepth          { get; }
            public bool   ExpectLicense          { get; }

            public override string ToString() => $"\"{Term}\"";
        }

        public class ParamList : IEnumerable<Param>
        {
            private static readonly IEnumerable<Param> Params = new[]
            {
                new Param("", "Dashboard", "Lobby / Dashboard", 2, true),
                new Param("dash", "Dashboard", "Lobby / Dashboard", 2, false),
                new Param("Reboot", "Reboot", "Power / Reboot", 2, false),
                new Param("boot", "Reboot", "Power / Reboot", 2, false),
                new Param("zyxabc", null, null, 0, false), 
            };

            public IEnumerator<Param> GetEnumerator()
                => Params.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }

        public Search_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void SetParameters(Param args)
        {
            Command.ParameterSet.SearchTerm = args.Term;
        }

        protected override void CheckResponse(Param args)
        {
            if (args.ExpectedItemId is null)
            {
                Assert.Empty(Command.Response);
                return;
            }
            
            Assert.NotEmpty(Command.Response);

            var item = Command.Response.FirstOrDefault(x => x.ID.Equals(args.ExpectedItemId, StringComparison.OrdinalIgnoreCase));
            Assert.NotNull(item);

            Assert.Equal(args.ExpectedItemBreadcrumb, item.Breadcrumb);
            Assert.Equal(args.ExpectedDepth, item.Depth);

            item = Command.Response.FirstOrDefault(x => x.ID.Equals("License", StringComparison.OrdinalIgnoreCase));

            if (args.ExpectLicense)
            {
                Assert.NotNull(item);
            }
            else
            {
                Assert.Null(item);
            }
        }
    }
}
