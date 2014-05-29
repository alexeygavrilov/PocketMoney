using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using System;

namespace PocketMoney.App
{
    public class BaseForm : Form
    {
        protected ICurrentUserProvider _currentDataProvider = null;
        protected IUser _currentUser = null;

        protected BaseForm()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                _currentDataProvider = ServiceLocator.Current.GetInstance<ICurrentUserProvider>();
                _currentUser = _currentDataProvider.GetCurrentUser();
            }
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
