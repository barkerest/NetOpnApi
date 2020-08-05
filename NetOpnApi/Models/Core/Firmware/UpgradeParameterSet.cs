using System;
using System.Collections.Generic;

namespace NetOpnApi.Models.Core.Firmware
{
    public class UpgradeParameterSet : IParameterSet
    {
        /// <summary>
        /// The type of upgrade to perform.
        /// </summary>
        public UpgradeType UpgradeType { get; set; }

        IReadOnlyList<string> IParameterSet.GetUrlParameters() => null;

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

        IReadOnlyList<KeyValuePair<string, string>> IParameterSet.GetQueryParameters() => null;

        object IParameterSet.GetRequestPayload() => new Dictionary<string,string>()
        {
            {"upgrade", UpgradeTypeString}
        };

        Type IParameterSet.GetRequestPayloadDataType() => typeof(Dictionary<string,string>);
    }
}
