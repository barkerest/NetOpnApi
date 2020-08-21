using System.Collections.Generic;
using NetOpnApi.Models.Diagnostics.Interface;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Diagnostics.Interface
{
    public class InterfaceStatistics_Should : BaseModelTest<InterfaceStatistics, InterfaceStatistics_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<InterfaceStatistics>> GetList()
                => new ParamBuilder(
                       @"{
    ""name"": ""em0"",
    ""flags"": ""0x8843"",
    ""mtu"": 1500,
    ""network"": ""<Link#1>"",
    ""address"": ""08:00:23:f0:b9:14"",
    ""received-packets"": 1217262,
    ""received-errors"": 0,
    ""dropped-packets"": 0,
    ""received-bytes"": 560269684,
    ""sent-packets"": 858894,
    ""send-errors"": 0,
    ""sent-bytes"": 79916024,
    ""collisions"": 0
}"
                   )
                   .AddTestsFor(x => x.Name)
                   .AddTestsFor(x => x.Flags)
                   .AddTestsFor(x => x.Mtu)
                   .AddTestsFor(x => x.Network)
                   .AddTestsFor(x => x.Address)
                   .AddTestsFor(x => x.ReceivedPackets)
                   .AddTestsFor(x => x.ReceivedErrors)
                   .AddTestsFor(x => x.DroppedPackets)
                   .AddTestsFor(x => x.ReceivedBytes)
                   .AddTestsFor(x => x.SentPackets)
                   .AddTestsFor(x => x.SentErrors)
                   .AddTestsFor(x => x.SentBytes)
                   .AddTestsFor(x => x.Collisions)
                   .ToArray();
        }

        public InterfaceStatistics_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(InterfaceStatistics expected, InterfaceStatistics actual)
        {
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Flags, actual.Flags);
            Assert.Equal(expected.Mtu, actual.Mtu);
            Assert.Equal(expected.Network, actual.Network);
            Assert.Equal(expected.Address, actual.Address);
            Assert.Equal(expected.ReceivedPackets, actual.ReceivedPackets);
            Assert.Equal(expected.ReceivedErrors, actual.ReceivedErrors);
            Assert.Equal(expected.DroppedPackets, actual.DroppedPackets);
            Assert.Equal(expected.ReceivedBytes, actual.ReceivedBytes);
            Assert.Equal(expected.SentPackets, actual.SentPackets);
            Assert.Equal(expected.SentErrors, actual.SentErrors);
            Assert.Equal(expected.SentBytes, actual.SentBytes);
            Assert.Equal(expected.Collisions, actual.Collisions);
        }

        protected override InterfaceStatistics Expected => new InterfaceStatistics()
        {
            Name            = "em0",
            Flags           = 0x8843,
            Mtu             = 1500,
            Network         = "<Link#1>",
            Address         = "08:00:23:f0:b9:14",
            ReceivedPackets = 1217262,
            ReceivedErrors  = 0,
            DroppedPackets  = 0,
            ReceivedBytes   = 560269684,
            SentPackets     = 858894,
            SentErrors      = 0,
            SentBytes       = 79916024,
            Collisions      = 0
        };
    }
}
