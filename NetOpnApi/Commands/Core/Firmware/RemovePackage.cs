using NetOpnApi.Models;
using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Remove an installed package.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/remove/$package
    /// </remarks>
    public class RemovePackage : BaseCommand, ICommandWithResponseAndParameterSet<StatusWithUuid, PackageParameterSet>
    {
        /// <inheritdoc />
        public StatusWithUuid      Response     { get; set; }

        /// <inheritdoc />
        public PackageParameterSet ParameterSet { get; } = new PackageParameterSet();

        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        public RemovePackage()
            : base("remove")
        {
            
        }
    }
}
