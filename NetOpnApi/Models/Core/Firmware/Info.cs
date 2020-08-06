using System;
using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models.Core.Firmware
{
    /// <summary>
    /// Firmware information from the device.
    /// </summary>
    public class Info
    {
        /// <summary>
        /// Information about a package or plugin.
        /// </summary>
        public class PackageOrPlugin
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
            
            /// <summary>
            /// A short description.
            /// </summary>
            [JsonPropertyName("comment")]
            public string Comment { get; set; }
            
            /// <summary>
            /// The size of the package/plugin.
            /// </summary>
            [JsonPropertyName("flatsize")]
            public string FlatSize { get; set; }
            
            /// <summary>
            /// True if the package/plugin is locked.
            /// </summary>
            [JsonPropertyName("locked")]
            [JsonConverter(typeof(AlwaysBool))]
            public bool Locked { get; set; }
            
            /// <summary>
            /// The license.
            /// </summary>
            [JsonPropertyName("license")]
            public string License { get; set; }
            
            /// <summary>
            /// The source repository.
            /// </summary>
            [JsonPropertyName("repository")]
            public string Repository { get; set; }
            
            /// <summary>
            /// The origin.
            /// </summary>
            [JsonPropertyName("origin")]
            public string Origin { get; set; }
            
            /// <summary>
            /// 
            /// </summary>
            [JsonPropertyName("provided")]
            [JsonConverter(typeof(AlwaysBool))]
            public bool Provided { get; set; }
            
            /// <summary>
            /// True if the package/plugin is installed.
            /// </summary>
            [JsonPropertyName("installed")]
            [JsonConverter(typeof(AlwaysBool))]
            public bool Installed { get; set; }
            
            /// <summary>
            /// The path.
            /// </summary>
            [JsonPropertyName("path")]
            public string Path { get; set; }
            
            /// <summary>
            /// True if the package/plugin has been configured.
            /// </summary>
            [JsonPropertyName("configured")]
            [JsonConverter(typeof(AlwaysBool))]
            public bool Configured { get; set; }
        }

        /// <summary>
        /// Information about version history.
        /// </summary>
        public class ChangeLogEntry
        {
            /// <summary>
            /// The product series.
            /// </summary>
            [JsonPropertyName("series")]
            public string Series { get; set; }
            
            /// <summary>
            /// The product version.
            /// </summary>
            [JsonPropertyName("version")]
            public string Version { get; set; }
            
            /// <summary>
            /// The date for the version.
            /// </summary>
            [JsonPropertyName("date")]
            [JsonConverter(typeof(AlwaysDateTime))]
            public DateTime Date { get; set; }
        }
        
        /// <summary>
        /// The name of the product.
        /// </summary>
        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }
        
        /// <summary>
        /// The version of the product.
        /// </summary>
        [JsonPropertyName("product_version")]
        public string ProductVersion { get; set; }
        
        /// <summary>
        /// The packages available.
        /// </summary>
        [JsonPropertyName("package")]
        public PackageOrPlugin[] Packages { get; set; }
        
        /// <summary>
        /// The plugins available.
        /// </summary>
        [JsonPropertyName("plugin")]
        public PackageOrPlugin[] Plugins { get; set; }
        
        /// <summary>
        /// The change log.
        /// </summary>
        [JsonPropertyName("changelog")]
        public ChangeLogEntry[] ChangeLog { get; set; }
    }
}
