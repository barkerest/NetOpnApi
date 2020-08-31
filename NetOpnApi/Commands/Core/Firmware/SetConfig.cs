using System;
using System.Collections.Generic;
using NetOpnApi.Models;
using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Set the firmware configuration.
    /// </summary>
    /// <remarks>
    /// POST: /api/core/firmware/setfirmwareconfig
    /// </remarks>
    public class SetConfig : BaseCommand, ICommandWithResponseAndParameterSet<StatusOnly>
    {
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        /// <inheritdoc />
        public StatusOnly Response { get; set; }

        /// <summary>
        /// The values being set.
        /// </summary>
        public Config Values { get; } = new Config();
        
        IReadOnlyList<string> ICommandWithParameterSet.GetUrlParameters() => null;

        IReadOnlyList<KeyValuePair<string, string>> ICommandWithParameterSet.GetQueryParameters() => null;

        object ICommandWithParameterSet.GetRequestPayload() => Values;
        
        Type ICommandWithParameterSet.GetRequestPayloadDataType() => typeof(Config);
        
        public SetConfig()
            : base("setfirmwareconfig")
        {
            
        }
    }
}
