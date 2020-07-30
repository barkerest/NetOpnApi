﻿using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using NotImplementedException = System.NotImplementedException;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class SetFirmwareConfig_Should : BaseCommandTheoryTest<NetOpnApi.Commands.Core.Firmware.SetFirmwareConfig, string, SetFirmwareConfig_Should.ParamList>
    {
        public class ParamList : IEnumerable<string>
        {
            private static readonly IEnumerable<string> Params = new[]
            {
                "",
                "https://pkg.opnsense.org"
            };

            public IEnumerator<string> GetEnumerator() => Params.GetEnumerator();
            
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public SetFirmwareConfig_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void SetParameters(string mirror)
        {
            Command.ParameterSet.Mirror = mirror;
        }

        protected override void CheckResponse(string mirror)
        {
            Assert.NotNull(Command.Response);
            Assert.Equal("ok", Command.Response.Status, ignoreCase: true);
        }
    }
}
