using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Model.External.Results;
using PocketMoney.Service.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PocketMoney.App
{
    public partial class TaskListForm : Form
    {
        private ITaskService _taskService;

        public TaskListForm()
        {
            InitializeComponent();
            _taskService = ServiceLocator.Current.GetInstance<ITaskService>();
        }

        private void TaskListForm_Load(object sender, EventArgs e)
        {
            Fill(0);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill(comboBox1.SelectedIndex);
        }

        private void Fill(int index)
        {
            TaskListResult result = null;
            switch (index)
            {
                case 0:
                    result = _taskService.AllTasks(Request.Empty);
                    break;
                default:
                    break;
            }
            if (result != null)
            {
                if (result.Success)
                {
                    foreach (var item in result.List)
                    {
                        listView1.Items.Add(new ListViewItem { Tag = item.TaskId, ToolTipText = item.TaskType.ToString(), Text = item.Title });
                    }
                }
                else
                {
                    MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Form form = null;
                Guid taskId = (Guid)listView1.SelectedItems[0].Tag;
                switch (listView1.SelectedItems[0].ToolTipText)
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
                    form.ShowDialog();
                }
            }
        }

    }
}
