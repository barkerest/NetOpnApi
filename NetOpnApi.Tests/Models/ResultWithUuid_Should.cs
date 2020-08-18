using System;
using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models
{
    public class ResultWithUuid_Should : BaseModelTest<ResultWithUuid, ResultWithUuid_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<ResultWithUuid>> GetList()
                => new ParamBuilder(@"{""result"": ""ok"", ""uuid"": ""6ead6b35-c968-467c-a820-582a23f5a228""}")
                   .AddTestsFor(m => m.Result)
                   .AddTestsFor(m => m.Uuid)
                   .ToArray();
        }


        public ResultWithUuid_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(ResultWithUuid expected, ResultWithUuid actual)
        {
            Assert.Equal(expected.Result, actual.Result);
            Assert.Equal(expected.Uuid, actual.Uuid);
        }

        protected override ResultWithUuid Expected => new ResultWithUuid() {Result = "ok", Uuid = Guid.Parse("6ead6b35-c968-467c-a820-582a23f5a228")};
    }
}
