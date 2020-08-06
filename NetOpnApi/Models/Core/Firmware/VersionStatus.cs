using System.Collections.Generic;
using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models.Core.Firmware
{
    /// <summary>
    /// Firmware status information.
    /// </summary>
    public class VersionStatus
    {
        /// <summary>
        /// A new (or reinstall) package.
        /// </summary>
        public class NewPackage
        {
            /// <summary>
            /// The name.
            /// </summary>
            [JsonPropertyName("name")]
            public string Name { get; set; }
            
            /// <summary>
            /// The version.
            /// </summary>
            [JsonPropertyName("version")]
            public string Version { get; set; }
        }

        /// <summary>
        /// An upgrade (or downgrade) package.
        /// </summary>
        public class UpgradePackage
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            
            [JsonPropertyName("current_version")]
            public string CurrentVersion { get; set; }
            
            [JsonPropertyName("new_version")]
            public string NewVersion { get; set; }
        }

        /// <summary>
        /// A package undergoing a change.
        /// </summary>
        public class ChangePackage
        {
            /// <summary>
            /// The change taking place.
            /// </summary>
            [JsonPropertyName("reason")]
            public string Change { get; set; }
            
            /// <summary>
            /// The new version.
            /// </summary>
            [JsonPropertyName("new")]
            public string NewVersion { get; set; }
            
            /// <summary>
            /// The old version.
            /// </summary>
            [JsonPropertyName("old")]
            public string OldVersion { get; set; }
            
            /// <summary>
            /// The package name.
            /// </summary>
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("connection")]
        public string ConnectionStatus { get; set; }
        
        /// <summary>
        /// The size of the packages to download.
        /// </summary>
        [JsonPropertyName("download_size")]
        public string DownloadSize { get; set; }
        
        /// <summary>
        /// The last date/time a check was performed.
        /// </summary>
        [JsonPropertyName("last_check")]
        public string LastCheck { get; set; }
        
        /// <summary>
        /// The version of the underlying operating system.
        /// </summary>
        [JsonPropertyName("os_version")]
        public string OsVersion { get; set; }
        
        /// <summary>
        /// The product name.
        /// </summary>
        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }
        
        /// <summary>
        /// The product version.
        /// </summary>
        [JsonPropertyName("product_version")]
        public string ProductVersion { get; set; }
        
        /// <summary>
        /// The status of the repository.
        /// </summary>
        [JsonPropertyName("repository")]
        public string RepositoryStatus { get; set; }
        
        /// <summary>
        /// The number of updates to be made.
        /// </summary>
        [JsonPropertyName("updates")]
        [JsonConverter(typeof(AlwaysInt))]
        public int Updates { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("upgrade_major_message")]
        public string UpgradeMajorMessage { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("upgrade_major_version")]
        public string UpgradeMajorVersion { get; set; }
        
        /// <summary>
        /// True if the device will need to be rebooted to complete the upgrade.
        /// </summary>
        [JsonPropertyName("upgrade_needs_reboot")]
        [JsonConverter(typeof(AlwaysBool))]
        public bool UpgradeNeedsReboot { get; set; }
        
        /// <summary>
        /// The upgrade action.
        /// </summary>
        [JsonPropertyName("status_upgrade_action")]
        public string StatusUpgradeAction { get; set; }
        
        /// <summary>
        /// The status code for the request.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        /// <summary>
        /// A message describing the current status of the firmware.
        /// </summary>
        [JsonPropertyName("status_msg")]
        public string StatusMessage { get; set; }
        
        /// <summary>
        /// New packages to install.
        /// </summary>
        [JsonPropertyName("new_packages")]
        public NewPackage[] NewPackages { get; set; }
        
        /// <summary>
        /// Existing packages to reinstall.
        /// </summary>
        [JsonPropertyName("reinstall_packages")]
        public NewPackage[] ReinstallPackages { get; set; }
        
        /// <summary>
        /// Existing packages to remove.
        /// </summary>
        [JsonPropertyName("remove_packages")]
        public NewPackage[] RemovePackages { get; set; }
        
        /// <summary>
        /// Packages to upgrade.
        /// </summary>
        [JsonPropertyName("upgrade_packages")]
        public UpgradePackage[] UpgradePackages { get; set; }
        
        /// <summary>
        /// Packages to downgrade.
        /// </summary>
        [JsonPropertyName("downgrade_packages")]
        public UpgradePackage[] DowngradePackages { get; set; }
        
        /// <summary>
        /// All packages that have changes to be made.
        /// </summary>
        [JsonPropertyName("all_packages")]
        [JsonConverter(typeof(AlwaysDictionary<ChangePackage>))]
        public Dictionary<string, ChangePackage> AllPackages { get; set; }
    }
}
