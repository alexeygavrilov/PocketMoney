using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using PocketMoney.Model;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results.Clients;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Results = PocketMoney.Model.External.Results;

namespace PocketMoney.ChildApp
{
    public partial class MainForm : Form
    {
        #region Members

        private readonly ICurrentUserProvider _currentDataProvider = null;
        private readonly IFamilyService _familyService = null;
        private readonly IClientService _clientService = null;
        private Guid _familyId;
        #endregion

        #region Ctors & Load

        public MainForm()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                Program.Register();
                
                _currentDataProvider = ServiceLocator.Current.GetInstance<ICurrentUserProvider>();
                _familyService = ServiceLocator.Current.GetInstance<IFamilyService>();
                _clientService = ServiceLocator.Current.GetInstance<IClientService>();
            }
            taskControl.OnClose += control_OnClose;
            deedControl.OnClose += control_OnClose;
            shoppingControl.OnClose += control_OnClose;

            taskControl.OnProcess += (s, e) => FillData();
            shoppingControl.OnProcess += (s, e) => FillData();
            deedControl.OnProcess += (s, e) =>
            {
                CloseControls(); 
                FillGoodWorksList();
            };
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
        #endregion

        #region Methods

        private void FillData()
        {
            var cursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                this.CloseControls();
                var currentUserResult = _clientService.GetCurrentUser(Request.Empty);
                if (!currentUserResult.Success)
                {
                    MessageBox.Show(currentUserResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                labelPoints.Text = currentUserResult.Data.Points.ToString() + " points";

                FillTaskList();
                FillGoalList();
                FillGoodWorksList();
            }
            finally
            {
                this.Cursor = cursor;
            }
        }

        private void FillTaskList()
        {
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

        private void FillGoalList()
        {
            var goalResult = _clientService.GetGoalList(Request.Empty);
            if (!goalResult.Success)
            {
                MessageBox.Show(goalResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            listGoals.Items.Clear();
            foreach (var goal in goalResult.List)
            {
                listGoals.Items.Add(new ListViewItem { Tag = goal, Text = goal.Text + ": " + goal.Reward });
            }
        }

        private void FillGoodWorksList()
        {
            var result = _clientService.GetGoodDeedList(Request.Empty);
            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            listGoodWorks.Items.Clear();
            foreach (var deed in result.List)
            {
                listGoodWorks.Items.Add(new ListViewItem { Tag = deed, Text = deed.Text + ": " + deed.Reward });
            }

        }

        private void CloseControls()
        {
            this.mainControl.Visible = true;
            this.deedControl.Visible = false;
            this.taskControl.CloseTask();
            this.shoppingControl.CloseTask();
        }

        private void OpenTaskDialog(ListView listView)
        {
            if (listView.SelectedItems.Count > 0)
            {
                this.mainControl.Visible = false;
                var task = (TaskView)listView.SelectedItems[0].Tag;
                if (task.TaskType == TaskType.SHOPPING_TYPE)
                {
                    this.shoppingControl.OpenTask((ShoppingTaskView)task);
                }
                else
                {
                    this.taskControl.OpenTask(task);
                }
            }
        }
        #endregion

        #region Handlers

        private void listTasksToday_MouseClick(object sender, MouseEventArgs e)
        {
            OpenTaskDialog(listTasksToday);
        }

        private void listTasksYesterday_MouseClick(object sender, MouseEventArgs e)
        {
            OpenTaskDialog(listTasksYesterday);
        }

        private void listTasksTomorrow_MouseClick(object sender, MouseEventArgs e)
        {
            OpenTaskDialog(listTasksTomorrow);
        }

        private void listTasksFloating_MouseClick(object sender, MouseEventArgs e)
        {
            OpenTaskDialog(listTasksFloating);
        }

        private void listGoals_MouseClick(object sender, MouseEventArgs e)
        {
            if (listGoals.SelectedItems.Count > 0)
            {
                this.mainControl.Visible = false;
                this.taskControl.OpenGoal((GoalView)listGoals.SelectedItems[0].Tag);
            }
        }

        private void buttonAddGoodDeed_Click(object sender, EventArgs e)
        {
            this.mainControl.Visible = false;
            this.deedControl.Visible = true;
        }

        private void comboLoggedUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            var userView = ((Results.UserView)comboLoggedUser.Items[comboLoggedUser.SelectedIndex]);
            _currentDataProvider.AddCurrentUser(new WrapperUser(userView.UserName, userView.UserId, _familyId));
            FillData();
        }

        private void control_OnClose(object sender, EventArgs e)
        {
            mainControl.Visible = true;
        }

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
