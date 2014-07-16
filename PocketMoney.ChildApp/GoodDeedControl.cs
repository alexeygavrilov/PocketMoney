using Microsoft.Practices.ServiceLocation;
using PocketMoney.Model.External.Requests;
using PocketMoney.Service.Interfaces;
using System;
using System.Windows.Forms;

namespace PocketMoney.ChildApp
{
    public partial class GoodDeedControl : UserControl
    {
        public event EventHandler OnClose;
        public event EventHandler OnProcess;

        public GoodDeedControl()
        {
            InitializeComponent();
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            var clientService = ServiceLocator.Current.GetInstance<IClientService>();
            var result = clientService.AddGoodDeed(new AddAttainmentRequest { Text = textNote.Text });
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

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            if (this.OnClose != null)
            {
                OnClose(this, EventArgs.Empty);
            }
        }
    }
}
