using System;
using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class InfoChangeLog_Should : BaseModelTest<Info.ChangeLogEntry, InfoChangeLog_Should.Params>
    {
        public class Params : IEnumerable<ModelTestParam<Info.ChangeLogEntry>>
        {
            private static readonly IEnumerable<ModelTestParam<Info.ChangeLogEntry>> ParamList
                = new ParamBuilder(@"{""series"": ""20.1"",""version"": ""20.1.9"",""date"": ""2020-07-23""}")
                  .AddTestsFor(m => m.Series)
                  .AddTestsFor(m => m.Version)
                  .AddTestsFor(m => m.Date)
                  .ToArray();

            public IEnumerator<ModelTestParam<Info.ChangeLogEntry>> GetEnumerator() => ParamList.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


        public InfoChangeLog_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(Info.ChangeLogEntry expected, Info.ChangeLogEntry actual)
        {
            Assert.Equal(expected.Series, actual.Series);
            Assert.Equal(expected.Version, actual.Version);
            Assert.Equal(expected.Date, actual.Date);
        }

        protected override Info.ChangeLogEntry Expected => new Info.ChangeLogEntry()
        {
            Series  = "20.1",
            Version = "20.1.9",
            Date    = new DateTime(2020, 7, 23)
        };
    }
}
