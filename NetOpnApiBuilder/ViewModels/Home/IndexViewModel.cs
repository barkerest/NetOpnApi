using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.ViewModels.Home
{
    public class IndexViewModel
    {
        public bool RepositoriesCloned { get; }

        public bool DeviceConfigured { get; }
        
        public string DeviceHost { get; }

        public string DeviceStatus    { get; }
        public string ObjectTypeStats { get; }
        public string CommandStats    { get; }

        public IndexViewModel(Repos repos, BuilderDb db)
        {
            RepositoriesCloned = repos.InitComplete;
            var device = db.GetTestDevice();
            DeviceConfigured = device.Test();
            DeviceHost       = device.Host;

        }
    }
}
