using System;
using System.Collections;
using System.Collections.Generic;
using NetOpnApi.Commands.Cron.Settings;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Commands.Cron.Settings
{
    public class GetJobDetails_Should : BaseCommandTheoryTest<GetJobDetails, Guid?, GetJobDetails_Should.ParamList>
    {
        public class ParamList : IEnumerable<Guid?>
        {
            private static readonly IEnumerable<Guid?> Params = new[]
            {
                (Guid?)null,
                Guid.Empty,
                Guid.NewGuid(),
            };

            public IEnumerator<Guid?> GetEnumerator() => Params.GetEnumerator();
            
            IEnumerator IEnumerable.GetEnumerator() => Params.GetEnumerator();
        }

        public GetJobDetails_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void SetParameters(Guid? args)
        {
            Command.ParameterSet.JobId = args;
        }

        protected override void CheckResponse(Guid? args)
        {
            if (args is null)
            {
                // when arg is null, the default values should be returned.
                Assert.NotNull(Command.Response);
                Assert.False(string.IsNullOrEmpty(Command.Response.Origin));
            }
            else
            {
                // ironic, but when the arg is present, it should be invalid, resulting in no response.
                Assert.Null(Command.Response);
            }
        }
    }
}
