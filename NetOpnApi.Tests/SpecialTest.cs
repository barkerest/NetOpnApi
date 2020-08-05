namespace NetOpnApi.Tests
{
    public enum SpecialTest
    {
        /// <summary>
        /// No special tests will be run, just standard tests.
        /// </summary>
        None,
        
        /// <summary>
        /// Only test Core/System/Halt
        /// </summary>
        /// <remarks>
        /// Device will need reset afterwards.
        /// </remarks>
        CoreSystemHalt,
        
        /// <summary>
        /// Only test Core/System/Reboot
        /// </summary>
        CoreSystemReboot,
        
        /// <summary>
        /// Only test Core/Firmware/Poweroff
        /// </summary>
        CoreFirmwarePoweroff,
        
        /// <summary>
        /// Only test Core/Firmware/Reboot
        /// </summary>
        CoreFirmwareReboot,
        
        /// <summary>
        /// Only test Core/Firmware/Audit
        /// </summary>
        CoreFirmwareAudit,
        
        /// <summary>
        /// Only test Core/Firmware/Health
        /// </summary>
        CoreFirmwareHealth,
        
        /// <summary>
        /// Only test Core/Firmware/Info
        /// </summary>
        CoreFirmwareInfo,
        
        /// <summary>
        /// Only test Core/Firmware/Status
        /// </summary>
        CoreFirmwareStatus,
        
        /// <summary>
        /// Only test Core/Firmware/Upgrade
        /// </summary>
        CoreFirmwareUpgrade,
    }
}
