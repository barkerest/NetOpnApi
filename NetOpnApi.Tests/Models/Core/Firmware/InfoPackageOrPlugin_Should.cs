using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class InfoPackageOrPlugin_Should : BaseModelTest<Info.PackageOrPlugin, InfoPackageOrPlugin_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<Info.PackageOrPlugin>> GetList()
                => new ParamBuilder(@"{""name"": ""acme.sh"",""version"": ""2.8.6"",""comment"": ""ACME protocol client written in shell"",""flatsize"": ""758KiB"",""locked"": ""N/A"",""license"": ""GPLv3+"",""repository"": ""OPNsense"",""origin"": ""security/acme.sh"",""provided"": ""1"",""installed"": ""0"",""path"": ""OPNsense/security/acme.sh"",""configured"": ""0""}")
                   .AddTestsFor(m => m.Name)
                   .AddTestsFor(m => m.Version)
                   .AddTestsFor(m => m.Comment)
                   .AddTestsFor(m => m.FlatSize)
                   .AddTestsFor(m => m.Locked)
                   .AddTestsFor(m => m.License)
                   .AddTestsFor(m => m.Repository)
                   .AddTestsFor(m => m.Origin)
                   .AddTestsFor(m => m.Provided)
                   .AddTestsFor(m => m.Installed)
                   .AddTestsFor(m => m.Path)
                   .AddTestsFor(m => m.Configured)
                   .ToArray();
        }

        public InfoPackageOrPlugin_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(Info.PackageOrPlugin expected, Info.PackageOrPlugin actual)
        {
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Version, actual.Version);
            Assert.Equal(expected.Comment, actual.Comment);
            Assert.Equal(expected.FlatSize, actual.FlatSize);
            Assert.Equal(expected.Locked, actual.Locked);
            Assert.Equal(expected.License, actual.License);
            Assert.Equal(expected.Repository, actual.Repository);
            Assert.Equal(expected.Origin, actual.Origin);
            Assert.Equal(expected.Provided, actual.Provided);
            Assert.Equal(expected.Installed, actual.Installed);
            Assert.Equal(expected.Path, actual.Path);
            Assert.Equal(expected.Configured, actual.Configured);
        }

        protected override Info.PackageOrPlugin Expected => new Info.PackageOrPlugin()
        {
            Name       = "acme.sh",
            Version    = "2.8.6",
            Comment    = "ACME protocol client written in shell",
            FlatSize   = "758KiB",
            Locked     = false,
            License    = "GPLv3+",
            Repository = "OPNsense",
            Origin     = "security/acme.sh",
            Provided   = true,
            Installed  = false,
            Path       = "OPNsense/security/acme.sh",
            Configured = false
        };
    }
}
