using System;
using System.Collections.Generic;
using NetOpnApi.Models;
using NetOpnApi.Models.Cron.Settings;

namespace NetOpnApi.Commands.Cron.Settings
{
    /// <summary>
    /// Add a new job.
    /// </summary>
    /// <remarks>
    /// POST: /api/cron/settings/addjob
    /// </remarks>
    public class AddJob : BaseCommand, ICommandWithResponseAndParameterSet<ResultWithUuid>
    {
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public ResultWithUuid     Response     { get; set; }

        /// <summary>
        /// The values for the new job.
        /// </summary>
        public AddUpdateJobRequest.Values Values { get; } = new AddUpdateJobRequest.Values();
        
        IReadOnlyList<string> ICommandWithParameterSet.GetUrlParameters() => null;

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
    }
}
