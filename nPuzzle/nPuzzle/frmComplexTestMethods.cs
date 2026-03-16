using System;
using System.Linq;
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
    public partial class frmComplexTestMethods : Form
    {
        // Test count
        int testsCount;

        // Statistic for A* (id; timer, sec.; nodes count)
        List<(int, long, long)> stat_AStar =
            new List<(int, long, long)>();
        // Statistic for IDA* (id; ttimer, sec.; nodes count)
        List<(int, long, long)> stat_IDA =
            new List<(int, long, long)>();
        // Statistic for BA* (id; ttimer, sec.; nodes count)
        List<(int, long, long)> stat_BA =
            new List<(int, long, long)>();

        // Process busy
        bool busy = false;


        /// <summary>
        /// Ctor
        /// </summary>
        public frmComplexTestMethods()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Command: Run tests
        /// </summary>
        /// <param name="sender">Active object which generate event</param>
        /// <param name="e">Event parameters</param>
        private void lnkRunTests_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Process unavailable
            if (busy)
            {
                MessageBox.Show("Wait until current process complete!", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Read tests preets
            testsCount = Convert.ToInt32(nudTestsCount.Value);

            // Clear result lists
            stat_BA.Clear();
            stat_IDA.Clear();
            stat_AStar.Clear();

            // Prepare visual controls
            chartStat.Series["sreDuration"].Points.Clear();
            chartStat.Series["sreNodesCount"].Points.Clear();

            pbarProgress.Maximum = testsCount * 3;

            // Start
            Task.Run(() => TestsRun((ushort)(rbtBoard_3x3.Checked ? 8 : 15)));
        }

        /// <summary>
        /// Test function
        /// </summary>
        /// <param name="gameSize">Game board size</param>
        private void TestsRun(ushort gameSize)
        {
            // Process busy
            busy = true;

            // Progress bar counter
            int progress_counter = 0;

            // Create timer
            Stopwatch timer = new Stopwatch();

            // For all of tests
            for (int i = 0; i < testsCount; i++)
            {
                // Create game   
                NPuzzle game = new NPuzzle(N: gameSize);

                // ------------------ A* ------------------

                // Prepare A*-solver
                (int Col, int Row) = game.FindTileByNumber(0);
                BoardStateNode _initialState = new BoardStateNode(game.Board, Row, Col, 0);
                BoardStateNode _goalState = new BoardStateNode(game.GetGoalState(),
                    game.Width - 1, game.Width - 1, 0);
                Heuristics.Heuristics.SetGoalState(_goalState.State);
                AStarSolver.AStarSolver astar_solver = new AStarSolver.AStarSolver(
                        _initialState: _initialState,
                        _goalState: _goalState,
                        HeuristicFunction: Heuristics.Heuristics.CalcLinearConflictHeuristic);
                
                // Run using LC-heuristic
                timer.Start();
                BoardStateNode goal = astar_solver.Run();
                timer.Stop();

                // Collect stats for A*
                stat_AStar.Add((i, timer.ElapsedMilliseconds, astar_solver.VisitedCounter));

                // Progress bar increment
                pbarProgress.BeginInvoke(new Action(() => { pbarProgress.Value = ++progress_counter; }));


                // ------------------ IDA* ------------------

                // Prepare IDA*-solver
                IDAStarSolver ida_solver = new IDAStarSolver(game);

                // Run solver
                timer.Restart();
                BoardStateNode ida_goal = new BoardStateNode(
                        ida_solver.Run(),
                        game.Width - 1,
                        game.Width - 1,
                        ida_solver.VisitedCounter);
                timer.Stop();

                // Collect stats for IDA*
                stat_IDA.Add((i, timer.ElapsedMilliseconds, ida_solver.VisitedCounter));

                // Progress bar increment
                pbarProgress.BeginInvoke(new Action(() => { pbarProgress.Value = ++progress_counter; }));


                // ------------------ BA* ------------------

                // Prepare BA*-solver
                BAStarSolver ba_solver = new BAStarSolver(_initialState, _goalState);

                // Run solver
                timer.Restart();
                ba_solver.Run();
                timer.Stop();

                // Collect stats for BA*
                stat_BA.Add((i, timer.ElapsedMilliseconds, ba_solver.VisitedCounter));

                // Progress bar increment
                pbarProgress.BeginInvoke(new Action(() => { pbarProgress.Value = ++progress_counter; }));
            }

            // Process completed
            busy = false;

            // Call result show function
            ShowResults();
        }


        /// <summary>
        /// Result show function
        /// </summary>
        private void ShowResults()
        {
            // Stats calculating
            // for A*:
            (double duration, double nodes_count, double nd_per_sec) stat_astar =
                (
                    stat_AStar.Average(x => x.Item2) / 1000.0,
                    stat_AStar.Average(x => x.Item3),
                    1000.0 * stat_AStar.Average(x => x.Item3) / stat_AStar.Average(x => x.Item2)
                );
            // for IDA*:
            (double duration, double nodes_count, double nd_per_sec) stat_ida =
                (
                    stat_IDA.Average(x => x.Item2) / 1000.0,
                    stat_IDA.Average(x => x.Item3),
                    1000.0 * stat_IDA.Average(x => x.Item3) / stat_IDA.Average(x => x.Item2)
                );
            // for BA*:
            (double duration, double nodes_count, double nd_per_sec) stat_ba =
                (
                    stat_BA.Average(x => x.Item2) / 1000.0, 
                    stat_BA.Average(x => x.Item3),
                    1000.0 * stat_BA.Average(x => x.Item3) / stat_BA.Average(x => x.Item2)
                );

            // Table stats
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html>")
              .AppendLine("<head>")
              .AppendLine("<style type='text/css'>")
              .AppendLine("TABLE {")
              .AppendLine("width: 100%;")
              .AppendLine("border-collapse: collapse;")
              .AppendLine("}")
              .AppendLine("TD, TH {")
              .AppendLine("padding: 3px;")
              .AppendLine("border: 1px solid black;")
              .AppendLine("}")
              .AppendLine("</style>")
              .AppendLine("</head>")
              .AppendLine("<body>")
              .AppendLine("<table>")
              .AppendLine("<tr>")
              .AppendLine("<td align=center rowspan=2><b>Metric</b></td>")
              .AppendLine("<td align=center colspan=3><b>Algorithm</b></td>")
              .AppendLine("</tr>")
              .AppendLine("<tr>")
              .AppendLine("<td align=center><b>A* (LC)</b></td>")
              .AppendLine("<td align=center><b>IDA* (LC)</b></td>")
              .AppendLine("<td align=center><b>BA* (LC)</b></td>")
              .AppendLine("</tr>")
              .AppendLine("<tr>")
              .AppendLine("<td><b>Time elapsed, sec.<br>(TE)</b></td>")
              .AppendLine($"<td align=right>{stat_astar.duration:F4}</td>")
              .AppendLine($"<td align=right>{stat_ida.duration:F4}</td>")
              .AppendLine($"<td align=right>{stat_ba.duration:F4}</td>")
              .AppendLine("</tr>")
              .AppendLine("<td><b>Visited nodes count<br>(VN)</b></td>")
              .AppendLine($"<td align=right>{stat_astar.nodes_count:F2}</td>")
              .AppendLine($"<td align=right>{stat_ida.nodes_count:F2}</td>")
              .AppendLine($"<td align=right>{stat_ba.nodes_count:F2}</td>")
              .AppendLine("</tr>")
              .AppendLine("<td><b>Nodes per Second<br>(NpS)</b></td>")
              .AppendLine($"<td align=right>{stat_astar.nd_per_sec:F2}</td>")
              .AppendLine($"<td align=right>{stat_ida.nd_per_sec:F2}</td>")
              .AppendLine($"<td align=right>{stat_ba.nd_per_sec:F2}</td>")
              .AppendLine("</tr>")
              .AppendLine("</table>")
              .AppendLine("</body>")
              .AppendLine("</html>");

            // Write HTML table to control
            webbStats.BeginInvoke(new Action(() => {
                webbStats.DocumentText = sb.ToString();
            }));

            // Visualize chart
            chartStat.BeginInvoke(new Action(() => {
                chartStat.Series["sreDuration"].Points.AddXY("A*", stat_astar.duration);
                chartStat.Series["sreDuration"].Points.AddXY("IDA*", stat_ida.duration);
                chartStat.Series["sreDuration"].Points.AddXY("BA*", stat_ba.duration);

                chartStat.Series["sreNodesCount"].Points.AddXY("A*", stat_astar.nodes_count);
                chartStat.Series["sreNodesCount"].Points.AddXY("IDA*", stat_ida.nodes_count);
                chartStat.Series["sreNodesCount"].Points.AddXY("BA*", stat_ba.nodes_count);
            }));
        }
    }
}