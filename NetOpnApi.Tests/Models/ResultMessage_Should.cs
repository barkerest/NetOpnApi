using System.Collections.Generic;
using NetOpnApi.Models;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models
{
    public class ResultMessage_Should : BaseModelTest<ResultOnly, ResultMessage_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<ResultOnly>> GetList()
                => new ParamBuilder(@"{""result"":""OK""}")
                   .AddTestsFor(x => x.Result)
                   .ToArray();
        }

        public ResultMessage_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(ResultOnly expected, ResultOnly actual)
        {
            Assert.Equal(expected.Result, actual.Result);
        }

        protected override ResultOnly Expected => new ResultOnly() {Result = "OK"};
    }
}
