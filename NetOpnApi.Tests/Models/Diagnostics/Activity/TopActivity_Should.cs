using System.Collections.Generic;
using NetOpnApi.Models.Diagnostics.Activity;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Diagnostics.Activity
{
    public class TopActivity_Should : BaseModelTest<TopActivity, TopActivity_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<TopActivity>> GetList()
                => new ParamBuilder(
                       @"{
    ""headers"": [
        ""last pid: 91248;  load averages:  0.33,  0.35,  0.30  up 0+07:52:48    19:50:15"",
        ""109 threads:   3 running, 91 sleeping, 15 waiting"",
        ""CPU:  0.4% user,  0.0% nice,  0.5% system,  0.0% interrupt, 99.0% idle"",
        ""Mem: 48M Active, 78M Inact, 189M Wired, 117M Buf, 1640M Free"",
        ""Swap: 4096M Total, 4096M Free""
    ],
    ""details"": [
        {
            ""PID"": ""11"",
            ""USERNAME"": ""root"",
            ""PRI"": ""155"",
            ""NICE"": ""ki31"",
            ""SIZE"": ""0"",
            ""RES"": ""32K"",
            ""STATE"": ""CPU1"",
            ""C"": ""1"",
            ""TIME"": ""468:51"",
            ""WCPU"": ""100.00%"",
            ""COMMAND"": ""[idle{idle: cpu1}]""
        },
        {
            ""PID"": ""11"",
            ""USERNAME"": ""root"",
            ""PRI"": ""155"",
            ""NICE"": ""ki31"",
            ""SIZE"": ""0"",
            ""RES"": ""32K"",
            ""STATE"": ""RUN"",
            ""C"": ""0"",
            ""TIME"": ""467:20"",
            ""WCPU"": ""100.00%"",
            ""COMMAND"": ""[idle{idle: cpu0}]""
        }
	]
}"
                   )
                   .AddTestsFor(x => x.Headers, new[] {"Hello", "World"})
                   .AddTestsFor(
                       x => x.Details, new[]
                       {
                           new TopActivity.Detail()
                           {
                               ProcessID       = 1234,
                               Command         = "do this",
                               CpuTime         = "1:23",
                               ProcessorNumber = 1,
                               NicePriority    = "-5",
                               ProcessPriority = "5",
                               Size            = "123K",
                               ResidentMemory  = "64K",
                               State           = "RUN",
                               Username        = "bob"
                           },
                           new TopActivity.Detail()
                           {
                               ProcessID       = 4321,
                               Command         = "do that",
                               CpuTime         = "755:55",
                               ProcessorNumber = 4,
                               NicePriority    = "55",
                               ProcessPriority = "-55",
                               Size            = "321K",
                               ResidentMemory  = "46K",
                               State           = "SLEEP",
                               Username        = "jane"
                           },
                       }
                   )
                   .ToArray();
        }

        public TopActivity_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(TopActivity expected, TopActivity actual)
        {
            if (expected.Headers is null)
            {
                Assert.Null(actual.Headers);
            }
            else
            {
                Assert.NotNull(actual.Headers);
                Assert.Equal(expected.Headers.Length, actual.Headers.Length);
                for (var i = 0; i < expected.Headers.Length; i++)
                {
                    Assert.Equal(expected.Headers[i], actual.Headers[i]);
                }
            }

            if (expected.Details is null)
            {
                Assert.Null(actual.Details);
            }
            else
            {
                Assert.NotNull(actual.Details);
                Assert.Equal(expected.Details.Length, actual.Details.Length);
                for (var i = 0; i < expected.Details.Length; i++)
                {
                    var e = expected.Details[i];
                    var a = actual.Details[i];
                    Assert.Equal(e.ProcessID, a.ProcessID);
                    Assert.Equal(e.Username, a.Username);
                    Assert.Equal(e.ProcessPriority, a.ProcessPriority);
                    Assert.Equal(e.NicePriority, a.NicePriority);
                    Assert.Equal(e.Size, a.Size);
                    Assert.Equal(e.ResidentMemory, a.ResidentMemory);
                    Assert.Equal(e.State, a.State);
                    Assert.Equal(e.ProcessorNumber, a.ProcessorNumber);
                    Assert.Equal(e.CpuTime, a.CpuTime);
                    Assert.Equal(e.WeightedCpuPercentage, a.WeightedCpuPercentage);
                    Assert.Equal(e.Command, a.Command);
                }
            }
        }

        protected override TopActivity Expected => new TopActivity()
        {
            Headers = new[]
            {
                "last pid: 91248;  load averages:  0.33,  0.35,  0.30  up 0+07:52:48    19:50:15",
                "109 threads:   3 running, 91 sleeping, 15 waiting",
                "CPU:  0.4% user,  0.0% nice,  0.5% system,  0.0% interrupt, 99.0% idle",
                "Mem: 48M Active, 78M Inact, 189M Wired, 117M Buf, 1640M Free",
                "Swap: 4096M Total, 4096M Free"
            },
            Details = new[]
            {
                new TopActivity.Detail()
                {
                    ProcessID             = 11,
                    Username              = "root",
                    ProcessPriority       = "155",
                    NicePriority          = "ki31",
                    Size                  = "0",
                    ResidentMemory        = "32K",
                    State                 = "CPU1",
                    ProcessorNumber       = 1,
                    CpuTime               = "468:51",
                    WeightedCpuPercentage = "100.00%",
                    Command               = "[idle{idle: cpu1}]"
                },
                new TopActivity.Detail()
                {
                    ProcessID             = 11,
                    Username              = "root",
                    ProcessPriority       = "155",
                    NicePriority          = "ki31",
                    Size                  = "0",
                    ResidentMemory        = "32K",
                    State                 = "RUN",
                    ProcessorNumber       = 0,
                    CpuTime               = "467:20",
                    WeightedCpuPercentage = "100.00%",
                    Command               = "[idle{idle: cpu0}]"
                },
            }
        };
    }
}
