using System.Collections.Generic;
using NetOpnApi.Models.Cron.Settings;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Cron.Settings
{
    public class JobDetails_Should : BaseModelTest<JobDetails, JobDetails_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<JobDetails>> GetList()
                => new ParamBuilder(
                       @"{
    ""origin"": ""cron"",
    ""enabled"": ""1"",
    ""minutes"": ""0"",
    ""hours"": ""0"",
    ""days"": ""*"",
    ""months"": ""*"",
    ""weekdays"": ""*"",
    ""who"": ""root"",
    ""command"": {
        ""firmware auto-update"": {
            ""value"": ""Automatic firmware update"",
            ""selected"": 0
        },
        ""proxy fetchacls"": {
            ""value"": ""Download and reload external proxy ACLs"",
            ""selected"": 0
        },
        ""proxy downloadacls"": {
            ""value"": ""Download external proxy ACLs"",
            ""selected"": 0
        },
        ""unbound dnsbl"": {
            ""value"": ""Download Unbound DNSBLs and restart"",
            ""selected"": 0
        },
        ""unboundplus dnsbl"": {
            ""value"": ""Download Unbound DNSBLs and restart"",
            ""selected"": 0
        },
        ""dyndns reload"": {
            ""value"": ""Dynamic DNS Update"",
            ""selected"": 0
        },
        ""firmware changelog fetch"": {
            ""value"": ""Firmware changelog update"",
            ""selected"": 0
        },
        ""system reboot"": {
            ""value"": ""Issue a reboot"",
            ""selected"": 0
        },
        ""interface reconfigure"": {
            ""value"": ""Periodic interface reset"",
            ""selected"": 0
        },
        ""plugins configure"": {
            ""value"": ""Reconfigure a plugin facility"",
            ""selected"": 0
        },
        ""system ssl dhparam"": {
            ""value"": ""Regenerate DH parameters"",
            ""selected"": 0
        },
        ""ids reload"": {
            ""value"": ""Reload intrusion detection rules"",
            ""selected"": 0
        },
        ""system remote backup"": {
            ""value"": ""Remote backup"",
            ""selected"": 0
        },
        ""captiveportal restart"": {
            ""value"": ""Restart Captive Portal service"",
            ""selected"": 0
        },
        ""ipsec restart"": {
            ""value"": ""Restart IPsec service"",
            ""selected"": 0
        },
        ""filter refresh_aliases"": {
            ""value"": ""Update and reload firewall aliases"",
            ""selected"": 0
        },
        ""ids update"": {
            ""value"": ""Update and reload intrusion detection rules"",
            ""selected"": 0
        }
    },
    ""parameters"": """",
    ""description"": """"
}"
                   )
                   .AddTestsFor(x => x.Origin)
                   .AddTestsFor(x => x.Enabled)
                   .AddTestsFor(x => x.Minutes)
                   .AddTestsFor(x => x.Hours)
                   .AddTestsFor(x => x.Days)
                   .AddTestsFor(x => x.Months)
                   .AddTestsFor(x => x.Weekdays)
                   .AddTestsFor(x => x.Username)
                   .AddTestsFor(x => x.Parameters)
                   .AddTestsFor(x => x.Description)
                   .AddTestsFor(
                       x => x.Commands, new Dictionary<string, JobDetails.Command>()
                       {
                           {"do something", new JobDetails.Command() {Value = "Something that needs doing", Selected = true}}
                       }
                   )
                   .ToArray();
        }


        public JobDetails_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(JobDetails expected, JobDetails actual)
        {
            Assert.Equal(expected.Origin, actual.Origin);
            Assert.Equal(expected.Enabled, actual.Enabled);
            Assert.Equal(expected.Minutes, actual.Minutes);
            Assert.Equal(expected.Hours, actual.Hours);
            Assert.Equal(expected.Days, actual.Days);
            Assert.Equal(expected.Months, actual.Months);
            Assert.Equal(expected.Weekdays, actual.Weekdays);
            Assert.Equal(expected.Username, actual.Username);
            Assert.Equal(expected.Parameters, actual.Parameters);
            Assert.Equal(expected.Description, actual.Description);
            if (expected.Commands is null)
            {
                Assert.Null(actual.Commands);
            }
            else
            {
                Assert.NotNull(actual.Commands);
                Assert.Equal(expected.Commands.Count, actual.Commands.Count);
                foreach (var item in expected.Commands)
                {
                    Assert.True(actual.Commands.TryGetValue(item.Key, out var actualItem));
                    Assert.NotNull(actualItem);
                    Assert.NotNull(item.Value);
                    Assert.Equal(item.Value.Value, actualItem.Value);
                    Assert.Equal(item.Value.Selected, actualItem.Selected);
                }
            }
        }

        protected override JobDetails Expected =>
            new JobDetails()
            {
                Origin      = "cron",
                Enabled     = true,
                Minutes     = "0",
                Hours       = "0",
                Days        = "*",
                Months      = "*",
                Weekdays    = "*",
                Username    = "root",
                Parameters  = "",
                Description = "",
                Commands = new Dictionary<string, JobDetails.Command>()
                {
                    {
                        "firmware auto-update", new JobDetails.Command
                        {
                            Value    = "Automatic firmware update",
                            Selected = false
                        }
                    },
                    {
                        "proxy fetchacls", new JobDetails.Command
                        {
                            Value    = "Download and reload external proxy ACLs",
                            Selected = false
                        }
                    },
                    {
                        "proxy downloadacls", new JobDetails.Command
                        {
                            Value    = "Download external proxy ACLs",
                            Selected = false
                        }
                    },
                    {
                        "unbound dnsbl", new JobDetails.Command
                        {
                            Value    = "Download Unbound DNSBLs and restart",
                            Selected = false
                        }
                    },
                    {
                        "unboundplus dnsbl", new JobDetails.Command
                        {
                            Value    = "Download Unbound DNSBLs and restart",
                            Selected = false
                        }
                    },
                    {
                        "dyndns reload", new JobDetails.Command
                        {
                            Value    = "Dynamic DNS Update",
                            Selected = false
                        }
                    },
                    {
                        "firmware changelog fetch", new JobDetails.Command
                        {
                            Value    = "Firmware changelog update",
                            Selected = false
                        }
                    },
                    {
                        "system reboot", new JobDetails.Command
                        {
                            Value    = "Issue a reboot",
                            Selected = false
                        }
                    },
                    {
                        "interface reconfigure", new JobDetails.Command
                        {
                            Value    = "Periodic interface reset",
                            Selected = false
                        }
                    },
                    {
                        "plugins configure", new JobDetails.Command
                        {
                            Value    = "Reconfigure a plugin facility",
                            Selected = false
                        }
                    },
                    {
                        "system ssl dhparam", new JobDetails.Command
                        {
                            Value    = "Regenerate DH parameters",
                            Selected = false
                        }
                    },
                    {
                        "ids reload", new JobDetails.Command
                        {
                            Value    = "Reload intrusion detection rules",
                            Selected = false
                        }
                    },
                    {
                        "system remote backup", new JobDetails.Command
                        {
                            Value    = "Remote backup",
                            Selected = false
                        }
                    },
                    {
                        "captiveportal restart", new JobDetails.Command
                        {
                            Value    = "Restart Captive Portal service",
                            Selected = false
                        }
                    },
                    {
                        "ipsec restart", new JobDetails.Command
                        {
                            Value    = "Restart IPsec service",
                            Selected = false
                        }
                    },
                    {
                        "filter refresh_aliases", new JobDetails.Command
                        {
                            Value    = "Update and reload firewall aliases",
                            Selected = false
                        }
                    },
                    {
                        "ids update", new JobDetails.Command
                        {
                            Value    = "Update and reload intrusion detection rules",
                            Selected = false
                        }
                    }
                }
            };
    }
}
