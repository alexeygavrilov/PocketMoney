﻿namespace PocketMoney.App
{
    partial class ShoppingTaskForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxReminderPM = new System.Windows.Forms.ComboBox();
            this.comboBoxReminderMinutes = new System.Windows.Forms.ComboBox();
            this.comboBoxReminderHour = new System.Windows.Forms.ComboBox();
            this.checkBoxReminder = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ItemNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(142, 452);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 37);
            this.button1.TabIndex = 8;
            this.button1.Text = "Save Task";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxReminderPM
            // 
            this.comboBoxReminderPM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReminderPM.Enabled = false;
            this.comboBoxReminderPM.FormattingEnabled = true;
            this.comboBoxReminderPM.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.comboBoxReminderPM.Location = new System.Drawing.Point(344, 356);
            this.comboBoxReminderPM.Name = "comboBoxReminderPM";
            this.comboBoxReminderPM.Size = new System.Drawing.Size(42, 21);
            this.comboBoxReminderPM.TabIndex = 38;
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
            this.comboBoxReminderMinutes.Location = new System.Drawing.Point(296, 356);
            this.comboBoxReminderMinutes.Name = "comboBoxReminderMinutes";
            this.comboBoxReminderMinutes.Size = new System.Drawing.Size(42, 21);
            this.comboBoxReminderMinutes.TabIndex = 37;
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
            this.comboBoxReminderHour.Location = new System.Drawing.Point(248, 355);
            this.comboBoxReminderHour.Name = "comboBoxReminderHour";
            this.comboBoxReminderHour.Size = new System.Drawing.Size(42, 21);
            this.comboBoxReminderHour.TabIndex = 36;
            // 
            // checkBoxReminder
            // 
            this.checkBoxReminder.AutoSize = true;
            this.checkBoxReminder.Location = new System.Drawing.Point(248, 339);
            this.checkBoxReminder.Name = "checkBoxReminder";
            this.checkBoxReminder.Size = new System.Drawing.Size(71, 17);
            this.checkBoxReminder.TabIndex = 35;
            this.checkBoxReminder.Text = "Reminder";
            this.checkBoxReminder.UseVisualStyleBackColor = true;
            this.checkBoxReminder.CheckedChanged += new System.EventHandler(this.checkBoxReminder_CheckedChanged);
            // 
            // textBox2
            // 
            this.textBox2.AutoCompleteCustomSource.AddRange(new string[] {
            "Targets",
            "Fast Foods "});
            this.textBox2.Location = new System.Drawing.Point(127, 137);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(222, 20);
            this.textBox2.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Shop Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(181, 358);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "points";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(55, 415);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(55, 356);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 339);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Reward";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(55, 391);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(68, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Deadline";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Assigned to ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Description";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(55, 189);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(294, 40);
            this.textBox1.TabIndex = 1;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(55, 267);
            this.checkedListBox1.MultiColumn = true;
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(294, 64);
            this.checkedListBox1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemNameColumn,
            this.QtyColumn});
            this.dataGridView1.Location = new System.Drawing.Point(50, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 21;
            this.dataGridView1.Size = new System.Drawing.Size(336, 96);
            this.dataGridView1.TabIndex = 39;
            // 
            // ItemNameColumn
            // 
            this.ItemNameColumn.FillWeight = 200F;
            this.ItemNameColumn.HeaderText = "Item Name";
            this.ItemNameColumn.MaxInputLength = 255;
            this.ItemNameColumn.MinimumWidth = 50;
            this.ItemNameColumn.Name = "ItemNameColumn";
            // 
            // QtyColumn
            // 
            this.QtyColumn.HeaderText = "Qty";
            this.QtyColumn.MaxInputLength = 50;
            this.QtyColumn.Name = "QtyColumn";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Shopping List";
            // 
            // ShoppingTaskForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 509);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBoxReminderPM);
            this.Controls.Add(this.comboBoxReminderMinutes);
            this.Controls.Add(this.comboBoxReminderHour);
            this.Controls.Add(this.checkBoxReminder);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkedListBox1);
            this.Name = "ShoppingTaskForm";
            this.Text = "ShoppingTaskForm";
            this.Load += new System.EventHandler(this.OnetimeTaskForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxReminder;
        private System.Windows.Forms.ComboBox comboBoxReminderHour;
        private System.Windows.Forms.ComboBox comboBoxReminderMinutes;
        private System.Windows.Forms.ComboBox comboBoxReminderPM;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyColumn;
        private System.Windows.Forms.Label label5;
    }
}