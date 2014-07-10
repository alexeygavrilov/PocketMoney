namespace PocketMoney.ParentApp
{
    partial class HomeworkTaskForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.comboBoxReminderPM = new System.Windows.Forms.ComboBox();
            this.comboBoxReminderMinutes = new System.Windows.Forms.ComboBox();
            this.comboBoxReminderHour = new System.Windows.Forms.ComboBox();
            this.checkBoxReminder = new System.Windows.Forms.CheckBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkFloating = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Assigned to ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(362, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Description";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(362, 32);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(294, 51);
            this.textBox1.TabIndex = 8;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(362, 110);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(294, 94);
            this.checkedListBox1.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(268, 366);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 37);
            this.button1.TabIndex = 13;
            this.button1.Text = "Save Task";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Date range";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(192, 192);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(146, 20);
            this.dateTimePicker1.TabIndex = 15;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(192, 227);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(146, 20);
            this.dateTimePicker2.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(151, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Start";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(151, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "End";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Month",
            "Quarter",
            "Half-Year",
            "Year"});
            this.comboBox1.Location = new System.Drawing.Point(21, 209);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 19;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Location = new System.Drawing.Point(23, 90);
            this.checkedListBox2.MultiColumn = true;
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(291, 64);
            this.checkedListBox2.TabIndex = 20;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(26, 161);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 17);
            this.checkBox1.TabIndex = 21;
            this.checkBox1.Text = "Include Holidays";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Every week on:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.radioButton5);
            this.groupBox2.Controls.Add(this.radioButton6);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Location = new System.Drawing.Point(362, 221);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 100);
            this.groupBox2.TabIndex = 62;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reward";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(73, 52);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(228, 37);
            this.textBox3.TabIndex = 32;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Checked = true;
            this.radioButton5.Location = new System.Drawing.Point(13, 19);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(54, 17);
            this.radioButton5.TabIndex = 26;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Points";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.Reward_CheckedChanged);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(13, 52);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(41, 17);
            this.radioButton6.TabIndex = 27;
            this.radioButton6.Text = "Gift";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.Reward_CheckedChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(73, 19);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 31;
            // 
            // comboBoxReminderPM
            // 
            this.comboBoxReminderPM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReminderPM.Enabled = false;
            this.comboBoxReminderPM.FormattingEnabled = true;
            this.comboBoxReminderPM.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.comboBoxReminderPM.Location = new System.Drawing.Point(537, 323);
            this.comboBoxReminderPM.Name = "comboBoxReminderPM";
            this.comboBoxReminderPM.Size = new System.Drawing.Size(42, 21);
            this.comboBoxReminderPM.TabIndex = 61;
            // 
            // comboBoxReminderMinutes
            // 
            this.comboBoxReminderMinutes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReminderMinutes.Enabled = false;
            this.comboBoxReminderMinutes.FormattingEnabled = true;
            this.comboBoxReminderMinutes.Items.AddRange(new object[] {
            "00",
            "05",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55"});
            this.comboBoxReminderMinutes.Location = new System.Drawing.Point(489, 323);
            this.comboBoxReminderMinutes.Name = "comboBoxReminderMinutes";
            this.comboBoxReminderMinutes.Size = new System.Drawing.Size(42, 21);
            this.comboBoxReminderMinutes.TabIndex = 60;
            // 
            // comboBoxReminderHour
            // 
            this.comboBoxReminderHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReminderHour.Enabled = false;
            this.comboBoxReminderHour.FormattingEnabled = true;
            this.comboBoxReminderHour.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.comboBoxReminderHour.Location = new System.Drawing.Point(442, 323);
            this.comboBoxReminderHour.Name = "comboBoxReminderHour";
            this.comboBoxReminderHour.Size = new System.Drawing.Size(42, 21);
            this.comboBoxReminderHour.TabIndex = 59;
            // 
            // checkBoxReminder
            // 
            this.checkBoxReminder.AutoSize = true;
            this.checkBoxReminder.Location = new System.Drawing.Point(368, 325);
            this.checkBoxReminder.Name = "checkBoxReminder";
            this.checkBoxReminder.Size = new System.Drawing.Size(71, 17);
            this.checkBoxReminder.TabIndex = 58;
            this.checkBoxReminder.Text = "Reminder";
            this.checkBoxReminder.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(26, 32);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(288, 21);
            this.comboBox2.TabIndex = 64;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(26, 13);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(60, 17);
            this.checkBox2.TabIndex = 65;
            this.checkBox2.Text = "Lesson";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkFloating
            // 
            this.checkFloating.AutoSize = true;
            this.checkFloating.Location = new System.Drawing.Point(593, 90);
            this.checkFloating.Name = "checkFloating";
            this.checkFloating.Size = new System.Drawing.Size(63, 17);
            this.checkFloating.TabIndex = 66;
            this.checkFloating.Text = "Floating";
            this.checkFloating.UseVisualStyleBackColor = true;
            this.checkFloating.CheckedChanged += new System.EventHandler(this.checkFloating_CheckedChanged);
            // 
            // HomeworkTaskForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 420);
            this.Controls.Add(this.checkFloating);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.comboBoxReminderPM);
            this.Controls.Add(this.comboBoxReminderMinutes);
            this.Controls.Add(this.comboBoxReminderHour);
            this.Controls.Add(this.checkBoxReminder);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.checkedListBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkedListBox1);
            this.Name = "HomeworkTaskForm";
            this.Text = "Homework Task";
            this.Load += new System.EventHandler(this.HomeworkTaskForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ComboBox comboBoxReminderPM;
        private System.Windows.Forms.ComboBox comboBoxReminderMinutes;
        private System.Windows.Forms.ComboBox comboBoxReminderHour;
        private System.Windows.Forms.CheckBox checkBoxReminder;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkFloating;
    }
}