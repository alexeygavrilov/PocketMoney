using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using PocketMoney.Model.External.Requests;
using Results = PocketMoney.Model.External.Results;
using PocketMoney.Model.External.Results.Clients;
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

namespace PocketMoney.ChildApp
{
    public partial class MainForm : Form
    {
        private readonly ICurrentUserProvider _currentDataProvider = null;
        private readonly IFamilyService _familyService = null;
        private readonly IClientService _clientService = null;
        private Guid _familyId;

        public MainForm()
        {
            Program.Register();
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                _currentDataProvider = ServiceLocator.Current.GetInstance<ICurrentUserProvider>();
                _familyService = ServiceLocator.Current.GetInstance<IFamilyService>();
                _clientService = ServiceLocator.Current.GetInstance<IClientService>();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var authResult = _familyService.Login(new LoginRequest { UserName = "Dad", Password = "include" });
            if (!authResult.Success)
            {
                MessageBox.Show(authResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _familyId = authResult.Data.Family.Id;
            _currentDataProvider.AddCurrentUser(authResult.Data);

            var usersResult = _familyService.GetUsers(Request.Empty);
            if (!usersResult.Success)
            {
                MessageBox.Show(usersResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            comboLoggedUser.Items.Clear();
            foreach (var uv in usersResult.List.Where(x => x.RoleName == "Children"))
            {
                comboLoggedUser.Items.Add(uv);
            }
            comboLoggedUser.SelectedIndex = 0;
            tabControl2.SelectedIndex = 1;
        }

        private void comboLoggedUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            var userView = ((Results.UserView)comboLoggedUser.Items[comboLoggedUser.SelectedIndex]);
            _currentDataProvider.AddCurrentUser(new WrapperUser(userView.UserName, userView.UserId, _familyId));
            FillData();
        }

        private void FillData()
        {
            var cursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var currentUserResult = _clientService.GetCurrentUser(Request.Empty);
                if (!currentUserResult.Success)
                {
                    MessageBox.Show(currentUserResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                labelPoints.Text = currentUserResult.Data.Points.ToString() + " points";

                var taskResult = _clientService.GetTaskList(Request.Empty);
                if (!taskResult.Success)
                {
                    MessageBox.Show(taskResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                listTasksYesterday.Items.Clear();
                foreach (var task in taskResult.List.Where(x => x.DateType == eDateType.Yesterday))
                {
                    listTasksYesterday.Items.Add(new ListViewItem { Tag = task, Text = task.Title + ": " + task.Reward });
                }

                listTasksToday.Items.Clear();
                foreach (var task in taskResult.List.Where(x => x.DateType == eDateType.Today))
                {
                    listTasksToday.Items.Add(new ListViewItem { Tag = task, Text = task.Title + ": " + task.Reward });
                }

                listTasksTomorrow.Items.Clear();
                foreach (var task in taskResult.List.Where(x => x.DateType == eDateType.Tomorrow))
                {
                    listTasksTomorrow.Items.Add(new ListViewItem { Tag = task, Text = task.Title + ": " + task.Reward });
                }

                var floatingResult = _clientService.GetFloatingTaskList(Request.Empty);
                if (!floatingResult.Success)
                {
                    MessageBox.Show(floatingResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                listTasksFloating.Items.Clear();
                foreach (var task in floatingResult.List)
                {
                    listTasksFloating.Items.Add(new ListViewItem { Tag = task, Text = task.Title + ": " + task.Reward });
                }
            }
            finally
            {
                this.Cursor = cursor;
            }
        }

        #region Handlers

        private void tabControl2_DrawItem(object sender, DrawItemEventArgs e)
        {

            // This event is called once for each tab button in your tab control

            // First paint the background with a color based on the current tab

            // e.Index is the index of the tab in the TabPages collection.
            switch (e.Index)
            {
                case 0:
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), e.Bounds);
                    break;
                case 1:
                    e.Graphics.FillRectangle(new SolidBrush(Color.Orange), e.Bounds);
                    break;
                case 2:
                    e.Graphics.FillRectangle(new SolidBrush(Color.Green), e.Bounds);
                    break;
                case 3:
                    e.Graphics.FillRectangle(new SolidBrush(Color.Blue), e.Bounds);
                    break;
                default:
                    break;
            }

            // Then draw the current tab button text 
            Rectangle paddedBounds = e.Bounds;
            paddedBounds.Inflate(-2, -2);
            e.Graphics.DrawString(tabControl2.TabPages[e.Index].Text, tabControl2.Font, SystemBrushes.HighlightText, paddedBounds);

        }
        #endregion
    }
}
