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
            this.tabAttainment = new System.Windows.Forms.TabPage();
            this.listAttainments = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.tabUser = new System.Windows.Forms.TabPage();
            this.listUsers = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabFamily = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tabTask.SuspendLayout();
            this.tabGoal.SuspendLayout();
            this.tabAttainment.SuspendLayout();
            this.tabUser.SuspendLayout();
            this.tabFamily.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.groupBox3.SuspendLayout();
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
            this.label2.Location = new System.Drawing.Point(3, 10);
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
            // tabAttainment
            // 
            this.tabAttainment.Controls.Add(this.listAttainments);
            this.tabAttainment.Controls.Add(this.label3);
            this.tabAttainment.Location = new System.Drawing.Point(4, 42);
            this.tabAttainment.Name = "tabAttainment";
            this.tabAttainment.Size = new System.Drawing.Size(758, 426);
            this.tabAttainment.TabIndex = 4;
            this.tabAttainment.Text = "Good Works";
            this.tabAttainment.UseVisualStyleBackColor = true;
            // 
            // listAttainments
            // 
            this.listAttainments.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listAttainments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listAttainments.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listAttainments.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listAttainments.FullRowSelect = true;
            this.listAttainments.GridLines = true;
            this.listAttainments.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listAttainments.Location = new System.Drawing.Point(0, 46);
            this.listAttainments.MultiSelect = false;
            this.listAttainments.Name = "listAttainments";
            this.listAttainments.RightToLeftLayout = true;
            this.listAttainments.Size = new System.Drawing.Size(758, 380);
            this.listAttainments.TabIndex = 7;
            this.listAttainments.UseCompatibleStateImageBehavior = false;
            this.listAttainments.View = System.Windows.Forms.View.Details;
            this.listAttainments.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listAttainments_MouseClick);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Text";
            this.columnHeader4.Width = 400;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Who";
            this.columnHeader5.Width = 200;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Reward";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 150;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 33);
            this.label3.TabIndex = 6;
            this.label3.Text = "All \"Good Works\"";
            // 
            // tabUser
            // 
            this.tabUser.Controls.Add(this.listUsers);
            this.tabUser.Controls.Add(this.buttonAddUser);
            this.tabUser.Controls.Add(this.label4);
            this.tabUser.Location = new System.Drawing.Point(4, 42);
            this.tabUser.Name = "tabUser";
            this.tabUser.Size = new System.Drawing.Size(758, 426);
            this.tabUser.TabIndex = 2;
            this.tabUser.Text = "Members";
            this.tabUser.UseVisualStyleBackColor = true;
            // 
            // listUsers
            // 
            this.listUsers.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.listUsers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listUsers.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listUsers.FullRowSelect = true;
            this.listUsers.GridLines = true;
            this.listUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listUsers.Location = new System.Drawing.Point(0, 46);
            this.listUsers.MultiSelect = false;
            this.listUsers.Name = "listUsers";
            this.listUsers.RightToLeftLayout = true;
            this.listUsers.Size = new System.Drawing.Size(758, 380);
            this.listUsers.TabIndex = 9;
            this.listUsers.UseCompatibleStateImageBehavior = false;
            this.listUsers.View = System.Windows.Forms.View.Details;
            this.listUsers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listUsers_MouseClick);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Name";
            this.columnHeader7.Width = 450;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Actions Count";
            this.columnHeader8.Width = 150;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Points";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 150;
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddUser.Location = new System.Drawing.Point(511, 4);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(239, 36);
            this.buttonAddUser.TabIndex = 8;
            this.buttonAddUser.Text = "Add User";
            this.buttonAddUser.UseVisualStyleBackColor = true;
            this.buttonAddUser.Click += new System.EventHandler(this.buttonAddUser_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(208, 33);
            this.label4.TabIndex = 7;
            this.label4.Text = "Family Members";
            // 
            // tabFamily
            // 
            this.tabFamily.Controls.Add(this.button1);
            this.tabFamily.Controls.Add(this.groupBox3);
            this.tabFamily.Controls.Add(this.groupBox2);
            this.tabFamily.Controls.Add(this.groupBox1);
            this.tabFamily.Location = new System.Drawing.Point(4, 42);
            this.tabFamily.Name = "tabFamily";
            this.tabFamily.Size = new System.Drawing.Size(758, 426);
            this.tabFamily.TabIndex = 3;
            this.tabFamily.Text = "Settings";
            this.tabFamily.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.numericUpDown3);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.numericUpDown2);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Enabled = false;
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(8, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(742, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pointing Policy";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(280, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 17);
            this.label7.TabIndex = 33;
            this.label7.Text = "points";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(219, 38);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(58, 27);
            this.numericUpDown1.TabIndex = 32;
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(20, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Grabbing a single floating task:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Enabled = false;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(8, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(742, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statistical Info";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(294, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "Not Implemented";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(350, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 17);
            this.label8.TabIndex = 34;
            this.label8.Text = "Completion of each";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(482, 38);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(58, 27);
            this.numericUpDown2.TabIndex = 35;
            this.numericUpDown2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(546, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 17);
            this.label9.TabIndex = 36;
            this.label9.Text = "tasks:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(657, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 17);
            this.label11.TabIndex = 38;
            this.label11.Text = "points";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(596, 38);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(58, 27);
            this.numericUpDown3.TabIndex = 37;
            this.numericUpDown3.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Enabled = false;
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(8, 227);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(742, 149);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Task Types Policy";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(294, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 19);
            this.label10.TabIndex = 0;
            this.label10.Text = "Not Implemented";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(590, 382);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 36);
            this.button1.TabIndex = 3;
            this.button1.Text = "Update Settings";
            this.button1.UseVisualStyleBackColor = true;
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
            this.Text = "Parent Back Office";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabMain.ResumeLayout(false);
            this.tabTask.ResumeLayout(false);
            this.tabTask.PerformLayout();
            this.tabGoal.ResumeLayout(false);
            this.tabGoal.PerformLayout();
            this.tabAttainment.ResumeLayout(false);
            this.tabAttainment.PerformLayout();
            this.tabUser.ResumeLayout(false);
            this.tabUser.PerformLayout();
            this.tabFamily.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listAttainments;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonAddUser;
        private System.Windows.Forms.ListView listUsers;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;

    }
}