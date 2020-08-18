using System.Collections.Generic;
using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models.Cron.Settings
{
    public class JobDetails
    {
        public class Command
        {
            [JsonPropertyName("value")]
            public string Value { get; set; }

            [JsonPropertyName("selected")]
            [JsonConverter(typeof(AlwaysBool))]
            public bool Selected { get; set; }
        }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("enabled")]
        [JsonConverter(typeof(AlwaysBool))]
        public bool Enabled { get; set; }

        [JsonPropertyName("minutes")]
        public string Minutes { get; set; }

        [JsonPropertyName("hours")]
        public string Hours { get; set; }

        [JsonPropertyName("days")]
        public string Days { get; set; }

        [JsonPropertyName("months")]
        public string Months { get; set; }

        [JsonPropertyName("weekdays")]
        public string Weekdays { get; set; }

        [JsonPropertyName("who")]
        public string Username { get; set; }

        [JsonPropertyName("command")]
        [JsonConverter(typeof(AlwaysDictionary<Command>))]
        public Dictionary<string, Command> Commands { get; set; }

        [JsonPropertyName("parameters")]
        public string Parameters { get; set; }
        
        [JsonPropertyName("description")]
        public string Description { get; set; }

        
        public override string ToString() => Description;
    }
}
