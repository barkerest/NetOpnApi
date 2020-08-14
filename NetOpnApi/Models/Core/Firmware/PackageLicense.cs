using System.Text.Json.Serialization;

namespace NetOpnApi.Models.Core.Firmware
{
    public class PackageLicense
    {
        /// <summary>
        /// License for the package.
        /// </summary>
        [JsonPropertyName("license")]
        public string License { get; set; }
    }
}
