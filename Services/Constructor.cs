using System;
using TreeCollections;

namespace Services
{
    public interface Constructor
    {
        void Build();
        MutableEntityTreeNode<string, SubnetDTO> Tree { get; }
    }
}
