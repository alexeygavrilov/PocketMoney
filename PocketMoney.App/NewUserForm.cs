using Microsoft.Practices.ServiceLocation;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PocketMoney.ParentApp
{
    public partial class NewUserForm : Form
    {
        public NewUserForm()
        {
            InitializeComponent();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            var familyService = ServiceLocator.Current.GetInstance<IFamilyService>();
            var result = familyService.AddUser(new AddUserRequest
            {
                UserName = textName.Text,
                AdditionalName = textAdditionalName.Text,
                Password = textPassword.Text,
                ConfirmPassword = textConfirmPassword.Text,
                SendNotification = false,
                Email = textEmail.Text,
                Phone = textPhone.Text,
                RoleId = Roles.Define((string)comboRoles.SelectedItem).Id
            });
            if (result.Success)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
