using System.Collections.Generic;
using NetOpnApi.Models;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models
{
    public class NonJsonContent_Should : BaseModelTest<NonJsonContent, NonJsonContent_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<NonJsonContent>> GetList()
                => new ParamBuilder(@"{""content"":""<span>Hello</span>"",""contentType"":""text/html""}")
                   .AddTestsFor(x => x.Content)
                   .AddTestsFor(x => x.ContentType)
                   .ToArray();
        }

        public NonJsonContent_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(NonJsonContent expected, NonJsonContent actual)
        {
            Assert.Equal(expected.Content, actual.Content);
            Assert.Equal(expected.ContentType, actual.ContentType);
        }

        protected override NonJsonContent Expected => new NonJsonContent()
        {
            Content     = "<span>Hello</span>",
            ContentType = "text/html"
        };
    }
}
