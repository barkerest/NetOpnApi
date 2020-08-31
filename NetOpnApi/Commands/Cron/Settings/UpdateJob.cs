using System;
using System.Collections.Generic;
using NetOpnApi.Models;
using NetOpnApi.Models.Cron.Settings;

namespace NetOpnApi.Commands.Cron.Settings
{
    /// <summary>
    /// Updates an existing job.
    /// </summary>
    /// <remarks>
    /// POST: /api/cron/settings/setjob/$jobid
    /// </remarks>
    public class UpdateJob : BaseCommand, ICommandWithResponseAndParameterSet<ResultOnly>
    {
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public ResultOnly Response { get; set; }

        /// <summary>
        /// The ID for the job being updated.
        /// </summary>
        public Guid JobId { get; set; }
        
        /// <summary>
        /// The values to update the job with.
        /// </summary>
        public AddUpdateJobRequest.Values Values { get; } = new AddUpdateJobRequest.Values();

        IReadOnlyList<string> ICommandWithParameterSet.GetUrlParameters() => new[] {JobId.ToString()};

        IReadOnlyList<KeyValuePair<string, string>> ICommandWithParameterSet.GetQueryParameters() => null;

        object ICommandWithParameterSet.GetRequestPayload()
        {
            return new AddUpdateJobRequest()
            {
                Job =
                {
                    Origin      = Values.Origin,
                    Enabled     = Values.Enabled,
                    Command     = Values.Command,
                    Parameters  = Values.Parameters,
                    Description = Values.Description,
                    Minutes     = Values.Minutes,
                    Hours       = Values.Hours,
                    Days        = Values.Days,
                    Months      = Values.Months,
                    Weekdays    = Values.Weekdays,
                }
            };
        }

        Type ICommandWithParameterSet.GetRequestPayloadDataType() => typeof(AddUpdateJobRequest);
        
        
        public UpdateJob()
            : base("setjob")
        {
        }
    }
}
