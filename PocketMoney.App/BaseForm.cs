using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using System;
using PocketMoney.Service.Interfaces;
using System.Globalization;
using PocketMoney.Model.External.Results;
using PocketMoney.Model.External.Requests;

namespace PocketMoney.App
{
    public class BaseForm : Form
    {
        protected ICurrentUserProvider _currentDataProvider = null;
        protected IUser _currentUser = null;
        protected Guid _currentTaskId = Guid.Empty;
        protected ITaskService _taskService = null;

        protected BaseForm()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                _currentDataProvider = ServiceLocator.Current.GetInstance<ICurrentUserProvider>();
                _currentUser = _currentDataProvider.GetCurrentUser();
                _taskService = ServiceLocator.Current.GetInstance<ITaskService>();
            }
        }

        protected BaseForm(Guid taskId)
            : this()
        {
            _currentTaskId = taskId;
        }

        protected void FillData<TView>(Func<GuidRequest, ResultData<TView>> get, Action<TView> set) where TView : TaskView
        {
            if (_currentTaskId != Guid.Empty)
            {
                var taskResult = get(new GuidRequest { Data = _currentTaskId });
                if (!taskResult.Success)
                {
                    MessageBox.Show(taskResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                var task = taskResult.Data;

                ((TextBox)this.Controls["textBox1"]).Text = task.Text;

                this.SetReminderTime(task.ReminderTime);

                ((NumericUpDown)this.Controls["numericUpDown1"]).Value = (decimal)task.Points;

                var __checkedListBox1 = (CheckedListBox)this.Controls["checkedListBox1"];
                for (int i = 0; i < __checkedListBox1.Items.Count; i++)
                {
                    if (task.AssignedTo.ContainsKey(((UserView)__checkedListBox1.Items[i]).UserId))
                    {
                        __checkedListBox1.SetItemChecked(i, true);
                    }
                }

                set(task);
            }

        }

        protected TimeSpan? GetReminderTime()
        {
            if (((CheckBox)this.Controls["checkBoxReminder"]).Checked)
            {
                TimeSpan time =
                    TimeSpan.FromHours(Convert.ToInt32(((ComboBox)this.Controls["comboBoxReminderHour"]).SelectedItem))
                    +
                    TimeSpan.FromMinutes(Convert.ToInt32(((ComboBox)this.Controls["comboBoxReminderMinutes"]).SelectedItem));
                if ((string)((ComboBox)this.Controls["comboBoxReminderPM"]).SelectedItem == "AM")
                {
                    if (time.Hours == 12 && time.Minutes == 0)
                        time = TimeSpan.Zero;
                }
                else
                {
                    if (time.Hours < 12)
                        time += TimeSpan.FromHours(12);
                }
                return time;
            }
            else
                return null;
        }

        protected void SetReminderTime(TimeSpan? time)
        {
            if (time.HasValue)
            {
                DateTime date = DateTime.MinValue.Add(time.Value);
                ((ComboBox)this.Controls["comboBoxReminderHour"]).Enabled =
                    ((ComboBox)this.Controls["comboBoxReminderMinutes"]).Enabled =
                    ((ComboBox)this.Controls["comboBoxReminderPM"]).Enabled =
                    ((CheckBox)this.Controls["checkBoxReminder"]).Checked = true;
                ((ComboBox)this.Controls["comboBoxReminderHour"]).SelectedItem = date.ToString("hh");
                ((ComboBox)this.Controls["comboBoxReminderMinutes"]).SelectedItem = date.ToString("mm");
                ((ComboBox)this.Controls["comboBoxReminderPM"]).SelectedItem = date.ToString("tt", CultureInfo.InvariantCulture);
            }
            else
            {
                ((ComboBox)this.Controls["comboBoxReminderHour"]).Enabled =
                    ((ComboBox)this.Controls["comboBoxReminderMinutes"]).Enabled =
                    ((ComboBox)this.Controls["comboBoxReminderPM"]).Enabled =
                    ((CheckBox)this.Controls["checkBoxReminder"]).Checked = false;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseForm";
            this.ResumeLayout(false);

        }
    }
}
