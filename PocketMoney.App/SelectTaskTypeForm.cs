using System;
using System.Windows.Forms;

namespace PocketMoney.ParentApp
{
    public partial class SelectTaskTypeForm : Form
    {
        public SelectTaskTypeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnetimeTaskForm oneTime = new OnetimeTaskForm();
            if (oneTime.ShowDialog() == DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HomeworkTaskForm homework = new HomeworkTaskForm();
            if (homework.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CleanTaskForm clean = new CleanTaskForm();
            if (clean.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RepeatTaskForm workout = new RepeatTaskForm();
            if (workout.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShoppingTaskForm shopping = new ShoppingTaskForm();
            if (shopping.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
