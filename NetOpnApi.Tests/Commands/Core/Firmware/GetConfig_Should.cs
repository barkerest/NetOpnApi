﻿using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class GetConfig_Should : BaseCommandFactTest<NetOpnApi.Commands.Core.Firmware.GetConfig>
    {
        public GetConfig_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void CheckResponse()
        {
            Assert.NotNull(Command.Response);
            // the default is that the config is blank, so we really don't have anything else to test here.
        }
    }
}
