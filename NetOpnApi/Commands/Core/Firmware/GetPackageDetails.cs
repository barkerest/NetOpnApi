using NetOpnApi.Models;
using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get details for a package.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/details/$package
    /// </remarks>
    public class GetPackageDetails : BaseCommand, ICommandWithResponseAndParameterSet<PackageDetails, PackageParameterSet>
    {
        /// <inheritdoc />
        public PackageDetails Response { get; set; }

        /// <inheritdoc />
        public PackageParameterSet ParameterSet { get; } = new PackageParameterSet();

        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        public GetPackageDetails()
            : base("details")
        {
        }
    }
}
