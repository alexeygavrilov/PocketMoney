using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PocketMoney.ParentApp.Properties;
using PocketMoney.Service.Interfaces;
using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;

namespace PocketMoney.ParentApp
{
    public partial class MainForm : Form
    {
        private ITaskService _taskService;
        private IGoalService _goalService;

        public MainForm()
        {
            Program.Register();
            InitializeComponent();
            _taskService = ServiceLocator.Current.GetInstance<ITaskService>();
            _goalService = ServiceLocator.Current.GetInstance<IGoalService>();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Settings.Default.UserId == Guid.Empty)
            {
                LoginForm login = new LoginForm();
                login.ShowDialog();
            }
            if (Settings.Default.UserId != Guid.Empty)
            {
                FillTaskList();
            }
            else
            {
                tabMain.Enabled = false;
            }
        }

        #region Task Handlers

        private void buttonAddTask_Click(object sender, EventArgs e)
        {
            SelectTaskTypeForm taskType = new SelectTaskTypeForm();
            if (taskType.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FillTaskList();
            }
        }

        private void listTasks_MouseClick(object sender, MouseEventArgs e)
        {
            if (listTasks.SelectedItems.Count > 0)
            {
                Form form = null;
                Guid taskId = (Guid)listTasks.SelectedItems[0].Tag;
                switch (listTasks.SelectedItems[0].ToolTipText)
                {
                    case "1":
                        form = new OnetimeTaskForm(taskId);
                        break;
                    case "2":
                        form = new RepeatTaskForm(taskId);
                        break;
                    case "3":
                        form = new HomeworkTaskForm(taskId);
                        break;
                    case "4":
                        form = new CleanTaskForm(taskId);
                        break;
                    case "5":
                        form = new ShoppingTaskForm(taskId);
                        break;
                    default:
                        break;
                }
                if (form != null)
                {
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        FillTaskList();
                    }
                }
            }

        }

        private void FillTaskList()
        {
            var result = _taskService.AllTasks(Request.Empty);

            if (result.Success)
            {
                listTasks.Items.Clear();
                foreach (var item in result.List)
                {
                    var itemView = new ListViewItem { Tag = item.TaskId, ToolTipText = item.TaskType.ToString(), Text = item.Title };
                    var subitems = new ListViewItem.ListViewSubItemCollection(itemView);
                    subitems.Add(new ListViewItem.ListViewSubItem { Text = item.Responsibility });
                    subitems.Add(new ListViewItem.ListViewSubItem { Text = item.Reward });
                    listTasks.Items.Add(itemView);
                }
            }
            else
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Goal Handlers

        private void buttonGoal_Click(object sender, EventArgs e)
        {
            GoalForm goalForm = new GoalForm();
            if (goalForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FillGoalList();
            }
        }

        private void listGoals_MouseClick(object sender, MouseEventArgs e)
        {
            if (listGoals.SelectedItems.Count > 0)
            {
                GoalForm goalForm = new GoalForm((Guid)listGoals.SelectedItems[0].Tag);
                if (goalForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FillGoalList();
                }
            }

        }

        private void FillGoalList()
        {
            var result = _goalService.AllGoals(Request.Empty);

            if (result.Success)
            {
                listGoals.Items.Clear();
                foreach (var item in result.List)
                {
                    var itemView = new ListViewItem { Tag = item.GoalId, Text = item.Text };
                    var subitems = new ListViewItem.ListViewSubItemCollection(itemView);
                    subitems.Add(new ListViewItem.ListViewSubItem { Text = item.Responsibility });
                    subitems.Add(new ListViewItem.ListViewSubItem { Text = item.Reward });
                    listGoals.Items.Add(itemView);
                }
            }
            else
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        #endregion

    }
}
