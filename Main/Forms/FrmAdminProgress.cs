using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// In VB.NET, controls are often accessible by default between forms, 
/// but in C#, WinForms controls are private by default in the designer-generated code. 
/// Here’s how to bridge the gap between VB.NET and C# access patterns:
/// </summary>
namespace TimeAttendanceManager.Main.Forms
{
    public partial class FrmAdminProgress : Form
    {
        public FrmAdminProgress()
        {
            InitializeComponent();
        }

    }
}
