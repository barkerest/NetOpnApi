using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetOpnApiBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(
                    config =>
                        config
                            .AddFile(AppDataPath + "/application.log", LogLevel.Debug)
                )
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        private static string _appDataPath;

        public static string AppDataPath
        {
            get
            {
                if (!string.IsNullOrEmpty(_appDataPath)) return _appDataPath;

                var locations = new[]
                {
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                };

                var location = locations.FirstOrDefault(
                    p =>
                    {
                        if (string.IsNullOrEmpty(p)) return false;
                        if (!Directory.Exists(p))
                        {
                            try
                            {
                                Directory.CreateDirectory(p);
                            }
                            catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
                            {
                                return false;
                            }
                        }

                        var f = p.TrimEnd('/', '\\') + "/test.txt";
                        try
                        {
                            File.WriteAllText(f, "This is a test file, you can safely delete this file.");
                        }
                        catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
                        {
                            return false;
                        }

                        try
                        {
                            File.Delete(f);
                        }
                        catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
                        {
                            return false;
                        }

                        return true;
                    }
                );

                if (string.IsNullOrEmpty(location))
                {
                    throw new ApplicationException("Failed to locate application data.");
                }

                location = location.TrimEnd('/', '\\') + "/NetOpnApi.Builder";
                if (!Directory.Exists(location))
                {
                    Directory.CreateDirectory(location);
                }

                _appDataPath = location;
                return _appDataPath;
            }
        }
    }
}
