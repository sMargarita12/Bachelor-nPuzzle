using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;


namespace nPuzzle.Heuristics
{
    /// <summary>
    /// Arguments for MD heuristic optimization algorithm
    /// </summary>
    public class MDArguments
    {
        // Parent heuristic
        public int BasicHeuristicValue;

        // Moveable tile value
        public byte TileValue;

        // Previous tile location (before moving)
        public int PreviousPos_R;
        public int PreviousPos_C;

        // Current tile location (after moving)
        public int CurrentPos_R;
        public int CurrentPos_C;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="BasicHeuristicValue">Parent heuristic</param>
        /// <param name="TileValue">Moveable tile value</param>
        /// <param name="PreviousPos_R">Previous tile location (before moving): row</param>
        /// <param name="PreviousPos_C">Previous tile location (before moving): column</param>
        /// <param name="CurrentPos_R">Current tile location (after moving): row</param>
        /// <param name="CurrentPos_C">Current tile location (after moving): column</param>
        public MDArguments(int BasicHeuristicValue, byte TileValue,
            int PreviousPos_R, int PreviousPos_C, int CurrentPos_R, int CurrentPos_C)
        {
            this.TileValue = TileValue;
            this.CurrentPos_R = CurrentPos_R;
            this.CurrentPos_C = CurrentPos_C;
            this.PreviousPos_R = PreviousPos_R;
            this.PreviousPos_C = PreviousPos_C;
            this.BasicHeuristicValue = BasicHeuristicValue;
        }
    }


    public class Heuristics
    {
        const string _BoardSizeDiffersFromGoalExceptionMessage =
            "Size of board does not match the size of Goal state!";

        const string _GoalStateNotSpecifiedExceptionMessage =
            "The Goal state was not specified! Please, set Heuristics.Goal at first!";


        /// <summary>
        /// Patterns DB dictionary
        /// </summary>
        private static Dictionary<string, int> PatternsDB 
            = new Dictionary<string, int>();

        /// <summary>
        /// Loadatterns database
        /// </summary>
        /// <param name="fileName">Specified file name</param>
        /// <returns>Patterns DB dictionary</returns>
        public static Dictionary<string, int> LoadPatternsDB(string fileName)
        {
            Dictionary<string, int> _pdb = new Dictionary<string, int>();

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] row = sr.ReadLine().Split(new char[] { '?' },
                            StringSplitOptions.RemoveEmptyEntries);
                        if (!_pdb.ContainsKey(row[0]))
                            _pdb.Add(row[0], Convert.ToInt32(row[1]));
                    }

                }
            }

            return _pdb;
        }

        /// <summary>
        /// Set pattern DB from data file
        /// </summary>
        /// <param name="fileName">Specified file name</param>
        /// <returns>True if OK</returns>
        public static bool SetPatternsDB(string fileName)
        {
            try
            {
                PatternsDB = LoadPatternsDB(fileName);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return false;
            }
        }

        /// <summary>
        /// Clear patterns DB
        /// </summary>
        public static void ResetPatternsDB() => PatternsDB.Clear();

        /// <summary>
        /// The goal state for a game
        /// </summary>
        public static byte[,] Goal { get; private set; }

        /// <summary>
        /// Set the goal state for a game
        /// </summary>
        /// <param name="_goal">The goal state for a game</param>
        public static void SetGoalState(byte[,] _goal)
        {
            Goal = _goal;
        }

        /// <summary>
        /// Calculate the Misplaced Tiles (Hamming) heuristics
        /// </summary>
        /// <param name="_tiles">Current state of the game's board to calculate</param>
        /// <returns>Calculated heuristic on the specified board</returns>
        public static int CalcHammingHeuristic(AStarSolver.BoardStateNode bsNode, object args = null)
        {
            if (Goal == null)
                throw new NullReferenceException(_GoalStateNotSpecifiedExceptionMessage);

            // Heuristic variable
            int _heuristic = 0;

            // Calculate
            for (short _c = 0; _c < bsNode.State.GetLength(0); _c++)
            {
                for (short _r = 0; _r < bsNode.State.GetLength(1); _r++)
                {
                    if (bsNode.State[_c, _r] != Goal[_c, _r] && bsNode.State[_c, _r] != 0)
                    {
                        _heuristic++;
                    }
                }
            }

            // Return calculated heuristic
            return _heuristic;
        }

        /// <summary>
        /// Calculate heuristic based on Linear Conflict
        /// </summary>
        /// <param name="_tiles">Current state of the game's board to calculate</param>
        /// <returns>Calculated heuristic on the specified board</returns>
        private static int LCH(AStarSolver.BoardStateNode bsNode)
        {
            // Heuristic variable
            int _heuristic = 0;

            // Calculate

            // Row linear conflicts
            for (int _r = 0; _r < bsNode.State.GetLength(1); _r++)
            {
                // List of tiles which in coflict
                List<int> _cflt = new List<int>();

                // Scan tuples in row _r for conflicts
                for (int _c1 = 0; _c1 < bsNode.State.GetLength(0) - 1; _c1++)
                {
                    // already in conflict - skip
                    if (_cflt.Contains(_c1)) continue;

                    // Skip the hole
                    if (bsNode.State[_c1, _r] == 0) continue;

                    // Scan next tiles
                    for (int _c2 = _c1 + 1; _c2 < bsNode.State.GetLength(0); _c2++)
                    {
                        // already in conflict - skip
                        if (_cflt.Contains(_c2)) continue;

                        // get tile C1 position in goal state
                        (int _c1_g, int _r1_g) = getTilePosition(bsNode.State[_c1, _r], Goal);
                        // (int _c1_g, int _r1_g) =
                        //    ((bsNode.State[_c1, _r] - 1) % Goal.GetLength(0),
                        //    (bsNode.State[_c1, _r] - 1) / Goal.GetLength(1));

                        // skip if another row
                        if (_r1_g != _r) continue;

                        // get tile C2 position in goal state
                        (int _c2_g, int _r2_g) = getTilePosition(bsNode.State[_c2, _r], Goal);
                        //  (int _c2_g, int _r2_g) =
                        //    ((bsNode.State[_c2, _r] - 1) % Goal.GetLength(0),
                        //    (bsNode.State[_c2, _r] - 1) / Goal.GetLength(1));

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
            for (int _c = 0; _c < bsNode.State.GetLength(0); _c++)
            {
                // List of tiles which in coflict
                List<int> _cflt = new List<int>();

                // Scan tuples in column _c for conflicts
                for (int _r1 = 0; _r1 < bsNode.State.GetLength(1) - 1; _r1++)
                {
                    // already in conflict - skip
                    if (_cflt.Contains(_r1)) continue;

                    // Skip the hole
                    if (bsNode.State[_c, _r1] == 0) continue;

                    // Scan next tiles
                    for (int _r2 = _r1 + 1; _r2 < bsNode.State.GetLength(1); _r2++)
                    {
                        // already in conflict - skip
                        if (_cflt.Contains(_r2)) continue;

                        // get tile R1 position in goal state
                        (int _c1_g, int _r1_g) = getTilePosition(bsNode.State[_c, _r1], Goal);
                        //  (int _c1_g, int _r1_g) =
                        //    ((bsNode.State[_c, _r1] - 1) % Goal.GetLength(0),
                        //    (bsNode.State[_c, _r1] - 1) / Goal.GetLength(1));

                        // skip if another column
                        if (_c1_g != _c) continue;

                        // get tile R2 position in goal state
                        (int _c2_g, int _r2_g) = getTilePosition(bsNode.State[_c, _r2], Goal);
                        //  (int _c2_g, int _r2_g) =
                        //    ((bsNode.State[_c, _r2] - 1) % Goal.GetLength(0),
                        //    (bsNode.State[_c, _r2] - 1) / Goal.GetLength(1));

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
        /// <param name="_tiles">Current state of the game's board to calculate</param>
        /// <returns>Calculated heuristic on the specified board</returns>
        public static int CalcLinearConflictHeuristic(AStarSolver.BoardStateNode bsNode, object args = null)
        {
            if (Goal == null)
                throw new NullReferenceException(_GoalStateNotSpecifiedExceptionMessage);

            return LCH(bsNode) + CalcManhattanDistanceHeuristic(bsNode, args);
        }

        /// <summary>
        /// Calculate heuristic based on Manhattan Distance
        /// </summary>
        /// <param name="_tiles">Current state of the game's board to calculate</param>
        /// <returns>Calculated heuristic on the specified board</returns>
        public static int CalcManhattanDistanceHeuristic(AStarSolver.BoardStateNode bsNode, object args = null)
        {
            if (Goal == null)
                throw new NullReferenceException(_GoalStateNotSpecifiedExceptionMessage);

            // Heuristic variable
            int _heuristic = 0;

            // Check arguments
            if (args != null)
            {
                // Parse aguments
                var md_args = args as MDArguments;

                // Get new position of moved tile
                (int mr_c, int mt_r) mt_pos = 
                    ((md_args.TileValue - 1) % Goal.GetLength(0), 
                    (md_args.TileValue - 1) / Goal.GetLength(1));

                // Calculate incremental MD heuristic
                _heuristic = md_args.BasicHeuristicValue
                    - Math.Abs(md_args.PreviousPos_C - mt_pos.mr_c)
                    - Math.Abs(md_args.PreviousPos_R - mt_pos.mt_r)
                    + Math.Abs(md_args.CurrentPos_C - mt_pos.mr_c)
                    + Math.Abs(md_args.CurrentPos_R - mt_pos.mt_r);
            }
            else
            {
                // Calculate standart MD heuristic
                for (short _c = 0; _c < bsNode.State.GetLength(0); _c++)
                {
                    for (short _r = 0; _r < bsNode.State.GetLength(1); _r++)
                    {
                        // For each element on current board
                        byte _c_elem = bsNode.State[_c, _r];

                        // Note clalculate for null
                        if (_c_elem == 0) continue;

                        // Calculation : board tile mus be a number in correct order 1,2,3...N,0
                        int _c_goal = (_c_elem - 1) % Goal.GetLength(0);
                        int _r_goal = (_c_elem - 1) / Goal.GetLength(1);
                        _heuristic += Math.Abs(_c_goal - _c) + Math.Abs(_r_goal - _r);
                        
                        /*
                        // Calculation : any order of tilen in Goal

                        // Flag: calculated for this element
                        bool __el_calc = false;

                        // Calculate MD for element
                        for (int _c_goal = 0; _c_goal < Goal.GetLength(0); _c_goal++)
                        {
                            for (int _r_goal = 0; _r_goal < Goal.GetLength(0); _r_goal++)
                            {
                                if (Goal[_c_goal, _r_goal] == _c_elem)
                                {
                                    // Increment heurisic
                                    _heuristic += Math.Abs(_c_goal - _c) + Math.Abs(_r_goal - _r);
                                    __el_calc = true;
                                    break;
                                }
                            }

                            // MD for the current element was calculated
                            if (__el_calc) break;
                        } */
                    }
                }
            }

            // Return calculated heuristic
            return _heuristic;
        }

        /// <summary>
        /// Calculate heuristic using patterns database
        /// </summary>
        /// <param name="_tiles">Current state of the game's board to calculate</param>
        /// <returns>Calculated heuristic on the specified board</returns>
        public static int CalcPatthernsDBHeuristic(AStarSolver.BoardStateNode bsNode, object args = null)
        {
            if (Goal == null)
                throw new NullReferenceException(_GoalStateNotSpecifiedExceptionMessage);

            if (PatternsDB.ContainsKey(bsNode.Keystr))
                return PatternsDB[bsNode.Keystr];
            else
                return CalcLinearConflictHeuristic(bsNode);
        }

        /// <summary>
        /// Find coordinates of a specified tile in tiles array
        /// </summary>
        /// <param name="_tileValue">A specified tile value</param>
        /// <param name="_tilesArray">Array of tiles to search</param>
        /// <returns>Tuple with coordinates (col, row) of a specified tile in array</returns>
        public static (int, int) getTilePosition(byte _tileValue, byte[,] _tilesArray)
        {
            for (int _c = 0; _c < _tilesArray.GetLength(0); _c++)
                for (int _r = 0; _r < _tilesArray.GetLength(1); _r++)
                    if (_tilesArray[_c, _r] == _tileValue) return (_c, _r);
            return (-1, -1);
        }        
    }
}