using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

using nPuzzle.BASolver;
using nPuzzle.IDASolver;
using nPuzzle.AStarSolver;


namespace nPuzzle
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Board size cases
        /// </summary>
        enum FieldSize
        {
            Small = 8,
            Standart = 15,
            Big = 24,
            Large = 35
        }

        /// <summary>
        /// Heuristic cases
        /// </summary>
        enum Heuristic
        {
            Hamming = 0,
            LinearConflict,
            ManhattanDistance,
            PatternsDB
        }

        /// <summary>
        /// Solution method
        /// </summary>
        enum Method
        {
            AStar = 0,
            IDA,
            BA
        }


        /// <summary>
        /// Main form: ctor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }


        // n-Puzzle game Instance
        NPuzzle game;

        // Selected heuristic
        Heuristic heuristic = Heuristic.Hamming;

        // Process busy
        bool busy = false;

        /// <summary>
        /// Command of new game generation
        /// </summary>
        /// <param name="sender">Active object which generate event</param>
        /// <param name="e">Event parameters</param>
        private void btnGenerateGame_Click(object sender, EventArgs e)
        {
            // Game size                       
            FieldSize _fs;

            // Field size can be read from 'Tag' property of the menu item
            switch (Convert.ToInt32(((ToolStripMenuItem)sender).Tag))
            {
                case 8: _fs = FieldSize.Small; break;
                case 15: _fs = FieldSize.Standart; break;
                case 24: _fs = FieldSize.Big; break;
                case 35: _fs = FieldSize.Large; break;
                default: _fs = FieldSize.Standart; break;
            }

            // Create new game
            game = new NPuzzle(N: Convert.ToUInt16(_fs));

            // game.Board = new byte[,] {
            //    { 5,  8, 15, 2 },
            //    { 10, 3, 0,  11 },
            //    { 14, 6, 12, 4 },
            //    { 7,  1, 9,  13 }
            // }; 
            // game.Board = new byte[,] {
            //    { 11, 0,  3,  6 },
            //    { 13, 1,  8,  5 },
            //    { 10, 14, 4,  15 },
            //    { 7,  2,  12, 9 }
            // };

            // Fill game board
            ushort _size = game.Width;
            dgvField.RowCount = dgvField.ColumnCount = _size;
            for (int _c = 0; _c < _size; _c++)
                for (int _r = 0; _r < _size; _r++)
                    dgvField[_c, _r].Value =
                        game[_c, _r] == 0 ? "" : game[_c, _r].ToString();

            // Sizing
            for (int _c = 0; _c < _size; _c++)
                dgvField.Columns[_c].Width = dgvField.Width / _size;
            for (int _r = 0; _r < _size; _r++)
                dgvField.Rows[_r].Height = dgvField.Height / _size;
        }

        /// <summary>
        /// Command of current game solving by selected method
        /// </summary>
        /// <param name="sender">Active object which generate event</param>
        /// <param name="e">Event parameters</param>
        private void btnSolve_Click(object sender, EventArgs e)
        {
            // Process unavailable
            if (busy)
            {
                MessageBox.Show("Wait until current process complete!", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Diagnostic
            if (game == null)
            {
                MessageBox.Show("Generate a new game at first!", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Selected solution method can be read from 'Tag' property of the menu item
            Method _mth = (Method)Convert.ToInt32(((ToolStripMenuItem)sender).Tag);

            // Run 
            Task.Run(() => Solve(_mth));
        }

        /// <summary>
        /// Solving puzzle function (run in dedicated thread)
        /// </summary>
        /// <param name="method">Selected solution method</param>
        private void Solve(Method method)
        {
            // Process busy
            busy = true;

            // Process information
            string h_info = "";
            switch (heuristic)
            {
                case Heuristic.Hamming: h_info = "HH"; break;
                case Heuristic.LinearConflict: h_info = "LC"; break;
                case Heuristic.ManhattanDistance: h_info = "MD"; break;
                case Heuristic.PatternsDB: h_info = "PDB"; break;
                default: h_info = "??"; break;
            }
            string m_info = "";
            switch (method)
            {
                case Method.AStar: m_info = "A*"; break;
                case Method.IDA: m_info = "IDA*"; h_info = "MD"; break;
                case Method.BA: m_info = "BA*"; h_info = "LC"; break;
                default: m_info = "??"; break;
            }            
            string p_info = $"{m_info} [{(h_info)}]";


            // View progress
            lblResults.BeginInvoke(new Action(() =>
            {
                lblResults.Text = $"\n\r\n\rSolving in progress...\n\r\n\r{p_info}";
            }));

            // Timer
            Stopwatch timer = new Stopwatch();

            // Solution metrics
            long duration = 0;
            int depth = 0;
            int visitedNodes = 0;

            // Solution goal
            BoardStateNode goal = null;

            // Disposing of unused memory && measuring of allocated memory before starting
            Heuristics.Heuristics.ResetPatternsDB();
            GC.Collect();
            long _memBefore = GC.GetTotalMemory(true);

            // Run selected method
            switch (method)
            {
                case Method.AStar:

                    // Set states of board (initial & goal)
                    (int Col, int Row) = game.FindTileByNumber(0);
                    BoardStateNode _initialState = new BoardStateNode(game.Board, Row, Col, 0);
                    BoardStateNode _goalState = new BoardStateNode(game.GetGoalState(),
                        game.Width - 1, game.Width - 1, 0);

                    // Prepare heuristic
                    Heuristics.Heuristics.SetGoalState(_goalState.State);

                    // Create solver A*
                    AStarSolver.AStarSolver _astar_solver = new AStarSolver.AStarSolver(
                            _initialState, _goalState,
                            heuristic == Heuristic.Hamming
                                ? Heuristics.Heuristics.CalcHammingHeuristic
                                : heuristic == Heuristic.LinearConflict
                                    ? Heuristics.Heuristics.CalcLinearConflictHeuristic
                                    : heuristic == Heuristic.ManhattanDistance
                                        ? Heuristics.Heuristics.CalcManhattanDistanceHeuristic
                                        : (Func<BoardStateNode, object, int>)Heuristics.Heuristics.CalcPatthernsDBHeuristic
                        );

                    bool _pdb_err = false;
                    if (heuristic == Heuristic.PatternsDB)
                    {
                        // Load patterns DB
                        if (!Heuristics.Heuristics.SetPatternsDB($"pdb{game.N}.pdb"))
                        {
                            _pdb_err = true;
                        }
                    }

                    if (!_pdb_err)
                    {
                        // Statr solving & mesure time
                        timer.Start();
                        goal = _astar_solver.Run();
                        timer.Stop();

                        // Solution metrics
                        duration = timer.ElapsedMilliseconds;
                        depth = goal.Depth;
                        visitedNodes = _astar_solver.VisitedCounter;
                    }

                    // Complete
                    break;

                case Method.IDA:

                    // Create solver IDA*
                    IDAStarSolver _ida_solver = new IDAStarSolver(game);

                    // Statr solving & mesure time
                    timer.Start();
                    goal = new BoardStateNode(_ida_solver.Run(), 0, 0, _ida_solver.SolutionNodeLevel);
                    timer.Stop();

                    // Solution metrics
                    duration = timer.ElapsedMilliseconds;
                    depth = goal.Depth;
                    visitedNodes = _ida_solver.VisitedCounter;

                    // Complete
                    break;

                case Method.BA:

                    // Set states of board (initial & goal)
                    (int _Col, int _Row) = game.FindTileByNumber(0);
                    BoardStateNode _initialState_ba = new BoardStateNode(game.Board, _Row, _Col, 0);
                    BoardStateNode _goalState_ba = new BoardStateNode(game.GetGoalState(),
                        game.Width - 1, game.Width - 1, 0);

                    // Create solver BA*
                    BAStarSolver _ba_solver = new BAStarSolver(_initialState_ba, _goalState_ba);

                    // Statr solving & mesure time
                    timer.Start();
                    goal = _ba_solver.Run();
                    timer.Stop();

                    // Solution metrics
                    duration = timer.ElapsedMilliseconds;
                    depth = goal.Depth;
                    visitedNodes = _ba_solver.VisitedCounter;

                    // The goal node get from BA* is a node whic located depp in solution tree.
                    // To correct print the goal in DataGridView we can set the completed goal state
                    goal.State = _goalState_ba.State;

                    // Complete
                    break;

                default:

                    // Uknown method
                    MessageBox.Show("Unknown solution method was selected!", Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    // Complete
                    break;
            }

            // Measuring of allocated memory after solving
            long _memAfter = GC.GetTotalMemory(false);

            // Goal state - visualize
            int _size = game.Width;
            if (goal != null)
            {
                dgvGoal.BeginInvoke(new Action(() =>
                {
                    dgvGoal.RowCount = dgvGoal.ColumnCount = _size;
                    for (int _c = 0; _c < _size; _c++)
                        for (int _r = 0; _r < _size; _r++)
                            dgvGoal[_c, _r].Value =
                                goal.State[_c, _r] == 0 ? "" : goal.State[_c, _r].ToString();
                // Sizing
                for (int _c = 0; _c < _size; _c++)
                        dgvGoal.Columns[_c].Width = dgvGoal.Width / _size;
                    for (int _r = 0; _r < _size; _r++)
                        dgvGoal.Rows[_r].Height = dgvGoal.Height / _size;
                }));
            }

            // Results log
            StringBuilder sb = new StringBuilder();            

            // If goal has been reached
            if (goal != null)
            {
                sb.AppendLine($"Method <{p_info}> completed!")
                  .AppendLine($"Time elapsed: {duration} msec.")
                  .AppendLine($"Solution depth: {depth} levels.")
                  .AppendLine($"Visited counter: {visitedNodes}")
                  .AppendLine($"Total memory: {(_memAfter - _memBefore) / 1024 } KBytes");
            }
            else
            {
                sb.AppendLine($"Method <{p_info}> not started: ")
                  .AppendLine($"\n\rSolution was not found!");
            }

            lblResults.BeginInvoke(new Action(() =>
            {
                lblResults.Text = sb.ToString();
            }));

            // Process completed
            busy = false;
        }

        /// <summary>
        /// Hidden function: prepare DB of patterns
        /// </summary>
        /// <param name="sender">Active object which generate event</param>
        /// <param name="e">Event parameters</param>
        private void btnPatternsDBCreate_Click(object sender, EventArgs e)
        {
            // Process unavailable
            if (busy)
            {
                MessageBox.Show("Wait until current process complete!", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            busy = true;

            Task.Run(() => GenerateDB());
        }

        /// <summary>
        /// Patterns DB Generation
        /// </summary>
        private void GenerateDB()
        {
            long i_cnt = 0;

            int extern_i = 1;
            int extern_max = 100;
            int intern_max = 50;

            FieldSize _fs = FieldSize.Standart;
            NPuzzle game = null;

            pbarInternal.Invoke(new Action(() => { 
                pbarInternal.Maximum = intern_max;
                pbarInternal.Value = 0;
            }));
            pbarExternal.Invoke(new Action(() => { 
                pbarExternal.Maximum = extern_max;
                pbarExternal.Value = 0;
            }));

            while (extern_i <= extern_max)
            {
                int intern_i = 1;

                Dictionary<string, int> db = Heuristics.Heuristics.LoadPatternsDB($"pdb{(int)_fs}.pdb");
                Dictionary<string, int> _new_pdb = new Dictionary<string, int>();

                while (intern_i < intern_max)
                {
                    game = new NPuzzle(N: Convert.ToUInt16(_fs));

                    (int Col, int Row) = game.FindTileByNumber(0);
                    BoardStateNode _initialState = new BoardStateNode(game.Board, Row, Col, 0);
                    BoardStateNode _goalState = new BoardStateNode(game.GetGoalState(),
                        game.Width - 1, game.Width - 1, 0);

                    string key = _initialState.Keystr;
                    if (!db.ContainsKey(key) && !_new_pdb.ContainsKey(key))
                    {
                        Heuristics.Heuristics.SetGoalState(_goalState.State);
                        BAStarSolver solver = new BAStarSolver(_initialState, _goalState);
                        BoardStateNode goal = solver.Run();
                        _new_pdb.Add(key, goal.Depth);

                        intern_i++;
                        i_cnt++;
                    }

                    pbarInternal.Invoke(new Action(() => { pbarInternal.Value = intern_i; }));
                    lblGeneratedStates.Invoke(new Action(() => {
                        lblGeneratedStates.Text = $"{i_cnt}";
                    }));
                }

                pbarInternal.Invoke(new Action(() => { pbarInternal.Value = 0; }));

                using (var fs = new FileStream($"pdb{game.N}.pdb", FileMode.Append))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        foreach (var item in _new_pdb)
                        {
                            sw.WriteLine($"{item.Key}?{item.Value}");
                        }
                    }
                }

                pbarExternal.Invoke(new Action(() => { pbarExternal.Value = extern_i; }));

                extern_i++;
            }

            busy = false;
        }

        /// <summary>
        /// Go to experiments window (A* heuristics)
        /// </summary>
        /// <param name="sender">Active object which generate event</param>
        /// <param name="e">Event parameters</param>
        private void btnComplexTestConfigure_Click(object sender, EventArgs e)
        {
            new frmComplexTestAStar().Show();
        }

        /// <summary>
        /// Go to experiments window (A* heuristics)
        /// </summary>
        /// <param name="sender">Active object which generate event</param>
        /// <param name="e">Event parameters</param>
        private void aIDABASerieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmComplexTestMethods().Show();
        }

        /// <summary>
        /// Heuristic selection from menu items
        /// </summary>
        /// <param name="sender">Active object which generate event</param>
        /// <param name="e">Event parameters</param>
        private void SelectHeuristic(object sender, EventArgs e)
        {
            // Reset previous choice
            hammingHHToolStripMenuItem.Checked =
                manhattanDisanceMDHToolStripMenuItem.Checked =
                linearConflictLCHToolStripMenuItem.Checked =
                patternsDatabasePDBHToolStripMenuItem.Checked = false;

            // Select menu item
            ((ToolStripMenuItem)sender).Checked = true;

            // Select heuristic
            if (hammingHHToolStripMenuItem.Checked)
                heuristic = Heuristic.Hamming;
            if (manhattanDisanceMDHToolStripMenuItem.Checked)
                heuristic = Heuristic.ManhattanDistance;
            if (linearConflictLCHToolStripMenuItem.Checked)
                heuristic = Heuristic.LinearConflict;
            if (patternsDatabasePDBHToolStripMenuItem.Checked)
                heuristic = Heuristic.PatternsDB;
        }
    }
}