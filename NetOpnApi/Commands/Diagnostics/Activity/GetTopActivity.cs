using NetOpnApi.Models.Diagnostics.Activity;

namespace NetOpnApi.Commands.Diagnostics.Activity
{
    /// <summary>
    /// Get the current process activity on the device.
    /// </summary>
    /// <remarks>
    /// GET: /api/diagnostics/activity/getactivity
    /// </remarks>
    public class GetTopActivity : BaseCommand, ICommandWithResponse<TopActivity>
    {
        /// <inheritdoc />
        public TopActivity Response { get; set; }

        public GetTopActivity()
            : base("getactivity")
        {
            
        }
    }
}
