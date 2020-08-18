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
    public class AddJob : BaseCommand, ICommandWithResponseAndParameterSet<ResultWithUuid, AddJobParameterSet>
    {
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public ResultWithUuid     Response     { get; set; }

        /// <inheritdoc />
        public AddJobParameterSet ParameterSet { get; } = new AddJobParameterSet();
    }
}
