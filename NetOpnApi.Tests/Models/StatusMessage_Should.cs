using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models
{
    public class StatusMessage_Should : BaseModelTest<StatusOnly, StatusMessage_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<StatusOnly>> GetList()
                => new ParamBuilder(@"{""status"": ""ok""}")
                   .AddTestsFor(m => m.Status)
                   .ToArray();
        }


        public StatusMessage_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(StatusOnly expected, StatusOnly actual)
        {
            Assert.Equal(expected.Status, actual.Status);
        }

        protected override StatusOnly Expected => new StatusOnly() {Status = "ok"};
    }
}
