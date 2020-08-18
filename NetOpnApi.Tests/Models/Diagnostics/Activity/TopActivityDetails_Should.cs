using System.Collections.Generic;
using NetOpnApi.Models.Diagnostics.Activity;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Diagnostics.Activity
{
    public class TopActivityDetails_Should : BaseModelTest<TopActivity.Detail, TopActivityDetails_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<TopActivity.Detail>> GetList()
                => new ParamBuilder(
                       @"{
	""PID"": ""45352"",
	""USERNAME"": ""root"",
	""PRI"": ""21"",
	""NICE"": ""0"",
	""SIZE"": ""40M"",
	""RES"": ""21M"",
	""STATE"": ""accept"",
	""C"": ""1"",
	""TIME"": ""0:02"",
	""WCPU"": ""0.00%"",
	""COMMAND"": ""/usr/local/bin/php-cgi""
}"
                   )
                   .AddTestsFor(x => x.ProcessorNumber)
                   .AddTestsFor(x => x.ProcessID)
                   .AddTestsFor(x => x.Username)
                   .AddTestsFor(x => x.ProcessPriority)
                   .AddTestsFor(x => x.NicePriority)
                   .AddTestsFor(x => x.Size)
                   .AddTestsFor(x => x.ResidentMemory)
                   .AddTestsFor(x => x.State)
                   .AddTestsFor(x => x.CpuTime)
                   .AddTestsFor(x => x.WeightedCpuPercentage)
                   .AddTestsFor(x => x.Command)
                   .ToArray();
        }


        public TopActivityDetails_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(TopActivity.Detail expected, TopActivity.Detail actual)
        {
            Assert.Equal(expected.ProcessID, actual.ProcessID);
            Assert.Equal(expected.Username, actual.Username);
            Assert.Equal(expected.ProcessPriority, actual.ProcessPriority);
            Assert.Equal(expected.NicePriority, actual.NicePriority);
            Assert.Equal(expected.Size, actual.Size);
            Assert.Equal(expected.ResidentMemory, actual.ResidentMemory);
            Assert.Equal(expected.State, actual.State);
            Assert.Equal(expected.ProcessorNumber, actual.ProcessorNumber);
            Assert.Equal(expected.CpuTime, actual.CpuTime);
            Assert.Equal(expected.WeightedCpuPercentage, actual.WeightedCpuPercentage);
            Assert.Equal(expected.Command, actual.Command);
        }

        protected override TopActivity.Detail Expected => new TopActivity.Detail()
        {
            ProcessID             = 45352,
            Username              = "root",
            ProcessPriority       = "21",
            NicePriority          = "0",
            Size                  = "40M",
            ResidentMemory        = "21M",
            State                 = "accept",
            ProcessorNumber       = 1,
            CpuTime               = "0:02",
            WeightedCpuPercentage = "0.00%",
            Command               = "/usr/local/bin/php-cgi"
        };
    }
}
