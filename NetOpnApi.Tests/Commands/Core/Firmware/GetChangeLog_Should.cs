using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Commands.Core.Firmware;
using Xunit;
using Xunit.Abstractions;
using NotImplementedException = System.NotImplementedException;

namespace NetOpnApi.Tests.Commands.Core.Firmware
{
    public class GetChangeLog_Should : BaseCommandTheoryTest<GetChangeLog, string, GetChangeLog_Should.ParamList>
    {
        public class ParamList : IEnumerable<string>
        {
            private static readonly IEnumerable<string> Params = new[]
            {
                null,
                "update",
                "20.1.9",
                "20.7"
            };

            public IEnumerator<string> GetEnumerator() => Params.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => Params.GetEnumerator();
            
        }

        public GetChangeLog_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void SetParameters(string version)
        {
            if (version is null)
            {
                Command.ParameterSet.Update  = true;
                Command.ParameterSet.Version = "20.1";    // a valid version, so if sent instead of update will cause the response check to fail.
            }
            else
            {
                Command.ParameterSet.Update  = false;
                Command.ParameterSet.Version = version;
            }
        }

        protected override void CheckResponse(string version)
        {
            Assert.NotNull(Command.Response);
            
            if (version is null ||
                version == "update")
            {
                Assert.True(string.IsNullOrEmpty(Command.Response.Text));
                Assert.True(string.IsNullOrEmpty(Command.Response.Html));
            }
            else
            {
                Assert.False(string.IsNullOrEmpty(Command.Response.Text));
                Assert.False(string.IsNullOrEmpty(Command.Response.Html));
            }
        }
    }
}
