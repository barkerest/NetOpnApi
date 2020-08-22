using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NetOpnApi;
using NetOpnApi.Errors;

namespace NetOpnApiBuilder.Models
{
    public class TestDevice : IDeviceConfig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int    ID     { get; set; }
        
        /// <summary>
        /// The hostname or IP for the device to test against.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Host   { get; set; }
        
        ushort IDeviceConfig.Port => 443;

        bool IDeviceConfig.ValidateCertificate => false;
        string IDeviceConfig.ApiPath => "/api";

        /// <summary>
        /// The API key for the device to test against.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Key    { get; set; }
        
        /// <summary>
        /// The API secret for the device to test against.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Secret { get; set; }

        #region Test
        
        private class TestConnection : ICommand
        {
            public string        Module                       { get; } = "core";
            public string        Controller                   { get; } = "firmware";
            public string        Command                      { get; } = "running";
            public IDeviceConfig Config                       { get; set; }
            public ILogger       Logger                       { get; set; }
            public bool          UsePost                      { get; } = false;
            public string        ResponseRootElementName      { get; } = null;
            public JsonValueKind ResponseRootElementValueKind { get; } = JsonValueKind.Object;
        }
        
        /// <summary>
        /// Tests the configuration.
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public bool Test(ILogger logger = null)
        {
            var test = new TestConnection()
            {
                Config = this,
                Logger = logger ?? new NullLogger<TestConnection>()
            };
            try
            {
                test.Execute(1);
                return true;
            }
            catch (NetOpnApiException)
            {
                return false;
            }
        }

        #endregion

        #region Default
        
        /// <summary>
        /// Get a device with default settings.
        /// </summary>
        public static TestDevice Default => new TestDevice()
        {
            ID     = 1,
            Host   = "192.168.47.1",
            Key    = "your-api-key",
            Secret = "your-api-secret"
        };
        
        #endregion
    }
}
