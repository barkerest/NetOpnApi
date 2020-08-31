using System;
using System.Collections.Generic;
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
    public class GetPackageLicense : BaseCommand, ICommandWithResponseAndParameterSet<PackageLicense>
    {
        /// <inheritdoc />
        public PackageLicense Response { get; set; }

        /// <summary>
        /// The name of the package being managed.
        /// </summary>
        public string PackageName { get; set; }

        IReadOnlyList<string> ICommandWithParameterSet.GetUrlParameters() => new[] {PackageName};

        IReadOnlyList<KeyValuePair<string, string>> ICommandWithParameterSet.GetQueryParameters() => null;

        object ICommandWithParameterSet.GetRequestPayload() => null;

        Type ICommandWithParameterSet.GetRequestPayloadDataType() => null;

        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        public GetPackageLicense()
            : base("license")
        {
        }
    }
}
