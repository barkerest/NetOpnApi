using System;
using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models.Cron.Settings
{
    public class JobSearchEntry
    {
        /// <summary>
        /// UUID of job.
        /// </summary>
        [JsonPropertyName("uuid")]
        [JsonConverter(typeof(AlwaysGuid))]
        public Guid JobId { get; set; } 
        
        /// <summary>
        /// Origin of job.
        /// </summary>
        [JsonPropertyName("origin")]
        public string Origin { get; set; }
        
        /// <summary>
        /// True if the job is enabled.
        /// </summary>
        [JsonPropertyName("enabled")]
        [JsonConverter(typeof(AlwaysBool))]
        public bool Enabled { get; set; }
        
        /// <summary>
        /// Cron: minutes
        /// </summary>
        [JsonPropertyName("minutes")]
        public string Minutes { get; set; }
        
        /// <summary>
        ///  Cron: hours
        /// </summary>
        [JsonPropertyName("hours")]
        public string Hours { get; set; }
        
        /// <summary>
        /// Cron: days
        /// </summary>
        [JsonPropertyName("days")]
        public string Days { get; set; }
        
        /// <summary>
        /// Cron: months
        /// </summary>
        [JsonPropertyName("months")]
        public string Months { get; set; }
        
        /// <summary>
        /// Cron: weekdays
        /// </summary>
        [JsonPropertyName("weekdays")]
        public string Weekdays { get; set; }
        
        /// <summary>
        /// The description of the job.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        /// <summary>
        /// The description of the command to execute.  This will match up with a value from "GetJobDetails".
        /// </summary>
        [JsonPropertyName("command")]
        public string Command { get; set; }
    }
}
