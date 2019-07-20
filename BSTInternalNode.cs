using System.Collections.Generic;

namespace BinarySearchTree
{

    public partial class Dictionary
    {

        /// <summary>
        /// This class implements the internal node object for the Binary Search List.
        /// </summary>
        private class BSTInternalNode : IBSTNode
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
            /// A reference to the parent node of this node.
            /// </summary>
            public IBSTNode Parent { get; set; }

            /// <summary>
            /// The left child of this node.
            /// </summary>
            public IBSTNode Left { get; set; }

            /// <summary>
            /// The right child of this node.
            /// </summary>
            public IBSTNode Right { get; set; }

            #region Constructors
            public BSTInternalNode(KeyValuePair<int, string> element)
            {
                this.Key = element.Key;
                this.Value = element.Value;
                this.Parent = null;
                this.Left = null;
                this.Right = null;
            }

            public BSTInternalNode(KeyValuePair<int, string> element, IBSTNode parent)
            {
                this.Key = element.Key;
                this.Value = element.Value;
                this.Parent = parent;
                this.Left = null;
                this.Right = null;
            }

            public BSTInternalNode(KeyValuePair<int, string> element, IBSTNode parent, IBSTNode left, IBSTNode right)
            {
                this.Key = element.Key;
                this.Value = element.Value;
                this.Parent = parent;
                this.Left = left;
                this.Right = right;
            }

            public BSTInternalNode(int key, string value)
            {
                this.Key = key;
                this.Value = value;
                this.Parent = null;
                this.Left = null;
                this.Right = null;
            }

            public BSTInternalNode(int key, string value, IBSTNode parent)
            {
                this.Key = key;
                this.Value = value;
                this.Parent = parent;
                this.Left = null;
                this.Right = null;
            }

            public BSTInternalNode(int key, string value, IBSTNode parent, IBSTNode left, IBSTNode right)
            {
                this.Key = key;
                this.Value = value;
                this.Parent = parent;
                this.Left = left;
                this.Right = right;
            }
            #endregion

            /// <summary>
            /// Check if node is a leaf.
            /// </summary>
            /// <returns>False.</returns>
            public bool isLeaf()
            {
                return false;
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

}
