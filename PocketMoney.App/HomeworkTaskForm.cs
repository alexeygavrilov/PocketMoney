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
            var usersResult = familyService.GetUsers(new FamilyRequest { Data = _currentUser.Family });
            if (usersResult.Success)
            {
                checkedListBox1.Items.Clear();
                foreach (var ui in usersResult.List.Where(x => x.UserId != _currentUser.Id))
                {
                    checkedListBox1.Items.Add(ui);
                }
            }
            else
            {
                MessageBox.Show(usersResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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

            var lessonsResult = _settigsService.GetLessons(Request.Empty);

            if (lessonsResult.Success)
            {
                var lessons = new AutoCompleteStringCollection();
                lessonsResult.List.Select(s => lessons.Add(s));
                comboBox2.AutoCompleteCustomSource = lessons;
            }
            else
            {
                MessageBox.Show(lessonsResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.FillData<HomeworkTaskView>(
                x => _taskService.GetHomeworkTask(x),
                task =>
                {
                    for (int i = 0; i < checkedListBox2.Items.Count; i++)
                    {
                        if (task.Form.DaysOfWeek.Contains((int)((DayOfWeek)checkedListBox2.Items[i])))
                        {
                            checkedListBox2.SetItemChecked(i, true);
                        }
                    }
                    checkBox1.Checked = task.Form.IncludeHolidays;
                    comboBox1.SelectedIndex = task.Form.DateRangeIndex;
                    dateTimePicker1.Value = task.Form.DateRangeFrom;
                    dateTimePicker2.Value = task.Form.DateRangeTo;
                    comboBox2.Text = task.Lesson ?? string.Empty;
                    checkBox2.Checked = comboBox2.Enabled = !string.IsNullOrEmpty(task.Lesson);
                });
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

            Data.Result result = null;
            if (_currentTaskId == Guid.Empty)
            {
                result = _taskService.AddHomeworkTask(new AddHomeworkTaskRequest
                {
                    Lesson = checkBox2.Checked ? comboBox2.Text : null,
                    AssignedTo = assignedTo.ToArray(),
                    Points = radioButton5.Checked ? Convert.ToInt32(numericUpDown1.Value) : 0,
                    Gift = radioButton6.Checked ? textBox3.Text : null,
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
            }
            else
            {
                result = _taskService.UpdateHomeworkTask(new UpdateHomeworkTaskRequest
                {
                    Id = _currentTaskId,
                    Lesson = checkBox2.Checked ? comboBox2.Text : null,
                    AssignedTo = assignedTo.ToArray(),
                    Points = radioButton5.Checked ? Convert.ToInt32(numericUpDown1.Value) : 0,
                    Gift = radioButton6.Checked ? textBox3.Text : null,
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = checkBox2.Checked;
        }

        private void Reward_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = radioButton5.Checked;
            textBox3.Enabled = radioButton6.Checked;
        }

    }
}
