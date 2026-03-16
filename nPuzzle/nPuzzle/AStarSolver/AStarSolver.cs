using System;
using System.Collections.Generic;


namespace nPuzzle.AStarSolver
{
    public class AStarSolver
    {
        // Saved goal state
        private readonly BoardStateNode _goalState;
        // Saved initial state
        private readonly BoardStateNode _initialState;
        // Priority queue for solution-tree nodes
        private readonly PriorityQueue _queue;
        // Hashset for visited nodes
        private readonly HashSet<string> _visited;

        // Functor defined selected heuristic
        private readonly Func<BoardStateNode, object, int> HeuristicFunction;

        /// <summary>
        /// Visited nodes counter
        /// </summary>
        public int VisitedCounter = 0;


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="_initialState">Initial State</param>
        /// <param name="_initialState">Goal State</param>
        public AStarSolver(BoardStateNode _initialState, 
            BoardStateNode _goalState,
            Func<BoardStateNode, object, int> HeuristicFunction)
        {
            VisitedCounter = 0;

            // Set references
            this._goalState = _goalState;
            this._initialState = _initialState;
            this.HeuristicFunction = HeuristicFunction;
            
            // Add inisial state into queue
            _queue = new PriorityQueue();
            _queue.Enqueue(_initialState);

            // Hashset for visited states
            _visited = new HashSet<string>();
        }


        /// <summary>
        /// Run the NPuzzle solver
        /// </summary>
        /// <returns>Return completed (solved) node</returns>
        public BoardStateNode Run()
        {
            _visited.Add(_initialState.Keystr);

            // Run loop
            while (_queue.Count > 0)
            {
                // Get node with minimal cost
                BoardStateNode _currentState = _queue.Dequeue();
                VisitedCounter++;

                // Solution found
                if (_currentState.Keystr.Equals(_goalState.Keystr))
                    return _currentState;

                // Expand node
                GoToNode(_currentState);
            }

            return null;
        }

        /// <summary>
        /// Expand node
        /// </summary>
        /// <param name="_node">Node to expand</param>
        private void GoToNode(BoardStateNode _node)
        {
            // New state
            BoardStateNode _bsnState;
            

            // Add  [0 X] => [X 0] state --  Left moving
            if (_node.ZeroColumn > 0)
            {
                // Get new state
                _bsnState = new BoardStateNode(_node.State, 
                    _node.ZeroRow, _node.ZeroColumn - 1, _node.Depth + 1,
                    _create_keystring: false);                

                // Move tile
                _bsnState.State[_node.ZeroColumn, _node.ZeroRow] =
                    _bsnState.State[_node.ZeroColumn - 1, _node.ZeroRow];
                _bsnState.State[_node.ZeroColumn - 1, _node.ZeroRow] = 0;

                // Update node keystring
                _bsnState.UpdateKeyString(_node.Keystr, 
                    _node.State[_node.ZeroColumn - 1, _node.ZeroRow], 0);

                // Calculate new state
                if (!_visited.Contains(_bsnState.Keystr))
                {
                    // Set arguments for increment MD heuristic
                    Heuristics.MDArguments hmda = new Heuristics.MDArguments(
                            _node.Cost - _node.Depth,
                            _node.State[_node.ZeroColumn - 1, _node.ZeroRow],
                            _node.ZeroRow,
                            _node.ZeroColumn - 1,
                            _node.ZeroRow,
                            _node.ZeroColumn
                        );

                    // Cost of the new state
                    _bsnState.Cost = _bsnState.Depth + HeuristicFunction(_bsnState, 
                        _node.Cost > 0 ? hmda : null);
                   
                    // Add into queue
                    _queue.Enqueue(_bsnState);

                    // Addinto hashset
                    _visited.Add(_bsnState.Keystr);
                }
            }


            // Add  [X 0] => [0 X] state --  Right moving
            if (_node.ZeroColumn < _node.State.GetLength(0) - 1)
            {
                // Get new state
                _bsnState = new BoardStateNode(_node.State,
                    _node.ZeroRow, _node.ZeroColumn + 1, _node.Depth + 1,
                    _create_keystring: false);

                // Move tile
                _bsnState.State[_node.ZeroColumn, _node.ZeroRow] =
                    _bsnState.State[_node.ZeroColumn + 1, _node.ZeroRow];
                _bsnState.State[_node.ZeroColumn + 1, _node.ZeroRow] = 0;

                // Update node keystring
                _bsnState.UpdateKeyString(_node.Keystr,
                    _node.State[_node.ZeroColumn + 1, _node.ZeroRow], 0);

                // Calculate new state
                if (!_visited.Contains(_bsnState.Keystr))
                {
                    // Set arguments for increment MD heuristic
                    Heuristics.MDArguments hmda = new Heuristics.MDArguments(
                            _node.Cost - _node.Depth,
                            _node.State[_node.ZeroColumn + 1, _node.ZeroRow],
                            _node.ZeroRow,
                            _node.ZeroColumn + 1,
                            _node.ZeroRow,
                            _node.ZeroColumn
                        );

                    // Cost of the new state
                    _bsnState.Cost = _bsnState.Depth + HeuristicFunction(_bsnState,
                        _node.Cost > 0 ? hmda : null);
                   
                    // Add into queue
                    _queue.Enqueue(_bsnState);
                    
                    // Addinto hashset
                    _visited.Add(_bsnState.Keystr);
                }
            }


            //      [0] => [X]
            // Add  [X] => [0] state --  Up moving
            if (_node.ZeroRow > 0)
            {
                // Get new state
                _bsnState = new BoardStateNode(_node.State,
                    _node.ZeroRow - 1, _node.ZeroColumn, _node.Depth + 1,
                    _create_keystring: false);

                // Move tile
                _bsnState.State[_node.ZeroColumn, _node.ZeroRow] =
                    _bsnState.State[_node.ZeroColumn, _node.ZeroRow - 1];
                _bsnState.State[_node.ZeroColumn, _node.ZeroRow - 1] = 0;

                // Update node keystring
                _bsnState.UpdateKeyString(_node.Keystr,
                    _node.State[_node.ZeroColumn, _node.ZeroRow - 1], 0);

                // Calculate new state
                if (!_visited.Contains(_bsnState.Keystr))
                {
                    // Set arguments for increment MD heuristic
                    Heuristics.MDArguments hmda = new Heuristics.MDArguments(
                            _node.Cost - _node.Depth,
                            _node.State[_node.ZeroColumn, _node.ZeroRow - 1],
                            _node.ZeroRow - 1,
                            _node.ZeroColumn,
                            _node.ZeroRow,
                            _node.ZeroColumn
                        );

                    // Cost of the new state
                    _bsnState.Cost = _bsnState.Depth + HeuristicFunction(_bsnState,
                        _node.Cost > 0 ? hmda : null);
                   
                    // Add into queue
                    _queue.Enqueue(_bsnState);
                   
                    // Addinto hashset
                    _visited.Add(_bsnState.Keystr);
                }
            }


            //      [X] => [0]
            // Add  [0] => [X] state --  Down moving
            if (_node.ZeroRow < _node.State.GetLength(1) - 1)
            {
                // Get new state
                _bsnState = new BoardStateNode(_node.State,
                    _node.ZeroRow + 1, _node.ZeroColumn, _node.Depth + 1,
                    _create_keystring: false);

                // Move tile
                _bsnState.State[_node.ZeroColumn, _node.ZeroRow] =
                    _bsnState.State[_node.ZeroColumn, _node.ZeroRow + 1];
                _bsnState.State[_node.ZeroColumn, _node.ZeroRow + 1] = 0;

                // Update node keystring
                _bsnState.UpdateKeyString(_node.Keystr,
                    _node.State[_node.ZeroColumn, _node.ZeroRow + 1], 0);

                // Calculate new state
                if (!_visited.Contains(_bsnState.Keystr))
                {
                    // Set arguments for increment MD heuristic
                    Heuristics.MDArguments hmda = new Heuristics.MDArguments(
                            _node.Cost - _node.Depth,
                            _node.State[_node.ZeroColumn, _node.ZeroRow + 1],
                            _node.ZeroRow + 1,
                            _node.ZeroColumn,
                            _node.ZeroRow,
                            _node.ZeroColumn
                        );

                    // Cost of the new state
                    _bsnState.Cost = _bsnState.Depth + HeuristicFunction(_bsnState,
                        _node.Cost > 0 ? hmda : null);
                    
                    // Add into queue
                    _queue.Enqueue(_bsnState);
                   
                    // Addinto hashset
                    _visited.Add(_bsnState.Keystr);
                }
            }
        }
    }
}