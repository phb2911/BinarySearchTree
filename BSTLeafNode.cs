using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{

    /// <summary>
    /// This class implements a leaf node of the Binary Search Tree.
    /// </summary>
    public class BSTLeafNode : IBSTNode
    {

        /// <summary>
        /// The key value of this node.
        /// </summary>
        public int Key { get; }

        /// <summary>
        /// The value of this node.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Reference to the parent node of this node.
        /// </summary>
        public IBSTNode Parent { get; set; }

        #region Constructor
        public BSTLeafNode(int key, string value)
        {
            this.Key = key;
            this.Value = value;
            this.Parent = null;
        }

        public BSTLeafNode(KeyValuePair<int, string> keyValuePair)
        {
            this.Key = keyValuePair.Key;
            this.Value = keyValuePair.Value;
            this.Parent = null;
        }

        public BSTLeafNode(int key, string value, IBSTNode parent)
        {
            this.Key = key;
            this.Value = value;
            this.Parent = parent;
        }

        public BSTLeafNode(KeyValuePair<int, string> keyValuePair, IBSTNode parent)
        {
            this.Key = keyValuePair.Key;
            this.Value = keyValuePair.Value;
            this.Parent = parent;
        }
        #endregion

        /// <summary>
        /// Check if node is a leaf.
        /// </summary>
        /// <returns>True.</returns>
        public bool isLeaf()
        {
            return true;
        }

        /// <summary>
        /// Checks if this node is the root node.
        /// </summary>
        /// <returns></returns>
        public bool isRoot()
        {
            return (this.Parent == null);
        }

        /// <summary>
        /// The status of the current node.
        /// </summary>
        public BSTNodeStatus Status
        {
            get
            {
                if (this.Parent == null) return BSTNodeStatus.Root;
                if (((BSTInternalNode)this.Parent).Left == this) return BSTNodeStatus.Left;
                return BSTNodeStatus.Right;
            }
        }

    }
}
