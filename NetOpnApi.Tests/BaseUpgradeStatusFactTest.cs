using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using NetOpnApi.Models;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests
{
    public abstract class BaseUpgradeStatusFactTest<TCommand> : BaseCommandFactTest<TCommand>
        where TCommand : class,ICommand,ICommandWithResponse<StatusWithUuid>,new()
    {
        protected BaseUpgradeStatusFactTest(ITestOutputHelper output)
            : base(output)
        {
        }

        protected virtual string ExpectedStatus { get; } = "ok";
        
        protected sealed override void CheckResponse()
        {
            var cmdName = typeof(TCommand).Name.Split('.').Last();
            
            Assert.NotNull(Command.Response);
            Assert.Equal(ExpectedStatus, Command.Response.Status);
            
            var cmd = new NetOpnApi.Commands.Core.Firmware.GetUpgradeProgress()
            {
                Config = Command.Config,
                Logger = Command.Logger
            };
            
            cmd.Execute();
            Assert.NotNull(cmd.Response);
            while (cmd.Response.Status != "done")
            {
                if (cmd.Response.Status == "reboot")
                {
                    this.LogInformation($"{cmdName} returned a 'reboot' status.");
                    this.LogDebug(cmd.Response.Log);
                    return;
                }

                Assert.Equal("running", cmd.Response.Status);

                this.LogDebug($"waiting 5 seconds for {cmdName}...");
                Thread.Sleep(5000);
                cmd.Execute();
                Assert.NotNull(cmd.Response);
            }

            this.LogInformation($"{cmdName} has completed.");
            this.LogDebug(cmd.Response.Log);
        }
    }
}
