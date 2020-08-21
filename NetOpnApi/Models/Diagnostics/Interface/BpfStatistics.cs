using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models.Diagnostics.Interface
{
    public class BpfStatistics
    {
        [JsonPropertyName("bpf-entry")]
        [JsonConverter(typeof(AlwaysArray<BpfEntry>))]
        public BpfEntry[] Entries { get; set; }
    }
}
