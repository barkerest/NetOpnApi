﻿using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class UpgradeStatus_Should : BaseCommandFactTest<NetOpnApi.Commands.Core.Firmware.UpgradeStatus>
    {
        public UpgradeStatus_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            Assert.Contains(Command.Response.Status, new []{"running", "done", "error", "reboot"});
        }
    }
}
