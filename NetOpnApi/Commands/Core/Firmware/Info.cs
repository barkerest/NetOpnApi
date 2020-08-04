﻿using NetOpnApi.Models.Core.Firmware;

namespace NetOpnApi.Commands.Core.Firmware
{
    /// <summary>
    /// Get information about the firmware.
    /// </summary>
    public class Info : BaseCommand, ICommandWithResponse<Information>
    {
        /// <inheritdoc />
        public Information Response { get; set; }
    }
}
