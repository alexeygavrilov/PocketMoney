using Microsoft.Practices.ServiceLocation;
using PocketMoney.Model.External.Requests;
using PocketMoney.Service.Interfaces;
using System;
using System.Windows.Forms;

namespace PocketMoney.ParentApp
{
    public partial class WithdrawForm : Form
    {
        private Guid _userId;
        private string _useerName;

        public WithdrawForm()
        {
            InitializeComponent();
        }

        public WithdrawForm(Guid userId, string userName)
            : this()
        {
            _userId = userId;
            _useerName = userName;
        }

        private void WithdrawForm_Load(object sender, EventArgs e)
        {
            textName.Text = _useerName;
        }

        private void buttonWithdraw_Click(object sender, EventArgs e)
        {
            var familyService = ServiceLocator.Current.GetInstance<IFamilyService>();
            var result = familyService.Withdraw(new WithdrawRequest { UserId = _userId, Points = (int)numericUpDown1.Value });

            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
