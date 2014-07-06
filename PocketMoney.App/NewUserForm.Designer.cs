namespace PocketMoney.ParentApp
{
    partial class NewUserForm
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
            this.label10 = new System.Windows.Forms.Label();
            this.textPhone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textEmail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textAdditionalName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboRoles = new System.Windows.Forms.ComboBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textConfirmPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkSendNotify = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(47, 322);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 64;
            this.label10.Text = "Role:";
            // 
            // textPhone
            // 
            this.textPhone.Location = new System.Drawing.Point(50, 287);
            this.textPhone.Name = "textPhone";
            this.textPhone.Size = new System.Drawing.Size(247, 20);
            this.textPhone.TabIndex = 63;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 62;
            this.label7.Text = "Phone:";
            // 
            // textEmail
            // 
            this.textEmail.Location = new System.Drawing.Point(50, 234);
            this.textEmail.Name = "textEmail";
            this.textEmail.Size = new System.Drawing.Size(247, 20);
            this.textEmail.TabIndex = 61;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 60;
            this.label6.Text = "Email:";
            // 
            // textAdditionalName
            // 
            this.textAdditionalName.Location = new System.Drawing.Point(50, 82);
            this.textAdditionalName.Name = "textAdditionalName";
            this.textAdditionalName.Size = new System.Drawing.Size(247, 20);
            this.textAdditionalName.TabIndex = 59;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 58;
            this.label1.Text = "Additional Name:";
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(50, 34);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(247, 20);
            this.textName.TabIndex = 57;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 56;
            this.label4.Text = "User Name (Login):";
            // 
            // comboRoles
            // 
            this.comboRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRoles.FormattingEnabled = true;
            this.comboRoles.Items.AddRange(new object[] {
            "Children",
            "Parent"});
            this.comboRoles.Location = new System.Drawing.Point(50, 338);
            this.comboRoles.Name = "comboRoles";
            this.comboRoles.Size = new System.Drawing.Size(247, 21);
            this.comboRoles.TabIndex = 65;
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(50, 131);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(247, 20);
            this.textPassword.TabIndex = 67;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 66;
            this.label2.Text = "Password:";
            // 
            // textConfirmPassword
            // 
            this.textConfirmPassword.Location = new System.Drawing.Point(50, 180);
            this.textConfirmPassword.Name = "textConfirmPassword";
            this.textConfirmPassword.Size = new System.Drawing.Size(247, 20);
            this.textConfirmPassword.TabIndex = 69;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 68;
            this.label3.Text = "Confirm Password:";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(49, 407);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(128, 37);
            this.buttonCreate.TabIndex = 70;
            this.buttonCreate.Text = "Create a New User";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(194, 407);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(103, 37);
            this.buttonCancel.TabIndex = 71;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkSendNotify
            // 
            this.checkSendNotify.AutoSize = true;
            this.checkSendNotify.Enabled = false;
            this.checkSendNotify.Location = new System.Drawing.Point(50, 374);
            this.checkSendNotify.Name = "checkSendNotify";
            this.checkSendNotify.Size = new System.Drawing.Size(107, 17);
            this.checkSendNotify.TabIndex = 72;
            this.checkSendNotify.Text = "Send Notification";
            this.checkSendNotify.UseVisualStyleBackColor = true;
            // 
            // NewUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(334, 457);
            this.Controls.Add(this.checkSendNotify);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.textConfirmPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboRoles);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textPhone);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textEmail);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textAdditionalName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewUserForm";
            this.Text = "New User Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textPhone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textEmail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textAdditionalName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboRoles;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textConfirmPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkSendNotify;
    }
}