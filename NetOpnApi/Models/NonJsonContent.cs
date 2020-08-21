using System.Text.Json.Serialization;

namespace NetOpnApi.Models
{
    public class NonJsonContent
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }
        
        [JsonPropertyName("contentType")]
        public string ContentType { get; set; }
    }
}
