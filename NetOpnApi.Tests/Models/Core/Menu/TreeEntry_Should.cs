using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Menu;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Menu
{
    public class TreeEntry_Should : BaseModelTest<TreeEntry, TreeEntry_Should.Params>
    {
        public class Params : IEnumerable<JParam>
        {
            private static readonly IEnumerable<JParam> ParamList = new[]
            {
                new JParam(
                    "Default",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Alpha.Order = '1234'",
                    @"{""Id"": ""Alpha"", ""Order"": ""1234"", ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Alpha.IsExternal = 'false'",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": ""false"", ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Alpha.IsExternal = 'no'",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": ""no"", ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Alpha.IsExternal = 0",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": 0, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Alpha.Selected = 'false'",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": ""false"", ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Alpha.Selected = 'N'",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": ""N"", ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Alpha.Selected = '0'",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": ""0"", ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Alpha.IsVisible = 101",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": 101}"
                ),
                new JParam(
                    "Alpha.IsVisible = -1",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": -1}"
                ),
                new JParam(
                    "Alpha.IsVisible = 'y'",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": ""y""}"
                ),
                new JParam(
                    "Bravo.Selected = 'true'",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": ""true"", ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Bravo.Selected = 'yes'",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": ""yes"", ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Bravo.Selected = '1'",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": ""1"", ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Bravo.Selected = 1",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": 1, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Charlie.IsVisible = 'F'",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": ""F""}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Charlie.IsVisible = 'N/A'",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": ""N/A""}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Delta.IsExternal = 1",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": 1, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
                new JParam(
                    "Delta.IsExternal = 't'",
                    @"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": ""t"", ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}"
                ),
            };

            public IEnumerator<JParam> GetEnumerator() => ParamList.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        protected override TreeEntry Expected { get; } = new TreeEntry()
        {
            ID          = "Alpha",
            Order       = 1234,
            VisibleName = "Alpha",
            CssClass    = "something",
            Url         = "",
            IsExternal  = false,
            Visibility  = "all",
            Selected    = false,
            Children = new[]
            {
                new TreeEntry()
                {
                    ID          = "Bravo",
                    Order       = 0,
                    VisibleName = "Bravo",
                    CssClass    = "something",
                    Url         = "/index.php",
                    IsExternal  = false,
                    Visibility  = "all",
                    Selected    = true,
                    Children = new[]
                    {
                        new TreeEntry()
                        {
                            ID          = "Charlie",
                            Order       = 0,
                            VisibleName = "Charlie",
                            CssClass    = "",
                            Url         = "/index.php*",
                            IsExternal  = false,
                            Visibility  = "hidden",
                            Selected    = false,
                            Children    = new TreeEntry[0],
                            IsVisible   = false
                        },
                    },
                    IsVisible = true
                },
                new TreeEntry()
                {
                    ID          = "Delta",
                    Order       = 1,
                    VisibleName = "Delta",
                    CssClass    = "something",
                    Url         = "/ui/core/something",
                    IsExternal  = true,
                    Visibility  = "all",
                    Selected    = false,
                    Children    = new TreeEntry[0],
                    IsVisible   = true
                },
            },
            IsVisible = true
        };

        protected override void Compare(TreeEntry a, TreeEntry b)
        {
            Output.WriteLine($"Comparing entry {a.ID}...");
            Assert.NotNull(b);
            Assert.Equal(a.ID, b.ID);
            Assert.Equal(a.Order, b.Order);
            Assert.Equal(a.VisibleName, b.VisibleName);
            Assert.Equal(a.CssClass, b.CssClass);
            Assert.Equal(a.Url, b.Url);
            Assert.Equal(a.IsExternal, b.IsExternal);
            Assert.Equal(a.Visibility, b.Visibility);
            Assert.Equal(a.Selected, b.Selected);
            Assert.Equal(a.IsVisible, b.IsVisible);
            Assert.Equal(a.Children?.Length ?? 0, b.Children?.Length ?? 0);
            if (a.Children is null ||
                a.Children.Length < 1) return;
            Assert.NotNull(b.Children);

            for (var i = 0; i < a.Children.Length; i++)
            {
                Compare(a.Children[i], b.Children[i]);
            }
        }

        public TreeEntry_Should(ITestOutputHelper output)
            : base(output)
        {
        }
    }
}
