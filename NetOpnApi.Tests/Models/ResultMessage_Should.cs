using System.Collections.Generic;
using NetOpnApi.Models;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models
{
    public class ResultMessage_Should : BaseModelTest<ResultMessage, ResultMessage_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<ResultMessage>> GetList()
                => new ParamBuilder(@"{""result"":""OK""}")
                   .AddTestsFor(x => x.Result)
                   .ToArray();
        }

        public ResultMessage_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(ResultMessage expected, ResultMessage actual)
        {
            Assert.Equal(expected.Result, actual.Result);
        }

        protected override ResultMessage Expected => new ResultMessage() {Result = "OK"};
    }
}
