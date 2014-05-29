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
    public partial class RepeatTaskForm : BaseForm
    {
        public RepeatTaskForm()
        {
            InitializeComponent();
        }

        private void RepeatTaskForm_Load(object sender, EventArgs e)
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
            dateTimePicker2.Value = DateTime.Now.AddMonths(1);
            comboBoxReminderHour.SelectedIndex = 11;
            comboBoxReminderMinutes.SelectedIndex = 0;
            comboBoxReminderPM.SelectedIndex = 1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var taskService = ServiceLocator.Current.GetInstance<ITaskService>();
            IList<Guid> assignedTo = new List<Guid>();
            foreach (var checkedItem in checkedListBox1.CheckedItems)
            {
                assignedTo.Add(((UserInfo)checkedItem).UserId);
            }
            IList<int> daysOfWeek = new List<int>();
            foreach (var checkedItem in checkedListBox2.CheckedItems)
            {
                daysOfWeek.Add((int)((DayOfWeek)checkedItem));
            }
            eOccurrenceType occurrenceType = eOccurrenceType.None;
            if (radioButton1.Checked)
            {
                occurrenceType = eOccurrenceType.Day;
            }
            else if (radioButton2.Checked)
            {
                occurrenceType = eOccurrenceType.Weekday;
            }
            else if (radioButton3.Checked)
            {
                occurrenceType = eOccurrenceType.Week;
            }
            else if (radioButton4.Checked)
            {
                occurrenceType = eOccurrenceType.Month;
            }

            var result = taskService.AddRepeatTask(new AddRepeatTaskRequest
            {
                AssignedTo = assignedTo.ToArray(),
                Points = Convert.ToInt32(numericUpDown1.Value),
                Text = textBox1.Text,
                ReminderTime = this.GetReminderTime(), 
                Name = textBox2.Text,
                Form = new RepeatForm
                {
                    OccurrenceType = occurrenceType,
                    EveryDay = (int)numericUpDown3.Value,
                    EveryWeek = (int)numericUpDown4.Value,
                    EveryMonth = (int)numericUpDown6.Value,
                    DayOfMonth = (int)numericUpDown5.Value,
                    DaysOfWeek = daysOfWeek.ToArray(),
                    DateRangeFrom = dateTimePicker1.Value,
                    OccurrenceNumber = radioEndAfter.Checked ? new int?((int)numericUpDown2.Value) : null,
                    DateRangeTo = radioEndBy.Checked ? new DateTime?(dateTimePicker2.Value) : null
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
