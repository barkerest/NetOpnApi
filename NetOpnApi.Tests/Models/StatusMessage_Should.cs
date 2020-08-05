using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models
{
    public class StatusMessage_Should : BaseModelTest<StatusMessage, StatusMessage_Should.Params>
    {
        public class Params : IEnumerable<JParam>
        {
            private static readonly IEnumerable<JParam> ParamList = new[]
            {
                new JParam(
                    "Default",
                    @"{""status"": ""ok""}"
                ),
            };

            public IEnumerator<JParam> GetEnumerator() => ParamList.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


        public StatusMessage_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(StatusMessage expected, StatusMessage actual)
        {
            Assert.Equal(expected.Status, actual.Status);
        }

        protected override StatusMessage Expected { get; } = new StatusMessage(){ Status = "ok" };
        
    }
}
