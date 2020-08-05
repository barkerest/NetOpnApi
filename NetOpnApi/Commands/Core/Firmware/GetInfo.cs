using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get information about the firmware.
    /// </summary>
    /// <remarks>
    /// GET: /api/core/firmware/info
    /// </remarks>
    public class GetInfo : BaseCommand, ICommandWithResponse<Info>
    {
        /// <inheritdoc />
        public Info Response { get; set; }

        public GetInfo()
            : base("info")
        {
            
        }
    }
}
