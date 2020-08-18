using NetOpnApi.Models.Cron.Settings;

namespace NetOpnApi.Commands.Cron.Settings
{
    public class SearchJobs : BaseCommand, ICommandWithResponseAndParameterSet<JobSearchResult, JobSearchParameterSet>
    {
        /// <inheritdoc />
        public JobSearchResult       Response     { get; set; }

        /// <inheritdoc />
        public JobSearchParameterSet ParameterSet { get; } = new JobSearchParameterSet();

    }
}
