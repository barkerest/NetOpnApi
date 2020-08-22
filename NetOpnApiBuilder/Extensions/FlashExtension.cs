using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.Extensions
{
    public static class FlashExtension
    {
        public static IEnumerable<FlashMessage> GetFlashMessages(this IHtmlHelper self)
        {
            var json = self.TempData.ContainsKey("flash") ? self.TempData["flash"]?.ToString() ?? "[]" : "[]";
            return JsonSerializer.Deserialize<FlashMessage[]>(json);
        }

        public static void AddFlashMessage(this Controller self, string message, AlertType type = AlertType.Info)
        {
            var json = self.TempData.ContainsKey("flash") ? self.TempData["flash"]?.ToString() ?? "[]" : "[]";
            var list = new List<FlashMessage>(JsonSerializer.Deserialize<FlashMessage[]>(json));
            list.Add(new FlashMessage(){Type = type, Message = message});
            self.TempData["flash"] = JsonSerializer.Serialize(list.ToArray());
        }
    }
}
