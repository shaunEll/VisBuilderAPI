using Services;
using System.Collections.Generic;
using TreeCollections;

namespace Constructors
{
    public class NetworkConstructor : Constructor
    {
        //Root node for ALL network diagrams
        readonly SubnetDTO rootNode = new SubnetDTO() { IPAddress = "0.0.0.0", Network = "0.0.0.0", Cidr = 0, Description = "Internet" };

        MutableEntityTreeNode<string, SubnetDTO> tree;
        SourceGateway sg;
        SubnetController sc;

        public MutableEntityTreeNode<string, SubnetDTO> Tree
        {
            get => tree;
        }

        public NetworkConstructor(SourceGateway sg, SubnetController sc)
        {
            this.sc = sc;
            this.sg = sg;
            tree = new MutableEntityTreeNode<string, SubnetDTO>(s => s.Network, rootNode);
        }

        public void Build()
        {
            
            foreach (SubnetDTO subnet in GetSubnetsFromSource())
            {
                //First insert the new subnet
                var lastNode = tree.Root;

                foreach(var node in tree)
                {
                    if(sc.IsInSubnet(node.Item,subnet))
                    {
                        lastNode = node;                      
                    }
                }

                lastNode.AddChild(subnet);

                //Now check if existing siblings are actually children
                var siblings = new List<MutableEntityTreeNode<string, SubnetDTO>>();
   
                foreach (var childnode in tree[lastNode.Id].Children)
                {
                    if (sc.IsInSubnet(subnet, childnode.Item) && childnode.Item != subnet)
                    {
                        siblings.Add(childnode);
                    }
                }

                //Add any siblings to the new parent
                siblings.ForEach(s => s.MoveToParent(subnet.Network));
            }
        }

        List<SubnetDTO> GetSubnetsFromSource()
        {
            List<SubnetDTO> subnetsFromSource = new List<SubnetDTO>();

            sg.RestartReader();

            do
            {
                subnetsFromSource.Add(sc.Create(sg.ReadNext()));
            }
            while (!sg.EndOfSource);

            return subnetsFromSource;
        }
    }
}
