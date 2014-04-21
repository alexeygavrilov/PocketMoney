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
using PocketMoney.Model.External.Results;
using PocketMoney.Service.Interfaces;

namespace PocketMoney.App
{
    public partial class OnetimeTaskForm : Form
    {
        public OnetimeTaskForm()
        {
            InitializeComponent();
        }

        private void OnetimeTaskForm_Load(object sender, EventArgs e)
        {
            var currendData = ServiceLocator.Current.GetInstance<ICurrentUserProvider>();
            var user = currendData.GetCurrentUser();
            var familyService = ServiceLocator.Current.GetInstance<IFamilyService>();
            var result = familyService.GetUsers(new Model.External.Requests.FamilyRequest { Data = user.Family });
            if (result.Success)
            {
                
                checkedListBox1.Items.Clear();
                foreach (var ui in result.List.Where(x => x.UserId != user.Id))
                {
                    checkedListBox1.Items.Add(ui);
                }
            }
        }
    }
}
