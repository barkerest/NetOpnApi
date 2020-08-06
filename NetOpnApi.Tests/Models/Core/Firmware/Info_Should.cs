using System;
using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class Info_Should : BaseModelTest<Info, Info_Should.Params>
    {
        public class Params : IEnumerable<ModelTestParam<Info>>
        {
            private static readonly IEnumerable<ModelTestParam<Info>> ParamList
                = new ParamBuilder(@"{""product_name"": ""opnsense"",""product_version"": ""20.7"",""package"": [{""name"": ""acme"",""version"": ""1.2.3"",""comment"": ""ACME protocol""}],""plugin"": [{""name"": ""os-acme-client"",""version"": ""1.23"",""comment"": ""ACME client""}],""changelog"": [{""series"": ""20.7"",""version"": ""20.7"",""date"": ""2020-07-30""},{""series"": ""20.1"",""version"": ""20.1.9"",""date"": ""2020-07-23""}]}")
                  .AddTestsFor(m => m.ProductName)
                  .AddTestsFor(m => m.ProductVersion)
                  .AddTestsFor(m => m.Packages, new[] {new Info.PackageOrPlugin() {Name   = "Alpha", Version = "0.1", Comment = "The test item"},})
                  .AddTestsFor(m => m.Plugins, new[] {new Info.PackageOrPlugin() {Name    = "Alpha", Version = "0.1", Comment = "The test item"},})
                  .AddTestsFor(m => m.ChangeLog, new[] {new Info.ChangeLogEntry() {Series = "1", Version     = "1.0", Date    = DateTime.UtcNow.Date},})
                  .ToArray();

            public IEnumerator<ModelTestParam<Info>> GetEnumerator() => ParamList.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public Info_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(Info expected, Info actual)
        {
            Assert.Equal(expected.ProductName, actual.ProductName);
            Assert.Equal(expected.ProductVersion, actual.ProductVersion);
            if (expected.Packages is null)
            {
                Assert.Null(actual.Packages);
            }
            else
            {
                Assert.NotNull(actual.Packages);
                Assert.Equal(expected.Packages.Length, actual.Packages.Length);
                for (var i = 0; i < expected.Packages.Length; i++)
                {
                    var a = expected.Packages[i];
                    var b = actual.Packages[i];
                    Assert.NotNull(a);
                    Assert.NotNull(b);
                    Assert.Equal(a.Name, b.Name);
                    Assert.Equal(a.Version, b.Version);
                    Assert.Equal(a.Comment, b.Comment);
                }
            }
            if (expected.Plugins is null)
            {
                Assert.Null(actual.Plugins);
            }
            else
            {
                Assert.NotNull(actual.Plugins);
                Assert.Equal(expected.Plugins.Length, actual.Plugins.Length);
                for (var i = 0; i < expected.Plugins.Length; i++)
                {
                    var a = expected.Plugins[i];
                    var b = actual.Plugins[i];
                    Assert.NotNull(a);
                    Assert.NotNull(b);
                    Assert.Equal(a.Name, b.Name);
                    Assert.Equal(a.Version, b.Version);
                    Assert.Equal(a.Comment, b.Comment);
                }
            }

            if (expected.ChangeLog is null)
            {
                Assert.Null(actual.ChangeLog);
            }
            else
            {
                Assert.NotNull(actual.ChangeLog);
                Assert.Equal(expected.ChangeLog.Length, actual.ChangeLog.Length);
                for (var i = 0; i < expected.ChangeLog.Length; i++)
                {
                    var a = expected.ChangeLog[i];
                    var b = actual.ChangeLog[i];
                    Assert.NotNull(a);
                    Assert.NotNull(b);
                    Assert.Equal(a.Series, b.Series);
                    Assert.Equal(a.Version, b.Version);
                    Assert.Equal(a.Date, b.Date);
                }
            }
        }

        protected override Info Expected => new Info()
        {
            ProductName    = "opnsense",
            ProductVersion = "20.7",
            Packages = new[]
            {
                new Info.PackageOrPlugin()
                {
                    Name    = "acme",
                    Version = "1.2.3",
                    Comment = "ACME protocol"
                },
            },
            Plugins = new[]
            {
                new Info.PackageOrPlugin()
                {
                    Name    = "os-acme-client",
                    Version = "1.23",
                    Comment = "ACME client"
                },
            },
            ChangeLog = new[]
            {
                new Info.ChangeLogEntry()
                {
                    Series  = "20.7",
                    Version = "20.7",
                    Date    = new DateTime(2020, 7, 30)
                },
                new Info.ChangeLogEntry()
                {
                    Series  = "20.1",
                    Version = "20.1.9",
                    Date    = new DateTime(2020, 7, 23)
                },
            }
        };
    }
}
