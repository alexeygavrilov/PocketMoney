using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Service.Interfaces;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.App
{
    public partial class OnetimeTaskForm : BaseForm
    {
        IFamilyService _familyService;
        ITaskService _taskService;

        public OnetimeTaskForm()
            : base()
        {
            InitializeComponent();
        }

        private void OnetimeTaskForm_Load(object sender, EventArgs e)
        {
            _familyService = ServiceLocator.Current.GetInstance<IFamilyService>();
            _taskService = ServiceLocator.Current.GetInstance<ITaskService>();
            var result = _familyService.GetUsers(new FamilyRequest { Data = _currentUser.Family });
            if (result.Success)
            {
                checkedListBox1.Items.Clear();
                foreach (var ui in result.List.Where(x => x.UserId != _currentUser.Id))
                {
                    checkedListBox1.Items.Add(ui);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IList<Guid> assignedTo = new List<Guid>();
            foreach(var checkedItem in checkedListBox1.CheckedItems)
            {
                assignedTo.Add(((UserInfo)checkedItem).UserId);
            }
            var result = _taskService.AddOneTimeTask(new AddOneTimeTaskRequest
            {
                AssignedTo = assignedTo.ToArray(),
                DeadlineDate = checkBox1.Checked ? new DateTime?(dateTimePicker1.Value) : null,
                Points = Convert.ToInt32(numericUpDown1.Value),
                Text = textBox1.Text
            });
            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
