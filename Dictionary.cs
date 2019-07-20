using System;
using System.Collections;
using System.Collections.Generic;

namespace BinarySearchTree
{
    /// <summary>
    /// This class implements a Binary Search Tree list.
    /// </summary>
    public partial class Dictionary : IEnumerable<KeyValuePair<int, string>>
    {

        // global private variable
        private IBSTNode _root = null;
        private int _count = 0;

        // status enum
        private enum BSTNodeStatus
        {
            Root = 0,
            Left = 1,
            Right = 2
        }

        /// <summary>
        /// Provides implementation so that the items of the list can be accessed by
        /// foreach loop iterations.
        /// </summary>
        /// <returns>A KeyValuePair&lt;int, string&gt; object.</returns>
        public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
        {

            // create a new stack of nodes
            BSTStack stack = new BSTStack();

            // populate stack, start from root
            this.getElements(this._root, stack);

            // return values from top element then pop it out
            while (!stack.isEmpty())
            {
                yield return new KeyValuePair<int, string>(stack.Top.Key, stack.Top.Value);
                stack.Pop();
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Removes element associated to the key argument.
        /// </summary>
        /// <param name="key">The key of the element to be removed.</param>
        /// <returns>True if element is removed, falso if key not found.</returns>
        public bool Remove(int key)
        {

            // removes element from list based on key

            int iter = 0; // no use in this context

            // first stpe, find node to be removed
            IBSTNode node = this.SearchNode(this._root, key, ref iter);

            // key not found
            if (node == null) return false;

            // if node is leaf, remove reference from parent and that's all
            // or if it's root, set to null
            if (node.isLeaf())
            {
                if (node.Status == BSTNodeStatus.Root) this._root = null;
                else if (node.Status == BSTNodeStatus.Left) ((BSTInternalNode)node.Parent).Left = null;
                else ((BSTInternalNode)node.Parent).Right = null;
            }
            else if (node.isRoot()) // check if node is root
            {

                // add reference to children
                IBSTNode leftSubtree = ((BSTInternalNode)this._root).Left;
                IBSTNode rightSubtree = ((BSTInternalNode)this._root).Right;

                // check if left subtree is empty, if not, it becomes the new tree
                if (leftSubtree != null)
                {
                    leftSubtree.Parent = null; // erase reference to old parent
                    this._root = leftSubtree;

                    // find an empty spot to accomodate the right subtree
                    // if it is not null
                    if (rightSubtree != null) this.InsertNode(this._root, rightSubtree);

                }
                else
                {
                    // right subtree is the new tree
                    // at this point it can't be null
                    rightSubtree.Parent = null;
                    this._root = rightSubtree;
                }

            }
            else
            {

                // add reference to children
                IBSTNode leftSubtree = ((BSTInternalNode)node).Left;
                IBSTNode rightSubtree = ((BSTInternalNode)node).Right;

                // remove reference from parent
                if (node.Status == BSTNodeStatus.Left) ((BSTInternalNode)node.Parent).Left = null;
                else ((BSTInternalNode)node.Parent).Right = null;

                // find new location for subtees
                if (leftSubtree != null) this.InsertNode(this._root, leftSubtree);
                if (rightSubtree != null) this.InsertNode(this._root, rightSubtree);

            }

            // decrement count
            this._count--;

            return true;

        }

        /// <summary>
        /// Clears list.
        /// </summary>
        public void Clear()
        {
            // removes all elements from list
            this._root = null;
            this._count = 0;
        }

        /// <summary>
        /// Gets the value from the list according to the key argument.
        /// </summary>
        /// <param name="key">The key to be searched.</param>
        /// <param name="value">The value referent to the key argument.</param>
        /// <param name="iterations">The number of iterations necessary to find the key, or until program realizes key is not in list. (for debug only)</param>
        /// <returns></returns>
        public bool getValue(int key, out string value, ref int iterations)
        {

            // search list and find corresponding value
            // return true if key found
            IBSTNode result = this.SearchNode(this._root, key, ref iterations);

            if (result == null)
            {
                // key not found
                value = string.Empty;
                return false;
            }

            // key found
            value = result.Value;
            return true;

        }

        /// <summary>
        /// Number of items in the list.
        /// It differs from the regular count because this method iterates throuh
        /// all items and counts them.
        /// </summary>
        /// <returns>Number of items in list.</returns>
        public int FullCount()
        {
            // returns the number of nodes in list
            // start at root
            return this.CountNodes(this._root, 0);
        }

        /// <summary>
        /// Number of items in the list.
        /// This method differs from FullCount() because it only accesses a variable
        /// that keeps count of the elements added or removed from the list.
        /// </summary>
        /// <returns></returns>
        public int Count() { return this._count; }

        /// <summary>
        /// Adds several elements to the list at once.
        /// </summary>
        /// <param name="elements">An array of KeyValuePair&lt;int, string&gt; objects.</param>
        public void AddRange(KeyValuePair<int, string>[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                this.Add(elements[i].Key, elements[i].Value);
            }
        }

        /// <summary>
        /// Adds a new element to the list.
        /// </summary>
        /// <param name="element">A KeyValuePair&lt;int, string&gt; object.</param>
        public void Add(KeyValuePair<int, string> element)
        {
            this.Add(element.Key, element.Value);
        }

        /// <summary>
        /// Adds a new element to the list.
        /// </summary>
        /// <param name="key">The key of the element.</param>
        /// <param name="value">The value value of the element.</param>
        public void Add(int key, string value)
        {

            // add new element
            // check if list is empty
            if (this._root == null)
            {

                // create new leaf node and set root
                this._root = new BSTLeafNode(key, value);
                this._count++;

            }
            else {

                // search appropriate location and add new leaf
                this.InsertNewLeafNode(this._root, key, value);

            }

        }

        #region Private methods

        private void InsertNewLeafNode(IBSTNode currNode, int key, string value)
        {

            // current node cannot be null
            if (currNode == null) throw new ArgumentNullException("currNode");

            // if value is the same, duplicate key found, replace value.
            if (currNode.Key == key)
            {
                currNode.Value = value;
                return;
            }

            // check if node is a leaf
            if (currNode.isLeaf())
            {

                // check if current node is root
                if (currNode.isRoot())
                {

                    // convert leaf into internal with the same parameters
                    this._root = new BSTInternalNode(this._root.Key, this._root.Value);

                    // insert new leaf node 
                    this.InsertNewLeafNode(this._root, key, value);

                }
                else
                {

                    // unbox parent
                    BSTInternalNode parent = (BSTInternalNode)currNode.Parent;

                    // create new internal node using current value
                    BSTInternalNode newIntNode = new BSTInternalNode(currNode.Key, currNode.Value, parent);

                    // replace old leaf with new internal
                    // add reference to correct branch of parent
                    if (parent.Left == currNode) parent.Left = newIntNode;
                    else parent.Right = newIntNode;

                    // insert new leaf node 
                    this.InsertNewLeafNode(newIntNode, key, value);

                }

            }
            else
            {

                // current node not leaf, unbox it
                BSTInternalNode node = (BSTInternalNode)currNode;

                // redirect to correct branch
                if (key > node.Key)
                {

                    // check if left child is empty, if so add new leaf
                    if (node.Left == null)
                    {
                        node.Left = new BSTLeafNode(key, value, node);
                        this._count++;
                    }
                    else this.InsertNewLeafNode(node.Left, key, value); // redirect to left branch

                }
                else
                {

                    // check if right child is empty, if so add new leaf
                    if (node.Right == null)
                    {
                        node.Right = new BSTLeafNode(key, value, node);
                        this._count++;
                    }
                    else this.InsertNewLeafNode(node.Right, key, value); // redirect to right branch

                }

            }

        }

        private void InsertNode(IBSTNode currNode, IBSTNode node)
        {

            // neither current node nor node can be null
            if (currNode == null) throw new ArgumentNullException("currNode");
            else if (node == null) throw new ArgumentNullException("node");

            // if current node is a leaf, convert it to internal and repeat process
            if (currNode.isLeaf())
            {

                if (currNode.isRoot())
                {
                    // current node is root and leaf
                    // convert root to internal node (no parent reference)
                    this._root = new BSTInternalNode(this._root.Key, this._root.Value);
                    // add node to it
                    this.InsertNode(this._root, node);
                }
                else
                {
                    // unbox parent
                    BSTInternalNode parent = (BSTInternalNode)currNode.Parent;

                    // create new internal
                    BSTInternalNode newIntNode = new BSTInternalNode(currNode.Key, currNode.Value, parent);

                    // add child to right side of the tree
                    if (parent.Left == currNode) parent.Left = newIntNode;
                    else parent.Right = newIntNode;

                    // add node to it
                    this.InsertNode(newIntNode, node);

                }

            }
            else
            {

                // unbox current node
                BSTInternalNode currIntNode = (BSTInternalNode)currNode;

                if (node.Key > currIntNode.Key)
                {
                    // check if left child is empty
                    if (currIntNode.Left == null)
                    {
                        // location found
                        currIntNode.Left = node;
                        // add parent reference
                        node.Parent = currIntNode;
                    }
                    else
                    {
                        // curent node is left
                        this.InsertNode(currIntNode.Left, node);
                    }

                }
                else
                {

                    // check if right child is empty
                    if (currIntNode.Right == null)
                    {
                        // location found
                        currIntNode.Right = node;
                        // add parent reference
                        node.Parent = currIntNode;
                    }
                    else
                    {
                        // curent node is right
                        this.InsertNode(currIntNode.Right, node);
                    }

                }
                
            }

        }

        private int CountNodes(IBSTNode currNode, int count)
        {

            // check if current node is null
            if (currNode == null) return count;

            // increment count
            count++;

            // check if current node is leaf
            if (currNode.isLeaf()) return count;

            // current node is internal, unbox it
            BSTInternalNode node = (BSTInternalNode)currNode;


            // go to left branch
            count = this.CountNodes(node.Left, count);
            // go to right branch
            count = this.CountNodes(node.Right, count);

            return count;

        }

        private IBSTNode SearchNode(IBSTNode currNode, int key, ref int iterations)
        {

            iterations++;

            // key not found
            if (currNode == null) return null;

            // key found
            if (currNode.Key == key) return currNode;
          
            // at this point, if current node is leaf, value not found
            if (currNode.isLeaf()) return null;

            // redirect to left or right branch
            return this.SearchNode(key > currNode.Key ? ((BSTInternalNode)currNode).Left : ((BSTInternalNode)currNode).Right, key, ref iterations);

        }

        private void getElements(IBSTNode currNode, BSTStack stack)
        {

            // add nodes into stack to use at getEnumerator()

            // null nodes not allowed
            if (currNode == null) return;

            if (currNode.isLeaf())
            {
                // add element to stack
                stack.Push(currNode);
            }
            else
            {

                // unbox
                BSTInternalNode node = (BSTInternalNode)currNode;

                // get left node
                this.getElements(node.Left, stack);

                // add current element to stack
                stack.Push(node);

                // get right nodes
                this.getElements(node.Right, stack);

            }

        }

        #endregion

    }

}
