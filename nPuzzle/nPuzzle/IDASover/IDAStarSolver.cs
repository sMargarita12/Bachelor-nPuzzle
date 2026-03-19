using System;
using System.Collections.Generic;


namespace nPuzzle.IDASolver
{
    public class IDAStarSolver
    {
        delegate int heuristic();
        heuristic SelectedHeuristic;
        
        // nPuzzle game instance
        NPuzzle game;

        // Game board array
        private byte[,] board;

        // Hole tile position at the game board
        (int c, int r) hole;

        // Iterative-changeable depth limit
        int depth_limit;

        // Minimum way depth for ways
        int next_dept_limit = int.MaxValue;

        /// <summary>
        /// Visited nodes counter
        /// </summary>
        public int VisitedCounter { get; private set; } = 0;

        /// <summary>
        /// Depth for solution node
        /// </summary>
        public int SolutionNodeLevel { get; private set; }

        // Mask for make tile movings
        (int cOffset, int rOffset)[] moves_mask = new[]
        {
            (0, -1),  // left moving
            (0, 1),   // right moving
            (-1, 0),  // up moving
            (1, 0)    // down moving
        };

        // Opposite movings indexes
        int[] move_ind_opposite = new int[]
        {
            1, // (0, 1) -> moved_mask[1] is opposite for (0, -1) -> moved_mask[0]
            0, // (0, -1) -> moved_mask[0] is opposite for (0, 1) -> moved_mask[1]
            3, // (-1, 0) -> moved_mask[3] is opposite for (1, 0) -> moved_mask[2]
            2  // (1, 0) -> moved_mask[2] is opposite for (-1, 0) -> moved_mask[3]
        };


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="game">nPuzzle game instance</param>
        public IDAStarSolver(NPuzzle game)
        {
            // Set the current game & get board array
            this.game = game;
            board = game.Board.Clone() as byte[,];


            // Here U can change heuristic for IDA*:
            
            //   comment row below & uncomment next row to use LC heuristic
            SelectedHeuristic = heuristic_MD;
           
            //   comment row below & uncomment previous row to use MD heuristic
            // SelectedHeuristic = heuristic_LC;


            // Reset metrics
            VisitedCounter = 0;
            SolutionNodeLevel = 0;

            // Find the hole tile
            for (int _c = 0; _c < game.Width; _c++)
            {
                bool found = false;
                for (int _r = 0; _r < game.Width; _r++)
                {
                    if (board[_c, _r] == 0)
                    {
                        hole.c = _c;
                        hole.r = _r;
                        found = true;
                        break;
                    }
                }
                if (found) break;
            }
        }


        /// <summary>
        /// Run the solver
        /// </summary>
        /// <returns></returns>
        public byte[,] Run()
        {
            // Start depth
            depth_limit = SelectedHeuristic();

            // Flag: loop result
            bool lresult;

            // Run loop until:
            //   no solution will be found 
            //   or depth will more than allowed
            do
            {
                // Reset operating values
                VisitedCounter = 0;

                // Run for search solution
                lresult = deepSearch(0, -1, hole, depth_limit);

                // Increase depth limit 
                depth_limit = next_dept_limit;

            } while (!lresult && depth_limit <= 80);

            // Return board of solution
            return board;
        }

        /// <summary>
        /// Solution search proces
        /// </summary>
        /// <param name="localDepth">Node depth</param>
        /// <param name="movingCode">Code of moveing action to this node</param>
        /// <param name="holeTile">Hole tile location</param>
        /// <param name="depthLimit">Depth limitation</param>
        /// <returns>Flag: true if solution was found</returns>
        private bool deepSearch(int localDepth, int movingCode, (int c, int r) holeTile, int depthLimit)
        {
            // Visited counter
            VisitedCounter++;

            // Calculate heuristic at current iteration board
            int local_heuristic = SelectedHeuristic();

            // Solution found
            if (local_heuristic == 0)
            {
                // Set metrics
                SolutionNodeLevel = localDepth;

                // Return result
                return true;
            }

            // Local cost
            int local_cost = localDepth + local_heuristic;

            // Cut way if local heuristic + localdepth > allowed depth (depthLimit)
            if (local_cost > depthLimit)
            {
                next_dept_limit = local_cost;
                return false;
            }

            int min = int.MaxValue;

            // Make moves by mask: 
            //     for every possible direction of movement
            for (int i_mv = 0; i_mv < moves_mask.Length; i_mv++)
            {
                // except movement in the opposite direction
                if (move_ind_opposite[i_mv] != movingCode)
                {
                    // Set new hole position for moving with code = i_mv
                    (int c, int r) _hole = (
                        holeTile.c + moves_mask[i_mv].cOffset,
                        holeTile.r + moves_mask[i_mv].rOffset);

                    // Check moving by out of board
                    if (_hole.c < game.Width && _hole.c >= 0 &&
                        _hole.r < game.Width && _hole.r >= 0)
                    {
                        // Make moving
                        swapTiles(holeTile, _hole);

                        // Search deeper using recursion
                        if (deepSearch(localDepth + 1, i_mv, _hole, depthLimit))
                        {
                            // Solution was found
                            return true;
                        }

                        // Cancel moving (solution not found)
                        swapTiles(holeTile, _hole);

                        if (next_dept_limit < min) min = next_dept_limit;
                    }
                }
            }

            next_dept_limit = min;

            // Solution not found
            return false;
        }

        /// <summary>
        /// Swaps two tiles
        /// </summary>
        /// <param name="tile1">First tile as a tuple (column, row)</param>
        /// <param name="tile2">Second tile as a tuple (column, row)</param>
        private void swapTiles((int c, int r) tile1, (int c, int r) tile2)
        {
            byte _temp_var = board[tile1.c, tile1.r];
            board[tile1.c, tile1.r] = board[tile2.c, tile2.r];
            board[tile2.c, tile2.r] = _temp_var;
        }

        /// <summary>
        /// Calculate heuristic based on Manhattan Distance 
        /// </summary>
        /// <returns>Calculated heuristic on the current board</returns>
        private int heuristic_MD()
        {
            // Heuristic variable
            int _heuristic = 0;

            // Calculate standart MD heuristic
            for (short _c = 0; _c < board.GetLength(0); _c++)
            {
                for (short _r = 0; _r < board.GetLength(1); _r++)
                {
                    // For each element on current board
                    byte _c_elem = board[_c, _r];

                    // Note clalculate for null
                    if (_c_elem == 0) continue;

                    // Calculation : board tile must be a number in correct order 1,2,3...N,0
                    int _c_goal = (_c_elem - 1) % board.GetLength(0);
                    int _r_goal = (_c_elem - 1) / board.GetLength(1);
                    _heuristic += Math.Abs(_c_goal - _c) + Math.Abs(_r_goal - _r);
                }
            }

            // Return calculated heuristic
            return _heuristic;
        }


        /// <summary>
        /// Calculate heuristic based on Linear Conflict
        /// </summary>
        /// <returns>Calculated heuristic on the specified board</returns>
        private int LCH()
        {
            // Heuristic variable
            int _heuristic = 0;

            // Calculate

            // Row linear conflicts
            for (int _r = 0; _r < board.GetLength(1); _r++)
            {
                // List of tiles which in coflict
                List<int> _cflt = new List<int>();

                // Scan tuples in row _r for conflicts
                for (int _c1 = 0; _c1 < board.GetLength(0) - 1; _c1++)
                {
                    // already in conflict - skip
                    if (_cflt.Contains(_c1)) continue;

                    // Skip the hole
                    if (board[_c1, _r] == 0) continue;

                    // Scan next tiles
                    for (int _c2 = _c1 + 1; _c2 < board.GetLength(0); _c2++)
                    {
                        // already in conflict - skip
                        if (_cflt.Contains(_c2)) continue;

                        // get tile C1 position in goal state
                        (int _c1_g, int _r1_g) =
                            ((board[_c1, _r] - 1) % board.GetLength(0),
                            (board[_c1, _r] - 1) / board.GetLength(1));

                        // skip if another row
                        if (_r1_g != _r) continue;

                        // get tile C2 position in goal state
                        (int _c2_g, int _r2_g) =
                            ((board[_c2, _r] - 1) % board.GetLength(0),
                            (board[_c2, _r] - 1) / board.GetLength(1));

                        // skip if another row
                        if (_r2_g != _r) continue;

                        // check conflict condition
                        if (_c2_g < _c1_g)
                        {
                            _heuristic += 2;
                            _cflt.AddRange(new int[] { _c1, _c2 });
                        }
                    }
                }
            }

            // Clolumn linear conflicts
            for (int _c = 0; _c < board.GetLength(0); _c++)
            {
                // List of tiles which in coflict
                List<int> _cflt = new List<int>();

                // Scan tuples in column _c for conflicts
                for (int _r1 = 0; _r1 < board.GetLength(1) - 1; _r1++)
                {
                    // already in conflict - skip
                    if (_cflt.Contains(_r1)) continue;

                    // Skip the hole
                    if (board[_c, _r1] == 0) continue;

                    // Scan next tiles
                    for (int _r2 = _r1 + 1; _r2 < board.GetLength(1); _r2++)
                    {
                        // already in conflict - skip
                        if (_cflt.Contains(_r2)) continue;

                        // get tile R1 position in goal state
                        (int _c1_g, int _r1_g) =
                            ((board[_c, _r1] - 1) % board.GetLength(0),
                            (board[_c, _r1] - 1) / board.GetLength(1));

                        // skip if another column
                        if (_c1_g != _c) continue;

                        // get tile R2 position in goal state
                        (int _c2_g, int _r2_g) =
                            ((board[_c, _r2] - 1) % board.GetLength(0),
                            (board[_c, _r2] - 1) / board.GetLength(1));

                        // skip if another column
                        if (_c2_g != _c) continue;

                        // check conflict condition
                        if (_r2_g < _r1_g)
                        {
                            _heuristic += 2;
                            _cflt.AddRange(new int[] { _r1, _r2 });
                        }
                    }
                }
            }

            // Return calculated heuristic
            return _heuristic;
        }

        /// <summary>
        /// Calculate heuristic based on Linear Conflict
        /// </summary>
        /// <returns>Calculated heuristic on the specified board</returns>
        public int heuristic_LC() => LCH() + heuristic_MD();
    }
}
