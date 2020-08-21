using NetOpnApi.Models;

namespace NetOpnApi.Commands.Diagnostics.Interface
{
    public class FlushARP : BaseCommand, ICommandWithResponse<NonJsonContent>
    {
        /// <inheritdoc />
        public NonJsonContent Response { get; set; }

        /// <inheritdoc />
        public override bool UsePost { get; } = true;
    }
}
