using System;
using System.Collections.Generic;

namespace NetOpnApi.Models.Cron.Settings
{
    public class UpdateJobParameterSet : AddUpdateJobRequest.Values, IParameterSet
    {
        /// <summary>
        /// The ID for the job being updated.
        /// </summary>
        public Guid JobId { get; set; }

        IReadOnlyList<string> IParameterSet.GetUrlParameters() => new[] {JobId.ToString()};

        IReadOnlyList<KeyValuePair<string, string>> IParameterSet.GetQueryParameters() => null;

        object IParameterSet.GetRequestPayload()
        {
            return new AddUpdateJobRequest()
            {
                Job =
                {
                    Origin      = Origin,
                    Enabled     = Enabled,
                    Command     = Command,
                    Parameters  = Parameters,
                    Description = Description,
                    Minutes     = Minutes,
                    Hours       = Hours,
                    Days        = Days,
                    Months      = Months,
                    Weekdays    = Weekdays,
                }
            };
        }

        Type IParameterSet.GetRequestPayloadDataType() => typeof(AddUpdateJobRequest);
    }
}
