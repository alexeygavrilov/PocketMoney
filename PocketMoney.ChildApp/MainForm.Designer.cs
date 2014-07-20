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
            this.mainControl = new System.Windows.Forms.TabControl();
            this.tabTasks = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listTasksYesterday = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listTasksToday = new System.Windows.Forms.ListView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listTasksTomorrow = new System.Windows.Forms.ListView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listTasksFloating = new System.Windows.Forms.ListView();
            this.tabGoals = new System.Windows.Forms.TabPage();
            this.listGoals = new System.Windows.Forms.ListView();
            this.tabGoodWorks = new System.Windows.Forms.TabPage();
            this.listGoodWorks = new System.Windows.Forms.ListView();
            this.buttonAddGoodDeed = new System.Windows.Forms.Button();
            this.labelPoints = new System.Windows.Forms.Label();
            this.deedControl = new PocketMoney.ChildApp.GoodDeedControl();
            this.taskControl = new PocketMoney.ChildApp.TaskControl();
            this.shoppingControl = new PocketMoney.ChildApp.ShoppingControl();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.mainControl.SuspendLayout();
            this.tabTasks.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabGoals.SuspendLayout();
            this.tabGoodWorks.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "You logged as ";
            // 
            // comboLoggedUser
            // 
            this.comboLoggedUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLoggedUser.FormattingEnabled = true;
            this.comboLoggedUser.Location = new System.Drawing.Point(161, 2);
            this.comboLoggedUser.Name = "comboLoggedUser";
            this.comboLoggedUser.Size = new System.Drawing.Size(142, 21);
            this.comboLoggedUser.TabIndex = 1;
            this.comboLoggedUser.SelectedIndexChanged += new System.EventHandler(this.comboLoggedUser_SelectedIndexChanged);
            // 
            // mainControl
            // 
            this.mainControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.mainControl.Controls.Add(this.tabTasks);
            this.mainControl.Controls.Add(this.tabGoals);
            this.mainControl.Controls.Add(this.tabGoodWorks);
            this.mainControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mainControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainControl.Location = new System.Drawing.Point(0, 28);
            this.mainControl.Name = "mainControl";
            this.mainControl.SelectedIndex = 0;
            this.mainControl.Size = new System.Drawing.Size(397, 428);
            this.mainControl.TabIndex = 2;
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
            this.listTasksYesterday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listTasksYesterday.Location = new System.Drawing.Point(3, 3);
            this.listTasksYesterday.MultiSelect = false;
            this.listTasksYesterday.Name = "listTasksYesterday";
            this.listTasksYesterday.Size = new System.Drawing.Size(369, 334);
            this.listTasksYesterday.TabIndex = 0;
            this.listTasksYesterday.UseCompatibleStateImageBehavior = false;
            this.listTasksYesterday.View = System.Windows.Forms.View.Tile;
            this.listTasksYesterday.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listTasksYesterday_MouseClick);
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
            // listTasksToday
            // 
            this.listTasksToday.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listTasksToday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTasksToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listTasksToday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listTasksToday.Location = new System.Drawing.Point(3, 3);
            this.listTasksToday.MultiSelect = false;
            this.listTasksToday.Name = "listTasksToday";
            this.listTasksToday.Size = new System.Drawing.Size(369, 334);
            this.listTasksToday.TabIndex = 1;
            this.listTasksToday.UseCompatibleStateImageBehavior = false;
            this.listTasksToday.View = System.Windows.Forms.View.Tile;
            this.listTasksToday.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listTasksToday_MouseClick);
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
            // listTasksTomorrow
            // 
            this.listTasksTomorrow.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listTasksTomorrow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTasksTomorrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listTasksTomorrow.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listTasksTomorrow.Location = new System.Drawing.Point(0, 0);
            this.listTasksTomorrow.MultiSelect = false;
            this.listTasksTomorrow.Name = "listTasksTomorrow";
            this.listTasksTomorrow.Size = new System.Drawing.Size(375, 340);
            this.listTasksTomorrow.TabIndex = 2;
            this.listTasksTomorrow.UseCompatibleStateImageBehavior = false;
            this.listTasksTomorrow.View = System.Windows.Forms.View.Tile;
            this.listTasksTomorrow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listTasksTomorrow_MouseClick);
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
            this.listTasksFloating.View = System.Windows.Forms.View.Tile;
            this.listTasksFloating.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listTasksFloating_MouseClick);
            // 
            // tabGoals
            // 
            this.tabGoals.Controls.Add(this.listGoals);
            this.tabGoals.Location = new System.Drawing.Point(4, 41);
            this.tabGoals.Name = "tabGoals";
            this.tabGoals.Padding = new System.Windows.Forms.Padding(3);
            this.tabGoals.Size = new System.Drawing.Size(389, 383);
            this.tabGoals.TabIndex = 1;
            this.tabGoals.Text = "Goals";
            this.tabGoals.UseVisualStyleBackColor = true;
            // 
            // listGoals
            // 
            this.listGoals.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listGoals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listGoals.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listGoals.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listGoals.Location = new System.Drawing.Point(3, 3);
            this.listGoals.MultiSelect = false;
            this.listGoals.Name = "listGoals";
            this.listGoals.Size = new System.Drawing.Size(383, 377);
            this.listGoals.TabIndex = 1;
            this.listGoals.UseCompatibleStateImageBehavior = false;
            this.listGoals.View = System.Windows.Forms.View.Tile;
            this.listGoals.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listGoals_MouseClick);
            // 
            // tabGoodWorks
            // 
            this.tabGoodWorks.Controls.Add(this.listGoodWorks);
            this.tabGoodWorks.Controls.Add(this.buttonAddGoodDeed);
            this.tabGoodWorks.Location = new System.Drawing.Point(4, 41);
            this.tabGoodWorks.Name = "tabGoodWorks";
            this.tabGoodWorks.Size = new System.Drawing.Size(389, 383);
            this.tabGoodWorks.TabIndex = 2;
            this.tabGoodWorks.Text = "Good Works";
            this.tabGoodWorks.UseVisualStyleBackColor = true;
            // 
            // listGoodWorks
            // 
            this.listGoodWorks.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listGoodWorks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listGoodWorks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listGoodWorks.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listGoodWorks.LabelWrap = false;
            this.listGoodWorks.Location = new System.Drawing.Point(0, 47);
            this.listGoodWorks.MultiSelect = false;
            this.listGoodWorks.Name = "listGoodWorks";
            this.listGoodWorks.Size = new System.Drawing.Size(389, 336);
            this.listGoodWorks.TabIndex = 2;
            this.listGoodWorks.UseCompatibleStateImageBehavior = false;
            this.listGoodWorks.View = System.Windows.Forms.View.Tile;
            // 
            // buttonAddGoodDeed
            // 
            this.buttonAddGoodDeed.Location = new System.Drawing.Point(54, 3);
            this.buttonAddGoodDeed.Name = "buttonAddGoodDeed";
            this.buttonAddGoodDeed.Size = new System.Drawing.Size(274, 38);
            this.buttonAddGoodDeed.TabIndex = 0;
            this.buttonAddGoodDeed.Text = "Add Good Deed";
            this.buttonAddGoodDeed.UseVisualStyleBackColor = true;
            this.buttonAddGoodDeed.Click += new System.EventHandler(this.buttonAddGoodDeed_Click);
            // 
            // labelPoints
            // 
            this.labelPoints.AutoSize = true;
            this.labelPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPoints.Location = new System.Drawing.Point(309, 5);
            this.labelPoints.Name = "labelPoints";
            this.labelPoints.Size = new System.Drawing.Size(0, 17);
            this.labelPoints.TabIndex = 3;
            // 
            // deedControl
            // 
            this.deedControl.Location = new System.Drawing.Point(0, 28);
            this.deedControl.Name = "deedControl";
            this.deedControl.Size = new System.Drawing.Size(397, 428);
            this.deedControl.TabIndex = 3;
            this.deedControl.Visible = false;
            // 
            // taskControl
            // 
            this.taskControl.Location = new System.Drawing.Point(0, 28);
            this.taskControl.Name = "taskControl";
            this.taskControl.Size = new System.Drawing.Size(397, 428);
            this.taskControl.TabIndex = 4;
            this.taskControl.Visible = false;
            // 
            // shoppingControl
            // 
            this.shoppingControl.Location = new System.Drawing.Point(0, 28);
            this.shoppingControl.Name = "shoppingControl";
            this.shoppingControl.Size = new System.Drawing.Size(397, 428);
            this.shoppingControl.TabIndex = 5;
            this.shoppingControl.Visible = false;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(19, 1);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(52, 21);
            this.buttonRefresh.TabIndex = 6;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 456);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.shoppingControl);
            this.Controls.Add(this.deedControl);
            this.Controls.Add(this.taskControl);
            this.Controls.Add(this.labelPoints);
            this.Controls.Add(this.mainControl);
            this.Controls.Add(this.comboLoggedUser);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Child App";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainControl.ResumeLayout(false);
            this.tabTasks.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabGoals.ResumeLayout(false);
            this.tabGoodWorks.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboLoggedUser;
        private System.Windows.Forms.TabControl mainControl;
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
        private TaskControl taskControl;
        private System.Windows.Forms.ListView listGoals;
        private System.Windows.Forms.Button buttonAddGoodDeed;
        private System.Windows.Forms.ListView listGoodWorks;
        private GoodDeedControl deedControl;
        private ShoppingControl shoppingControl;
        private System.Windows.Forms.Button buttonRefresh;
    }
}

