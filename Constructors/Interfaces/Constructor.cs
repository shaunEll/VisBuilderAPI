using System;
using TreeCollections;

namespace Constructors
{
    public interface Constructor
    {
        void Build();
        MutableEntityTreeNode<string, Subnet> Tree { get; }
    }
}
