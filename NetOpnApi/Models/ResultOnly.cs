using System.Text.Json.Serialization;

namespace NetOpnApi.Models
{
    public class ResultOnly
    {
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}
