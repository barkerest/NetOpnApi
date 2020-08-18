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
    public class GetJobDetails : BaseCommand, ICommandWithResponseAndParameterSet<JobDetails, GetJobDetailsParameterSet>
    {
        /// <inheritdoc />
        public JobDetails                Response     { get; set; }
        
        /// <inheritdoc />
        public GetJobDetailsParameterSet ParameterSet { get; } = new GetJobDetailsParameterSet();

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
