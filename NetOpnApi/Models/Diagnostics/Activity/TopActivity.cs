using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models.Diagnostics.Activity
{
    public class TopActivity
    {
        public class Detail
        {
            [JsonPropertyName("PID")]
            [JsonConverter(typeof(AlwaysInt))]
            public int ProcessID { get; set; }
            
            [JsonPropertyName("USERNAME")]
            public string Username { get; set; }
            
            [JsonPropertyName("PRI")]
            public string ProcessPriority { get; set; }
            
            [JsonPropertyName("NICE")]
            public string NicePriority { get; set; }
            
            [JsonPropertyName("SIZE")]
            public string Size { get; set; }
            
            [JsonPropertyName("RES")]
            public string ResidentMemory { get; set; }
            
            [JsonPropertyName("STATE")]
            public string State { get; set; }
            
            [JsonPropertyName("C")]
            [JsonConverter(typeof(AlwaysInt))]
            public int ProcessorNumber { get; set; }
            
            [JsonPropertyName("TIME")]
            public string CpuTime { get; set; }
            
            [JsonPropertyName("WCPU")]
            public string WeightedCpuPercentage { get; set; }
            
            [JsonPropertyName("COMMAND")]
            public string Command { get; set; }
        }
        
        [JsonPropertyName("headers")]
        [JsonConverter(typeof(AlwaysArray<string>))]
        public string[] Headers { get; set; }
        
        [JsonPropertyName("details")]
        [JsonConverter(typeof(AlwaysArray<Detail>))]
        public Detail[] Details { get; set; }
    }
}
