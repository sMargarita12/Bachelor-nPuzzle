using System;


namespace nPuzzle.IDASolver
{
    public class IDAStarSolver
    {
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
            depth_limit = heuristic_MDLC();

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
            int local_heuristic = heuristic_MDLC();

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
        /// Calculate heuristic based on Manhattan Distance + Linear Conflict
        /// </summary>
        /// <returns>Calculated heuristic on the current board</returns>
        private int heuristic_MDLC()
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

                    // Calculation : board tile mus be a number in correct order 1,2,3...N,0
                    int _c_goal = (_c_elem - 1) % board.GetLength(0);
                    int _r_goal = (_c_elem - 1) / board.GetLength(1);
                    _heuristic += Math.Abs(_c_goal - _c) + Math.Abs(_r_goal - _r);
                }
            }

            // Return calculated heuristic
            return _heuristic;
        }
    }
}