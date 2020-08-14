using System;
using System.Collections.Generic;

namespace NetOpnApi.Models.Core.Firmware
{
    public class PackageParameterSet : IParameterSet
    {
        /// <summary>
        /// The name of the package being managed.
        /// </summary>
        public string PackageName { get; set; }

        IReadOnlyList<string> IParameterSet.GetUrlParameters() => new[] {PackageName};

        IReadOnlyList<KeyValuePair<string, string>> IParameterSet.GetQueryParameters() => null;

        object IParameterSet.GetRequestPayload() => null;

        Type IParameterSet.GetRequestPayloadDataType() => null;
    }
}
