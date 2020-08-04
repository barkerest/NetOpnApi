using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Menu;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Menu
{
    public class SearchEntry_Should : BaseModelTest<SearchEntry, SearchEntry_Should.Params>
    {
        public class Params : IEnumerable<JParam>
        {
            private static readonly IEnumerable<JParam> ParamList = new []
            {
                new JParam(
                    "Default",
                    @"{""Id"": ""Alpha"", ""Order"": 42, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": ""/path/to/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": true, ""isVisible"": true, ""breadcrumb"": ""Alpha / Alpha"", ""depth"": 2}"
                ),
                new JParam(
                    "Alpha.Order = '42'",
                    @"{""Id"": ""Alpha"", ""Order"": ""42"", ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": ""/path/to/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": true, ""isVisible"": true, ""breadcrumb"": ""Alpha / Alpha"", ""depth"": 2}"
                ),
                new JParam(
                    "Alpha.IsExternal = 'yes'",
                    @"{""Id"": ""Alpha"", ""Order"": 42, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": ""/path/to/something"", ""IsExternal"": ""yes"", ""Visibility"": ""all"", ""Selected"": true, ""isVisible"": true, ""breadcrumb"": ""Alpha / Alpha"", ""depth"": 2}"
                ),
                new JParam(
                    "Alpha.Selected = 'yes'",
                    @"{""Id"": ""Alpha"", ""Order"": 42, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": ""/path/to/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": ""yes"", ""isVisible"": true, ""breadcrumb"": ""Alpha / Alpha"", ""depth"": 2}"
                ),
                new JParam(
                    "Alpha.IsVisible = 'yes'",
                    @"{""Id"": ""Alpha"", ""Order"": 42, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": ""/path/to/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": true, ""isVisible"": ""yes"", ""breadcrumb"": ""Alpha / Alpha"", ""depth"": 2}"
                ),
                new JParam(
                    "Alpha.Depth = '2'",
                    @"{""Id"": ""Alpha"", ""Order"": 42, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": ""/path/to/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": true, ""isVisible"": true, ""breadcrumb"": ""Alpha / Alpha"", ""depth"": ""2""}"
                ),
            };

            public IEnumerator<JParam> GetEnumerator() => ParamList.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
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

        protected override SearchEntry Expected { get; } = new SearchEntry()
        {
            ID = "Alpha",
            Order = 42,
            VisibleName = "Alpha",
            CssClass = "something",
            Url = "/path/to/something",
            IsExternal = true,
            Visibility = "all",
            Selected = true,
            IsVisible = true,
            Breadcrumb = "Alpha / Alpha",
            Depth = 2
        };
    }
}
