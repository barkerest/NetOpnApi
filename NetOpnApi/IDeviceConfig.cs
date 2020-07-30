namespace NetOpnApi
{
    /// <summary>
    /// The configuration used to connect to the HTTPS API on a device.
    /// </summary>
    public interface IDeviceConfig
    {
        /// <summary>
        /// The hostname or IP address of the device.
        /// </summary>
        public string Host { get; }
        
        /// <summary>
        /// The HTTPS port to connect to (default: 443).
        /// </summary>
        public ushort Port { get; }
        
        /// <summary>
        /// If true, the certificate used for HTTPS will be validated and the host must be a FQDN (default: false).
        /// </summary>
        /// <remarks>
        /// By default OPNsense devices will use a self-signed certificate that can't be validated here.
        /// If you use certificates that can be validated you should set this to true to increase security.
        /// </remarks>
        public bool ValidateCertificate { get; }
        
        /// <summary>
        /// The relative path to the API on the host (default: "/api").
        /// </summary>
        public string ApiPath { get; }
        
        /// <summary>
        /// The API key to connect with.
        /// </summary>
        public string Key { get; }
        
        /// <summary>
        /// The API secret to connect with.
        /// </summary>
        public string Secret { get; }
    }
}
