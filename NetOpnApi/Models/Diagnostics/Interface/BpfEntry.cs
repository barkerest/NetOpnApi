using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models.Diagnostics.Interface
{
    public class BpfEntry
    {
        [JsonPropertyName("pid")]
        [JsonConverter(typeof(AlwaysInt))]
        public int    ProcessID         { get; set; }
        
        [JsonPropertyName("process")]
        public string Process           { get; set; }
        
        [JsonPropertyName("interface-name")]
        public string InterfaceName     { get; set; }
        
        [JsonPropertyName("promiscuous")]
        [JsonConverter(typeof(AlwaysBool))]
        public bool   Promiscuous       { get; set; }
        
        [JsonPropertyName("header-complete")]
        [JsonConverter(typeof(AlwaysBool))]
        public bool   HeaderComplete    { get; set; }
        
        [JsonPropertyName("direction")]
        public string Direction         { get; set; }
        
        [JsonPropertyName("received-packets")]
        [JsonConverter(typeof(AlwaysLong))]
        public long   ReceivedPackets   { get; set; }
        
        [JsonPropertyName("dropped-packets")]
        [JsonConverter(typeof(AlwaysLong))]
        public long   DroppedPackets    { get; set; }
        
        [JsonPropertyName("filter-packets")]
        [JsonConverter(typeof(AlwaysLong))]
        public long   FilterPackets     { get; set; }
        
        [JsonPropertyName("store-buffer-length")]
        [JsonConverter(typeof(AlwaysLong))]
        public long   StoreBufferLength { get; set; }
        
        [JsonPropertyName("hold-buffer-length")]
        [JsonConverter(typeof(AlwaysLong))]
        public long   HoldBufferLength  { get; set; }
        
    }
}
