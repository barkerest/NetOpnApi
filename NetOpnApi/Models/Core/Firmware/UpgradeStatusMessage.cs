using System.Text.Json.Serialization;

namespace NetOpnApi.Models.Core.Firmware
{
    /// <summary>
    /// An upgrade status message.
    /// </summary>
    public class UpgradeStatusMessage
    {
        /// <summary>
        /// The current upgrade status.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        /// <summary>
        /// The current upgrade log.
        /// </summary>
        [JsonPropertyName("log")]
        public string Log { get; set; }
    }
}
