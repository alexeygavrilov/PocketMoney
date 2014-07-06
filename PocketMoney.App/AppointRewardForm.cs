using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Model.External.Requests;
using PocketMoney.Service.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PocketMoney.ParentApp
{
    public partial class AppointRewardForm : Form
    {
        protected ICurrentUserProvider _currentDataProvider = null;
        protected IGoalService _goalService = null;
        private readonly Guid _attaintmentId;

        public AppointRewardForm(Guid attainnmentId)
        {
            InitializeComponent();
            _attaintmentId = attainnmentId;
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                _currentDataProvider = ServiceLocator.Current.GetInstance<ICurrentUserProvider>();
                _goalService = ServiceLocator.Current.GetInstance<IGoalService>();
            }

        }

        private void AppointRewardForm_Load(object sender, EventArgs e)
        {
            var attaintmentResult = _goalService.GetAttainment(new GuidRequest(_attaintmentId));
            if (!attaintmentResult.Success)
            {
                MessageBox.Show(attaintmentResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                textBox1.Text = attaintmentResult.Data.Text;

                if (!string.IsNullOrEmpty(attaintmentResult.Data.Gift) || attaintmentResult.Data.Points > 0)
                {
                    if (string.IsNullOrEmpty(attaintmentResult.Data.Gift))
                    {
                        textBox3.Text = string.Empty;
                        numericUpDown1.Value = (decimal)attaintmentResult.Data.Points;
                        radioButton5.Checked = true;
                    }
                    else
                    {
                        textBox3.Text = attaintmentResult.Data.Gift;
                        numericUpDown1.Value = decimal.Zero;
                        radioButton6.Checked = true;
                    }
                    textBox3.Enabled = numericUpDown1.Enabled = radioButton5.Enabled = radioButton6.Enabled = button1.Enabled = false;
                    label1.Text = string.Format("Reward for {0}'s good deed has been appointed", attaintmentResult.Data.UserName);
                }
                else
                {
                    radioButton5.Enabled = radioButton6.Enabled = button1.Enabled = true;
                    label1.Text = string.Format(label1.Text, attaintmentResult.Data.UserName);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = _goalService.AppointReward(new AppointRewardRequest
            {
                Id = _attaintmentId,
                Points = radioButton5.Checked ? Convert.ToInt32(numericUpDown1.Value) : 0,
                Gift = radioButton6.Checked ? textBox3.Text : null,
            });
            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Reward_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = radioButton5.Checked;
            textBox3.Enabled = radioButton6.Checked;
        }


    }
}
