using NetOpnApi.Models;
using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Unlock a package.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/unlock/$package
    /// </remarks>
    public class UnlockPackage : BaseCommand, ICommandWithResponseAndParameterSet<StatusWithUuid, PackageParameterSet>
    {
        /// <inheritdoc />
        public StatusWithUuid      Response     { get; set; }

        /// <inheritdoc />
        public PackageParameterSet ParameterSet { get; } = new PackageParameterSet();

        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        public UnlockPackage()
            : base("unlock")
        {
            
        }
    }
}
