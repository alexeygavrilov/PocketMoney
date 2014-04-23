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
using PocketMoney.Model.External.Requests;
using PocketMoney.Service.Interfaces;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.App
{
    public partial class HomeworkTaskForm : BaseForm
    {
        public HomeworkTaskForm()
        {
            InitializeComponent();
        }

        private void HomeworkTaskForm_Load(object sender, EventArgs e)
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
            comboBox1.SelectedIndex = 0;
            checkedListBox2.Items.Clear();
            checkedListBox2.Items.AddRange(Enum.GetNames(typeof(DayOfWeek)));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            dateTimePicker1.Value = now;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                default:
                    dateTimePicker2.Value = now.AddMonths(1);
                    break;
                case 1:
                    dateTimePicker2.Value = now.AddMonths(3);
                    break;
                case 2:
                    dateTimePicker2.Value = now.AddMonths(6);
                    break;
                case 3:
                    dateTimePicker2.Value = now.AddYears(1);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
