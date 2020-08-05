using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get the options available for firmware configuration.
    /// </summary>
    /// <remarks>
    /// GET: /api/core/firmware/getfirmwareoptions
    /// </remarks>
    public class GetOptions : BaseCommand, ICommandWithResponse<Options>
    {
        /// <inheritdoc />
        public Options Response { get; set; }

        public GetOptions()
            : base("getfirmwareoptions")
        {
            
        }
    }
}
