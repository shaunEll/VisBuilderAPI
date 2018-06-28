using System.Collections.Generic;
using TreeCollections;

namespace Constructors
{
    public class NetworkConstructor : Constructor
    {
        //Root node for ALL network diagrams
        readonly Subnet rootNode = new Subnet() { IPAddress = "0.0.0.0", Network = "0.0.0.0", Cidr = 0, Description = "Internet" };

        MutableEntityTreeNode<string, Subnet> tree;

        SourceGateway sg;
        SubnetController sc;

        public MutableEntityTreeNode<string, Subnet> Tree
        {
            get => tree;
        }

        public NetworkConstructor(SourceGateway sg, SubnetController sc)
        {
            this.sc = sc;
            this.sg = sg;

            tree = new MutableEntityTreeNode<string, Subnet>(s => s.Network, rootNode);
        }

        public void Build()
        {           
            //Read all the subnets from source
            foreach (Subnet subnet in GetSubnetsFromSource())
            {
                //Insert the subnet into the tree
                InsertNewSubnet(subnet);

                //Move any siblings that belong inside the new subnet 
                MoveSiblingsToChildren(subnet);
            }
        }

        void InsertNewSubnet(Subnet subnet)
        {
            //First insert the new subnet
            var lastNode = tree.Root;

            foreach (var node in tree)
            {
                if(sc.IsInSubnet(node.Item, subnet))
                {
                    lastNode = node;
                }
            }

            lastNode.AddChild(subnet);
        }

        void MoveSiblingsToChildren(Subnet subnet)
        {
            //Now check if existing siblings are actually children
            var siblings = new List<MutableEntityTreeNode<string, Subnet>>();

            foreach (var childnode in (tree[subnet.Network].Parent).Children)
            {
                if (sc.IsInSubnet(subnet, childnode.Item) && childnode.Item != subnet)
                {
                    siblings.Add(childnode);
                }
            }

            //Add any siblings to the new parent
            siblings.ForEach(s => s.MoveToParent(subnet.Network));
        }

        List<Subnet> GetSubnetsFromSource()
        {
            List<Subnet> subnetsFromSource = new List<Subnet>();

            sg.RestartReader();

            do
            {
                var subnetSource = sg.ReadNext();
                subnetsFromSource.Add(sc.Create(subnetSource.IPAddress,subnetSource.Cidr,subnetSource.Description));
            }
            while (!sg.EndOfSource);

            return subnetsFromSource;
        }
    }
}
