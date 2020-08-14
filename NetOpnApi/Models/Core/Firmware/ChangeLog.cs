using System.Text.Json.Serialization;

namespace NetOpnApi.Models.Core.Firmware
{
    public class ChangeLog
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("html")]
        public string Html { get; set; }
    }
}
