using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models.Diagnostics.Interface
{
    public class ArpEntry
    {
        [JsonPropertyName("mac")]
        public string MacAddress           { get; set; }
        
        [JsonPropertyName("ip")]
        public string IpAddress            { get; set; }
        
        [JsonPropertyName("intf")]
        public string Interface            { get; set; }
        
        [JsonPropertyName("intf_description")]
        public string InterfaceDescription { get; set; }
        
        [JsonPropertyName("expired")]
        [JsonConverter(typeof(AlwaysBool))]
        public bool   Expired              { get; set; }
        
        [JsonPropertyName("expires")]
        [JsonConverter(typeof(AlwaysInt))]
        public int    Expires              { get; set; }
        
        [JsonPropertyName("permanent")]
        [JsonConverter(typeof(AlwaysBool))]
        public bool   Permanent            { get; set; }
        
        [JsonPropertyName("type")]
        public string Type                 { get; set; }
        
        [JsonPropertyName("manufacturer")]
        public string Manufacturer         { get; set; }
        
        [JsonPropertyName("hostname")]
        public string Hostname             { get; set; }
    }
}
