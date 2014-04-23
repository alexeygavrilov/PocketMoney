using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PocketMoney.App
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
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HomeworkTaskForm homework = new HomeworkTaskForm();
            if (homework.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
