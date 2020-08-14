using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class VersionStatusUpgradePackage_Should : BaseModelTest<VersionStatus.UpgradePackage, VersionStatusUpgradePackage_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<VersionStatus.UpgradePackage>> GetList()
                => new ParamBuilder(@"{""name"":""aThing"",""current_version"":""1.2.3.3"",""new_version"":""1.2.3.4""}")
                   .AddTestsFor(m => m.Name)
                   .AddTestsFor(m => m.CurrentVersion)
                   .AddTestsFor(m => m.NewVersion)
                   .ToArray();
        }

        public VersionStatusUpgradePackage_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(VersionStatus.UpgradePackage expected, VersionStatus.UpgradePackage actual)
        {
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.CurrentVersion, actual.CurrentVersion);
            Assert.Equal(expected.NewVersion, actual.NewVersion);
        }

        protected override VersionStatus.UpgradePackage Expected => new VersionStatus.UpgradePackage()
        {
            Name           = "aThing",
            CurrentVersion = "1.2.3.3",
            NewVersion     = "1.2.3.4"
        };
    }
}
