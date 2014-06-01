using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Practices.ServiceLocation;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Service.Interfaces;

namespace PocketMoney.App
{
    public partial class OnetimeTaskForm : BaseForm
    {
        public OnetimeTaskForm()
            : base()
        {
            InitializeComponent();
        }

        public OnetimeTaskForm(Guid taskId)
            : base(taskId)
        {
            InitializeComponent();
        }

        private void OnetimeTaskForm_Load(object sender, EventArgs e)
        {
            var familyService = ServiceLocator.Current.GetInstance<IFamilyService>();
            var result = familyService.GetUsers(new FamilyRequest { Data = _currentUser.Family });
            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            checkedListBox1.Items.Clear();
            foreach (var ui in result.List.Where(x => x.UserId != _currentUser.Id))
            {
                checkedListBox1.Items.Add(ui);
            }
            comboBoxReminderHour.SelectedIndex = 11;
            comboBoxReminderMinutes.SelectedIndex = 0;
            comboBoxReminderPM.SelectedIndex = 1;

            if (_currentTaskId != Guid.Empty)
            {
                var taskResult = _taskService.GetOneTimeTask(new GuidRequest { Data = _currentTaskId });
                if (!taskResult.Success)
                {
                    MessageBox.Show(taskResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                var task = taskResult.Data;
                textBox2.Text = task.Name;
                textBox1.Text = task.Text;
                this.SetReminderTime(task.ReminderTime);
                numericUpDown1.Value = (decimal)task.Points;
                if (task.DeadlineDate.HasValue)
                {
                    dateTimePicker1.Enabled = checkBox1.Checked = true;
                    dateTimePicker1.Value = task.DeadlineDate.Value;
                }
                else
                {
                    dateTimePicker1.Enabled = checkBox1.Checked = false;
                }

                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (task.AssignedTo.ContainsKey(((UserView)checkedListBox1.Items[i]).UserId))
                    {
                        checkedListBox1.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IList<Guid> assignedTo = new List<Guid>();
            foreach (var checkedItem in checkedListBox1.CheckedItems)
            {
                assignedTo.Add(((UserView)checkedItem).UserId);
            }
            var result = _taskService.AddOneTimeTask(new AddOneTimeTaskRequest
            {
                AssignedTo = assignedTo.ToArray(),
                DeadlineDate = checkBox1.Checked ? new DateTime?(dateTimePicker1.Value) : null,
                Points = Convert.ToInt32(numericUpDown1.Value),
                Text = textBox1.Text,
                Name = textBox2.Text,
                ReminderTime = this.GetReminderTime()
            });
            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void checkBoxReminder_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxReminderHour.Enabled = comboBoxReminderMinutes.Enabled = comboBoxReminderPM.Enabled = checkBoxReminder.Checked;
        }
    }
}
