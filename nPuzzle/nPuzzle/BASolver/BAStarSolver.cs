using System;
using System.Collections.Generic;

using nPuzzle.AStarSolver;


namespace nPuzzle.BASolver
{
    public class BAStarSolver
    {
        // Saved goal state
        private readonly BoardStateNode _goalState;
        // Saved initial state
        private readonly BoardStateNode _initialState;
        // Priority queue for solution-tree nodes (forward direction search)
        private readonly PriorityQueue _fwd_queue;
        // Priority queue for solution-tree nodes (backward direction search)
        private readonly PriorityQueue _bwd_queue;
        // Hashset for visited nodes (forward direction search)
        private readonly HashSet<string> _fwd_visited;
        // Hashset for visited nodes (backward direction search)
        private readonly HashSet<string> _bwd_visited;

        /// <summary>
        /// Visited nodes counter (forward search)
        /// </summary>
        public int VisitedCounterF { get; private set; } = 0;

        /// <summary>
        /// Visited nodes counter (backward search)
        /// </summary>
        public int VisitedCounterB { get; private set; } = 0;

        /// <summary>
        /// Visited nodes counter
        /// </summary>
        public int VisitedCounter => VisitedCounterF + VisitedCounterB;


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="_initialState">Initial State</param>
        /// <param name="_initialState">Goal State</param>
        public BAStarSolver(BoardStateNode _initialState, BoardStateNode _goalState)
        {
            VisitedCounterF = VisitedCounterB = 0;

            // Set references
            this._goalState = _goalState;
            this._initialState = _initialState;

            // Add inisial state into forward-serch queue
            _fwd_queue = new PriorityQueue();
            _fwd_queue.Enqueue(_initialState);

            // Add goal state into backward-serch queue
            _bwd_queue = new PriorityQueue();
            _bwd_queue.Enqueue(_goalState);

            // Hashset for visited states
            _fwd_visited = new HashSet<string>();
            _bwd_visited = new HashSet<string>();

            _fwd_visited.Add(_initialState.Keystr);
            _bwd_visited.Add(_goalState.Keystr);
        }


        /// <summary>
        /// Run the NPuzzle solver
        /// </summary>
        /// <returns>Return completed (solved) node</returns>
        public BoardStateNode Run()
        {
            // Running while both queues is contains at least 1 node
            while (_fwd_queue.Count > 0 && _bwd_queue.Count > 0)
            {
                // Get node with minimal cost (forward direction)
                BoardStateNode _currentState_fwd = _fwd_queue.Dequeue();

                // Get node with minimal cost (backward direction)
                BoardStateNode _currentState_bwd = _bwd_queue.Dequeue();

                // Solution found
                if (_fwd_visited.Contains(_currentState_bwd.Keystr) ||
                    _bwd_visited.Contains(_currentState_fwd.Keystr))
                {
                    return _currentState_fwd;
                }

                // Expand node (forward direction)
                VisitedCounterF++;
                GoToNode(_currentState_fwd, _goalState, _fwd_queue, _fwd_visited);

                // Expand node (backward direction)
                VisitedCounterB++;
                GoToNode(_currentState_bwd, _initialState, _bwd_queue, _bwd_visited);
            }

            return null;
        }

        /// <summary>
        /// Expand node
        /// </summary>
        /// <param name="_currentNode">Node to expand</param>
        /// <param name="_goal">Goal node (to evaluate heuristic)</param>
        /// <param name="_queue">Priority queue to place successors</param>
        /// <param name="_visited">Hashsetof closed nodes to exclude from successors</param>
        private void GoToNode(BoardStateNode _currentNode, BoardStateNode _goal,
            PriorityQueue _queue, HashSet<string> _visited)
        {
            // New state
            BoardStateNode _bsnState;
            

            // Add  [0 X] => [X 0] state --  Left moving
            if (_currentNode.ZeroColumn > 0)
            {
                // Get new state
                _bsnState = new BoardStateNode(_currentNode.State,
                    _currentNode.ZeroRow, _currentNode.ZeroColumn - 1, _currentNode.Depth + 1,
                    _create_keystring: false);

                // Move tile
                _bsnState.State[_currentNode.ZeroColumn, _currentNode.ZeroRow] =
                    _bsnState.State[_currentNode.ZeroColumn - 1, _currentNode.ZeroRow];
                _bsnState.State[_currentNode.ZeroColumn - 1, _currentNode.ZeroRow] = 0;

                // Update node keystring
                _bsnState.UpdateKeyString(_currentNode.Keystr,
                    _currentNode.State[_currentNode.ZeroColumn - 1, _currentNode.ZeroRow], 0);

                // Calculate new state
                if (!_visited.Contains(_bsnState.Keystr))
                {
                    // Cost of the new state
                    _bsnState.Cost = _bsnState.Depth + GetMDLC(_bsnState.State, _goal.State);
                   
                    // Add into queue
                    _queue.Enqueue(_bsnState);
                    
                    // Addinto hashset
                    _visited.Add(_bsnState.Keystr);
                }
            }


            // Add  [X 0] => [0 X] state --  Right moving
            if (_currentNode.ZeroColumn < _currentNode.State.GetLength(0) - 1)
            {
                // Get new state
                _bsnState = new BoardStateNode(_currentNode.State,
                    _currentNode.ZeroRow, _currentNode.ZeroColumn + 1, _currentNode.Depth + 1,
                    _create_keystring: false);

                // Move tile
                _bsnState.State[_currentNode.ZeroColumn, _currentNode.ZeroRow] =
                    _bsnState.State[_currentNode.ZeroColumn + 1, _currentNode.ZeroRow];
                _bsnState.State[_currentNode.ZeroColumn + 1, _currentNode.ZeroRow] = 0;

                // Update node keystring
                _bsnState.UpdateKeyString(_currentNode.Keystr,
                    _currentNode.State[_currentNode.ZeroColumn + 1, _currentNode.ZeroRow], 0);

                // Calculate new state
                if (!_visited.Contains(_bsnState.Keystr))
                {
                    // Cost of the new state
                    _bsnState.Cost = _bsnState.Depth + GetMDLC(_bsnState.State, _goal.State);

                    // Add into queue
                    _queue.Enqueue(_bsnState);
                  
                    // Addinto hashset
                    _visited.Add(_bsnState.Keystr);
                }
            }


            //      [0] => [X]
            // Add  [X] => [0] state --  Up moving
            if (_currentNode.ZeroRow > 0)
            {
                // Get new state
                _bsnState = new BoardStateNode(_currentNode.State,
                    _currentNode.ZeroRow - 1, _currentNode.ZeroColumn, _currentNode.Depth + 1,
                    _create_keystring: false);

                // Move tile
                _bsnState.State[_currentNode.ZeroColumn, _currentNode.ZeroRow] =
                    _bsnState.State[_currentNode.ZeroColumn, _currentNode.ZeroRow - 1];
                _bsnState.State[_currentNode.ZeroColumn, _currentNode.ZeroRow - 1] = 0;

                // Update node keystring
                _bsnState.UpdateKeyString(_currentNode.Keystr,
                    _currentNode.State[_currentNode.ZeroColumn, _currentNode.ZeroRow - 1], 0);

                // Calculate new state
                if (!_visited.Contains(_bsnState.Keystr))
                {
                    // Cost of the new state
                    _bsnState.Cost = _bsnState.Depth + GetMDLC(_bsnState.State, _goal.State);

                    // Add into queue
                    _queue.Enqueue(_bsnState);
                 
                    // Addinto hashset
                    _visited.Add(_bsnState.Keystr);
                }
            }


            //      [X] => [0]
            // Add  [0] => [X] state --  Down moving
            if (_currentNode.ZeroRow < _currentNode.State.GetLength(1) - 1)
            {
                // Get new state
                _bsnState = new BoardStateNode(_currentNode.State,
                    _currentNode.ZeroRow + 1, _currentNode.ZeroColumn, _currentNode.Depth + 1,
                    _create_keystring: false);

                // Move tile
                _bsnState.State[_currentNode.ZeroColumn, _currentNode.ZeroRow] =
                    _bsnState.State[_currentNode.ZeroColumn, _currentNode.ZeroRow + 1];
                _bsnState.State[_currentNode.ZeroColumn, _currentNode.ZeroRow + 1] = 0;

                // Update node keystring
                _bsnState.UpdateKeyString(_currentNode.Keystr,
                    _currentNode.State[_currentNode.ZeroColumn, _currentNode.ZeroRow + 1], 0);

                // Calculate new state
                if (!_visited.Contains(_bsnState.Keystr))
                {
                    // Cost of the new state
                    _bsnState.Cost = _bsnState.Depth + GetMDLC(_bsnState.State, _goal.State);

                    // Add into queue
                    _queue.Enqueue(_bsnState);
                   
                    // Addinto hashset
                    _visited.Add(_bsnState.Keystr);
                }
            }
        }

        /// <summary>
        /// Calculate Manhattan distance + Linear conflicts
        /// </summary>
        /// <param name="state">Current state board array</param>
        /// <param name="goal">Target state board array</param>
        /// <returns>Calculated MD + LC</returns>
        private int GetMDLC(byte[,] state, byte[,] goal)
        {
            // Manhattan distance
            int _md = 0;

            // Linear conflicts
            int _lc = 0;

            // Calculate manhatan distance
            for (int _c_s = 0; _c_s < state.GetLength(0); _c_s++)
            {
                for (int _r_s = 0; _r_s < state.GetLength(1); _r_s++)
                {
                    int _tv = state[_c_s, _r_s];
                    for (int _c_t = 0; _c_t < goal.GetLength(0); _c_t++)
                    {
                        bool found = false;
                        for (int _r_t = 0; _r_t < goal.GetLength(1); _r_t++)
                        {
                            if (goal[_c_t, _r_t] == _tv)
                            {
                                _md += Math.Abs(_c_s - _c_t) + Math.Abs(_r_s - _r_t);
                                found = false;
                                break;
                            }

                        }
                        if (found) break;
                    }
                }
            }

            // Row linear conflicts
            for (int _r = 0; _r < state.GetLength(1); _r++)
            {
                // List of tiles which in coflict
                List<int> _cflt = new List<int>();

                // Scan tuples in row _r for conflicts
                for (int _c1 = 0; _c1 < state.GetLength(0) - 1; _c1++)
                {
                    // already in conflict - skip
                    if (_cflt.Contains(_c1)) continue;

                    // Skip the hole
                    if (state[_c1, _r] == 0) continue;

                    // Scan next tiles
                    for (int _c2 = _c1 + 1; _c2 < state.GetLength(0); _c2++)
                    {
                        // already in conflict - skip
                        if (_cflt.Contains(_c2)) continue;

                        // get tile C1 position in goal state
                        (int _c1_g, int _r1_g) = Heuristics.Heuristics.getTilePosition(state[_c1, _r], goal);

                        // skip if another row
                        if (_r1_g != _r) continue;

                        // get tile C2 position in goal state
                        (int _c2_g, int _r2_g) = Heuristics.Heuristics.getTilePosition(state[_c2, _r], goal);
                        
                        // skip if another row
                        if (_r2_g != _r) continue;

                        // check conflict condition
                        if (_c2_g < _c1_g)
                        {
                            _lc += 2;
                            _cflt.AddRange(new int[] { _c1, _c2 });
                        }
                    }
                }
            }

            // Clolumn linear conflicts
            for (int _c = 0; _c < state.GetLength(0); _c++)
            {
                // List of tiles which in coflict
                List<int> _cflt = new List<int>();

                // Scan tuples in column _c for conflicts
                for (int _r1 = 0; _r1 < state.GetLength(1) - 1; _r1++)
                {
                    // already in conflict - skip
                    if (_cflt.Contains(_r1)) continue;

                    // Skip the hole
                    if (state[_c, _r1] == 0) continue;

                    // Scan next tiles
                    for (int _r2 = _r1 + 1; _r2 < state.GetLength(1); _r2++)
                    {
                        // already in conflict - skip
                        if (_cflt.Contains(_r2)) continue;

                        // get tile R1 position in goal state
                        (int _c1_g, int _r1_g) = Heuristics.Heuristics.getTilePosition(state[_c, _r1], goal);

                        // skip if another column
                        if (_c1_g != _c) continue;

                        // get tile R2 position in goal state
                        (int _c2_g, int _r2_g) = Heuristics.Heuristics.getTilePosition(state[_c, _r2], goal);

                        // skip if another column
                        if (_c2_g != _c) continue;

                        // check conflict condition
                        if (_r2_g < _r1_g)
                        {
                            _lc += 2;
                            _cflt.AddRange(new int[] { _r1, _r2 });
                        }
                    }
                }
            }

            // Return calculated heuristic (MD + LC)
            return _md + _lc;
        }
    }
}