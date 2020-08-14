using NetOpnApi.Models;
using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Install a package.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/install/$package
    /// </remarks>
    public class InstallPackage : BaseCommand, ICommandWithResponseAndParameterSet<StatusWithUuid, PackageParameterSet>
    {
        /// <inheritdoc />
        public StatusWithUuid      Response     { get; set; }

        /// <inheritdoc />
        public PackageParameterSet ParameterSet { get; } = new PackageParameterSet();

        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        public InstallPackage()
            : base("install")
        {
            
        }
    }
}
