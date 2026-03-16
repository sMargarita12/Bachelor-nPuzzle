namespace nPuzzle
{
    partial class frmComplexTestMethods
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComplexTestMethods));
            this.lblTestsCount = new System.Windows.Forms.Label();
            this.nudTestsCount = new System.Windows.Forms.NumericUpDown();
            this.lnkRunTests = new System.Windows.Forms.LinkLabel();
            this.pbarProgress = new System.Windows.Forms.ProgressBar();
            this.splcCommonContainer = new System.Windows.Forms.SplitContainer();
            this.webbStats = new System.Windows.Forms.WebBrowser();
            this.chartStat = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rbtBoard_3x3 = new System.Windows.Forms.RadioButton();
            this.rbtBoard_4x4 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestsCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splcCommonContainer)).BeginInit();
            this.splcCommonContainer.Panel1.SuspendLayout();
            this.splcCommonContainer.Panel2.SuspendLayout();
            this.splcCommonContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStat)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTestsCount
            // 
            this.lblTestsCount.AutoSize = true;
            this.lblTestsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTestsCount.Location = new System.Drawing.Point(7, 7);
            this.lblTestsCount.Name = "lblTestsCount";
            this.lblTestsCount.Size = new System.Drawing.Size(78, 13);
            this.lblTestsCount.TabIndex = 11;
            this.lblTestsCount.Text = "Tests count:";
            // 
            // nudTestsCount
            // 
            this.nudTestsCount.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudTestsCount.Location = new System.Drawing.Point(90, 4);
            this.nudTestsCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudTestsCount.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudTestsCount.Name = "nudTestsCount";
            this.nudTestsCount.Size = new System.Drawing.Size(66, 20);
            this.nudTestsCount.TabIndex = 10;
            this.nudTestsCount.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lnkRunTests
            // 
            this.lnkRunTests.AutoSize = true;
            this.lnkRunTests.Location = new System.Drawing.Point(312, 7);
            this.lnkRunTests.Name = "lnkRunTests";
            this.lnkRunTests.Size = new System.Drawing.Size(52, 13);
            this.lnkRunTests.TabIndex = 9;
            this.lnkRunTests.TabStop = true;
            this.lnkRunTests.Text = "Run tests";
            this.lnkRunTests.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRunTests_LinkClicked);
            // 
            // pbarProgress
            // 
            this.pbarProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbarProgress.Location = new System.Drawing.Point(0, 438);
            this.pbarProgress.Name = "pbarProgress";
            this.pbarProgress.Size = new System.Drawing.Size(584, 23);
            this.pbarProgress.TabIndex = 12;
            // 
            // splcCommonContainer
            // 
            this.splcCommonContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splcCommonContainer.Location = new System.Drawing.Point(0, 32);
            this.splcCommonContainer.Name = "splcCommonContainer";
            this.splcCommonContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splcCommonContainer.Panel1
            // 
            this.splcCommonContainer.Panel1.Controls.Add(this.webbStats);
            // 
            // splcCommonContainer.Panel2
            // 
            this.splcCommonContainer.Panel2.Controls.Add(this.chartStat);
            this.splcCommonContainer.Size = new System.Drawing.Size(584, 400);
            this.splcCommonContainer.SplitterDistance = 157;
            this.splcCommonContainer.TabIndex = 13;
            // 
            // webbStats
            // 
            this.webbStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webbStats.Location = new System.Drawing.Point(0, 0);
            this.webbStats.MinimumSize = new System.Drawing.Size(20, 20);
            this.webbStats.Name = "webbStats";
            this.webbStats.Size = new System.Drawing.Size(584, 157);
            this.webbStats.TabIndex = 1;
            // 
            // chartStat
            // 
            chartArea1.AxisX.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.Title = "t, sec.";
            chartArea1.AxisY2.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY2.MinorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY2.MinorTickMark.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY2.Title = "nodes";
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.chartStat.ChartAreas.Add(chartArea1);
            this.chartStat.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this.chartStat.Legends.Add(legend1);
            this.chartStat.Location = new System.Drawing.Point(0, 0);
            this.chartStat.Name = "chartStat";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.Blue;
            series1.IsValueShownAsLabel = true;
            series1.LabelFormat = "F2";
            series1.Legend = "Legend1";
            series1.LegendText = "Duration";
            series1.Name = "sreDuration";
            series1.SmartLabelStyle.CalloutStyle = System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle.None;
            series1.SmartLabelStyle.MinMovingDistance = 5D;
            series2.ChartArea = "ChartArea1";
            series2.Color = System.Drawing.Color.ForestGreen;
            series2.Label = "#VAL";
            series2.LabelFormat = "F2";
            series2.Legend = "Legend1";
            series2.LegendText = "Visited nodes count";
            series2.Name = "sreNodesCount";
            series2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.chartStat.Series.Add(series1);
            this.chartStat.Series.Add(series2);
            this.chartStat.Size = new System.Drawing.Size(584, 239);
            this.chartStat.TabIndex = 4;
            title1.Name = "Title1";
            title1.Text = "Statistic values of experiment";
            this.chartStat.Titles.Add(title1);
            // 
            // rbtBoard_3x3
            // 
            this.rbtBoard_3x3.AutoSize = true;
            this.rbtBoard_3x3.Checked = true;
            this.rbtBoard_3x3.Location = new System.Drawing.Point(162, 5);
            this.rbtBoard_3x3.Name = "rbtBoard_3x3";
            this.rbtBoard_3x3.Size = new System.Drawing.Size(65, 17);
            this.rbtBoard_3x3.TabIndex = 14;
            this.rbtBoard_3x3.TabStop = true;
            this.rbtBoard_3x3.Text = "8-Puzzle";
            this.rbtBoard_3x3.UseVisualStyleBackColor = true;
            // 
            // rbtBoard_4x4
            // 
            this.rbtBoard_4x4.AutoSize = true;
            this.rbtBoard_4x4.Location = new System.Drawing.Point(233, 5);
            this.rbtBoard_4x4.Name = "rbtBoard_4x4";
            this.rbtBoard_4x4.Size = new System.Drawing.Size(71, 17);
            this.rbtBoard_4x4.TabIndex = 15;
            this.rbtBoard_4x4.Text = "15-Puzzle";
            this.rbtBoard_4x4.UseVisualStyleBackColor = true;
            // 
            // frmComplexTestMethods
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.rbtBoard_4x4);
            this.Controls.Add(this.rbtBoard_3x3);
            this.Controls.Add(this.splcCommonContainer);
            this.Controls.Add(this.pbarProgress);
            this.Controls.Add(this.lblTestsCount);
            this.Controls.Add(this.nudTestsCount);
            this.Controls.Add(this.lnkRunTests);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "frmComplexTestMethods";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Methods ComplexTest";
            ((System.ComponentModel.ISupportInitialize)(this.nudTestsCount)).EndInit();
            this.splcCommonContainer.Panel1.ResumeLayout(false);
            this.splcCommonContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splcCommonContainer)).EndInit();
            this.splcCommonContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartStat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTestsCount;
        private System.Windows.Forms.NumericUpDown nudTestsCount;
        private System.Windows.Forms.LinkLabel lnkRunTests;
        private System.Windows.Forms.ProgressBar pbarProgress;
        private System.Windows.Forms.SplitContainer splcCommonContainer;
        private System.Windows.Forms.WebBrowser webbStats;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStat;
        private System.Windows.Forms.RadioButton rbtBoard_3x3;
        private System.Windows.Forms.RadioButton rbtBoard_4x4;
    }
}