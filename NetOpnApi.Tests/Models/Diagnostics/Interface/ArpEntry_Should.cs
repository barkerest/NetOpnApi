using System.Collections.Generic;
using NetOpnApi.Models.Diagnostics.Interface;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests.Models.Diagnostics.Interface
{
    public class ArpEntry_Should : BaseModelTest<ArpEntry, ArpEntry_Should.Params>
    {
        public class Params : ParamList
        {
            public override IEnumerable<ModelTestParam<ArpEntry>> GetList()
                => new ParamBuilder(@"{""mac"":""01:02:03:04:05:06"",""ip"":""1.2.3.4"",""intf"":""em0"",""expired"":false,""expires"":4321,""permanent"":false,""type"":""ethernet"",""manufacturer"":""bob"",""hostname"":""bob.example.com"",""intf_description"":""lan""}")
                   .AddTestsFor(x => x.MacAddress)
                   .AddTestsFor(x => x.IpAddress)
                   .AddTestsFor(x => x.Interface)
                   .AddTestsFor(x => x.InterfaceDescription)
                   .AddTestsFor(x => x.Expired)
                   .AddTestsFor(x => x.Expires)
                   .AddTestsFor(x => x.Permanent)
                   .AddTestsFor(x => x.Type)
                   .AddTestsFor(x => x.Manufacturer)
                   .AddTestsFor(x => x.Hostname)
                   .ToArray();
        }

        public ArpEntry_Should(ITestOutputHelper output)
            : base(output)
        {
        }

        protected override void Compare(ArpEntry expected, ArpEntry actual)
        {
            Assert.Equal(expected.MacAddress, actual.MacAddress);
            Assert.Equal(expected.IpAddress, actual.IpAddress);
            Assert.Equal(expected.Interface, actual.Interface);
            Assert.Equal(expected.Expired, actual.Expired);
            Assert.Equal(expected.Expires, actual.Expires);
            Assert.Equal(expected.Permanent, actual.Permanent);
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.Manufacturer, actual.Manufacturer);
            Assert.Equal(expected.Hostname, actual.Hostname);
            Assert.Equal(expected.InterfaceDescription, actual.InterfaceDescription);
        }

        protected override ArpEntry Expected => new ArpEntry()
        {
            MacAddress           = "01:02:03:04:05:06",
            IpAddress            = "1.2.3.4",
            Interface            = "em0",
            Expired              = false,
            Expires              = 4321,
            Permanent            = false,
            Type                 = "ethernet",
            Manufacturer         = "bob",
            Hostname             = "bob.example.com",
            InterfaceDescription = "lan"
        };
    }
}
