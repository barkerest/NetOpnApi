using System.Text.Json.Serialization;

namespace NetOpnApi.Models.Core.System.Firmware
{
    /// <summary>
    /// The firmware configuration.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// The flavor to install.
        /// </summary>
        [JsonPropertyName("flavour")]
        public string Flavor { get; set; } = "";

        /// <summary>
        /// The mirror to install from.
        /// </summary>
        [JsonPropertyName("mirror")]
        public string Mirror { get; set; } = "";

        /// <summary>
        /// The release type to install.
        /// </summary>
        [JsonPropertyName("type")]
        public string ReleaseType { get; set; } = "";
        
        /// <summary>
        /// The subscription.
        /// </summary>
        [JsonPropertyName("subscription")]
        public string Subscription { get; set; }
    }
}
