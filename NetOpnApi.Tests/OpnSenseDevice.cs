namespace NetOpnApi.Tests
{
    public class OpnSenseDevice : IDeviceConfig
    {
        public string Host { get; set; }
        public ushort Port { get; set; }
        public bool ValidateCertificate { get; set; }
        public string ApiPath { get; set; }
        public string Key { get; set; }
        public string Secret { get; set; }

        private static OpnSenseDevice _instance = null;
        
        public static OpnSenseDevice Instance
        {
            get
            {
                if (!(_instance is null)) return _instance;

                var cfg = Config.Instance.GetSection("OpnSenseDevice");
                _instance = new OpnSenseDevice()
                {
                    Host = cfg["Host"],
                    Port = ushort.TryParse(cfg["Port"], out var p) ? p : (ushort)443,
                    ValidateCertificate = cfg["ValidateCertificate"]?.ToLower() == "true",
                    ApiPath = (cfg["ApiPath"] is string s && !string.IsNullOrEmpty(s)) ? s : "/api",
                    Key = cfg["Key"],
                    Secret = cfg["Secret"]
                };

                return _instance;
            }
        }
    }
}
