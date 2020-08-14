using NetOpnApi.Models;
using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Lock a package.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/lock/$package
    /// </remarks>
    public class LockPackage : BaseCommand, ICommandWithResponseAndParameterSet<StatusWithUuid, PackageParameterSet>
    {
        /// <inheritdoc />
        public StatusWithUuid      Response     { get; set; }

        /// <inheritdoc />
        public PackageParameterSet ParameterSet { get; } = new PackageParameterSet();

        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        public LockPackage()
            : base("lock")
        {
            
        }
    }
}
