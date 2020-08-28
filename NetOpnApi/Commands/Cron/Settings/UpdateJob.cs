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
    public class UpdateJob : BaseCommand, ICommandWithResponseAndParameterSet<ResultOnly, UpdateJobParameterSet>
    {
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public ResultOnly Response { get; set; }

        /// <inheritdoc />
        public UpdateJobParameterSet ParameterSet { get; } = new UpdateJobParameterSet();

        public UpdateJob()
            : base("setjob")
        {
        }
    }
}
