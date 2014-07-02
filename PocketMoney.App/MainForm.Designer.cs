namespace PocketMoney.ParentApp
{
    partial class MainForm
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabTask = new System.Windows.Forms.TabPage();
            this.buttonAddTask = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listTasks = new System.Windows.Forms.ListView();
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colResponsibility = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPoints = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabGoal = new System.Windows.Forms.TabPage();
            this.buttonGoal = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listGoals = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabUser = new System.Windows.Forms.TabPage();
            this.tabFamily = new System.Windows.Forms.TabPage();
            this.tabAttainment = new System.Windows.Forms.TabPage();
            this.tabMain.SuspendLayout();
            this.tabTask.SuspendLayout();
            this.tabGoal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabTask);
            this.tabMain.Controls.Add(this.tabGoal);
            this.tabMain.Controls.Add(this.tabAttainment);
            this.tabMain.Controls.Add(this.tabUser);
            this.tabMain.Controls.Add(this.tabFamily);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(766, 472);
            this.tabMain.TabIndex = 0;
            // 
            // tabTask
            // 
            this.tabTask.Controls.Add(this.buttonAddTask);
            this.tabTask.Controls.Add(this.label1);
            this.tabTask.Controls.Add(this.listTasks);
            this.tabTask.Location = new System.Drawing.Point(4, 42);
            this.tabTask.Name = "tabTask";
            this.tabTask.Padding = new System.Windows.Forms.Padding(3);
            this.tabTask.Size = new System.Drawing.Size(758, 426);
            this.tabTask.TabIndex = 0;
            this.tabTask.Text = "Tasks";
            this.tabTask.UseVisualStyleBackColor = true;
            // 
            // buttonAddTask
            // 
            this.buttonAddTask.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddTask.Location = new System.Drawing.Point(511, 7);
            this.buttonAddTask.Name = "buttonAddTask";
            this.buttonAddTask.Size = new System.Drawing.Size(239, 33);
            this.buttonAddTask.TabIndex = 3;
            this.buttonAddTask.Text = "Add Task";
            this.buttonAddTask.UseVisualStyleBackColor = true;
            this.buttonAddTask.Click += new System.EventHandler(this.buttonAddTask_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 33);
            this.label1.TabIndex = 2;
            this.label1.Text = "All Tasks ";
            // 
            // listTasks
            // 
            this.listTasks.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTitle,
            this.colResponsibility,
            this.colPoints});
            this.listTasks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listTasks.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listTasks.FullRowSelect = true;
            this.listTasks.GridLines = true;
            this.listTasks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listTasks.Location = new System.Drawing.Point(3, 43);
            this.listTasks.MultiSelect = false;
            this.listTasks.Name = "listTasks";
            this.listTasks.RightToLeftLayout = true;
            this.listTasks.Size = new System.Drawing.Size(752, 380);
            this.listTasks.TabIndex = 1;
            this.listTasks.UseCompatibleStateImageBehavior = false;
            this.listTasks.View = System.Windows.Forms.View.Details;
            this.listTasks.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listTasks_MouseClick);
            // 
            // colTitle
            // 
            this.colTitle.Text = "Title";
            this.colTitle.Width = 350;
            // 
            // colResponsibility
            // 
            this.colResponsibility.Text = "Responsibility";
            this.colResponsibility.Width = 250;
            // 
            // colPoints
            // 
            this.colPoints.Text = "Reward";
            this.colPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colPoints.Width = 148;
            // 
            // tabGoal
            // 
            this.tabGoal.Controls.Add(this.buttonGoal);
            this.tabGoal.Controls.Add(this.label2);
            this.tabGoal.Controls.Add(this.listGoals);
            this.tabGoal.Location = new System.Drawing.Point(4, 42);
            this.tabGoal.Name = "tabGoal";
            this.tabGoal.Size = new System.Drawing.Size(758, 426);
            this.tabGoal.TabIndex = 1;
            this.tabGoal.Text = "Goals";
            this.tabGoal.UseVisualStyleBackColor = true;
            // 
            // buttonGoal
            // 
            this.buttonGoal.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonGoal.Location = new System.Drawing.Point(511, 4);
            this.buttonGoal.Name = "buttonGoal";
            this.buttonGoal.Size = new System.Drawing.Size(239, 36);
            this.buttonGoal.TabIndex = 6;
            this.buttonGoal.Text = "Add Goal";
            this.buttonGoal.UseVisualStyleBackColor = true;
            this.buttonGoal.Click += new System.EventHandler(this.buttonGoal_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 33);
            this.label2.TabIndex = 5;
            this.label2.Text = "All Goals";
            // 
            // listGoals
            // 
            this.listGoals.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listGoals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listGoals.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listGoals.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listGoals.FullRowSelect = true;
            this.listGoals.GridLines = true;
            this.listGoals.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listGoals.Location = new System.Drawing.Point(0, 46);
            this.listGoals.MultiSelect = false;
            this.listGoals.Name = "listGoals";
            this.listGoals.RightToLeftLayout = true;
            this.listGoals.Size = new System.Drawing.Size(758, 380);
            this.listGoals.TabIndex = 4;
            this.listGoals.UseCompatibleStateImageBehavior = false;
            this.listGoals.View = System.Windows.Forms.View.Details;
            this.listGoals.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listGoals_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Title";
            this.columnHeader1.Width = 350;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Responsibility";
            this.columnHeader2.Width = 250;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Reward";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 148;
            // 
            // tabUser
            // 
            this.tabUser.Location = new System.Drawing.Point(4, 42);
            this.tabUser.Name = "tabUser";
            this.tabUser.Size = new System.Drawing.Size(758, 426);
            this.tabUser.TabIndex = 2;
            this.tabUser.Text = "Users";
            this.tabUser.UseVisualStyleBackColor = true;
            // 
            // tabFamily
            // 
            this.tabFamily.Location = new System.Drawing.Point(4, 42);
            this.tabFamily.Name = "tabFamily";
            this.tabFamily.Size = new System.Drawing.Size(758, 426);
            this.tabFamily.TabIndex = 3;
            this.tabFamily.Text = "Settings";
            this.tabFamily.UseVisualStyleBackColor = true;
            // 
            // tabAttainment
            // 
            this.tabAttainment.Location = new System.Drawing.Point(4, 42);
            this.tabAttainment.Name = "tabAttainment";
            this.tabAttainment.Size = new System.Drawing.Size(758, 426);
            this.tabAttainment.TabIndex = 4;
            this.tabAttainment.Text = "Attainments";
            this.tabAttainment.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 472);
            this.Controls.Add(this.tabMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Parent App";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabMain.ResumeLayout(false);
            this.tabTask.ResumeLayout(false);
            this.tabTask.PerformLayout();
            this.tabGoal.ResumeLayout(false);
            this.tabGoal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabTask;
        private System.Windows.Forms.TabPage tabGoal;
        private System.Windows.Forms.TabPage tabUser;
        private System.Windows.Forms.TabPage tabFamily;
        private System.Windows.Forms.ListView listTasks;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAddTask;
        private System.Windows.Forms.ColumnHeader colResponsibility;
        private System.Windows.Forms.ColumnHeader colPoints;
        private System.Windows.Forms.Button buttonGoal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listGoals;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TabPage tabAttainment;

    }
}