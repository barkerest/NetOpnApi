using System;
using System.Collections.Generic;
using System.Text.Json;
using NetOpnApi.Models.Cron.Settings;

namespace NetOpnApi.Commands.Cron.Settings
{
    /// <summary>
    /// Get the details of a specific job or default settings for a new job.
    /// </summary>
    /// <remarks>
    /// GET: /api/cron/settings/getjob/$jobid
    /// </remarks>
    public class GetJobDetails : BaseCommand, ICommandWithResponseAndParameterSet<JobDetails>
    {
        /// <inheritdoc />
        public JobDetails                Response     { get; set; }
        
        
        /// <summary>
        /// The ID of the job to retrieve (or null for default job settings).
        /// </summary>
        public Guid? JobId { get; set; }

        IReadOnlyList<string> ICommandWithParameterSet.GetUrlParameters()
            => JobId is null ? null : new[] {JobId.ToString()};

        IReadOnlyList<KeyValuePair<string, string>> ICommandWithParameterSet.GetQueryParameters() => null;

        object ICommandWithParameterSet.GetRequestPayload() => null;

        Type ICommandWithParameterSet.GetRequestPayloadDataType() => null;
        
        /// <inheritdoc />
        public override string ResponseRootElementName { get; } = "job";

        /// <inheritdoc />
        public override JsonValueKind ResponseRootElementValueKind { get; } = JsonValueKind.Object;

        public GetJobDetails()
            : base("getjob")
        {
            
        }
    }
}
