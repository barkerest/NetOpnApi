using System.Collections.Generic;
using NetOpnApi.Models.Diagnostics.Interface;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Diagnostics.Interface
{
    public class BpfEntry_Should : BaseModelTest<BpfEntry, BpfEntry_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<BpfEntry>> GetList()
                => new ParamBuilder(
                       @"{
    ""pid"": 33003,
    ""interface-name"": ""pflog0"",
    ""promiscuous"": true,
    ""header-complete"": true,
    ""direction"": ""bidirectional"",
    ""received-packets"": 123,
    ""dropped-packets"": 0,
    ""filter-packets"": 123,
    ""store-buffer-length"": 21524,
    ""hold-buffer-length"": 0,
    ""process"": ""filterlog""
}"
                   )
                   .AddTestsFor(x => x.ProcessID)
                   .AddTestsFor(x => x.Process)
                   .AddTestsFor(x => x.InterfaceName)
                   .AddTestsFor(x => x.Promiscuous)
                   .AddTestsFor(x => x.HeaderComplete)
                   .AddTestsFor(x => x.Direction)
                   .AddTestsFor(x => x.ReceivedPackets)
                   .AddTestsFor(x => x.DroppedPackets)
                   .AddTestsFor(x => x.FilterPackets)
                   .AddTestsFor(x => x.StoreBufferLength)
                   .AddTestsFor(x => x.HoldBufferLength)
                   .ToArray();
        }


        public BpfEntry_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void     Compare(BpfEntry expected, BpfEntry actual)
        {
            Assert.Equal(expected.ProcessID, actual.ProcessID);
            Assert.Equal(expected.InterfaceName, actual.InterfaceName);
            Assert.Equal(expected.Promiscuous, actual.Promiscuous);
            Assert.Equal(expected.HeaderComplete, actual.HeaderComplete);
            Assert.Equal(expected.Direction, actual.Direction);
            Assert.Equal(expected.ReceivedPackets, actual.ReceivedPackets);
            Assert.Equal(expected.DroppedPackets, actual.DroppedPackets);
            Assert.Equal(expected.FilterPackets, actual.FilterPackets);
            Assert.Equal(expected.StoreBufferLength, actual.StoreBufferLength);
            Assert.Equal(expected.HoldBufferLength, actual.HoldBufferLength);
            Assert.Equal(expected.Process, actual.Process);
        }

        protected override BpfEntry Expected => new BpfEntry()
        {
            ProcessID = 33003,
            InterfaceName = "pflog0",
            Promiscuous = true,
            HeaderComplete = true,
            Direction = "bidirectional",
            ReceivedPackets = 123,
            DroppedPackets = 0,
            FilterPackets = 123,
            StoreBufferLength = 21524,
            HoldBufferLength = 0,
            Process = "filterlog"
        };
    }
}
