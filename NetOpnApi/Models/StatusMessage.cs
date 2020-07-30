using System.Text.Json.Serialization;

namespace NetOpnApi.Models
{
    /// <summary>
    /// A simple status message.
    /// </summary>
    public class StatusMessage
    {
        /// <summary>
        /// The status from the request.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
