using System.Collections.Generic;
using System.Reflection;
using NetOpnApi.Models.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class PackageDetails_Should : BaseModelTest<PackageDetails, PackageDetails_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<PackageDetails>> GetList()
                => new ParamBuilder(@"{""details"":""This is some text.""}")
                   .AddTestsFor(x => x.Details)
                   .ToArray();
        }

        public PackageDetails_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(PackageDetails expected, PackageDetails actual)
        {
            Assert.Equal(expected.Details, actual.Details);
        }

        protected override PackageDetails Expected => new PackageDetails() {Details = "This is some text."};
    }
}
