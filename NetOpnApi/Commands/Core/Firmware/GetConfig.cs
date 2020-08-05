using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get the firmware configuration.
    /// </summary>
    /// <remarks>
    /// GET: /api/core/firmware/getfirmwareconfig
    /// </remarks>
    public class GetConfig : BaseCommand, ICommandWithResponse<Config>
    {
        /// <inheritdoc />
        public Config Response { get; set; }

        public GetConfig()
            : base("getfirmwareconfig")
        {
            
        }
    }
}
