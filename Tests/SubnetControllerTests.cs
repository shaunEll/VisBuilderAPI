using Constructors;
using SubnetControllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Services;

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

            var result = sc.Create(subnets[0]);

            Assert.IsType<SubnetDTO>(result);
            Assert.Equal("Base Subnet", result.Description);
        }

        [Fact]
        public void TestCreate2Subnets()
        {
            Setup();

            var result1 = sc.Create(subnets[0]);
            var result2 = sc.Create(subnets[1]);

            Assert.Equal("Base Subnet", result1.Description);
            Assert.Equal("First Child", result2.Description);
        }

        [Fact]
        public void TestSubnetIsInsideSubnet()
        {
            Setup();

            var result1 = sc.Create(subnets[0]);
            var result2 = sc.Create(subnets[1]);

            Assert.True(sc.IsInSubnet(result1, result2));
        }

        [Fact]
        public void TestSubnetNotInsideSubnet()
        {
            Setup();

            var result1 = sc.Create(subnets[0]);
            var result2 = sc.Create(subnets[1]);
            var result3 = sc.Create(subnets[2]);

            Assert.True(sc.IsInSubnet(result1, result3));
            Assert.False(sc.IsInSubnet(result2, result3));
        }
    }
}
