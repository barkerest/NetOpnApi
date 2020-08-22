using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NetOpnApiBuilder.Models
{
    public class Repos
    {
        private class CoreRepo : GitRepo
        {
            public CoreRepo(ILogger logger)
                : base("https://github.com/opnsense/core", logger)
            {
            }
        }

        private class PluginsRepo : GitRepo
        {
            public PluginsRepo(ILogger logger)
                : base("https://github.com/opnsense/plugins", logger)
            {
            }
        }

        private Task Init { get; }

        /// <summary>
        /// Determine if the repos have been initialized.
        /// </summary>
        public bool InitComplete => Init.IsCompleted;

        /// <summary>
        /// Get the core repo.
        /// </summary>
        public GitRepo Core { get; private set; }

        /// <summary>
        /// Get the plugins repo.
        /// </summary>
        public GitRepo Plugins { get; private set; }

        public Repos(ILoggerFactory loggerFactory)
        {
            Init = Task.Run(
                () =>
                {
                    Core    = new CoreRepo(loggerFactory.CreateLogger<CoreRepo>());
                    Plugins = new PluginsRepo(loggerFactory.CreateLogger<PluginsRepo>());
                }
            );
        }
    }
}
