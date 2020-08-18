using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models.Cron.Settings
{
    public class JobSearchResult
    {
        [JsonPropertyName("rows")]
        [JsonConverter(typeof(AlwaysArray<JobSearchEntry>))]
        public JobSearchEntry[] Rows { get; set; }
        
        [JsonPropertyName("rowCount")]
        [JsonConverter(typeof(AlwaysInt))]
        public int ItemsPerPage { get; set; }
        
        [JsonPropertyName("total")]
        [JsonConverter(typeof(AlwaysInt))]
        public int TotalItems { get; set; }
        
        [JsonPropertyName("current")]
        [JsonConverter(typeof(AlwaysInt))]
        public int CurrentPage { get; set; }
    }
}
