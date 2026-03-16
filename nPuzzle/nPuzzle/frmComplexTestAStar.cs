using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

using nPuzzle.AStarSolver;
using System.Threading.Tasks;


namespace nPuzzle
{
    public partial class frmComplexTestAStar : Form
    {
        // Statistic for Hamming heuristic (nr; timer, sec.; memory; nodes count)
        List<(int, long, long, int)> stat_HE =
                new List<(int, long, long, int)>();
        // Statistic for MD heuristic (nr; timer, sec.; memory; nodes count)
        List<(int, long, long, int)> stat_MDH =
            new List<(int, long, long, int)>();
        // Statistic for LC heuristic (nr; timer, sec.; memory; nodes count)
        List<(int, long, long, int)> stat_LCH =
            new List<(int, long, long, int)>();
        // Statistic for Patterns DB heuristic (nr; timer, sec.; memory; nodes count)
        List<(int, long, long, int)> stat_PDBH =
            new List<(int, long, long, int)>();

        // Process busy
        bool busy = false;

        // Test count
        int testsCount;


        /// <summary>
        /// Ctor
        /// </summary>
        public frmComplexTestAStar()
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
            stat_HE.Clear();
            stat_MDH.Clear();
            stat_LCH.Clear();
            stat_PDBH.Clear();

            // Prepare visual controls
            chartTiming.Series["sreETLC"].Points.Clear();
            chartTiming.Series["sreETHamming"].Points.Clear();
            chartTiming.Series["sreETManhattan"].Points.Clear();
            chartTiming.Series["sreETPatternsDB"].Points.Clear();
            chartMemoryAlloc.Series["sreETLC"].Points.Clear();
            chartMemoryAlloc.Series["sreETHamming"].Points.Clear();
            chartMemoryAlloc.Series["sreETManhattan"].Points.Clear();
            chartMemoryAlloc.Series["sreETPatternsDB"].Points.Clear();
            chartNodesVisited.Series["sreETLC"].Points.Clear();
            chartNodesVisited.Series["sreETHamming"].Points.Clear();
            chartNodesVisited.Series["sreETManhattan"].Points.Clear();
            chartNodesVisited.Series["sreETPatternsDB"].Points.Clear();
            chartStat.Series["sreETLC"].Points.Clear();
            chartStat.Series["sreETHamming"].Points.Clear();
            chartStat.Series["sreETManhattan"].Points.Clear();
            chartStat.Series["sreETPatternsDB"].Points.Clear();

            pbarProgress.Maximum = testsCount * 4;

            // Start
            Task.Run(() => TestsRun());
        }

        /// <summary>
        /// Test function
        /// </summary>
        private void TestsRun()
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
                NPuzzle game = new NPuzzle(N: 8);

                // Prepare 
                (int Col, int Row) = game.FindTileByNumber(0);
                BoardStateNode _initialState = new BoardStateNode(game.Board, Row, Col, 0);
                BoardStateNode _goalState = new BoardStateNode(game.GetGoalState(),
                    game.Width - 1, game.Width - 1, 0);
                Heuristics.Heuristics.SetGoalState(_goalState.State);

                // Run - Hamming heuristic
                AStarSolver.AStarSolver solver = new AStarSolver.AStarSolver(
                        _initialState: _initialState,
                        _goalState: _goalState,
                        HeuristicFunction: Heuristics.Heuristics.CalcHammingHeuristic);
                GC.Collect();
                long _memBefore = GC.GetTotalMemory(false);
                timer.Start();
                BoardStateNode goal = solver.Run();
                timer.Stop();
                long _memAfter = GC.GetTotalMemory(false);
                
                // Stat
                stat_HE.Add((i, timer.ElapsedMilliseconds,
                    (_memAfter - _memBefore) / 1024,
                    solver.VisitedCounter));

                // Progress increment
                pbarProgress.BeginInvoke(new Action(() => { pbarProgress.Value = ++progress_counter; }));


                // Run - Manhattan distance heuristic
                solver = new AStarSolver.AStarSolver(
                        _initialState: _initialState,
                        _goalState: _goalState,
                        HeuristicFunction: Heuristics.Heuristics.CalcManhattanDistanceHeuristic);
                GC.Collect();
                _memBefore = GC.GetTotalMemory(false);
                timer.Restart();
                goal = solver.Run();
                timer.Stop();
                _memAfter = GC.GetTotalMemory(false);

                // Stat
                stat_MDH.Add((i, timer.ElapsedMilliseconds,
                    (_memAfter - _memBefore) / 1024,
                    solver.VisitedCounter));

                // Progress increment
                pbarProgress.BeginInvoke(new Action(() => { pbarProgress.Value = ++progress_counter; }));


                // Run - LC distance heuristic
                solver = new AStarSolver.AStarSolver(
                        _initialState: _initialState,
                        _goalState: _goalState,
                        HeuristicFunction: Heuristics.Heuristics.CalcLinearConflictHeuristic);
                GC.Collect();
                _memBefore = GC.GetTotalMemory(false);
                timer.Restart();
                goal = solver.Run();
                timer.Stop();
                _memAfter = GC.GetTotalMemory(false);

                // Stat
                stat_LCH.Add((i, timer.ElapsedMilliseconds,
                    (_memAfter - _memBefore) / 1024,
                    solver.VisitedCounter));

                // Progress increment
                pbarProgress.BeginInvoke(new Action(() => { pbarProgress.Value = ++progress_counter; }));


                // Run - Patterns database heuristic
                GC.Collect();
                _memBefore = GC.GetTotalMemory(false);
                // Load patterns DB
                Heuristics.Heuristics.SetPatternsDB($"pdb8.pdb");
                solver = new AStarSolver.AStarSolver(
                        _initialState: _initialState,
                        _goalState: _goalState,
                        HeuristicFunction: Heuristics.Heuristics.CalcPatthernsDBHeuristic);
                timer.Restart();
                goal = solver.Run();
                timer.Stop();
                _memAfter = GC.GetTotalMemory(false);
                Heuristics.Heuristics.ResetPatternsDB();
                
                // Stat
                stat_PDBH.Add((i, timer.ElapsedMilliseconds,
                    (_memAfter - _memBefore) / 1024,
                    solver.VisitedCounter));

                // Progress increment
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

            // Visualize

            // Sort by timing
            stat_LCH.Sort((a, b) => a.Item2.CompareTo(b.Item2));
            stat_HE.Sort((a, b) => a.Item2.CompareTo(b.Item2));
            stat_MDH.Sort((a, b) => a.Item2.CompareTo(b.Item2));
            stat_PDBH.Sort((a, b) => a.Item2.CompareTo(b.Item2));

            chartTiming.BeginInvoke(new Action(() => {
                for (int t_i = 0; t_i < testsCount; t_i++)
                {
                    chartTiming.Series["sreETLC"].Points.AddXY(t_i + 1, stat_LCH[t_i].Item2);
                    chartTiming.Series["sreETHamming"].Points.AddXY(t_i + 1, stat_HE[t_i].Item2);
                    chartTiming.Series["sreETManhattan"].Points.AddXY(t_i + 1, stat_MDH[t_i].Item2);
                    chartTiming.Series["sreETPatternsDB"].Points.AddXY(t_i + 1, stat_PDBH[t_i].Item2);
                }
            }));

            // Sort by memory
            stat_LCH.Sort((a, b) => a.Item3.CompareTo(b.Item3));
            stat_HE.Sort((a, b) => a.Item3.CompareTo(b.Item3));
            stat_MDH.Sort((a, b) => a.Item3.CompareTo(b.Item3));
            stat_PDBH.Sort((a, b) => a.Item3.CompareTo(b.Item3));

            chartMemoryAlloc.BeginInvoke(new Action(() => {
                for (int t_i = 0; t_i < testsCount; t_i++)
                {
                    chartMemoryAlloc.Series["sreETLC"].Points.AddXY(t_i + 1, stat_LCH[t_i].Item3);
                    chartMemoryAlloc.Series["sreETHamming"].Points.AddXY(t_i + 1, stat_HE[t_i].Item3);
                    chartMemoryAlloc.Series["sreETManhattan"].Points.AddXY(t_i + 1, stat_MDH[t_i].Item3);
                    chartMemoryAlloc.Series["sreETPatternsDB"].Points.AddXY(t_i + 1, stat_PDBH[t_i].Item3);
                }
            }));

            // Sort by nodes count
            stat_LCH.Sort((a, b) => a.Item4.CompareTo(b.Item4));
            stat_HE.Sort((a, b) => a.Item4.CompareTo(b.Item4));
            stat_MDH.Sort((a, b) => a.Item4.CompareTo(b.Item4));
            stat_PDBH.Sort((a, b) => a.Item4.CompareTo(b.Item4));

            chartNodesVisited.BeginInvoke(new Action(() => {
                for (int t_i = 0; t_i < testsCount; t_i++)
                {
                    chartNodesVisited.Series["sreETLC"].Points.AddXY(t_i + 1, stat_LCH[t_i].Item4);
                    chartNodesVisited.Series["sreETHamming"].Points.AddXY(t_i + 1, stat_HE[t_i].Item4);
                    chartNodesVisited.Series["sreETManhattan"].Points.AddXY(t_i + 1, stat_MDH[t_i].Item4);
                    chartNodesVisited.Series["sreETPatternsDB"].Points.AddXY(t_i + 1, stat_PDBH[t_i].Item4);
                }
            }));

            // General Statistic

            // Save stats into variables
            double stat_te_min = stat_HE.Average(x => x.Item2);
            double tmp = stat_MDH.Average(x => x.Item2);
            if (tmp < stat_te_min) stat_te_min = tmp;
            tmp = stat_LCH.Average(x => x.Item2);
            if (tmp < stat_te_min) stat_te_min = tmp;
            tmp = stat_PDBH.Average(x => x.Item2);
            if (tmp < stat_te_min) stat_te_min = tmp;

            double stat_te_max = stat_HE.Average(x => x.Item2);
            tmp = stat_MDH.Average(x => x.Item2);
            if (tmp > stat_te_max) stat_te_max = tmp;
            tmp = stat_LCH.Average(x => x.Item2);
            if (tmp > stat_te_max) stat_te_max = tmp;
            tmp = stat_PDBH.Average(x => x.Item2);
            if (tmp > stat_te_max) stat_te_max = tmp;

            double stat_ma_min = stat_HE.Average(x => x.Item3);
            tmp = stat_MDH.Average(x => x.Item3);
            if (tmp < stat_ma_min) stat_ma_min = tmp;
            tmp = stat_LCH.Average(x => x.Item3);
            if (tmp < stat_ma_min) stat_ma_min = tmp;
            tmp = stat_PDBH.Average(x => x.Item3);
            if (tmp < stat_ma_min) stat_ma_min = tmp;

            double stat_ma_max = stat_HE.Average(x => x.Item3);
            tmp = stat_MDH.Average(x => x.Item3);
            if (tmp > stat_ma_max) stat_ma_max = tmp;
            tmp = stat_LCH.Average(x => x.Item3);
            if (tmp > stat_ma_max) stat_ma_max = tmp;
            tmp = stat_PDBH.Average(x => x.Item3);
            if (tmp > stat_ma_max) stat_ma_max = tmp;

            double stat_vc_min = stat_HE.Average(x => x.Item4);
            tmp = stat_MDH.Average(x => x.Item4);
            if (tmp < stat_vc_min) stat_vc_min = tmp;
            tmp = stat_LCH.Average(x => x.Item4);
            if (tmp < stat_vc_min) stat_vc_min = tmp;
            tmp = stat_PDBH.Average(x => x.Item4);
            if (tmp < stat_vc_min) stat_vc_min = tmp;

            double stat_vc_max = stat_HE.Average(x => x.Item4);
            tmp = stat_MDH.Average(x => x.Item4);
            if (tmp > stat_vc_max) stat_vc_max = tmp;
            tmp = stat_LCH.Average(x => x.Item4);
            if (tmp > stat_vc_max) stat_vc_max = tmp;
            tmp = stat_PDBH.Average(x => x.Item4);
            if (tmp > stat_vc_max) stat_vc_max = tmp;

            chartStat.BeginInvoke(new Action(() => {
                chartStat.Series["sreETLC"].Points.AddXY("TE", stat_LCH.Average(x => x.Item2));
                chartStat.Series["sreETHamming"].Points.AddXY("TE", stat_HE.Average(x => x.Item2));
                chartStat.Series["sreETManhattan"].Points.AddXY("TE", stat_MDH.Average(x => x.Item2));
                chartStat.Series["sreETPatternsDB"].Points.AddXY("TE", stat_PDBH.Average(x => x.Item2));

                chartStat.Series["sreETLC"].Points.AddXY("MA", stat_LCH.Average(x => x.Item3));
                chartStat.Series["sreETHamming"].Points.AddXY("MA", stat_HE.Average(x => x.Item3));
                chartStat.Series["sreETManhattan"].Points.AddXY("MA", stat_MDH.Average(x => x.Item3));
                chartStat.Series["sreETPatternsDB"].Points.AddXY("MA", stat_PDBH.Average(x => x.Item3));

                chartStat.Series["sreETLC"].Points.AddXY("VN", stat_LCH.Average(x => x.Item4));
                chartStat.Series["sreETHamming"].Points.AddXY("VN", stat_HE.Average(x => x.Item4));
                chartStat.Series["sreETManhattan"].Points.AddXY("VN", stat_MDH.Average(x => x.Item4));
                chartStat.Series["sreETPatternsDB"].Points.AddXY("VN", stat_PDBH.Average(x => x.Item4));
            }));

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
              .AppendLine("<td align=center colspan=4><b>Heuristicic</b></td>")
              .AppendLine("</tr>")
              .AppendLine("<tr>")
              .AppendLine("<td align=center><b>Hamming (HH)</b></td>")
              .AppendLine("<td align=center><b>Manhattan distance (MD)</b></td>")
              .AppendLine("<td align=center><b>Linear conflict (LC)</b></td>")
              .AppendLine("<td align=center><b>Patterns database (PDB)</b></td>")
              .AppendLine("</tr>")
              .AppendLine("<tr>")
              .AppendLine("<td><b>Time elapsed<br>(TE)</b></td>")
              .AppendLine(GetHTMLColValue(stat_HE.Average(x => x.Item2), stat_te_min, stat_te_max))
              .AppendLine(GetHTMLColValue(stat_MDH.Average(x => x.Item2), stat_te_min, stat_te_max))
              .AppendLine(GetHTMLColValue(stat_LCH.Average(x => x.Item2), stat_te_min, stat_te_max))
              .AppendLine(GetHTMLColValue(stat_PDBH.Average(x => x.Item2), stat_te_min, stat_te_max))
              .AppendLine("</tr>")
              .AppendLine("<td><b>Memory allocated<br>(MA)</b></td>")
              .AppendLine(GetHTMLColValue(stat_HE.Average(x => x.Item3), stat_ma_min, stat_ma_max))
              .AppendLine(GetHTMLColValue(stat_MDH.Average(x => x.Item3), stat_ma_min, stat_ma_max))
              .AppendLine(GetHTMLColValue(stat_LCH.Average(x => x.Item3), stat_ma_min, stat_ma_max))
              .AppendLine(GetHTMLColValue(stat_PDBH.Average(x => x.Item3), stat_ma_min, stat_ma_max))
              .AppendLine("</tr>")
              .AppendLine("<td><b>Visited nodes count<br>(VN)</b></td>")
              .AppendLine(GetHTMLColValue(stat_HE.Average(x => x.Item4), stat_vc_min, stat_vc_max))
              .AppendLine(GetHTMLColValue(stat_MDH.Average(x => x.Item4), stat_vc_min, stat_vc_max))
              .AppendLine(GetHTMLColValue(stat_LCH.Average(x => x.Item4), stat_vc_min, stat_vc_max))
              .AppendLine(GetHTMLColValue(stat_PDBH.Average(x => x.Item4), stat_vc_min, stat_vc_max))
              .AppendLine("</tr>")
              .AppendLine("</table>")
              .AppendLine("</body>")
              .AppendLine("</html>");

            // Write HTML table to control
            webbStats.BeginInvoke(new Action(() => {
                webbStats.DocumentText = sb.ToString();
            }));
        }

        /// <summary>
        /// Create HTML-text for statistic column
        /// (if value = min column will green-highlight)
        /// (if value = max column will red-highlight)
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximim value</param>
        /// <returns>HTML-text for statistic column</returns>
        protected string GetHTMLColValue(double value, double min, double max)
        {
            string scol = "<td align=right";
            if (value == min) scol += " bgcolor=lime";
            if (value == max) scol += " bgcolor=red";
            scol += $">{value:F2}</td>";
            return scol;
        }

        /// <summary>
        /// Hamming heuristic on/off updating event
        /// </summary>
        /// <param name="sender">Active object which generate event</param>
        /// <param name="e">Event parameters</param>
        private void chkHammingHOnOff_CheckedChanged(object sender, EventArgs e)
        {
            chartTiming.Series["sreETHamming"].Enabled =
                chartMemoryAlloc.Series["sreETHamming"].Enabled =
                chartNodesVisited.Series["sreETHamming"].Enabled =
                chartStat.Series["sreETHamming"].Enabled =
                chkHammingHOnOff.Checked;
        }

        /// <summary>
        /// Manhattan Distance heuristic on/off updating event
        /// </summary>
        /// <param name="sender">Active object which generate event</param>
        /// <param name="e">Event parameters</param>
        private void chkManhattanDistanceHOnOff_CheckedChanged(object sender, EventArgs e)
        {
            chartTiming.Series["sreETManhattan"].Enabled =
                chartMemoryAlloc.Series["sreETManhattan"].Enabled =
                chartNodesVisited.Series["sreETManhattan"].Enabled =
                chartStat.Series["sreETManhattan"].Enabled =
                chkManhattanDistanceHOnOff.Checked;
        }

        /// <summary>
        /// Linear Conflict heuristic on/off updating event
        /// </summary>
        /// <param name="sender">Active object which generate event</param>
        /// <param name="e">Event parameters</param>
        private void chkLinearConflictHOnOff_CheckedChanged(object sender, EventArgs e)
        {
            chartTiming.Series["sreETLC"].Enabled =
                chartMemoryAlloc.Series["sreETLC"].Enabled =
                chartNodesVisited.Series["sreETLC"].Enabled =
                chartStat.Series["sreETLC"].Enabled =
                chkLinearConflictHOnOff.Checked;
        }

        /// <summary>
        /// Patterns DB heuristic on/off updating event
        /// </summary>
        /// <param name="sender">Active object which generate event</param>
        /// <param name="e">Event parameters</param>
        private void chkPatternsDBHOnOff_CheckedChanged(object sender, EventArgs e)
        {
            chartTiming.Series["sreETPatternsDB"].Enabled =
                chartMemoryAlloc.Series["sreETPatternsDB"].Enabled =
                chartNodesVisited.Series["sreETPatternsDB"].Enabled =
                chartStat.Series["sreETPatternsDB"].Enabled =
                chkPatternsDBHOnOff.Checked;
        }

        /// <summary>
        /// Update charts axes scaling
        /// </summary>
        /// <param name="sender">Active object which generate event</param>
        /// <param name="e">Event parameters</param>
        private void lnkRefreshCharts_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            chartStat.ChartAreas[0].RecalculateAxesScale();
            chartTiming.ChartAreas[0].RecalculateAxesScale();
            chartMemoryAlloc.ChartAreas[0].RecalculateAxesScale();
            chartNodesVisited.ChartAreas[0].RecalculateAxesScale();
        }
    }
}