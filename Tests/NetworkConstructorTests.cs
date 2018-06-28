using Constructors;
using System;
using Xunit;
using SubnetControllers;
using Services;

namespace Tests
{
    public class NetworkConstructorTests
    {
        //Class that were testing
        Constructor nc;

        //Dependencies
        SourceGateway sourceGateway;
        SubnetController subnetController;

        void Setup()
        {
            sourceGateway = new MockGateway();
            subnetController = new StandardSubnetController();

            nc = new NetworkConstructor(sourceGateway, subnetController);
        }

        [Fact]
        public void TestTreeHasRoot()
        {
            Setup();

            var root = nc.Tree.Root.Item;

            Assert.NotNull(root);
            Assert.Equal("Internet", root.Description);
        }

        [Fact]
        public void TestBuildTree()
        {
            Setup();

            nc.Build();

            Assert.True(nc.Tree.Root.Children.Count > 0);
        }

        [Fact]
        public void TestBuildOrderedTree()
        {
            Setup();

            nc.Build();

            Assert.Single(nc.Tree.Root.Children);
            Assert.True(nc.Tree["10.0.0.0"].Children.Count == 2);
            Assert.Single(nc.Tree["10.2.0.0"].Children);
        }
    }
}
