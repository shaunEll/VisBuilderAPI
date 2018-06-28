using Constructors;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    internal class MockGateway : SourceGateway
    {
        List<SubnetSourceData> subnets = new List<SubnetSourceData>()
        {
            new SubnetSourceData() {IPAddress = "10.2.192.1", Cidr = 23, Description = "Third Child" },
            new SubnetSourceData() {IPAddress = "10.0.0.1", Cidr = 8, Description = "Base Subnet" },
            new SubnetSourceData() {IPAddress = "10.1.0.1", Cidr = 16, Description = "First Child" },
            new SubnetSourceData() {IPAddress = "10.2.0.1", Cidr = 16, Description = "Second Child" }
        };

        int currentIndex = 0;

        public bool EndOfSource
        {
            get => (currentIndex == subnets.Count);
        }

        public SubnetSourceData ReadNext()
        {
            if(currentIndex == subnets.Count)
            {
                throw new EndOfStreamException();
            }

            return subnets[currentIndex++];
        }

        public void RestartReader()
        {
            currentIndex = 0;
        }
    }
}
