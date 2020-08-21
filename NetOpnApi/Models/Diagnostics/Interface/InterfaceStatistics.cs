using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models.Diagnostics.Interface
{
    public class InterfaceStatistics
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("flags")]
        [JsonConverter(typeof(AlwaysInt))]
        public int Flags { get; set; }

        [JsonPropertyName("mtu")]
        [JsonConverter(typeof(AlwaysInt))]
        public int Mtu { get; set; }

        [JsonPropertyName("network")]
        public string Network { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("received-packets")]
        [JsonConverter(typeof(AlwaysLong))]
        public long ReceivedPackets { get; set; }

        [JsonPropertyName("received-errors")]
        [JsonConverter(typeof(AlwaysLong))]
        public long ReceivedErrors { get; set; }

        [JsonPropertyName("dropped-packets")]
        [JsonConverter(typeof(AlwaysLong))]
        public long DroppedPackets { get; set; }

        [JsonPropertyName("received-bytes")]
        [JsonConverter(typeof(AlwaysLong))]
        public long ReceivedBytes { get; set; }

        [JsonPropertyName("sent-packets")]
        [JsonConverter(typeof(AlwaysLong))]
        public long SentPackets { get; set; }

        [JsonPropertyName("send-errors")]
        [JsonConverter(typeof(AlwaysLong))]
        public long SentErrors { get; set; }

        [JsonPropertyName("sent-bytes")]
        [JsonConverter(typeof(AlwaysLong))]
        public long SentBytes { get; set; }

        [JsonPropertyName("collisions")]
        [JsonConverter(typeof(AlwaysLong))]
        public long Collisions { get; set; }
    }
}
