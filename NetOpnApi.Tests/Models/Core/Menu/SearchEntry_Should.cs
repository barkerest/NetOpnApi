using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Menu;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Menu
{
    public class SearchEntry_Should : BaseModelTest<SearchEntry, SearchEntry_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<SearchEntry>> GetList()
                => new ParamBuilder(@"{""Id"": ""Alpha"", ""Order"": 42, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": ""/path/to/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": true, ""isVisible"": true, ""breadcrumb"": ""Alpha / Alpha"", ""depth"": 2}")
                   .AddTestsFor(m => m.ID)
                   .AddTestsFor(m => m.Order)
                   .AddTestsFor(m => m.VisibleName)
                   .AddTestsFor(m => m.CssClass)
                   .AddTestsFor(m => m.Url)
                   .AddTestsFor(m => m.IsExternal)
                   .AddTestsFor(m => m.Visibility)
                   .AddTestsFor(m => m.Selected)
                   .AddTestsFor(m => m.IsVisible)
                   .AddTestsFor(m => m.Breadcrumb)
                   .AddTestsFor(m => m.Depth)
                   .ToArray();
        }


        public SearchEntry_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(SearchEntry expected, SearchEntry actual)
        {
            Assert.Equal(expected.ID, actual.ID);
            Assert.Equal(expected.Order, actual.Order);
            Assert.Equal(expected.VisibleName, actual.VisibleName);
            Assert.Equal(expected.CssClass, actual.CssClass);
            Assert.Equal(expected.Url, actual.Url);
            Assert.Equal(expected.IsExternal, actual.IsExternal);
            Assert.Equal(expected.Visibility, actual.Visibility);
            Assert.Equal(expected.Selected, actual.Selected);
            Assert.Equal(expected.IsVisible, actual.IsVisible);
            Assert.Equal(expected.Breadcrumb, actual.Breadcrumb);
            Assert.Equal(expected.Depth, actual.Depth);
        }

        protected override SearchEntry Expected => new SearchEntry()
        {
            ID          = "Alpha",
            Order       = 42,
            VisibleName = "Alpha",
            CssClass    = "something",
            Url         = "/path/to/something",
            IsExternal  = true,
            Visibility  = "all",
            Selected    = true,
            IsVisible   = true,
            Breadcrumb  = "Alpha / Alpha",
            Depth       = 2
        };
    }
}
