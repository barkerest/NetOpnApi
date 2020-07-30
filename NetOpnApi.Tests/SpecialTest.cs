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
        
        
    }
}
