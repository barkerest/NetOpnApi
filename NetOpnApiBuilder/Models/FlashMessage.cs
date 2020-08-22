using System;
using Microsoft.Extensions.Logging;
using NetOpnApiBuilder.Enums;

namespace NetOpnApiBuilder.Models
{
    public class FlashMessage
    {
        public AlertType Type    { get; set; }
        public string    Message { get; set; }

        public string AlertClass => $"alert alert-{Type.ToString().ToLower()}";
    }
}
