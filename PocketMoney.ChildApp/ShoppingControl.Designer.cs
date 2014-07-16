namespace PocketMoney.ChildApp
{
    partial class ShoppingControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonBack = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.textCustom = new System.Windows.Forms.TextBox();
            this.labelCustom = new System.Windows.Forms.Label();
            this.buttonDone = new System.Windows.Forms.Button();
            this.textReward = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelNote = new System.Windows.Forms.Label();
            this.textNote = new System.Windows.Forms.TextBox();
            this.listShopping = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // buttonBack
            // 
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBack.Location = new System.Drawing.Point(21, 14);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(358, 35);
            this.buttonBack.TabIndex = 0;
            this.buttonBack.Text = " <<  Back      ";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(17, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description:";
            // 
            // textDescription
            // 
            this.textDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textDescription.Location = new System.Drawing.Point(21, 316);
            this.textDescription.Multiline = true;
            this.textDescription.Name = "textDescription";
            this.textDescription.ReadOnly = true;
            this.textDescription.Size = new System.Drawing.Size(358, 47);
            this.textDescription.TabIndex = 4;
            // 
            // textCustom
            // 
            this.textCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textCustom.Location = new System.Drawing.Point(99, 220);
            this.textCustom.Name = "textCustom";
            this.textCustom.ReadOnly = true;
            this.textCustom.Size = new System.Drawing.Size(280, 29);
            this.textCustom.TabIndex = 6;
            // 
            // labelCustom
            // 
            this.labelCustom.AutoSize = true;
            this.labelCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCustom.Location = new System.Drawing.Point(17, 223);
            this.labelCustom.Name = "labelCustom";
            this.labelCustom.Size = new System.Drawing.Size(60, 24);
            this.labelCustom.TabIndex = 5;
            this.labelCustom.Text = "Shop:";
            // 
            // buttonDone
            // 
            this.buttonDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDone.ForeColor = System.Drawing.Color.Green;
            this.buttonDone.Location = new System.Drawing.Point(21, 372);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(358, 39);
            this.buttonDone.TabIndex = 7;
            this.buttonDone.Text = "I bought all!";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // textReward
            // 
            this.textReward.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textReward.Location = new System.Drawing.Point(99, 257);
            this.textReward.Name = "textReward";
            this.textReward.ReadOnly = true;
            this.textReward.Size = new System.Drawing.Size(280, 29);
            this.textReward.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(17, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Reward:";
            // 
            // labelNote
            // 
            this.labelNote.AutoSize = true;
            this.labelNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNote.Location = new System.Drawing.Point(17, 188);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(55, 24);
            this.labelNote.TabIndex = 10;
            this.labelNote.Text = "Note:";
            // 
            // textNote
            // 
            this.textNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textNote.Location = new System.Drawing.Point(99, 188);
            this.textNote.Name = "textNote";
            this.textNote.Size = new System.Drawing.Size(280, 26);
            this.textNote.TabIndex = 11;
            // 
            // listShopping
            // 
            this.listShopping.CheckBoxes = true;
            this.listShopping.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listShopping.GridLines = true;
            this.listShopping.Location = new System.Drawing.Point(21, 55);
            this.listShopping.Name = "listShopping";
            this.listShopping.Size = new System.Drawing.Size(358, 127);
            this.listShopping.TabIndex = 12;
            this.listShopping.UseCompatibleStateImageBehavior = false;
            this.listShopping.View = System.Windows.Forms.View.List;
            this.listShopping.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listShopping_ItemChecked);
            // 
            // ShoppingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listShopping);
            this.Controls.Add(this.textNote);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.textReward);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textCustom);
            this.Controls.Add(this.labelCustom);
            this.Controls.Add(this.textDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonBack);
            this.Name = "ShoppingControl";
            this.Size = new System.Drawing.Size(397, 428);
            this.Load += new System.EventHandler(this.TaskControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textDescription;
        private System.Windows.Forms.TextBox textCustom;
        private System.Windows.Forms.Label labelCustom;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.TextBox textReward;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.TextBox textNote;
        private System.Windows.Forms.ListView listShopping;
    }
}
