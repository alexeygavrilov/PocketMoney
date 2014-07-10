using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PocketMoney.ParentApp
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
            var result = familyService.GetUsers(Request.Empty);
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

            this.FillData<OneTimeTaskView>(
                x => _taskService.GetOneTimeTask(x),
                task =>
                {
                    textBox2.Text = task.Name;
                    if (task.DeadlineDate.HasValue)
                    {
                        dateTimePicker1.Enabled = checkBox1.Checked = true;
                        dateTimePicker1.Value = task.DeadlineDate.Value;
                    }
                    else
                    {
                        dateTimePicker1.Enabled = checkBox1.Checked = false;
                    }
                });
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Result result = null;
            IList<Guid> assignedTo = new List<Guid>();
            foreach (var checkedItem in checkedListBox1.CheckedItems)
            {
                assignedTo.Add(((UserView)checkedItem).UserId);
            }
            if (_currentTaskId == Guid.Empty)
            {
                result = _taskService.AddOneTimeTask(new AddOneTimeTaskRequest
                {
                    AssignedTo = assignedTo.ToArray(),
                    DeadlineDate = checkBox1.Checked ? new DateTime?(dateTimePicker1.Value) : null,
                    Points = radioButton5.Checked ? Convert.ToInt32(numericUpDown1.Value) : 0,
                    Gift = radioButton6.Checked ? textBox3.Text : null,
                    Text = textBox1.Text,
                    Name = textBox2.Text,
                    ReminderTime = this.GetReminderTime()
                });
            }
            else
            {
                result = _taskService.UpdateOneTimeTask(new UpdateOneTimeTaskRequest
                {
                    Id = _currentTaskId,
                    AssignedTo = assignedTo.ToArray(),
                    DeadlineDate = checkBox1.Checked ? new DateTime?(dateTimePicker1.Value) : null,
                    Points = radioButton5.Checked ? Convert.ToInt32(numericUpDown1.Value) : 0,
                    Gift = radioButton6.Checked ? textBox3.Text : null,
                    Text = textBox1.Text,
                    Name = textBox2.Text,
                    ReminderTime = this.GetReminderTime()
                });
            }
            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void checkBoxReminder_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxReminderHour.Enabled = comboBoxReminderMinutes.Enabled = comboBoxReminderPM.Enabled = checkBoxReminder.Checked;
        }

        private void Reward_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = radioButton5.Checked;
            textBox3.Enabled = radioButton6.Checked;
        }

        private void checkFloating_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFloating.Checked)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }
            checkedListBox1.Enabled = !checkFloating.Checked;
        }

    }
}
