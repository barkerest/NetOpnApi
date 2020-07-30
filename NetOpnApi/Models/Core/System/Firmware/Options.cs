using System.Collections.Generic;
using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models.Core.System.Firmware
{
    /// <summary>
    /// The options available for firmware config.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// A list of subscriptions.
        /// </summary>
        [JsonPropertyName("has_subscription")]
        public string[] Subscriptions { get; set; }
        
        /// <summary>
        /// The available flavors.
        /// </summary>
        [JsonPropertyName("flavours")]
        public Dictionary<string,string> Flavors { get; set; }
        
        /// <summary>
        /// The available release types.
        /// </summary>
        [JsonPropertyName("families")]
        public Dictionary<string,string> ReleaseTypes { get; set; }
        
        /// <summary>
        /// The available mirrors.
        /// </summary>
        [JsonPropertyName("mirrors")]
        public Dictionary<string,string> Mirrors { get; set; }
        
        /// <summary>
        /// True if the device allows custom values.
        /// </summary>
        [JsonPropertyName("allow_custom")]
        [JsonConverter(typeof(AlwaysBool))]
        public bool AllowCustom { get; set; }
    }
}
