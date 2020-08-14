using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class VersionStatusChangePackage_Should : BaseModelTest<VersionStatus.ChangePackage, VersionStatusChangePackage_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<VersionStatus.ChangePackage>> GetList()
                => new ParamBuilder(@"{""name"":""thePackage"",""reason"":""install"",""new"":""1.2.3.4"",""old"":""n/a""}")
                   .AddTestsFor(m => m.Name)
                   .AddTestsFor(m => m.Change)
                   .AddTestsFor(m => m.NewVersion)
                   .AddTestsFor(m => m.OldVersion)
                   .ToArray();
        }

        public VersionStatusChangePackage_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(VersionStatus.ChangePackage expected, VersionStatus.ChangePackage actual)
        {
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Change, actual.Change);
            Assert.Equal(expected.NewVersion, actual.NewVersion);
            Assert.Equal(expected.OldVersion, actual.OldVersion);
        }

        protected override VersionStatus.ChangePackage Expected => new VersionStatus.ChangePackage()
        {
            Name       = "thePackage",
            Change     = "install",
            NewVersion = "1.2.3.4",
            OldVersion = "n/a"
        };
    }
}
