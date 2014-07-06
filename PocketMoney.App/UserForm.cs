using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Model.External.Requests;
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
    public partial class UserForm : Form
    {
        private Guid _userId;
        protected ICurrentUserProvider _currentDataProvider = null;
        protected IFamilyService _familyService = null;

        public UserForm()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                _currentDataProvider = ServiceLocator.Current.GetInstance<ICurrentUserProvider>();
                _familyService = ServiceLocator.Current.GetInstance<IFamilyService>();
            }
        }

        public UserForm(Guid userId)
            : this()
        {
            _userId = userId;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            FillUser();
        }

        private void buttonWithdraw_Click(object sender, EventArgs e)
        {
            WithdrawForm form = new WithdrawForm(_userId, textName.Text);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FillUser();
            }
        }

        private void FillUser()
        {
            var userResult = _familyService.GetUser(new GuidRequest(_userId));

            if (userResult.Success)
            {
                textName.Text = userResult.Data.UserName;
                textAdditionalName.Text = userResult.Data.AdditionalName;
                textEmail.Text = userResult.Data.Email;
                textPhone.Text = userResult.Data.Phone;
                textRole.Text = userResult.Data.RoleName;
                textLastLogin.Text = userResult.Data.LastLoginDate;
                textPoints.Text = userResult.Data.Points.ToString();
                textCompletedCount.Text = userResult.Data.CompletedTaskCount.ToString();
                textGrabbedCount.Text = userResult.Data.GrabbedTaskCount.ToString();
                textGoalCount.Text = userResult.Data.GoalsCount.ToString();
                textGoodWorksCount.Text = userResult.Data.GoodDeedCount.ToString();
                textHistoryLog.Text = userResult.Data.HistoryLog;
            }
            else
            {
                MessageBox.Show(userResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var result = _familyService.UpdateUser(new UpdateUserRequest
            {
                AdditionalName = textAdditionalName.Text,
                UserName = textName.Text,
                UserId = _userId,
                Email = textEmail.Text,
                Phone = textPhone.Text
            });
            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
