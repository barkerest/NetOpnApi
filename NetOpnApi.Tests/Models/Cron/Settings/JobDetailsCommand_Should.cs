using System.Collections.Generic;
using NetOpnApi.Models.Cron.Settings;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Cron.Settings
{
    public class JobDetailsCommand_Should : BaseModelTest<JobDetails.Command, JobDetailsCommand_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<JobDetails.Command>> GetList()
                => new ParamBuilder(@"{""value"":""Something"",""selected"":true}")
                   .AddTestsFor(x => x.Value)
                   .AddTestsFor(x => x.Selected)
                   .ToArray();
        }

        public JobDetailsCommand_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(JobDetails.Command expected, JobDetails.Command actual)
        {
            Assert.Equal(expected.Value, actual.Value);
            Assert.Equal(expected.Selected, actual.Selected);
        }

        protected override JobDetails.Command Expected => new JobDetails.Command() {Value = "Something", Selected = true};
    }
}
