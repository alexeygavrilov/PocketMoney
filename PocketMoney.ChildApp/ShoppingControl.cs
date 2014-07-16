using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results.Clients;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PocketMoney.ChildApp
{
    public partial class ShoppingControl : UserControl
    {
        private ShoppingTaskView _task = null;
        private IClientService _clientService = null;

        public event EventHandler OnClose;
        public event EventHandler OnProcess;

        public ShoppingControl()
        {
            InitializeComponent();
        }

        public void OpenTask(ShoppingTaskView task)
        {
            _clientService = ServiceLocator.Current.GetInstance<IClientService>();

            _task = task;
            textNote.Text = string.Empty;
            textCustom.Text = _task.ShopName;
            textReward.Text = _task.Reward;
            textDescription.Text = _task.Text;

            FillShoppingList();

            this.Visible = true;
        }

        private void FillShoppingList()
        {
            listShopping.Items.Clear();

            var shoppingListResult = _clientService.GetShoppingList(new GuidRequest(_task.Id));
            if (shoppingListResult.Success)
            {
                foreach (var item in shoppingListResult.List)
                {
                    listShopping.Items.Add(new ListViewItem(item.ItemName + ". " + item.Qty)
                    {
                        Checked = item.Processed,
                        Tag = item.OrderNumber
                    });
                }
            }
            else
            {
                MessageBox.Show(shoppingListResult.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CloseTask()
        {
            _task = null;
            this.Visible = false;
            if (this.OnClose != null)
            {
                OnClose(this, EventArgs.Empty);
            }
        }

        private void TaskControl_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            CloseTask();
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (_task != null)
            {
                if (MessageBox.Show(this.Parent, "Have you made this task?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var result = _clientService.DoneTask(new DoneTaskRequest { Id = _task.Id, Note = textNote.Text, DateType = _task.DateType });
                    if (result.Success)
                    {
                        if (this.OnProcess != null)
                            this.OnProcess(this, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void listShopping_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var result = _clientService.CheckShopItem(new CheckShopItemRequest
            {
                Checked = e.Item.Checked,
                TaskId = _task.Id,
                OrderNumber = (int)e.Item.Tag
            });

            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
