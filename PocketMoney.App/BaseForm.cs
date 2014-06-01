using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using System;
using PocketMoney.Service.Interfaces;

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

        protected TimeSpan? GetReminderTime()
        {
            if (((CheckBox)this.Controls["checkBoxReminder"]).Checked)
            {
                TimeSpan time = TimeSpan.Zero;
                time.Add(TimeSpan.FromHours(Convert.ToInt32(((ComboBox)this.Controls["comboBoxReminderHour"]).SelectedValue)));
                time.Add(TimeSpan.FromMinutes(Convert.ToInt32(((ComboBox)this.Controls["comboBoxReminderMinutes"]).SelectedValue)));
                if (((ComboBox)this.Controls["comboBoxReminderPM"]).SelectedValue == "AM")
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
                ((ComboBox)this.Controls["comboBoxReminderHour"]).SelectedValue = date.ToString("hh");
                ((ComboBox)this.Controls["comboBoxReminderMinutes"]).SelectedValue = date.ToString("mm");
                ((ComboBox)this.Controls["comboBoxReminderPM"]).SelectedValue = date.ToString("tt");
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
