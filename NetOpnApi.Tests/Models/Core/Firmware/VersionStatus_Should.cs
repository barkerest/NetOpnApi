using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using NetOpnApi.Models.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class VersionStatus_Should : BaseModelTest<VersionStatus, VersionStatus_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<VersionStatus>> GetList()
                => new ParamBuilder(@"{""connection"":""ok"",""download_size"":""1MiB"",""last_check"":""2020-08-06 21:39:08Z"",""os_version"":""12.1"",""product_name"":""opnsense"",""product_version"":""20.7"",""repository"":""ok"",""updates"":6,""upgrade_major_message"":""nada"",""upgrade_major_version"":""20.8"",""upgrade_needs_reboot"":true,""status_upgrade_action"":""all"",""status"":""Ok"",""status_msg"":""There are updates available."",""new_packages"":[{""name"":""itemToAdd"",""version"":""1.0""}],""reinstall_packages"":[{""name"":""firstItemToReinstall"",""version"":""1.2""},{""name"":""anotherItemToReinstall"",""version"":""2.2""}],""remove_packages"":[{""name"":""itemToRemove"",""version"":""5.0""}],""upgrade_packages"":[{""name"":""itemToUpgrade"",""current_version"":""1.0"",""new_version"":""1.1""}],""downgrade_packages"":[{""name"":""itemToDowngrade"",""current_version"":""2.5"",""new_version"":""2.4""}],""all_packages"":{""itemToAdd"":{""reason"":""add"",""new"":""1.0"",""old"":""n/a"",""name"":""itemToAdd""},""firstItemToReinstall"":{""reason"":""reinstall"",""new"":""1.2"",""old"":""1.2"",""name"":""firstItemToReinstall""},""anotherItemToReinstall"":{""reason"":""reinstall"",""new"":""2.2"",""old"":""2.2"",""name"":""anotherItemToReinstall""},""itemToRemove"":{""reason"":""remove"",""new"":""n/a"",""old"":""5.0"",""name"":""itemToRemove""},""itemToUpgrade"":{""reason"":""upgrade"",""new"":""1.2"",""old"":""1.1"",""name"":""itemToUpgrade""},""itemToDowngrade"":{""reason"":""downgrade"",""new"":""2.4"",""old"":""2.5"",""name"":""itemToDowngrade""}}}")
                   .AddTestsFor(m => m.ConnectionStatus)
                   .AddTestsFor(m => m.DownloadSize)
                   .AddTestsFor(m => m.LastCheck)
                   .AddTestsFor(m => m.OsVersion)
                   .AddTestsFor(m => m.ProductName)
                   .AddTestsFor(m => m.ProductVersion)
                   .AddTestsFor(m => m.RepositoryStatus)
                   .AddTestsFor(m => m.Updates)
                   .AddTestsFor(m => m.UpgradeMajorMessage)
                   .AddTestsFor(m => m.UpgradeMajorVersion)
                   .AddTestsFor(m => m.UpgradeNeedsReboot)
                   .AddTestsFor(m => m.StatusUpgradeAction)
                   .AddTestsFor(m => m.Status)
                   .AddTestsFor(m => m.StatusMessage)
                   .AddTestsFor(m => m.NewPackages, new[] {new VersionStatus.NewPackage() {Name           = "testItem", Version        = "0.0"},})
                   .AddTestsFor(m => m.ReinstallPackages, new[] {new VersionStatus.NewPackage() {Name     = "testItem", Version        = "0.0"},})
                   .AddTestsFor(m => m.RemovePackages, new[] {new VersionStatus.NewPackage() {Name        = "testItem", Version        = "0.0"},})
                   .AddTestsFor(m => m.UpgradePackages, new[] {new VersionStatus.UpgradePackage() {Name   = "testItem", CurrentVersion = "0.0", NewVersion = "0.1"}})
                   .AddTestsFor(m => m.DowngradePackages, new[] {new VersionStatus.UpgradePackage() {Name = "testItem", CurrentVersion = "0.0", NewVersion = "0.1"}})
                   .AddTestsFor(
                       m => m.AllPackages, new Dictionary<string, VersionStatus.ChangePackage>()
                       {
                           {"testItem", new VersionStatus.ChangePackage() {Name = "testItem", Change = "none", NewVersion = "1.0", OldVersion = "1.0"}}
                       }
                   )
                   .ToArray();
        }

        public VersionStatus_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        private void CompareList(VersionStatus expected, VersionStatus actual, Expression<Func<VersionStatus, VersionStatus.NewPackage[]>> prop)
        {
            var propInfo     = (PropertyInfo) ((MemberExpression) prop.Body).Member;
            var expectedList = propInfo.GetValue(expected) as VersionStatus.NewPackage[];
            var actualList   = propInfo.GetValue(actual) as VersionStatus.NewPackage[];
            if (expectedList is null)
            {
                Assert.True((actualList is null), $"{propInfo.Name} should be null.");
            }
            else
            {
                Assert.False((actualList is null), $"{propInfo.Name} should not be null.");
                Assert.True((expectedList.Length == actualList.Length), $"{propInfo.Name} length mismatch.");
                for (var i = 0; i < expectedList.Length; i++)
                {
                    var e = expectedList[i];
                    var a = actualList[i];
                    Assert.True((e.Name == a.Name), $"{propInfo.Name}[{i}] name mismatch.");
                    Assert.True((e.Version == a.Version), $"{propInfo.Name}[{i}] version mismatch.");
                }
            }
        }

        private void CompareList(VersionStatus expected, VersionStatus actual, Expression<Func<VersionStatus, VersionStatus.UpgradePackage[]>> prop)
        {
            var propInfo     = (PropertyInfo) ((MemberExpression) prop.Body).Member;
            var expectedList = propInfo.GetValue(expected) as VersionStatus.UpgradePackage[];
            var actualList   = propInfo.GetValue(actual) as VersionStatus.UpgradePackage[];
            if (expectedList is null)
            {
                Assert.True((actualList is null), $"{propInfo.Name} should be null.");
            }
            else
            {
                Assert.False((actualList is null), $"{propInfo.Name} should not be null.");
                Assert.True((expectedList.Length == actualList.Length), $"{propInfo.Name} length mismatch.");
                for (var i = 0; i < expectedList.Length; i++)
                {
                    var e = expectedList[i];
                    var a = actualList[i];
                    Assert.True((e.Name == a.Name), $"{propInfo.Name}[{i}] name mismatch.");
                    Assert.True((e.CurrentVersion == a.CurrentVersion), $"{propInfo.Name}[{i}] current version mismatch.");
                    Assert.True((e.NewVersion == a.NewVersion), $"{propInfo.Name}[{i}] new version mismatch.");
                }
            }
        }

        private void CompareList(VersionStatus expected, VersionStatus actual, Expression<Func<VersionStatus, Dictionary<string, VersionStatus.ChangePackage>>> prop)
        {
            var propInfo     = (PropertyInfo) ((MemberExpression) prop.Body).Member;
            var expectedList = propInfo.GetValue(expected) as Dictionary<string, VersionStatus.ChangePackage>;
            var actualList   = propInfo.GetValue(actual) as Dictionary<string, VersionStatus.ChangePackage>;
            if (expectedList is null)
            {
                Assert.True((actualList is null), $"{propInfo.Name} should be null.");
            }
            else
            {
                Assert.False((actualList is null), $"{propInfo.Name} should not be null.");
                Assert.True((expectedList.Count == actualList.Count), $"{propInfo.Name} count mismatch.");
                foreach (var key in expectedList.Keys)
                {
                    var e = expectedList[key];
                    Assert.True(actualList.TryGetValue(key, out var a));
                    Assert.True((e.Name == a.Name), $"{propInfo.Name}[{key}] name mismatch.");
                    Assert.True((e.Change == a.Change), $"{propInfo.Name}[{key}] change mismatch.");
                    Assert.True((e.OldVersion == a.OldVersion), $"{propInfo.Name}[{key}] old version mismatch.");
                    Assert.True((e.NewVersion == a.NewVersion), $"{propInfo.Name}[{key}] new version mismatch.");
                }
            }
        }

        protected override void Compare(VersionStatus expected, VersionStatus actual)
        {
            Assert.Equal(expected.ConnectionStatus, actual.ConnectionStatus);
            Assert.Equal(expected.DownloadSize, actual.DownloadSize);
            Assert.Equal(expected.LastCheck, actual.LastCheck);
            Assert.Equal(expected.OsVersion, actual.OsVersion);
            Assert.Equal(expected.ProductName, actual.ProductName);
            Assert.Equal(expected.ProductVersion, actual.ProductVersion);
            Assert.Equal(expected.RepositoryStatus, actual.RepositoryStatus);
            Assert.Equal(expected.Updates, actual.Updates);
            Assert.Equal(expected.UpgradeMajorMessage, actual.UpgradeMajorMessage);
            Assert.Equal(expected.UpgradeMajorVersion, actual.UpgradeMajorVersion);
            Assert.Equal(expected.UpgradeNeedsReboot, actual.UpgradeNeedsReboot);
            Assert.Equal(expected.StatusUpgradeAction, actual.StatusUpgradeAction);
            Assert.Equal(expected.Status, actual.Status);
            Assert.Equal(expected.StatusMessage, actual.StatusMessage);
            CompareList(expected, actual, x => x.NewPackages);
            CompareList(expected, actual, x => x.ReinstallPackages);
            CompareList(expected, actual, x => x.RemovePackages);
            CompareList(expected, actual, x => x.UpgradePackages);
            CompareList(expected, actual, x => x.DowngradePackages);
            CompareList(expected, actual, x => x.AllPackages);
        }

        protected override VersionStatus Expected => new VersionStatus()
        {
            ConnectionStatus    = "ok",
            DownloadSize        = "1MiB",
            LastCheck           = new DateTime(2020, 8, 6, 21, 39, 8, DateTimeKind.Utc),
            OsVersion           = "12.1",
            ProductName         = "opnsense",
            ProductVersion      = "20.7",
            RepositoryStatus    = "ok",
            Updates             = 6,
            UpgradeMajorMessage = "nada",
            UpgradeMajorVersion = "20.8",
            UpgradeNeedsReboot  = true,
            StatusUpgradeAction = "all",
            Status              = "Ok",
            StatusMessage       = "There are updates available.",
            NewPackages         = new[] {new VersionStatus.NewPackage() {Name     = "itemToAdd", Version              = "1.0"},},
            ReinstallPackages   = new[] {new VersionStatus.NewPackage() {Name     = "firstItemToReinstall", Version   = "1.2"}, new VersionStatus.NewPackage() {Name = "anotherItemToReinstall", Version = "2.2"}},
            RemovePackages      = new[] {new VersionStatus.NewPackage() {Name     = "itemToRemove", Version           = "5.0"},},
            UpgradePackages     = new[] {new VersionStatus.UpgradePackage() {Name = "itemToUpgrade", CurrentVersion   = "1.0", NewVersion = "1.1"},},
            DowngradePackages   = new[] {new VersionStatus.UpgradePackage() {Name = "itemToDowngrade", CurrentVersion = "2.5", NewVersion = "2.4"},},
            AllPackages = new Dictionary<string, VersionStatus.ChangePackage>()
            {
                {
                    "itemToAdd", new VersionStatus.ChangePackage()
                    {
                        Name       = "itemToAdd",
                        Change     = "add",
                        NewVersion = "1.0",
                        OldVersion = "n/a"
                    }
                },
                {
                    "firstItemToReinstall", new VersionStatus.ChangePackage()
                    {
                        Name       = "firstItemToReinstall",
                        Change     = "reinstall",
                        NewVersion = "1.2",
                        OldVersion = "1.2"
                    }
                },
                {
                    "anotherItemToReinstall", new VersionStatus.ChangePackage()
                    {
                        Name       = "anotherItemToReinstall",
                        Change     = "reinstall",
                        NewVersion = "2.2",
                        OldVersion = "2.2"
                    }
                },
                {
                    "itemToRemove", new VersionStatus.ChangePackage()
                    {
                        Name       = "itemToRemove",
                        Change     = "remove",
                        NewVersion = "n/a",
                        OldVersion = "5.0"
                    }
                },
                {
                    "itemToUpgrade", new VersionStatus.ChangePackage()
                    {
                        Name       = "itemToUpgrade",
                        Change     = "upgrade",
                        NewVersion = "1.2",
                        OldVersion = "1.1"
                    }
                },
                {
                    "itemToDowngrade", new VersionStatus.ChangePackage()
                    {
                        Name       = "itemToDowngrade",
                        Change     = "downgrade",
                        NewVersion = "2.4",
                        OldVersion = "2.5"
                    }
                }
            }
        };
    }
}
