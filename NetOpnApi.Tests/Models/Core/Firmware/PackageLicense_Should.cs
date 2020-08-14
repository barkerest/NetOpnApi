using System.Collections.Generic;
using NetOpnApi.Models.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class PackageLicense_Should : BaseModelTest<PackageLicense, PackageLicense_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<PackageLicense>> GetList()
                => new ParamBuilder(@"{""license"":""This is the license.""}")
                   .AddTestsFor(x => x.License)
                   .ToArray();
        }

        public PackageLicense_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(PackageLicense expected, PackageLicense actual)
        {
            Assert.Equal(expected.License, actual.License);
        }

        protected override PackageLicense Expected => new PackageLicense() {License = "This is the license."};
    }
}
