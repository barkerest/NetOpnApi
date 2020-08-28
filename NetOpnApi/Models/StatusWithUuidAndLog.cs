using System.Text.Json.Serialization;

namespace NetOpnApi.Models
{
    /// <summary>
    /// A simple status message with an attached log and UUID.
    /// </summary>
    public class StatusWithUuidAndLog : StatusWithUuid
    {
        /// <summary>
        /// The log text.
        /// </summary>
        public string Log { get; set; }
    }
}
