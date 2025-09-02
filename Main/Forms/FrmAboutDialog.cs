using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security;
using System.Windows.Forms;
using Microsoft.Win32;
using static TimeAttendanceManager.Main.Classes.ClassGlobalFunctions;

/// <summary>
/// Generic About Box for use with my projects. Provides basic system information as well as
/// system up time and the current date and time. The header is a panel which my contain a
/// background image or color. A picture box (48x48 pixels) can display a custom image.
/// 
/// There are two buttons on the form, "Microsoft System Information," which starts msinfo32.exe
/// and "OK," which dismisses the form. Typing Escape on the keyboard will also dismiss the form.
/// 
/// There are two link labels. The first opens a file called "EULA.pdf" Imports the Adobe Reader that
/// is installed on the computer. The "EULA.pdf" file must reside in the application folder. The other
/// link label initiates an email to me. This is normally not visible for non-commercial applications.
/// the application folder.
/// </summary>
/// <remarks>The application will produce an error dialog if msinfo32.exe or EULA.pdf is not found.</remarks>
/// <copyright>Copyright © 2020 GTN Group of Industries</copyright>

namespace TimeAttendanceManager.Main.Forms
{
    public partial class FrmAboutDialog : Form
    {
        public FrmAboutDialog()
        {
            InitializeComponent();
            this.Load += FrmAboutDialog_Load;
            this.KeyDown += FrmAboutDialog_KeyDown;
        }

        #region Private Members
        private bool m_Is64Bit;
        private readonly string m_DisplayEmailAddress = "mailto:anil@gtnindustries.com;maheswararao@imperialgarments.in;satish_m@gtnengineering.net;";
        private readonly string m_DisplayWebAddress = "http://www.gtnindustries.com";
        private readonly string m_DisplayPhoneNumber = "+91 88888865660;9989534565;9948681756";
        private readonly string m_Programmer = " [ Anil K. Waghmare / M Maheswara Rao / Satish Maturu ] ";
        #endregion

        #region Form Events
        private void FrmAboutDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                this.Close();
            }
        }

        private void FrmAboutDialog_Load(object sender, EventArgs e)
        {
            try
            {
                //Application.EnableVisualStyles();

                // Determine if 32 or 64 bit OS
                m_Is64Bit = Directory.Exists(Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%"));

                // Enable timer
                TimerTickCount.Enabled = true;

                // Set version info
                var appInfo = Application.ProductVersion;
                var version = new Version(appInfo);
                LabelVersion.Text = $"{Application.ProductName} Version {version.Major}.{version.Minor} Build {version.Build} Revision {version.Revision}";

                // Set form title
                this.Text = Application.ProductName;

                // Fill in application information
                LabelTitle.Text = Application.ProductName;
                LabelCopyright.Text = Application.CompanyName; // Note: In C# this might need adjustment
                LabelDescription.Text = Application.ProductName; // Note: Description might need adjustment

                // Fill in user information
                LabelUser.Text = GetUserCustomerName();
                LabelOrganization.Text = GetUserCustomerOrganization();

                // Fill in company information
                LabelCompany.Text = $"{Application.ProductName} is a product of {Application.CompanyName}";

                // Support label
                LabelSupportAvailable.Text = $"Support is available from {Application.CompanyName}:{m_Programmer}";

                // Contact info
                LinkLabelEmail.Text = $"Email: {m_DisplayEmailAddress.Replace("mailto:", "")}";
                LinkLabelWebsite.Text = $"Website: {m_DisplayWebAddress}";
                LabelSupportPhone.Text = $"Phone: {m_DisplayPhoneNumber}";

                // System information
                LabelWindowsVersion.Text = System.Environment.OSVersion.VersionString;
                LabelOSDescription.Text = System.Environment.OSVersion.ToString();
                LabelOSDescription.Text += m_Is64Bit ? " (64-Bit)" : " (32-Bit)";

                LabelFramework.Text = $".NET Framework Version {GetInstalledFrameworkVersion()}";
                LabelClrVersion.Text = $"Common Language Runtime Version {GetFrameworkShortVersion()}";

                string servicePack = GetFrameworkServicePack();
                if (!string.IsNullOrEmpty(servicePack) && servicePack != "0")
                {
                    LabelClrVersion.Text += $" Service Pack {servicePack}";
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error Number: {ex.HResult}\n" +
                    $"Error Source: {ex.Source}\n" +
                    $"Error Message: {ex.Message}\n" +
                    "Form: frmAboutDialog\n" +
                    "Function: frmAboutDialog_Load",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Link Labels
        private void LinkLabelEULA_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string eula = "Eula.pdf";
            string eulaPath = Path.Combine(Application.StartupPath, eula);

            if (File.Exists(eulaPath))
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(eulaPath)
                    {
                        WindowStyle = ProcessWindowStyle.Normal
                    };
                    Process.Start(startInfo);
                }
                catch (Win32Exception)
                {
                    MessageBox.Show("The EULA file cannot be found, or Adobe Reader is not installed.",
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("The EULA file cannot be found.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LinkLabelEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(m_DisplayEmailAddress)
                {
                    WindowStyle = ProcessWindowStyle.Normal
                };
                Process.Start(startInfo);
            }
            catch (Win32Exception)
            {
                MessageBox.Show("Unable to send email.", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LinkLabelWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(m_DisplayWebAddress)
                {
                    WindowStyle = ProcessWindowStyle.Normal
                };
                Process.Start(startInfo);
            }
            catch (Win32Exception)
            {
                MessageBox.Show("Unable to open website.", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region .NET Framework Information
        private static string GetFrameworkShortVersion()
        {
            return Environment.Version.ToString().Substring(0, 3);
        }

        private static string GetFrameworkServicePack()
        {
            string frameworkMajorVersion = Environment.Version.Major.ToString();
            string frameworkMinorVersion = Environment.Version.Minor.ToString();
            string frameworkVersion = $"v{frameworkMajorVersion}.{frameworkMinorVersion}.{Environment.Version.Build}";
            RegistryKey rk = null;

            try
            {
                if (frameworkMajorVersion == "2" && frameworkMinorVersion == "0")
                {
                    rk = Registry.LocalMachine.OpenSubKey($@"SOFTWARE\Microsoft\NET Framework Setup\NDP\{frameworkVersion}", false);
                    string temp = rk?.GetValue("SP")?.ToString();
                    return temp != "0" ? temp : "";
                }
                return string.Empty;
            }
            catch (Win32Exception)
            {
                return "";
            }
            finally
            {
                rk?.Close();
            }
        }

        private static string ReturnHighestFrameworkVersion()
        {
            const string netV20 = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v2.0.50727";
            const string netV30 = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.0";
            const string netV35 = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5";
            const string netV40client = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Client";
            const string netV40full = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full";

            double version = 0;
            string exactVersion = "";
            string servicePack = "";
            string full = "";
            RegistryKey rk = null;

            try
            {
                // Check version 4.0 full
                if (version == 0)
                {
                    try
                    {
                        rk = Registry.LocalMachine.OpenSubKey(netV40full);
                        exactVersion = rk?.GetValue("Version")?.ToString();
                        if (!string.IsNullOrEmpty(exactVersion))
                        {
                            version = 4;
                            servicePack = rk?.GetValue("SP")?.ToString() ?? "";
                            full = " (Full) ";
                        }
                    }
                    catch { version = 0; }
                }

                // Check version 4.0 client
                if (version == 0)
                {
                    try
                    {
                        rk = Registry.LocalMachine.OpenSubKey(netV40client);
                        exactVersion = rk?.GetValue("Version")?.ToString();
                        if (!string.IsNullOrEmpty(exactVersion))
                        {
                            version = 4;
                            servicePack = rk?.GetValue("SP")?.ToString() ?? "";
                            full = " (Client) ";
                        }
                    }
                    catch { version = 0; }
                }

                // Check version 3.5
                if (version == 0)
                {
                    try
                    {
                        rk = Registry.LocalMachine.OpenSubKey(netV35);
                        exactVersion = rk?.GetValue("Version")?.ToString();
                        if (!string.IsNullOrEmpty(exactVersion))
                        {
                            version = 3.5;
                            servicePack = rk?.GetValue("SP")?.ToString() ?? "";
                        }
                    }
                    catch { version = 0; }
                }

                // Check version 3.0
                if (version == 0)
                {
                    try
                    {
                        rk = Registry.LocalMachine.OpenSubKey(netV30);
                        exactVersion = rk?.GetValue("Version")?.ToString();
                        if (!string.IsNullOrEmpty(exactVersion))
                        {
                            version = 3;
                            servicePack = rk?.GetValue("SP")?.ToString() ?? "";
                        }
                    }
                    catch { version = 0; }
                }

                // Check version 2.0
                if (version == 0)
                {
                    try
                    {
                        rk = Registry.LocalMachine.OpenSubKey(netV20);
                        exactVersion = rk?.GetValue("Version")?.ToString();
                        if (!string.IsNullOrEmpty(exactVersion))
                        {
                            version = 2;
                            servicePack = rk?.GetValue("SP")?.ToString() ?? "";
                        }
                    }
                    catch { version = 0; }
                }
            }
            catch (SecurityException ex)
            {
                MessageBox.Show($"An error occurred reading the registry. The system returned this information:\n{ex.Message}",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                rk?.Close();
            }

            if (!string.IsNullOrEmpty(servicePack))
            {
                return $"{version:0.0} ({exactVersion}) Service Pack {servicePack}{full}";
            }
            return $"{version:0.0} ({exactVersion}){full}";
        }
        #endregion

        #region User Information
        private string GetUserCustomerOrganization()
        {
            try
            {
                RegistryKey rk;
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32Windows:
                        rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion", false);
                        return rk?.GetValue("RegisteredOrganization")?.ToString() ?? "Unknown";

                    case PlatformID.Win32NT:
                        string keyPath = m_Is64Bit
                            ? @"SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion"
                            : @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
                        rk = Registry.LocalMachine.OpenSubKey(keyPath, false);
                        return rk?.GetValue("RegisteredOrganization")?.ToString() ?? "Unknown";

                    default:
                        return "Unknown";
                }
            }
            catch
            {
                return "Unknown";
            }
        }

        private string GetUserCustomerName()
        {
            try
            {
                RegistryKey rk;
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32Windows:
                        rk = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion", false);
                        return rk?.GetValue("RegisteredOwner")?.ToString() ?? "Unknown";

                    case PlatformID.Win32NT:
                        string keyPath = m_Is64Bit
                            ? @"SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion"
                            : @"Software\Microsoft\Windows NT\CurrentVersion";
                        rk = Registry.LocalMachine.OpenSubKey(keyPath, false);
                        return rk?.GetValue("RegisteredOwner")?.ToString() ?? "Unknown";

                    default:
                        return "Unknown";
                }
            }
            catch
            {
                return "Unknown";
            }
        }
        #endregion

        #region OS Up Time
        private static string GetOSUptime()
        {
            try
            {
                Application.DoEvents();
                long ticks = Environment.TickCount / 1000;
                long seconds = ticks % 60;
                int minutes = (int)((ticks / 60) % 60);
                int hours = (int)((ticks / 3600) % 24);
                int days = (int)(ticks / 3600 / 24);

                string displayDays;
                if (days == 0)
                {
                    displayDays = string.Empty;
                }
                else if (days.ToString().Length == 1)
                {
                    displayDays = $" {days}:";
                }
                else
                {
                    displayDays = $"{days}:";
                }

                string displayHours;
                if (hours == 0 && days == 0)
                {
                    displayHours = string.Empty;
                }
                else if (hours.ToString().Length == 1)
                {
                    displayHours = $"0{hours}:";
                }
                else
                {
                    displayHours = $"{hours}:";
                }

                string displayMinutes;
                if (minutes == 0)
                {
                    displayMinutes = "00:";
                }
                else if (minutes.ToString().Length == 1)
                {
                    displayMinutes = $"0{minutes}:";
                }
                else
                {
                    displayMinutes = $"{minutes}:";
                }

                string displaySeconds;
                if (seconds == 0)
                {
                    displaySeconds = "00";
                }
                else if (seconds.ToString().Length == 1)
                {
                    displaySeconds = $"0{seconds}";
                }
                else
                {
                    displaySeconds = $"{seconds}";
                }

                return $"{displayDays}{displayHours}{displayMinutes}{displaySeconds}";
            }
            catch (OverflowException)
            {
                return "";
            }
        }
        #endregion

        #region Timer Code
        private void TimerTickCount_Tick(object sender, EventArgs e)
        {
            ToolStripLabelDateTime.Text = $"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}";
            ToolStripLabelUpTime.Text = $"Up Time: {GetOSUptime()}";
        }
        #endregion

        #region Buttons
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ButtonSysInfo_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("msinfo32.exe")
                {
                    WindowStyle = ProcessWindowStyle.Normal
                };
                Process.Start(startInfo);
            }
            catch (Win32Exception)
            {
                MessageBox.Show("Microsoft System Information cannot be found.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

      
    }
}
