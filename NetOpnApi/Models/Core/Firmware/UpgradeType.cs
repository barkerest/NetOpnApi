namespace NetOpnApi.Models.Core.Firmware
{
    /// <summary>
    /// The type of upgrade request.
    /// </summary>
    public enum UpgradeType
    {
        /// <summary>
        /// Perform upgrades.
        /// </summary>
        All,
        
        /// <summary>
        /// Update the package repository.
        /// </summary>
        PackageRepository,
        
        /// <summary>
        /// Upgrade the major version.
        /// </summary>
        Major,
        
        /// <summary>
        /// Upgrade the release version.
        /// </summary>
        Release
    }
}
