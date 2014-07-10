using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PocketMoney.ParentApp
{
    public partial class GoalForm : Form
    {
        protected ICurrentUserProvider _currentDataProvider = null;
        protected ITaskService _taskService = null;
        protected IGoalService _goalService = null;
        protected IUser _currentUser = null;
        protected Guid _currentGoalId = Guid.Empty;


        public GoalForm()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                _currentDataProvider = ServiceLocator.Current.GetInstance<ICurrentUserProvider>();
                _currentUser = _currentDataProvider.GetCurrentUser();
                _taskService = ServiceLocator.Current.GetInstance<ITaskService>();
                _goalService = ServiceLocator.Current.GetInstance<IGoalService>();
            }
        }

        public GoalForm(Guid goalId)
            : this()
        {
            _currentGoalId = goalId;
        }


        private void GoalForm_Load(object sender, EventArgs e)
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
            if (_currentGoalId != Guid.Empty)
            {
                var goalResult = _goalService.GetGoal(new GuidRequest(_currentGoalId));
                if (!goalResult.Success)
                {
                    MessageBox.Show(goalResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                var goal = goalResult.Data;

                textBox1.Text = goal.Text;

                if (string.IsNullOrEmpty(goal.Gift))
                {
                    numericUpDown1.Value = (decimal)goal.Points;
                    radioEndAfter.Checked = true;
                }
                else
                {
                    textBox2.Text = goal.Gift;
                    radioEndBy.Checked = true;
                }
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (goal.AssignedTo.ContainsKey(((UserView)checkedListBox1.Items[i]).UserId))
                    {
                        checkedListBox1.SetItemChecked(i, true);
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IList<Guid> assignedTo = new List<Guid>();
            foreach (var checkedItem in checkedListBox1.CheckedItems)
            {
                assignedTo.Add(((UserView)checkedItem).UserId);
            }
            Data.Result result = null;
            if (_currentGoalId == Guid.Empty)
            {
                result = _goalService.AddGoal(new AddGoalRequest
                {
                    AssignedTo = assignedTo.ToArray(),
                    Points = radioEndAfter.Checked ? Convert.ToInt32(numericUpDown1.Value) : 0,
                    Gift = radioEndBy.Checked ? textBox2.Text : null,
                    Text = textBox1.Text
                });
            }
            else
            {
                result = _goalService.UpdateGoal(new UpdateGoalRequest
                {
                    Id = _currentGoalId,
                    AssignedTo = assignedTo.ToArray(),
                    Points = radioEndAfter.Checked ? Convert.ToInt32(numericUpDown1.Value) : 0,
                    Gift = radioEndBy.Checked ? textBox2.Text : null,
                    Text = textBox1.Text
                });
            }
            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Reward_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = radioEndAfter.Checked;
            textBox2.Enabled = radioEndBy.Checked;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
            {
                textBox1.Text = (string)comboBox1.Items[comboBox1.SelectedIndex];
            }
        }

    }
}
