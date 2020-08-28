using NetOpnApi.Models;

namespace NetOpnApi.Commands.Cron.Service
{
    /// <summary>
    /// Reload the CRON configuration and restart the daemon.
    /// </summary>
    /// <remarks>
    /// POST: /api/cron/service/reconfigure
    /// </remarks>
    public class Reconfigure : BaseCommand, ICommandWithResponse<StatusOnly>
    {
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public StatusOnly Response { get; set; }
    }
    
}
