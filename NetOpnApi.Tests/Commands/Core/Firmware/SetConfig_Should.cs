using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using NotImplementedException = System.NotImplementedException;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class SetConfig_Should : BaseCommandTheoryTest<NetOpnApi.Commands.Core.Firmware.SetConfig, string, SetConfig_Should.ParamList>
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

        public SetConfig_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void SetParameters(string mirror)
        {
            Command.Values.Mirror = mirror;
        }

        protected override void CheckResponse(string mirror)
        {
            Assert.NotNull(Command.Response);
            Assert.Equal("ok", Command.Response.Status, ignoreCase: true);

            var cmd = new NetOpnApi.Commands.Core.Firmware.GetConfig
            {
                Config = Command.Config,
                Logger = Command.Logger
            };
            cmd.Execute();
            Assert.Equal(mirror, cmd.Response.Mirror);
        }
    }
}
