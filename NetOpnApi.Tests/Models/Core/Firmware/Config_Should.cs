using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class Config_Should : BaseModelTest<NetOpnApi.Models.Core.Firmware.Config, Config_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<NetOpnApi.Models.Core.Firmware.Config>> GetList()
                => new ParamBuilder(@"{""flavour"": """",""mirror"": ""https://pkg.opnsense.org"",""type"": """"}")
                   .AddTestsFor(m => m.Flavor)
                   .AddTestsFor(m => m.Mirror)
                   .AddTestsFor(m => m.ReleaseType)
                   .AddTestsFor(m => m.Subscription)
                   .ToArray();
        }

        public Config_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(NetOpnApi.Models.Core.Firmware.Config expected, NetOpnApi.Models.Core.Firmware.Config actual)
        {
            Assert.Equal(expected.Flavor, actual.Flavor);
            Assert.Equal(expected.Mirror, actual.Mirror);
            Assert.Equal(expected.ReleaseType, actual.ReleaseType);
            Assert.Equal(expected.Subscription, actual.Subscription);
        }

        protected override NetOpnApi.Models.Core.Firmware.Config Expected => new NetOpnApi.Models.Core.Firmware.Config()
        {
            Flavor       = "",
            Mirror       = "https://pkg.opnsense.org",
            ReleaseType  = "",
            Subscription = null
        };
    }
}
