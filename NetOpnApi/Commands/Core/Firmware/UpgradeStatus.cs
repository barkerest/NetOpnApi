﻿using NetOpnApi.Models.Core.System.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get the current upgrade status.
    /// </summary>
    public class UpgradeStatus : BaseCommand, ICommandWithResponse<UpgradeStatusMessage>
    {
        /// <inheritdoc />
        public UpgradeStatusMessage Response { get; set; }
    }
}
