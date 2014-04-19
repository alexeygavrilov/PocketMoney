using PocketMoney.App.Properties;
using PocketMoney.Configuration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PocketMoney.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Settings.Default.UserId == Guid.Empty)
            {
                Application.Run(new Login());
            }
            else
            {
                Application.Run(new Main());
            }
            ContainerRegistration.Register(BuildManager.Instance);

        }
    }
}
