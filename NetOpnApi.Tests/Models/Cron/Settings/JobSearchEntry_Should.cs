using System;
using System.Collections.Generic;
using NetOpnApi.Models.Cron.Settings;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Cron.Settings
{
    public class JobSearchEntry_Should : BaseModelTest<JobSearchEntry, JobSearchEntry_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<JobSearchEntry>> GetList()
                => new ParamBuilder(@"{""origin"":""cron"",""command"":""Do Something"",""enabled"":true,""description"":""Do Something Cool"",""minutes"":""0"",""hours"":""0"",""days"":""*"",""months"":""*"",""weekdays"":""*"",""uuid"":""6ead6b35-c968-467c-a820-582a23f5a228""}")
                   .AddTestsFor(x => x.Origin)
                   .AddTestsFor(x => x.Enabled)
                   .AddTestsFor(x => x.Command)
                   .AddTestsFor(x => x.Description)
                   .AddTestsFor(x => x.Minutes)
                   .AddTestsFor(x => x.Hours)
                   .AddTestsFor(x => x.Days)
                   .AddTestsFor(x => x.Months)
                   .AddTestsFor(x => x.Weekdays)
                   .AddTestsFor(x => x.JobId)
                   .ToArray();
        }


        public JobSearchEntry_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(JobSearchEntry expected, JobSearchEntry actual)
        {
            Assert.Equal(expected.Origin, actual.Origin);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.Command, actual.Command);
            Assert.Equal(expected.Enabled, actual.Enabled);
            Assert.Equal(expected.Days, actual.Days);
            Assert.Equal(expected.Months, actual.Months);
            Assert.Equal(expected.Weekdays, actual.Weekdays);
            Assert.Equal(expected.Minutes, actual.Minutes);
            Assert.Equal(expected.Hours, actual.Hours);
            Assert.Equal(expected.JobId, actual.JobId);
        }

        protected override JobSearchEntry Expected => new JobSearchEntry()
        {
            Origin      = "cron",
            Command     = "Do Something",
            Enabled     = true,
            Description = "Do Something Cool",
            Minutes     = "0",
            Hours       = "0",
            Days        = "*",
            Months      = "*",
            Weekdays    = "*",
            JobId       = Guid.Parse("6ead6b35-c968-467c-a820-582a23f5a228")
        };
    }
}
