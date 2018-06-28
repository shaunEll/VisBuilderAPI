using Constructors;
using SubnetControllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class SubnetControllerTests
    {
        List<SubnetSourceData> subnets = new List<SubnetSourceData>()
        {
            new SubnetSourceData() {IPAddress = "10.0.0.1", Cidr = 8, Description = "Base Subnet" },
            new SubnetSourceData() {IPAddress = "10.1.0.1", Cidr = 16, Description = "First Child" },
            new SubnetSourceData() {IPAddress = "10.2.0.1", Cidr = 16, Description = "Second Child" }
        };

        SubnetController sc;

        void Setup()
        {
            sc = new StandardSubnetController();
        }

        [Fact]
        public void TestCreateSubnet()
        {
            Setup();

            var result = sc.Create(subnets[0].IPAddress,subnets[0].Cidr,subnets[0].Description);

            Assert.IsType<Subnet>(result);
            Assert.Equal("Base Subnet", result.Description);
        }

        [Fact]
        public void TestCreate2Subnets()
        {
            Setup();

            var result1 = sc.Create(subnets[0].IPAddress, subnets[0].Cidr, subnets[0].Description);
            var result2 = sc.Create(subnets[1].IPAddress, subnets[1].Cidr, subnets[1].Description);

            Assert.Equal("Base Subnet", result1.Description);
            Assert.Equal("First Child", result2.Description);
        }

        [Fact]
        public void TestSubnetIsInsideSubnet()
        {
            Setup();

            var result1 = sc.Create(subnets[0].IPAddress, subnets[0].Cidr, subnets[0].Description);
            var result2 = sc.Create(subnets[1].IPAddress, subnets[1].Cidr, subnets[1].Description);

            Assert.True(sc.IsInSubnet(result1, result2));
        }

        [Fact]
        public void TestSubnetNotInsideSubnet()
        {
            Setup();

            var result1 = sc.Create(subnets[0].IPAddress, subnets[0].Cidr, subnets[0].Description);
            var result2 = sc.Create(subnets[1].IPAddress, subnets[1].Cidr, subnets[1].Description);
            var result3 = sc.Create(subnets[2].IPAddress, subnets[2].Cidr, subnets[2].Description);

            Assert.True(sc.IsInSubnet(result1, result3));
            Assert.False(sc.IsInSubnet(result2, result3));
        }
    }
}
