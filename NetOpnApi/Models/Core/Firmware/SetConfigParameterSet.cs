using System;
using System.Collections.Generic;

namespace NetOpnApi.Models.Core.Firmware
{
    /// <summary>
    /// Parameters for setting firmware configuration.
    /// </summary>
    public class SetConfigParameterSet : Config, IParameterSet
    {
        IReadOnlyList<string> IParameterSet.GetUrlParameters() => null;

        IReadOnlyList<KeyValuePair<string, string>> IParameterSet.GetQueryParameters() => null;

        object IParameterSet.GetRequestPayload() => new Config()
        {
            Flavor       = Flavor,
            Mirror       = Mirror,
            ReleaseType  = ReleaseType,
            Subscription = Subscription
        };
        
        Type IParameterSet.GetRequestPayloadDataType() => typeof(Config);
    }
}
