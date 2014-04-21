using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PocketMoney.App.Properties;

namespace PocketMoney.App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            Program.Register();
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Settings.Default.UserId == Guid.Empty)
            {
                LoginForm login = new LoginForm();
                login.ShowDialog();
            }
            button1.Enabled = button2.Enabled = Settings.Default.UserId != Guid.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectTaskTypeForm taskType = new SelectTaskTypeForm();
            taskType.ShowDialog();
        }
    }
}
