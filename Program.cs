using TimeAttendanceManager.Auth.Forms;
using TimeAttendanceManager.Main.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeAttendanceManager
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var loginForm = new FrmAuthUsersLogin())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new MDIFrmDashBoard());
                }
                else
                {
                    // Exit if login failed or cancelled
                    Application.Exit();
                }
            }
        }
    }
}
