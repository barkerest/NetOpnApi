using NetOpnApi.Models;
using NetOpnApi.Models.Cron.Settings;

namespace NetOpnApi.Commands.Cron.Settings
{
    public class ToggleJob : BaseCommand, ICommandWithResponseAndParameterSet<ResultOnly, ToggleJobParameterSet>
    {
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public ResultOnly Response { get; set; }

        /// <inheritdoc />
        public ToggleJobParameterSet ParameterSet { get; } = new ToggleJobParameterSet();
    }
}
