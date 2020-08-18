using System.Linq;
using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models.Cron.Settings
{
    public class AddUpdateJobRequest
    {
        public class Values
        {
            public void FillFrom(AddUpdateJobRequest.Values values)
            {
                Origin      = values.Origin;
                Enabled     = values.Enabled;
                Command     = values.Command;
                Parameters  = values.Parameters;
                Description = values.Description;
                Minutes     = values.Minutes;
                Hours       = values.Hours;
                Days        = values.Days;
                Months      = values.Months;
                Weekdays    = values.Weekdays;
            }
        
            public void FillFrom(JobDetails values)
            {
                Origin      = values.Origin;
                Enabled     = values.Enabled;
                Command     = values.Commands.FirstOrDefault(x => x.Value.Selected).Key ?? "";
                Parameters  = values.Parameters;
                Description = values.Description;
                Minutes     = values.Minutes;
                Hours       = values.Hours;
                Days        = values.Days;
                Months      = values.Months;
                Weekdays    = values.Weekdays;
            }

            
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
            /// The command to execute.  This would be the key from the commands returned by GetJobDetails.
            /// </summary>
            [JsonPropertyName("command")]
            public string Command { get; set; }

            /// <summary>
            /// The parameters for the command.
            /// </summary>
            [JsonPropertyName("parameters")]
            public string Parameters { get; set; }
        }
        
        /// <summary>
        /// The job being added or updated.
        /// </summary>
        [JsonPropertyName("job")]
        public Values Job { get; } = new Values();
    }
}
