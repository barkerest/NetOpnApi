using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Menu;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Core.Menu
{
    public class TreeEntry_Should : BaseModelTest<TreeEntry, TreeEntry_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<TreeEntry>> GetList()
                => new ParamBuilder(@"{""Id"": ""Alpha"", ""Order"": 1234, ""VisibleName"": ""Alpha"", ""CssClass"": ""something"", ""Url"": """", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [{""Id"": ""Bravo"", ""Order"": 0, ""VisibleName"": ""Bravo"", ""CssClass"": ""something"", ""Url"": ""/index.php"", ""IsExternal"": false, ""Visibility"": ""all"", ""Selected"": true, ""Children"": [{""Id"": ""Charlie"", ""Order"": 0, ""VisibleName"": ""Charlie"", ""CssClass"": """", ""Url"": ""/index.php*"", ""IsExternal"": false, ""Visibility"": ""hidden"", ""Selected"": false, ""Children"": [], ""isVisible"": false}], ""isVisible"": true}, {""Id"": ""Delta"", ""Order"": 1, ""VisibleName"": ""Delta"", ""CssClass"": ""something"", ""Url"": ""/ui/core/something"", ""IsExternal"": true, ""Visibility"": ""all"", ""Selected"": false, ""Children"": [], ""isVisible"": true}], ""isVisible"": true}")
                   .AddTestsFor(m => m.ID)
                   .AddTestsFor(m => m.Order)
                   .AddTestsFor(m => m.VisibleName)
                   .AddTestsFor(m => m.CssClass)
                   .AddTestsFor(m => m.Url)
                   .AddTestsFor(m => m.IsExternal)
                   .AddTestsFor(m => m.Visibility)
                   .AddTestsFor(m => m.Selected)
                   .AddTestsFor(
                       m => m.Children,
                       new[]
                       {
                           new TreeEntry() {ID = "Gamma", VisibleName = "gamma"},
                           new TreeEntry() {ID = "Theta", VisibleName = "theta"}
                       }
                   )
                   .AddTestsFor(m => m.IsVisible)
                   .ToArray();
        }

        protected override TreeEntry Expected => new TreeEntry()
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
