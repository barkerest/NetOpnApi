using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class UpgradeProgress_Should : BaseModelTest<UpgradeProgress, UpgradeProgress_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<UpgradeProgress>> GetList()
                => new ParamBuilder(@"{""status"":""ok"",""log"":""System is idle.""}")
                   .AddTestsFor(m => m.Status)
                   .AddTestsFor(m => m.Log)
                   .ToArray();
        }

        public UpgradeProgress_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(UpgradeProgress expected, UpgradeProgress actual)
        {
            Assert.Equal(expected.Status, actual.Status);
            Assert.Equal(expected.Log, actual.Log);
        }

        protected override UpgradeProgress Expected => new UpgradeProgress()
        {
            Status = "ok",
            Log    = "System is idle."
        };
    }
}
