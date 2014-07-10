namespace PocketMoney.ChildApp
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboLoggedUser = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTasks = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listTasksYesterday = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabGoals = new System.Windows.Forms.TabPage();
            this.tabGoodWorks = new System.Windows.Forms.TabPage();
            this.labelPoints = new System.Windows.Forms.Label();
            this.listTasksToday = new System.Windows.Forms.ListView();
            this.listTasksTomorrow = new System.Windows.Forms.ListView();
            this.listTasksFloating = new System.Windows.Forms.ListView();
            this.tabControl1.SuspendLayout();
            this.tabTasks.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "You have logged as ";
            // 
            // comboLoggedUser
            // 
            this.comboLoggedUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLoggedUser.FormattingEnabled = true;
            this.comboLoggedUser.Location = new System.Drawing.Point(115, 2);
            this.comboLoggedUser.Name = "comboLoggedUser";
            this.comboLoggedUser.Size = new System.Drawing.Size(142, 21);
            this.comboLoggedUser.TabIndex = 1;
            this.comboLoggedUser.SelectedIndexChanged += new System.EventHandler(this.comboLoggedUser_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabTasks);
            this.tabControl1.Controls.Add(this.tabGoals);
            this.tabControl1.Controls.Add(this.tabGoodWorks);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(397, 428);
            this.tabControl1.TabIndex = 2;
            // 
            // tabTasks
            // 
            this.tabTasks.Controls.Add(this.tabControl2);
            this.tabTasks.Location = new System.Drawing.Point(4, 41);
            this.tabTasks.Name = "tabTasks";
            this.tabTasks.Padding = new System.Windows.Forms.Padding(3);
            this.tabTasks.Size = new System.Drawing.Size(389, 383);
            this.tabTasks.TabIndex = 0;
            this.tabTasks.Text = "  Tasks  ";
            this.tabTasks.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(383, 377);
            this.tabControl2.TabIndex = 0;
            this.tabControl2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl2_DrawItem);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.listTasksYesterday);
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(375, 340);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Yesterday";
            // 
            // listTasksYesterday
            // 
            this.listTasksYesterday.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listTasksYesterday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTasksYesterday.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listTasksYesterday.HoverSelection = true;
            this.listTasksYesterday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listTasksYesterday.Location = new System.Drawing.Point(3, 3);
            this.listTasksYesterday.MultiSelect = false;
            this.listTasksYesterday.Name = "listTasksYesterday";
            this.listTasksYesterday.Size = new System.Drawing.Size(369, 334);
            this.listTasksYesterday.TabIndex = 0;
            this.listTasksYesterday.UseCompatibleStateImageBehavior = false;
            this.listTasksYesterday.View = System.Windows.Forms.View.List;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.listTasksToday);
            this.tabPage2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(375, 340);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = " Today ";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.listTasksTomorrow);
            this.tabPage3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage3.Location = new System.Drawing.Point(4, 33);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(375, 340);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Tomorrow";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Transparent;
            this.tabPage4.Controls.Add(this.listTasksFloating);
            this.tabPage4.Location = new System.Drawing.Point(4, 33);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(375, 340);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Floating";
            // 
            // tabGoals
            // 
            this.tabGoals.Location = new System.Drawing.Point(4, 41);
            this.tabGoals.Name = "tabGoals";
            this.tabGoals.Padding = new System.Windows.Forms.Padding(3);
            this.tabGoals.Size = new System.Drawing.Size(389, 383);
            this.tabGoals.TabIndex = 1;
            this.tabGoals.Text = "Goals";
            this.tabGoals.UseVisualStyleBackColor = true;
            // 
            // tabGoodWorks
            // 
            this.tabGoodWorks.Location = new System.Drawing.Point(4, 41);
            this.tabGoodWorks.Name = "tabGoodWorks";
            this.tabGoodWorks.Size = new System.Drawing.Size(389, 383);
            this.tabGoodWorks.TabIndex = 2;
            this.tabGoodWorks.Text = "Good Works";
            this.tabGoodWorks.UseVisualStyleBackColor = true;
            // 
            // labelPoints
            // 
            this.labelPoints.AutoSize = true;
            this.labelPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPoints.Location = new System.Drawing.Point(263, 5);
            this.labelPoints.Name = "labelPoints";
            this.labelPoints.Size = new System.Drawing.Size(0, 17);
            this.labelPoints.TabIndex = 3;
            // 
            // listTasksToday
            // 
            this.listTasksToday.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listTasksToday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTasksToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listTasksToday.HoverSelection = true;
            this.listTasksToday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listTasksToday.Location = new System.Drawing.Point(3, 3);
            this.listTasksToday.MultiSelect = false;
            this.listTasksToday.Name = "listTasksToday";
            this.listTasksToday.Size = new System.Drawing.Size(369, 334);
            this.listTasksToday.TabIndex = 1;
            this.listTasksToday.UseCompatibleStateImageBehavior = false;
            this.listTasksToday.View = System.Windows.Forms.View.List;
            // 
            // listTasksTomorrow
            // 
            this.listTasksTomorrow.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listTasksTomorrow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTasksTomorrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listTasksTomorrow.HoverSelection = true;
            this.listTasksTomorrow.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listTasksTomorrow.Location = new System.Drawing.Point(0, 0);
            this.listTasksTomorrow.MultiSelect = false;
            this.listTasksTomorrow.Name = "listTasksTomorrow";
            this.listTasksTomorrow.Size = new System.Drawing.Size(375, 340);
            this.listTasksTomorrow.TabIndex = 2;
            this.listTasksTomorrow.UseCompatibleStateImageBehavior = false;
            this.listTasksTomorrow.View = System.Windows.Forms.View.List;
            // 
            // listTasksFloating
            // 
            this.listTasksFloating.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listTasksFloating.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTasksFloating.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listTasksFloating.HoverSelection = true;
            this.listTasksFloating.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listTasksFloating.Location = new System.Drawing.Point(0, 0);
            this.listTasksFloating.MultiSelect = false;
            this.listTasksFloating.Name = "listTasksFloating";
            this.listTasksFloating.Size = new System.Drawing.Size(375, 340);
            this.listTasksFloating.TabIndex = 2;
            this.listTasksFloating.UseCompatibleStateImageBehavior = false;
            this.listTasksFloating.View = System.Windows.Forms.View.List;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 456);
            this.Controls.Add(this.labelPoints);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.comboLoggedUser);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Child App";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabTasks.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboLoggedUser;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTasks;
        private System.Windows.Forms.TabPage tabGoals;
        private System.Windows.Forms.TabPage tabGoodWorks;
        private System.Windows.Forms.Label labelPoints;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listTasksYesterday;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView listTasksToday;
        private System.Windows.Forms.ListView listTasksTomorrow;
        private System.Windows.Forms.ListView listTasksFloating;
    }
}

