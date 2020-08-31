using System;
using Microsoft.Extensions.Logging;
using NetOpnApi.Models;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class PackageManagement_Should : ILogger
    {
        private readonly ITestOutputHelper _output;

        private const string TestPackageName = "vim-console";
        
        public PackageManagement_Should(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }
        
        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            => _output.WriteLine(formatter(state, exception));

        bool ILogger.IsEnabled(LogLevel logLevel) => true;

        IDisposable ILogger.BeginScope<TState>(TState state) => null;

        private void TestAction<TCommand>() 
            where TCommand : class, ICommandWithResponseAndParameterSet<StatusWithUuid>, new()
        {
            _output.WriteLine($"Testing {typeof(TCommand)} command.");
            var command = new TCommand()
            {
                Config = OpnSenseDevice.Instance,
                Logger = this
            };
            
            // command.PackageName = TestPackageName
            typeof(TCommand).GetProperty("PackageName")!.SetValue(command, TestPackageName);
            
            var result = command.ExecuteAndWait();

            Assert.NotNull(result);
            Assert.Equal("done", result.Status);
            Assert.False(string.IsNullOrEmpty(result.Log));
            _output.WriteLine(result.Log);
            
        }
        
        [Fact]
        public void InstallLockUnlockReinstallAndRemove()
        {
            TestAction<NetOpnApi.Commands.Core.Firmware.InstallPackage>();
            TestAction<NetOpnApi.Commands.Core.Firmware.LockPackage>();
            TestAction<NetOpnApi.Commands.Core.Firmware.UnlockPackage>();
            TestAction<NetOpnApi.Commands.Core.Firmware.ReinstallPackage>();
            TestAction<NetOpnApi.Commands.Core.Firmware.RemovePackage>();
        }
    }
}
