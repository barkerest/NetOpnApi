using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class VersionStatusNewPackage_Should : BaseModelTest<VersionStatus.NewPackage, VersionStatusNewPackage_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<VersionStatus.NewPackage>> GetList()
                => new ParamBuilder(@"{""name"":""something"",""version"":""1.2.3.4""}")
                   .AddTestsFor(m => m.Name)
                   .AddTestsFor(m => m.Version)
                   .ToArray();
        }

        public VersionStatusNewPackage_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(VersionStatus.NewPackage expected, VersionStatus.NewPackage actual)
        {
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Version, actual.Version);
        }

        protected override VersionStatus.NewPackage Expected => new VersionStatus.NewPackage()
        {
            Name    = "something",
            Version = "1.2.3.4"
        };
    }
}
