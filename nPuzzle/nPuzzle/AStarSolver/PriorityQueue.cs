using System;
using System.Collections.Generic;


namespace nPuzzle.AStarSolver
{
    public class PriorityQueue
    {
        // List of nodes
        protected List<BoardStateNode> queue = null;

        /// <summary>
        /// Queue elements count
        /// </summary>
        public int Count => queue.Count;


        /// <summary>
        /// Ctor
        /// </summary>
        public PriorityQueue()
        {
            queue = new List<BoardStateNode>();
        }


        /// <summary>
        /// Add node into queue
        /// </summary>
        /// <param name="element">Specified node to add</param>
        public void Enqueue(BoardStateNode element)
        {
            // Add node into queue
            queue.Add(element);

            // Move to place in according to its priority
            _moveUp(Count - 1);
        }

        /// <summary>
        /// Extract node from queue
        /// </summary>
        /// <returns>First node in the queue with minimal Cost value</returns>
        public BoardStateNode Dequeue()
        {
            // Empty queue
            if (Count == 0)
                throw new InvalidOperationException
                    ("Cannot exract element from empty Queue!");

            // Get root
            BoardStateNode _root = queue[0];

            // Last index in queue
            int _bound_index = Count - 1;

            // Set last element instead of root
            queue[0] = queue[_bound_index];
            queue.RemoveAt(_bound_index);

            // Move new root to place in according to its priority
            if (Count > 0) _moveDown(0);

            // Return root
            return _root;
        }

        /// <summary>
        /// Move Up element while its Priotity < Parent's priority
        /// </summary>
        /// <param name="_index">Index of an movable element</param>
        private void _moveUp(int _index)
        {
            // Move Up element while its Priotity < Parent's priority
            while (_index > 0)
            {
                // Get index of element's parent
                int _ind_parent = (_index - 1) / 2;

                // Check priority
                if (queue[_index].CompareTo(queue[_ind_parent]) >= 0)
                    break;

                // Move up
                BoardStateNode _tmpNode = queue[_index];
                queue[_index] = queue[_ind_parent];
                queue[_ind_parent] = _tmpNode;

                // Set new index
                _index = _ind_parent;
            }
        }

        /// <summary>
        /// Move Down element while its Priotity > Successor's priority
        /// </summary>
        /// <param name="_index">Index of an movable element</param>
        private void _moveDown(int _index)
        {
            // Run loop
            while (true)
            {
                // Get index of successors
                int _ls_index = 2 * _index + 1; // left successor
                int _rs_index = 2 * _index + 2; // right successor
                int _min_index = _index;

                // Find successor with priority < current (left branch)
                if (_ls_index < Count && 
                    queue[_ls_index].CompareTo(queue[_min_index]) < 0)
                    _min_index = _ls_index;

                // Find successor with priority < current (right branch)
                if (_rs_index < Count &&
                    queue[_rs_index].CompareTo(queue[_min_index]) < 0)
                    _min_index = _rs_index;

                // Complete
                if (_min_index == _index) break;

                // Move down
                BoardStateNode _tmpNode = queue[_index];
                queue[_index] = queue[_min_index];
                queue[_min_index] = _tmpNode;

                // Set new index
                _index = _min_index;
            }
        }

        /// <summary>
        /// Check queue for contains the specified element
        /// </summary>
        /// <param name="element">The specified element to search</param>
        /// <returns>Result of operation</returns>
        public bool Contains(BoardStateNode element)
            => queue.Contains(element);
    }
}