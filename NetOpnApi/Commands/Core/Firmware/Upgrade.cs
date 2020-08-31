using System;
using System.Collections.Generic;
using NetOpnApi.Models;
using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Perform an upgrade.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/upgrade
    /// </remarks>
    public class Upgrade : BaseCommand, ICommandWithResponseAndParameterSet<StatusWithUuid>
    {
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public StatusWithUuid Response { get; set; }

        /// <summary>
        /// The type of upgrade to perform.
        /// </summary>
        public UpgradeType UpgradeType { get; set; }

        IReadOnlyList<string> ICommandWithParameterSet.GetUrlParameters() => null;

        private string UpgradeTypeString
        {
            get
            {
                switch (UpgradeType)
                {
                    case UpgradeType.All:
                        return "all";
                    case UpgradeType.PackageRepository:
                        return "pkg";
                    case UpgradeType.Major:
                        return "maj";
                    case UpgradeType.Release:
                        return "rel";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        IReadOnlyList<KeyValuePair<string, string>> ICommandWithParameterSet.GetQueryParameters() => null;

        object ICommandWithParameterSet.GetRequestPayload() => new Dictionary<string,string>()
        {
            {"upgrade", UpgradeTypeString}
        };

        Type ICommandWithParameterSet.GetRequestPayloadDataType() => typeof(Dictionary<string,string>);
    }
}
