using NetOpnApi.Models;
using NetOpnApi.Models.Cron.Settings;

namespace NetOpnApi.Commands.Cron.Settings
{
    /// <summary>
    /// Delete a job.
    /// </summary>
    /// <remarks>
    /// POST: /api/cron/settings/deljob/$jobid
    /// WARNING: In 20.7, API does not fail gracefully if the ID is invalid.
    /// </remarks>
    public class DeleteJob : BaseCommand, ICommandWithResponseAndParameterSet<ResultOnly, DeleteJobParameterSet>
    {
        /// <inheritdoc />
        public ResultOnly         Response     { get; set; }

        /// <inheritdoc />
        public DeleteJobParameterSet ParameterSet { get; } = new DeleteJobParameterSet();

        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        public DeleteJob()
            : base("deljob")
        {
            
        }
    }
}
