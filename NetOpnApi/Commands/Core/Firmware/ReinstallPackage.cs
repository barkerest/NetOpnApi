using NetOpnApi.Models;
using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Reinstall a package.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/reinstall/$package
    /// </remarks>
    public class ReinstallPackage : BaseCommand, ICommandWithResponseAndParameterSet<StatusWithUuid, PackageParameterSet>
    {
        /// <inheritdoc />
        public StatusWithUuid      Response     { get; set; }

        /// <inheritdoc />
        public PackageParameterSet ParameterSet { get; } = new PackageParameterSet();

        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        public ReinstallPackage()
            : base("reinstall")
        {
            
        }
    }
}
