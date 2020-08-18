using System;
using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models
{
    /// <summary>
    /// A result containing a unique ID.
    /// </summary>
    public class ResultWithUuid
    {
        /// <summary>
        /// The status from the request.
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }
        
        /// <summary>
        /// The ID returned from the request.
        /// </summary>
        [JsonPropertyName("uuid")]
        [JsonConverter(typeof(AlwaysGuid))]
        public Guid Uuid { get; set; }
    }
}
