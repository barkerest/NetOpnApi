using NetOpnApi.Models;

namespace NetOpnApi.Commands.Core.System
{
    /// <summary>
    /// Halt the device.
    /// </summary>
    /// <remarks>
    /// GET: /api/core/system/halt
    /// 
    /// WARNING: Halting the device will cause it to no longer be available.
    /// </remarks>
    public class Halt : BaseCommand, ICommandWithResponse<StatusMessage>
    {
        /// <inheritdoc />
        public StatusMessage Response { get; set; }
    }
}
