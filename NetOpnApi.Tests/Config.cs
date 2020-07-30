using Microsoft.Extensions.Configuration;

namespace NetOpnApi.Tests
{
    public class Config
    {
        private static IConfiguration _instance;

        public static IConfiguration Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                
                return _instance;
            }
        }
    }
}
