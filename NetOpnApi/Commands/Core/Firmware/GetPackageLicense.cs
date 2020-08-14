using NetOpnApi.Models;
using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get license for package.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/license/$package
    /// </remarks>
    public class GetPackageLicense : BaseCommand, ICommandWithResponseAndParameterSet<PackageLicense, PackageParameterSet>
    {
        /// <inheritdoc />
        public PackageLicense Response { get; set; }

        /// <inheritdoc />
        public PackageParameterSet ParameterSet { get; } = new PackageParameterSet();

        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        public GetPackageLicense()
            : base("license")
        {
        }
    }
}
