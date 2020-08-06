using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetOpnApi.Models.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class Options_Should : BaseModelTest<Options, Options_Should.Params>
    {
        public class Params : IEnumerable<ModelTestParam<Options>>
        {
            private static readonly IEnumerable<ModelTestParam<Options>> ParamList
                = new ParamBuilder(@"{""has_subscription"": [""https://opnsense-update.deciso.com""],""flavours"": {"""": ""(default)"",""libressl"": ""LibreSSL"",""latest"": ""OpenSSL""},""families"": {"""": ""Production"",""devel"": ""Development""},""mirrors"": {"""": ""(default)"",""https://pkg.opnsense.org"": ""OPNsense (HTTPS, Amsterdam, NL)""},""allow_custom"": true}")
                  .AddTestsFor(m => m.Subscriptions, new[] {"alpha.example.com", "bravo.example.net", "charlie.example.org"})
                  .AddTestsFor(m => m.Flavors, new Dictionary<string, string>() {{"one", "The One"}})
                  .AddTestsFor(m => m.ReleaseTypes, new Dictionary<string, string>() {{"aaaa", "The letter AAAA"}})
                  .AddTestsFor(m => m.Mirrors, new Dictionary<string, string>() {{"", ""}, {"home", "home.example.com"}})
                  .AddTestsFor(m => m.AllowCustom)
                  .ToArray();

            public IEnumerator<ModelTestParam<Options>> GetEnumerator() => ParamList.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public Options_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(Options expected, Options actual)
        {
            Assert.Equal(expected.AllowCustom, actual.AllowCustom);
            if (expected.Subscriptions is null)
            {
                Assert.Null(actual.Subscriptions);
            }
            else
            {
                Assert.NotNull(actual.Subscriptions);
                Assert.Equal(expected.Subscriptions.Length, actual.Subscriptions.Length);
                Assert.True(expected.Subscriptions.SequenceEqual(actual.Subscriptions));
            }

            if (expected.Flavors is null)
            {
                Assert.Null(actual.Flavors);
            }
            else
            {
                Assert.NotNull(actual.Flavors);
                Assert.Equal(expected.Flavors.Count, actual.Flavors.Count);
                Assert.True(expected.Flavors.Keys.SequenceEqual(actual.Flavors.Keys));
                Assert.True(expected.Flavors.Values.SequenceEqual(actual.Flavors.Values));
            }

            if (expected.ReleaseTypes is null)
            {
                Assert.Null(actual.ReleaseTypes);
            }
            else
            {
                Assert.NotNull(actual.ReleaseTypes);
                Assert.Equal(expected.ReleaseTypes.Count, actual.ReleaseTypes.Count);
                Assert.True(expected.ReleaseTypes.Keys.SequenceEqual(actual.ReleaseTypes.Keys));
                Assert.True(expected.ReleaseTypes.Values.SequenceEqual(actual.ReleaseTypes.Values));
            }

            if (expected.Mirrors is null)
            {
                Assert.Null(actual.Mirrors);
            }
            else
            {
                Assert.NotNull(actual.Mirrors);
                Assert.Equal(expected.Mirrors.Count, actual.Mirrors.Count);
                Assert.True(expected.Mirrors.Keys.SequenceEqual(actual.Mirrors.Keys));
                Assert.True(expected.Mirrors.Values.SequenceEqual(actual.Mirrors.Values));
            }
        }

        protected override Options Expected => new Options()
        {
            Subscriptions = new[] {"https://opnsense-update.deciso.com"},
            Flavors = new Dictionary<string, string>()
            {
                {"", "(default)"},
                {"libressl", "LibreSSL"},
                {"latest", "OpenSSL"}
            },
            ReleaseTypes = new Dictionary<string, string>()
            {
                {"", "Production"},
                {"devel", "Development"}
            },
            Mirrors = new Dictionary<string, string>()
            {
                {"", "(default)"},
                {"https://pkg.opnsense.org", "OPNsense (HTTPS, Amsterdam, NL)"}
            },
            AllowCustom = true
        };
    }
}
