using NetOpnApi.Models;
using NetOpnApi.Models.Cron.Settings;

namespace NetOpnApi.Commands.Cron.Settings
{
    public class ToggleJob : BaseCommand, ICommandWithResponseAndParameterSet<ResultMessage, ToggleJobParameterSet>
    {
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public ResultMessage Response { get; set; }

        /// <inheritdoc />
        public ToggleJobParameterSet ParameterSet { get; } = new ToggleJobParameterSet();
    }
}
