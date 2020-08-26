using System.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace NetOpnApiBuilder.Extensions
{
    public static class ButtonExtensions
    {
        public const string EditButtonText = "&#x270e;";

        public const string EnableButtonText = "&#x2714;";

        public const string DisableButtonText = "&#x2716;";
        
        public static IHtmlContent EditButton(this IUrlHelper self, int ID, string controller = null, string action = "Edit")
        {
            var href = HttpUtility.HtmlEncode(self.Action(action, controller, new {id = ID}));
            return new HtmlString($"<a href=\"{href}\" class=\"btn btn-primary btn-sm\" title=\"Edit\">{EditButtonText}</a>");
        }

        public static IHtmlContent ToggleButton(this IUrlHelper self, int ID, bool current, string controller = null, string action = "Toggle")
        {
            var href  = HttpUtility.HtmlEncode(self.Action(action, controller, new {id = ID}));
            var text  = current ? DisableButtonText : EnableButtonText;
            var title = current ? "Disable" : "Enable";
            var cls   = current ? "warning" : "success";
            return new HtmlString($"<a href=\"{href}\" class=\"btn btn-{cls} btn-sm\" title=\"{title}\">{text}</a>");
        }

        public static IHtmlContent RemoveButton(this IUrlHelper self, int ID, string controller = null, string action = "Remove")
        {
            var href = HttpUtility.HtmlEncode(self.Action(action, controller, new {id = ID}));
            return new HtmlString($"<a href=\"{href}\" class=\"btn btn-danger btn-sm\" title=\"Remove\">{DisableButtonText}</a>");
        }
        
    }
}
