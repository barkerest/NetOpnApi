using System;
using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models
{
    public class StatusWithUuid_Should : BaseModelTest<StatusWithUuid, StatusWithUuid_Should.Params>
    {
        public class Params : IEnumerable<JParam>
        {
            private static readonly IEnumerable<JParam> ParamList = new[]
            {
                new JParam(
                    "Default",
                    @"{""status"": ""ok"", ""msg_uuid"": ""6ead6b35-c968-467c-a820-582a23f5a228""}"
                ),
            };

            public IEnumerator<JParam> GetEnumerator() => ParamList.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


        public StatusWithUuid_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(StatusWithUuid expected, StatusWithUuid actual)
        {
            Assert.Equal(expected.Status, actual.Status);
        }

        protected override StatusWithUuid Expected { get; } = new StatusWithUuid(){ Status = "ok", Uuid = "6ead6b35-c968-467c-a820-582a23f5a228" };
        
    }
}
