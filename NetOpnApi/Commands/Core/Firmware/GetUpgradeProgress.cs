using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get the current upgrade progress.
    /// </summary>
    /// <remarks>
    /// GET: /api/core/firmware/upgradestatus
    /// </remarks>
    public class GetUpgradeProgress : BaseCommand, ICommandWithResponse<UpgradeProgress>
    {
        /// <inheritdoc />
        public UpgradeProgress Response { get; set; }

        public GetUpgradeProgress()
            : base("upgradestatus")
        {
            
        }
    }
}
