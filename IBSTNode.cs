using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public interface IBSTNode
    {

        bool isLeaf();
        bool isRoot();
        int Key { get; }
        string Value { get; set; }
        IBSTNode Parent { get; set; }
        BSTNodeStatus Status { get; }

    }

    public enum BSTNodeStatus
    {
        Root = 0,
        Left = 1,
        Right = 2
    }

}
