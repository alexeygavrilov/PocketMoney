using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Model.External;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PocketMoney.ParentApp
{
    public partial class RepeatTaskForm : BaseForm
    {
        public RepeatTaskForm()
            : base()
        {
            InitializeComponent();
        }

        public RepeatTaskForm(Guid taskId)
            : base(taskId)
        {
            InitializeComponent();
        }

        private void RepeatTaskForm_Load(object sender, EventArgs e)
        {
            var familyService = ServiceLocator.Current.GetInstance<IFamilyService>();
            var result = familyService.GetUsers(Request.Empty);
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

            this.FillData<RepeatTaskView>(
                x => _taskService.GetRepeatTask(x),
                task =>
                {

                    textBox1.Text = task.Text;
                    textBox2.Text = task.Name;

                    for (int i = 0; i < checkedListBox2.Items.Count; i++)
                    {
                        if (task.Form.DaysOfWeek.Contains((int)((DayOfWeek)checkedListBox2.Items[i])))
                        {
                            checkedListBox2.SetItemChecked(i, true);
                        }
                    }
                    
                    radioButton1.Checked = task.Form.OccurrenceType == eOccurrenceType.Day;
                    radioButton2.Checked = task.Form.OccurrenceType == eOccurrenceType.Weekday;
                    radioButton3.Checked = task.Form.OccurrenceType == eOccurrenceType.Week;
                    radioButton4.Checked = task.Form.OccurrenceType == eOccurrenceType.Month;

                    numericUpDown3.Value = task.Form.EveryDay;
                    numericUpDown4.Value = task.Form.EveryWeek;
                    numericUpDown6.Value = task.Form.EveryMonth;
                    numericUpDown5.Value = task.Form.DayOfMonth;
                    dateTimePicker1.Value = task.Form.DateRangeFrom;

                    if (task.Form.OccurrenceNumber.HasValue)
                    {
                        radioEndAfter.Checked = true;
                        numericUpDown2.Value = task.Form.OccurrenceNumber.Value;
                    }
                    if (task.Form.DateRangeTo.HasValue)
                    {
                        radioEndBy.Checked = true;
                        dateTimePicker2.Value = task.Form.DateRangeTo.Value;
                    }
                });

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

            Result result = null;

            if (_currentTaskId == Guid.Empty)
            {
                result = taskService.AddRepeatTask(new AddRepeatTaskRequest
                {
                    AssignedTo = assignedTo.ToArray(),
                    Points = radioButton5.Checked ? Convert.ToInt32(numericUpDown1.Value) : 0,
                    Gift = radioButton6.Checked ? textBox3.Text : null,
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
            }
            else
            {
                result = taskService.UpdateRepeatTask(new UpdateRepeatTaskRequest
                {
                    Id = _currentTaskId,
                    AssignedTo = assignedTo.ToArray(),
                    Points = radioButton5.Checked ? Convert.ToInt32(numericUpDown1.Value) : 0,
                    Gift = radioButton6.Checked ? textBox3.Text : null,
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
