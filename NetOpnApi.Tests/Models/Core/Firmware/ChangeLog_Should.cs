using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Firmware;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Firmware
{
    public class ChangeLog_Should : BaseModelTest<ChangeLog, ChangeLog_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<ChangeLog>> GetList()
                => new ParamBuilder(@"{""text"":""This is some text."", ""html"":""<span>This is some text.</span>""}")
                   .AddTestsFor(x => x.Text)
                   .AddTestsFor(x => x.Html)
                   .ToArray();
        }

        public ChangeLog_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(ChangeLog expected, ChangeLog actual)
        {
            Assert.Equal(expected.Text, actual.Text);
            Assert.Equal(expected.Html, actual.Html);
        }

        protected override ChangeLog Expected => new ChangeLog()
        {
            Text = "This is some text.",
            Html = "<span>This is some text.</span>"
        };
    }
}
