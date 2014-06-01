using Microsoft.Practices.ServiceLocation;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PocketMoney.App
{
    public partial class CleanTaskForm : BaseForm
    {
        public CleanTaskForm()
        {
            InitializeComponent();
        }

        public CleanTaskForm(Guid taskId)
            : base(taskId)
        {
            InitializeComponent();
        }

        private void CleanTaskForm_Load(object sender, EventArgs e)
        {
            var familyService = ServiceLocator.Current.GetInstance<IFamilyService>();
            var result = familyService.GetUsers(new FamilyRequest { Data = _currentUser.Family });
            if (result.Success)
            {
                checkedListBox1.Items.Clear();
                foreach (var ui in result.List.Where(x => x.UserId != _currentUser.Id))
                {
                    checkedListBox1.Items.Add(ui);
                }
            }
            checkedListBox2.Items.Clear();
            checkedListBox2.Items.Add(DayOfWeek.Monday);
            checkedListBox2.Items.Add(DayOfWeek.Tuesday);
            checkedListBox2.Items.Add(DayOfWeek.Wednesday);
            checkedListBox2.Items.Add(DayOfWeek.Thursday);
            checkedListBox2.Items.Add(DayOfWeek.Friday);
            checkedListBox2.Items.Add(DayOfWeek.Saturday);
            checkedListBox2.Items.Add(DayOfWeek.Sunday);
            comboBoxReminderHour.SelectedIndex = 11;
            comboBoxReminderMinutes.SelectedIndex = 0;
            comboBoxReminderPM.SelectedIndex = 1;

            if (_currentTaskId != Guid.Empty)
            {
                //var 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var taskService = ServiceLocator.Current.GetInstance<ITaskService>();
            IList<Guid> assignedTo = new List<Guid>();
            foreach (var checkedItem in checkedListBox1.CheckedItems)
            {
                assignedTo.Add(((UserView)checkedItem).UserId);
            }
            IList<int> daysOfWeek = new List<int>();
            if (radioButton2.Checked)
            {
                foreach (var checkedItem in checkedListBox2.CheckedItems)
                {
                    daysOfWeek.Add((int)((DayOfWeek)checkedItem));
                }
            }
            var result = taskService.AddCleanTask(new AddCleanTaskRequest
            {
                AssignedTo = assignedTo.ToArray(),
                Points = Convert.ToInt32(numericUpDown1.Value),
                Text = textBox1.Text,
                RoomName = textBox2.Text,
                EveryDay = radioButton1.Checked,
                DaysOfWeek = daysOfWeek.ToArray(), 
                ReminderTime = this.GetReminderTime()
            });
            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBox2.Enabled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBox2.Enabled = false;
        }

        private void checkBoxReminder_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxReminderHour.Enabled = comboBoxReminderMinutes.Enabled = comboBoxReminderPM.Enabled = checkBoxReminder.Checked;
        }
    }
}
