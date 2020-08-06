using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class VersionStatusNewPackage_Should : BaseModelTest<VersionStatus.NewPackage, VersionStatusNewPackage_Should.Params>
    {
        public class Params : IEnumerable<ModelTestParam<VersionStatus.NewPackage>>
        {
            private static readonly IEnumerable<ModelTestParam<VersionStatus.NewPackage>> ParamList
                = new ParamBuilder(@"{""name"":""something"",""version"":""1.2.3.4""}")
                  .AddTestsFor(m => m.Name)
                  .AddTestsFor(m => m.Version)
                  .ToArray();

            public IEnumerator<ModelTestParam<VersionStatus.NewPackage>> GetEnumerator() => ParamList.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
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
            Name = "something",
            Version = "1.2.3.4"
        };
    }
}
