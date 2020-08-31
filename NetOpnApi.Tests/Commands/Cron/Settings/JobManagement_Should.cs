using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using NetOpnApi.Commands.Cron.Settings;
using NetOpnApi.Models.Cron.Settings;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Cron.Settings
{
    public class JobManagement_Should : ILogger
    {
        private readonly ITestOutputHelper _output;

        public JobManagement_Should(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            _searchCmd = new SearchJobs(){Config = OpnSenseDevice.Instance, Logger = this};
            _addCmd = new AddJob(){Config = OpnSenseDevice.Instance, Logger = this};
            _updateCmd = new UpdateJob(){Config = OpnSenseDevice.Instance, Logger = this};
            _toggleCmd = new ToggleJob(){Config = OpnSenseDevice.Instance, Logger = this};
            _getCmd = new GetJobDetails(){Config = OpnSenseDevice.Instance, Logger = this};
            _deleteCmd = new DeleteJob(){Config = OpnSenseDevice.Instance, Logger = this};
        }

        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            => _output.WriteLine(formatter(state, exception));

        bool ILogger.IsEnabled(LogLevel logLevel) => true;

        IDisposable ILogger.BeginScope<TState>(TState state) => null;

        private readonly SearchJobs    _searchCmd;
        private readonly AddJob        _addCmd;
        private readonly UpdateJob     _updateCmd;
        private readonly ToggleJob     _toggleCmd;
        private readonly GetJobDetails _getCmd;
        private readonly DeleteJob     _deleteCmd;

        [Fact]
        public void ManageCronJobs()
        {
            this.LogDebug("Getting initial job list...");
            _searchCmd.Execute();
            var initialJobs = _searchCmd.Response.Rows ?? new JobSearchEntry[0];

            this.LogDebug("Getting default job settings...");
            _getCmd.JobId = null;
            _getCmd.Execute();
            var defaults = _getCmd.Response;

            this.LogDebug("Creating new job...");
            _addCmd.Values.FillFrom(defaults);
            _addCmd.Values.Command     = defaults.Commands.First(x => x.Key.Contains("changelog", StringComparison.OrdinalIgnoreCase)).Key;
            _addCmd.Values.Description = "Update changelog at midnight..";
            _addCmd.Values.Enabled     = true;
            _addCmd.Execute();
            
            Assert.Equal("saved", _addCmd.Response.Result);
            var jobId = _addCmd.Response.Uuid;

            this.LogDebug("Getting new job list...");
            _searchCmd.Execute();
            var newJobs = _searchCmd.Response.Rows ?? new JobSearchEntry[0];
            Assert.True(newJobs.Length > initialJobs.Length);
            
            this.LogDebug("Toggling job...");
            _toggleCmd.JobId   = jobId;
            _toggleCmd.Enabled = null;
            _toggleCmd.Execute();
            
            Assert.Equal("disabled", _toggleCmd.Response.Result, ignoreCase: true);
            _toggleCmd.Execute();
            Assert.Equal("enabled", _toggleCmd.Response.Result, ignoreCase: true);

            _toggleCmd.Enabled = true;
            _toggleCmd.Execute();
            Assert.Equal("enabled", _toggleCmd.Response.Result, ignoreCase: true);
            _toggleCmd.Execute();
            Assert.Equal("enabled", _toggleCmd.Response.Result, ignoreCase: true);

            _toggleCmd.Enabled = false;
            _toggleCmd.Execute();
            Assert.Equal("disabled", _toggleCmd.Response.Result, ignoreCase: true);
            _toggleCmd.Execute();
            Assert.Equal("disabled", _toggleCmd.Response.Result, ignoreCase: true);

            this.LogDebug("Getting job details...");
            _getCmd.JobId = jobId;
            _getCmd.Execute();
            var details = _getCmd.Response;

            this.LogDebug("Updating job...");
            _updateCmd.JobId = jobId;
            _updateCmd.Values.FillFrom(details);
            _updateCmd.Values.Enabled     = true;
            _updateCmd.Values.Description = "Update changelog at midnight.";
            _updateCmd.Execute();
            Assert.Equal("saved", _updateCmd.Response.Result);

            this.LogDebug("Deleting job...");
            _deleteCmd.JobId = jobId;
            _deleteCmd.Execute();
            Assert.Equal("deleted", _deleteCmd.Response.Result);

            this.LogDebug("Getting list of jobs one last time...");
            _searchCmd.Execute();
            
            newJobs = _searchCmd.Response.Rows ?? new JobSearchEntry[0];
            Assert.Equal(initialJobs.Length, newJobs.Length);
        }
    }
}
