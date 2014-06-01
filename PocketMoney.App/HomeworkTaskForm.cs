using Microsoft.Practices.ServiceLocation;
using PocketMoney.Model.External;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PocketMoney.App
{
    public partial class HomeworkTaskForm : BaseForm
    {
        public HomeworkTaskForm()
        {
            InitializeComponent();
        }

        public HomeworkTaskForm(Guid taskId)
            : base(taskId)
        {
            InitializeComponent();
        }

        private void HomeworkTaskForm_Load(object sender, EventArgs e)
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
            comboBox1.SelectedIndex = 0;
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

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            dateTimePicker1.Value = now;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                default:
                    dateTimePicker2.Value = now.AddMonths(1);
                    break;
                case 1:
                    dateTimePicker2.Value = now.AddMonths(3);
                    break;
                case 2:
                    dateTimePicker2.Value = now.AddMonths(6);
                    break;
                case 3:
                    dateTimePicker2.Value = now.AddYears(1);
                    break;
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
            foreach (var checkedItem in checkedListBox2.CheckedItems)
            {
                daysOfWeek.Add((int)((DayOfWeek)checkedItem));
            }

            var result = taskService.AddHomeworkTask(new AddHomeworkTaskRequest
            {
                AssignedTo = assignedTo.ToArray(),
                Points = Convert.ToInt32(numericUpDown1.Value),
                Text = textBox1.Text, 
                ReminderTime = this.GetReminderTime(),
                Form = new HomeworkForm
                {
                    DateRangeIndex = comboBox1.SelectedIndex,
                    DaysOfWeek = daysOfWeek.ToArray(),
                    IncludeHolidays = checkBox1.Checked,
                    DateRangeFrom = dateTimePicker1.Value,
                    DateRangeTo = dateTimePicker2.Value
                }
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
