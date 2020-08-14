using System.Text.Json.Serialization;

namespace NetOpnApi.Models.Core.Firmware
{
    public class PackageDetails
    {
        /// <summary>
        /// Details for the package.
        /// </summary>
        [JsonPropertyName("details")]
        public string Details { get; set; }
    }
}
