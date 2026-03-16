namespace nPuzzle
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dgvField = new System.Windows.Forms.DataGridView();
            this.dgvGoal = new System.Windows.Forms.DataGridView();
            this.lblResults = new System.Windows.Forms.Label();
            this.btnPatternsDBCreate = new System.Windows.Forms.Button();
            this.splcMainContainer = new System.Windows.Forms.SplitContainer();
            this.grpInitialState = new System.Windows.Forms.GroupBox();
            this.grpFinalState = new System.Windows.Forms.GroupBox();
            this.mmuMainMenu = new System.Windows.Forms.MenuStrip();
            this.generateInitialStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gerate8PuzzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gerate15PuzzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heuristicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hammingHHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manhattanDisanceMDHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linearConflictLCHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patternsDatabasePDBHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iDAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expeimentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aSerieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aIDABASerieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plGeneration = new System.Windows.Forms.Panel();
            this.lblGeneratedStates = new System.Windows.Forms.Label();
            this.pbarInternal = new System.Windows.Forms.ProgressBar();
            this.pbarExternal = new System.Windows.Forms.ProgressBar();
            this.lblGeneratedStatesCap = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splcMainContainer)).BeginInit();
            this.splcMainContainer.Panel1.SuspendLayout();
            this.splcMainContainer.Panel2.SuspendLayout();
            this.splcMainContainer.SuspendLayout();
            this.grpInitialState.SuspendLayout();
            this.grpFinalState.SuspendLayout();
            this.mmuMainMenu.SuspendLayout();
            this.plGeneration.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvField
            // 
            this.dgvField.AllowUserToAddRows = false;
            this.dgvField.AllowUserToDeleteRows = false;
            this.dgvField.AllowUserToResizeColumns = false;
            this.dgvField.AllowUserToResizeRows = false;
            this.dgvField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvField.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvField.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvField.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvField.Location = new System.Drawing.Point(3, 19);
            this.dgvField.Name = "dgvField";
            this.dgvField.RowHeadersVisible = false;
            this.dgvField.RowHeadersWidth = 40;
            this.dgvField.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvField.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvField.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvField.Size = new System.Drawing.Size(301, 246);
            this.dgvField.TabIndex = 1;
            // 
            // dgvGoal
            // 
            this.dgvGoal.AllowUserToAddRows = false;
            this.dgvGoal.AllowUserToDeleteRows = false;
            this.dgvGoal.AllowUserToResizeColumns = false;
            this.dgvGoal.AllowUserToResizeRows = false;
            this.dgvGoal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGoal.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGoal.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGoal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGoal.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGoal.Location = new System.Drawing.Point(3, 19);
            this.dgvGoal.Name = "dgvGoal";
            this.dgvGoal.RowHeadersVisible = false;
            this.dgvGoal.RowHeadersWidth = 40;
            this.dgvGoal.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvGoal.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvGoal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvGoal.Size = new System.Drawing.Size(299, 246);
            this.dgvGoal.TabIndex = 4;
            // 
            // lblResults
            // 
            this.lblResults.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblResults.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblResults.Location = new System.Drawing.Point(12, 310);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(613, 99);
            this.lblResults.TabIndex = 8;
            // 
            // btnPatternsDBCreate
            // 
            this.btnPatternsDBCreate.Location = new System.Drawing.Point(21, 5);
            this.btnPatternsDBCreate.Name = "btnPatternsDBCreate";
            this.btnPatternsDBCreate.Size = new System.Drawing.Size(156, 24);
            this.btnPatternsDBCreate.TabIndex = 9;
            this.btnPatternsDBCreate.Text = "PatternsDBCreate";
            this.btnPatternsDBCreate.UseVisualStyleBackColor = true;
            this.btnPatternsDBCreate.Click += new System.EventHandler(this.btnPatternsDBCreate_Click);
            // 
            // splcMainContainer
            // 
            this.splcMainContainer.Location = new System.Drawing.Point(12, 39);
            this.splcMainContainer.Name = "splcMainContainer";
            // 
            // splcMainContainer.Panel1
            // 
            this.splcMainContainer.Panel1.Controls.Add(this.grpInitialState);
            // 
            // splcMainContainer.Panel2
            // 
            this.splcMainContainer.Panel2.Controls.Add(this.grpFinalState);
            this.splcMainContainer.Size = new System.Drawing.Size(616, 268);
            this.splcMainContainer.SplitterDistance = 307;
            this.splcMainContainer.TabIndex = 11;
            // 
            // grpInitialState
            // 
            this.grpInitialState.Controls.Add(this.dgvField);
            this.grpInitialState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInitialState.Location = new System.Drawing.Point(0, 0);
            this.grpInitialState.Name = "grpInitialState";
            this.grpInitialState.Size = new System.Drawing.Size(307, 268);
            this.grpInitialState.TabIndex = 0;
            this.grpInitialState.TabStop = false;
            this.grpInitialState.Text = "Initial State";
            // 
            // grpFinalState
            // 
            this.grpFinalState.Controls.Add(this.dgvGoal);
            this.grpFinalState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFinalState.Location = new System.Drawing.Point(0, 0);
            this.grpFinalState.Name = "grpFinalState";
            this.grpFinalState.Size = new System.Drawing.Size(305, 268);
            this.grpFinalState.TabIndex = 1;
            this.grpFinalState.TabStop = false;
            this.grpFinalState.Text = "Final State";
            // 
            // mmuMainMenu
            // 
            this.mmuMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateInitialStateToolStripMenuItem,
            this.heuristicToolStripMenuItem,
            this.solveToolStripMenuItem,
            this.expeimentsToolStripMenuItem});
            this.mmuMainMenu.Location = new System.Drawing.Point(0, 0);
            this.mmuMainMenu.Name = "mmuMainMenu";
            this.mmuMainMenu.Size = new System.Drawing.Size(633, 24);
            this.mmuMainMenu.TabIndex = 12;
            // 
            // generateInitialStateToolStripMenuItem
            // 
            this.generateInitialStateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gerate8PuzzleToolStripMenuItem,
            this.gerate15PuzzleToolStripMenuItem});
            this.generateInitialStateToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("generateInitialStateToolStripMenuItem.Image")));
            this.generateInitialStateToolStripMenuItem.Name = "generateInitialStateToolStripMenuItem";
            this.generateInitialStateToolStripMenuItem.Size = new System.Drawing.Size(143, 20);
            this.generateInitialStateToolStripMenuItem.Text = "Generate Initial State";
            // 
            // gerate8PuzzleToolStripMenuItem
            // 
            this.gerate8PuzzleToolStripMenuItem.Name = "gerate8PuzzleToolStripMenuItem";
            this.gerate8PuzzleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gerate8PuzzleToolStripMenuItem.Tag = "8";
            this.gerate8PuzzleToolStripMenuItem.Text = "Generate 8-Puzzle";
            this.gerate8PuzzleToolStripMenuItem.Click += new System.EventHandler(this.btnGenerateGame_Click);
            // 
            // gerate15PuzzleToolStripMenuItem
            // 
            this.gerate15PuzzleToolStripMenuItem.Name = "gerate15PuzzleToolStripMenuItem";
            this.gerate15PuzzleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gerate15PuzzleToolStripMenuItem.Tag = "15";
            this.gerate15PuzzleToolStripMenuItem.Text = "Generate 15-Puzzle";
            this.gerate15PuzzleToolStripMenuItem.Click += new System.EventHandler(this.btnGenerateGame_Click);
            // 
            // heuristicToolStripMenuItem
            // 
            this.heuristicToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hammingHHToolStripMenuItem,
            this.manhattanDisanceMDHToolStripMenuItem,
            this.linearConflictLCHToolStripMenuItem,
            this.patternsDatabasePDBHToolStripMenuItem});
            this.heuristicToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("heuristicToolStripMenuItem.Image")));
            this.heuristicToolStripMenuItem.Name = "heuristicToolStripMenuItem";
            this.heuristicToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.heuristicToolStripMenuItem.Text = "Heuristic";
            // 
            // hammingHHToolStripMenuItem
            // 
            this.hammingHHToolStripMenuItem.Checked = true;
            this.hammingHHToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hammingHHToolStripMenuItem.Name = "hammingHHToolStripMenuItem";
            this.hammingHHToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.hammingHHToolStripMenuItem.Text = "Hamming (HH)";
            this.hammingHHToolStripMenuItem.Click += new System.EventHandler(this.SelectHeuristic);
            // 
            // manhattanDisanceMDHToolStripMenuItem
            // 
            this.manhattanDisanceMDHToolStripMenuItem.Name = "manhattanDisanceMDHToolStripMenuItem";
            this.manhattanDisanceMDHToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.manhattanDisanceMDHToolStripMenuItem.Text = "Manhattan disance (MD)";
            this.manhattanDisanceMDHToolStripMenuItem.Click += new System.EventHandler(this.SelectHeuristic);
            // 
            // linearConflictLCHToolStripMenuItem
            // 
            this.linearConflictLCHToolStripMenuItem.Name = "linearConflictLCHToolStripMenuItem";
            this.linearConflictLCHToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.linearConflictLCHToolStripMenuItem.Text = "Linear Conflict (LC)";
            this.linearConflictLCHToolStripMenuItem.Click += new System.EventHandler(this.SelectHeuristic);
            // 
            // patternsDatabasePDBHToolStripMenuItem
            // 
            this.patternsDatabasePDBHToolStripMenuItem.Name = "patternsDatabasePDBHToolStripMenuItem";
            this.patternsDatabasePDBHToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.patternsDatabasePDBHToolStripMenuItem.Text = "Patterns database (PDB)";
            this.patternsDatabasePDBHToolStripMenuItem.Click += new System.EventHandler(this.SelectHeuristic);
            // 
            // solveToolStripMenuItem
            // 
            this.solveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem,
            this.iDAToolStripMenuItem,
            this.bAToolStripMenuItem});
            this.solveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("solveToolStripMenuItem.Image")));
            this.solveToolStripMenuItem.Name = "solveToolStripMenuItem";
            this.solveToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.solveToolStripMenuItem.Text = "Solve";
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aToolStripMenuItem.Tag = "0";
            this.aToolStripMenuItem.Text = "A*";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // iDAToolStripMenuItem
            // 
            this.iDAToolStripMenuItem.Name = "iDAToolStripMenuItem";
            this.iDAToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.iDAToolStripMenuItem.Tag = "1";
            this.iDAToolStripMenuItem.Text = "IDA*";
            this.iDAToolStripMenuItem.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // bAToolStripMenuItem
            // 
            this.bAToolStripMenuItem.Name = "bAToolStripMenuItem";
            this.bAToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bAToolStripMenuItem.Tag = "2";
            this.bAToolStripMenuItem.Text = "BA*";
            this.bAToolStripMenuItem.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // expeimentsToolStripMenuItem
            // 
            this.expeimentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aSerieToolStripMenuItem,
            this.aIDABASerieToolStripMenuItem});
            this.expeimentsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("expeimentsToolStripMenuItem.Image")));
            this.expeimentsToolStripMenuItem.Name = "expeimentsToolStripMenuItem";
            this.expeimentsToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.expeimentsToolStripMenuItem.Text = "Expeiments";
            // 
            // aSerieToolStripMenuItem
            // 
            this.aSerieToolStripMenuItem.Name = "aSerieToolStripMenuItem";
            this.aSerieToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.aSerieToolStripMenuItem.Text = "A* - serie";
            this.aSerieToolStripMenuItem.Click += new System.EventHandler(this.btnComplexTestConfigure_Click);
            // 
            // aIDABASerieToolStripMenuItem
            // 
            this.aIDABASerieToolStripMenuItem.Name = "aIDABASerieToolStripMenuItem";
            this.aIDABASerieToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.aIDABASerieToolStripMenuItem.Text = "A* / IDA* / BA* - serie";
            this.aIDABASerieToolStripMenuItem.Click += new System.EventHandler(this.aIDABASerieToolStripMenuItem_Click);
            // 
            // plGeneration
            // 
            this.plGeneration.Controls.Add(this.lblGeneratedStates);
            this.plGeneration.Controls.Add(this.pbarInternal);
            this.plGeneration.Controls.Add(this.pbarExternal);
            this.plGeneration.Controls.Add(this.lblGeneratedStatesCap);
            this.plGeneration.Controls.Add(this.btnPatternsDBCreate);
            this.plGeneration.Location = new System.Drawing.Point(431, 310);
            this.plGeneration.Name = "plGeneration";
            this.plGeneration.Size = new System.Drawing.Size(193, 106);
            this.plGeneration.TabIndex = 13;
            this.plGeneration.Visible = false;
            // 
            // lblGeneratedStates
            // 
            this.lblGeneratedStates.AutoSize = true;
            this.lblGeneratedStates.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblGeneratedStates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblGeneratedStates.Location = new System.Drawing.Point(109, 36);
            this.lblGeneratedStates.Name = "lblGeneratedStates";
            this.lblGeneratedStates.Size = new System.Drawing.Size(18, 18);
            this.lblGeneratedStates.TabIndex = 13;
            this.lblGeneratedStates.Text = "0";
            // 
            // pbarInternal
            // 
            this.pbarInternal.Location = new System.Drawing.Point(3, 81);
            this.pbarInternal.Name = "pbarInternal";
            this.pbarInternal.Size = new System.Drawing.Size(187, 19);
            this.pbarInternal.TabIndex = 12;
            // 
            // pbarExternal
            // 
            this.pbarExternal.Location = new System.Drawing.Point(3, 58);
            this.pbarExternal.Name = "pbarExternal";
            this.pbarExternal.Size = new System.Drawing.Size(187, 19);
            this.pbarExternal.TabIndex = 11;
            // 
            // lblGeneratedStatesCap
            // 
            this.lblGeneratedStatesCap.AutoSize = true;
            this.lblGeneratedStatesCap.Location = new System.Drawing.Point(5, 37);
            this.lblGeneratedStatesCap.Name = "lblGeneratedStatesCap";
            this.lblGeneratedStatesCap.Size = new System.Drawing.Size(103, 15);
            this.lblGeneratedStatesCap.TabIndex = 10;
            this.lblGeneratedStatesCap.Text = "Generated states: ";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(633, 419);
            this.Controls.Add(this.plGeneration);
            this.Controls.Add(this.splcMainContainer);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.mmuMainMenu);
            this.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mmuMainMenu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "nPuzzle Solver (Technische Universität Berlin)";
            ((System.ComponentModel.ISupportInitialize)(this.dgvField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoal)).EndInit();
            this.splcMainContainer.Panel1.ResumeLayout(false);
            this.splcMainContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splcMainContainer)).EndInit();
            this.splcMainContainer.ResumeLayout(false);
            this.grpInitialState.ResumeLayout(false);
            this.grpFinalState.ResumeLayout(false);
            this.mmuMainMenu.ResumeLayout(false);
            this.mmuMainMenu.PerformLayout();
            this.plGeneration.ResumeLayout(false);
            this.plGeneration.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvField;
        private System.Windows.Forms.DataGridView dgvGoal;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.Button btnPatternsDBCreate;
        private System.Windows.Forms.SplitContainer splcMainContainer;
        private System.Windows.Forms.GroupBox grpInitialState;
        private System.Windows.Forms.GroupBox grpFinalState;
        private System.Windows.Forms.MenuStrip mmuMainMenu;
        private System.Windows.Forms.ToolStripMenuItem generateInitialStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gerate8PuzzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gerate15PuzzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heuristicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hammingHHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manhattanDisanceMDHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linearConflictLCHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patternsDatabasePDBHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iDAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expeimentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aSerieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aIDABASerieToolStripMenuItem;
        private System.Windows.Forms.Panel plGeneration;
        private System.Windows.Forms.Label lblGeneratedStates;
        private System.Windows.Forms.ProgressBar pbarInternal;
        private System.Windows.Forms.ProgressBar pbarExternal;
        private System.Windows.Forms.Label lblGeneratedStatesCap;
    }
}

