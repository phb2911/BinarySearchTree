
namespace BinarySearchTree
{

    public partial class Dictionary
    {

        /// <summary>
        /// Interface that defines a node object.
        /// </summary>
        private interface IBSTNode
        {

            bool isLeaf();
            bool isRoot();
            int Key { get; }
            string Value { get; set; }
            IBSTNode Parent { get; set; }
            BSTNodeStatus Status { get; }

        }

    }

}
