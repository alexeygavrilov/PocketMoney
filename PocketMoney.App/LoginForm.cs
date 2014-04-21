using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Model.External.Requests;
using PocketMoney.Service.Interfaces;
using System;
using System.Windows.Forms;

namespace PocketMoney.App
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var familyService = ServiceLocator.Current.GetInstance<IFamilyService>();
            var result = familyService.Login(new LoginRequest { UserName = textBox1.Text, Password = textBox2.Text });
            if (result.Success)
            {
                var currentService = ServiceLocator.Current.GetInstance<ICurrentUserProvider>();
                currentService.AddCurrentUser(result.Data);
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
