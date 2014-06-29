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

namespace PocketMoney.App
{
    public partial class ShoppingTaskForm : BaseForm
    {
        public ShoppingTaskForm()
            : base()
        {
            InitializeComponent();
        }

        public ShoppingTaskForm(Guid taskId)
            : base(taskId)
        {
            InitializeComponent();
        }

        private void OnetimeTaskForm_Load(object sender, EventArgs e)
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
            comboBoxReminderHour.SelectedIndex = 11;
            comboBoxReminderMinutes.SelectedIndex = 0;
            comboBoxReminderPM.SelectedIndex = 1;

            this.FillData<ShoppingTaskView>(
                x => _taskService.GetShoppingTask(x),
                task =>
                {
                    textBox2.Text = task.ShopName;
                    if (task.DeadlineDate.HasValue)
                    {
                        dateTimePicker1.Enabled = checkBox1.Checked = true;
                        dateTimePicker1.Value = task.DeadlineDate.Value;
                    }
                    else
                    {
                        dateTimePicker1.Enabled = checkBox1.Checked = false;
                    }

                    foreach (var shopItem in task.ShoppingList.OrderBy(x => x.OrderNumber))
                    {
                        dataGridView1.Rows.Add(shopItem.ItemName, shopItem.Qty);
                    }
                });

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var taskService = ServiceLocator.Current.GetInstance<ITaskService>();
            IList<Guid> assignedTo = new List<Guid>();
            foreach (var checkedItem in checkedListBox1.CheckedItems)
            {
                assignedTo.Add(((UserView)checkedItem).UserId);
            }
            IList<ShopItem> shoppingList = new List<ShopItem>();
            int index = 0;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (!item.IsNewRow)
                {
                    shoppingList.Add(new ShopItem(index++, (string)item.Cells[0].Value, (string)item.Cells[1].Value));
                }
            }

            Result result = null;

            if (_currentTaskId == Guid.Empty)
            {
                result = taskService.AddShoppingTask(new AddShoppingTaskRequest
                {
                    AssignedTo = assignedTo.ToArray(),
                    DeadlineDate = checkBox1.Checked ? new DateTime?(dateTimePicker1.Value) : null,
                    Points = Convert.ToInt32(numericUpDown1.Value),
                    Text = textBox1.Text,
                    ShopName = textBox2.Text,
                    ReminderTime = this.GetReminderTime(),
                    ShoppingList = shoppingList.ToArray()
                });
            }
            else
            {
                result = taskService.UpdateShoppingTask(new UpdateShoppingTaskRequest
                {
                    Id = _currentTaskId,
                    AssignedTo = assignedTo.ToArray(),
                    DeadlineDate = checkBox1.Checked ? new DateTime?(dateTimePicker1.Value) : null,
                    Points = Convert.ToInt32(numericUpDown1.Value),
                    Text = textBox1.Text,
                    ShopName = textBox2.Text,
                    ReminderTime = this.GetReminderTime(),
                    ShoppingList = shoppingList.ToArray()
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
    }
}
