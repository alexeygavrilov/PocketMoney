using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;

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
