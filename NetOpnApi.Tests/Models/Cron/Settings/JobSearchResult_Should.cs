using System;
using System.Collections.Generic;
using System.Linq;
using NetOpnApi.Models.Cron.Settings;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Cron.Settings
{
    public class JobSearchResult_Should : BaseModelTest<JobSearchResult, JobSearchResult_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<JobSearchResult>> GetList()
                => new ParamBuilder(@"{""rows"":[{""uuid"":""fee18a63-19d7-4daa-8007-538cfd72e505"",""enabled"":""0"",""minutes"":""0"",""hours"":""0"",""days"":""*"",""months"":""*"",""weekdays"":""*"",""description"":""Download changelog at midnight."",""command"":""Firmware changelog update"",""origin"":""cron""}],""rowCount"":1,""total"":1,""current"":1}")
                   .AddTestsFor(x => x.TotalItems)
                   .AddTestsFor(x => x.ItemsPerPage)
                   .AddTestsFor(x => x.CurrentPage)
                   .AddTestsFor(
                       x => x.Rows, new[]
                       {
                           new JobSearchEntry() {JobId = Guid.NewGuid(), Origin = "test", Description = "Test Job 1", Enabled = false},
                           new JobSearchEntry() {JobId = Guid.NewGuid(), Origin = "test", Description = "Test Job 2", Enabled = true},
                           new JobSearchEntry() {JobId = Guid.NewGuid(), Origin = "test", Description = "Test Job 3", Enabled = false},
                           new JobSearchEntry() {JobId = Guid.NewGuid(), Origin = "test", Description = "Test Job 4", Enabled = true},
                       }
                   )
                   .ToArray();
        }


        public JobSearchResult_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(JobSearchResult expected, JobSearchResult actual)
        {
            Assert.Equal(expected.CurrentPage, actual.CurrentPage);
            Assert.Equal(expected.ItemsPerPage, actual.ItemsPerPage);
            Assert.Equal(expected.TotalItems, actual.TotalItems);
            if (expected.Rows is null)
            {
                Assert.Null(actual.Rows);
            }
            else
            {
                Assert.NotNull(actual.Rows);
                Assert.Equal(expected.Rows.Length, actual.Rows.Length);
                for (var i = 0; i < expected.Rows.Length; i++)
                {
                    var e = expected.Rows[i];
                    var a = actual.Rows[i];
                    Assert.Equal(e.JobId, a.JobId);
                    Assert.Equal(e.Enabled, a.Enabled);
                    Assert.Equal(e.Origin, a.Origin);
                    Assert.Equal(e.Description, a.Description);
                }
            }
        }

        protected override JobSearchResult Expected => new JobSearchResult()
        {
            Rows = new[]
            {
                new JobSearchEntry()
                {
                    JobId       = Guid.Parse("fee18a63-19d7-4daa-8007-538cfd72e505"),
                    Enabled     = false,
                    Minutes     = "0",
                    Hours       = "0",
                    Days        = "*",
                    Months      = "*",
                    Weekdays    = "*",
                    Description = "Download changelog at midnight.",
                    Command     = "Firmware changelog update",
                    Origin      = "cron"
                },
            },
            ItemsPerPage = 1,
            TotalItems   = 1,
            CurrentPage  = 1
        };
    }
}
