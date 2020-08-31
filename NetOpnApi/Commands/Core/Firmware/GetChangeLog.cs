using System;
using System.Collections.Generic;
using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get change log text.
    /// </summary>
    /// <remarks>
    ///POST: /api/core/firmware/changelog/$version
    /// </remarks>
    public class GetChangeLog : BaseCommand, ICommandWithResponseAndParameterSet<ChangeLog>
    {
        /// <inheritdoc />
        public ChangeLog                Response     { get; set; }

        /// <summary>
        /// Special flag to update the change logs.
        /// </summary>
        public bool Update { get; set; } = false;

        /// <summary>
        /// The version of a specific change log to retrieve.
        /// </summary>
        public string Version { get; set; } = "";
        
        IReadOnlyList<string> ICommandWithParameterSet.GetUrlParameters()
        {
            return new[] {Update ? "update" : Version};
        }

        IReadOnlyList<KeyValuePair<string, string>> ICommandWithParameterSet.GetQueryParameters() => null;

        object ICommandWithParameterSet.GetRequestPayload() => null;

        Type ICommandWithParameterSet.GetRequestPayloadDataType() => null;
        
        /// <inheritdoc />
        public override bool UsePost { get; } = true;

        public GetChangeLog()
            : base("changelog")
        {
            
        }
    }
}
