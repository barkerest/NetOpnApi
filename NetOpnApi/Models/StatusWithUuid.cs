using System;
using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models
{
    /// <summary>
    /// A status containing a unique ID.
    /// </summary>
    public class StatusWithUuid
    {
        /// <summary>
        /// The status from the request.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        /// <summary>
        /// The UUID for the request.
        /// </summary>
        [JsonPropertyName("msg_uuid")]
        [JsonConverter(typeof(AlwaysGuid))]
        public Guid Uuid { get; set; }
    }
}
