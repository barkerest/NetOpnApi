using System.Text.Json.Serialization;

namespace NetOpnApi.Models
{
    public class ResultMessage
    {
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}
