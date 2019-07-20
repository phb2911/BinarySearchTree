
namespace BinarySearchTree
{

    public partial class Dictionary
    {

        /// <summary>
        /// This class implements a stack of Binary Search Tree Nodes.
        /// </summary>
        private class BSTStack
        {

            private Node _top;

            /// <summary>
            /// Access top node without popping it from list.
            /// </summary>
            public IBSTNode Top
            {
                get { return this._top.Value; }
            }

            /// <summary>
            /// Checks if list is empty.
            /// </summary>
            /// <returns>True if the list is empty.</returns>
            public bool isEmpty()
            {
                return this._top == null;
            }

            /// <summary>
            /// Pushes a new element into stack.
            /// </summary>
            /// <param name="element">The element to be pushed into the stack.</param>
            public void Push(IBSTNode element)
            {

                // check if list is empty
                if (this._top == null)
                {
                    // add first node with no previous reference
                    this._top = new Node(element);
                }
                else
                {
                    // new node is on top with reference to previous
                    this._top = new Node(element, this._top);
                }

            }

            /// <summary>
            /// Pops the top element from the stack.
            /// </summary>
            /// <returns>The top element.</returns>
            public IBSTNode Pop()
            {

                // check if list is empty
                if (this._top == null) return null;

                // get last node value
                IBSTNode lastNodeValue = this._top.Value;

                // last node now is the previous
                this._top = this._top.Previous;

                // return value
                return lastNodeValue;

            }

            private class Node
            {
                public IBSTNode Value { get; }
                public Node Previous { get; set; }

                public Node(IBSTNode value)
                {
                    this.Value = value;
                }

                public Node(IBSTNode value, Node prev)
                {
                    this.Value = value;
                    this.Previous = prev;
                }

            }

        }

    }

}
