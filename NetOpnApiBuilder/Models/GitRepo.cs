using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


// ReSharper disable MethodSupportsCancellation

namespace NetOpnApiBuilder.Models
{
    public class GitRepo : IDisposable
    {
        public string Name { get; }

        public string RepositoryPath { get; }

        public string LocalPath { get; }

        private readonly ILogger _logger;
        
        public GitRepo(string repositoryPath, ILogger logger)
        {
            _logger        = logger ?? throw new ArgumentNullException(nameof(logger));
            
            RepositoryPath = repositoryPath;

            var tmpPath = Path.GetTempPath().TrimEnd('/', '\\') + Path.DirectorySeparatorChar;
            if (repositoryPath.StartsWith("https://"))
            {
                Name = repositoryPath.Substring(8).Split('/', 2)[1].Replace('/', Path.DirectorySeparatorChar);
            }
            else if (Regex.IsMatch(repositoryPath, @"[^@]+@[^:]+:"))
            {
                Name = repositoryPath.Split(':', 2)[1].Replace('/', Path.DirectorySeparatorChar);
            }
            else
            {
                throw new ApplicationException("Repository path must be in HTTPS or SSH pattern.");
            }

            if (Name.EndsWith(".git"))
            {
                Name = Name.Substring(0, Name.Length - 4);
            }

            tmpPath += Name;

            if (Directory.Exists(tmpPath))
            {
                Clean(new DirectoryInfo(tmpPath));
            }

            if (File.Exists(tmpPath))
            {
                File.Delete(tmpPath);
            }

            LocalPath = tmpPath;
            
            Clone();
        }

        private void ReadStdout(StreamReader stream, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var line = stream.ReadLine();
                if (line is null) break;
                lock (_logger)
                {
                    _logger.LogDebug(line);
                }
            }

            var rem = stream.ReadToEnd();
            if (string.IsNullOrEmpty(rem)) return;

            lock (_logger)
            {
                _logger.LogDebug(rem);
            }
        }

        private void ReadStderr(StreamReader stream, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var line = stream.ReadLine();
                if (line is null) break;
                lock (_logger)
                {
                    _logger.LogInformation(line);
                }
            }

            var rem = stream.ReadToEnd();
            if (string.IsNullOrEmpty(rem)) return;

            lock (_logger)
            {
                _logger.LogInformation(rem);
            }
        }
        
        private void Clone()
        {
            if (Directory.Exists(LocalPath))
            {
                Clean(new DirectoryInfo(LocalPath));
            }

            var tokenProvider = new CancellationTokenSource();
            
            using (var p = new Process())
            {
                p.StartInfo.FileName        = "git";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.ArgumentList.Add("clone");
                p.StartInfo.ArgumentList.Add("--depth=1");
                p.StartInfo.ArgumentList.Add(RepositoryPath);
                p.StartInfo.ArgumentList.Add(LocalPath);
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError  = true;
                p.Start();

                var stdoutReader = Task.Run(() => ReadStdout(p.StandardOutput, tokenProvider.Token));
                var stderrReader = Task.Run(() => ReadStderr(p.StandardError, tokenProvider.Token));
                
                while (!p.HasExited)
                {
                    Thread.Sleep(1);
                }

                tokenProvider.Cancel();

                stdoutReader.Wait();
                stderrReader.Wait();
                
                if (p.ExitCode != 0)
                {
                    throw new ApplicationException($"GIT returned {p.ExitCode}.");
                }
                _logger.LogInformation("Complete");
            }
        }

        private static void Clean(DirectoryInfo dir)
        {
            foreach (var sub in dir.GetDirectories())
            {
                Clean(sub);
            }

            foreach (var file in dir.GetFiles())
            {
                file.IsReadOnly = false;
                file.Delete();
            }

            dir.Delete();
        }

        public void Dispose()
        {
            if (!string.IsNullOrEmpty(LocalPath))
            {
                if (Directory.Exists(LocalPath))
                {
                    try
                    {
                        Clean(new DirectoryInfo(LocalPath));
                    }
                    catch (IOException)
                    {
                    }
                }
            }
        }
    }
}
