using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace TimeAttendanceManager.Features.Masters.Employee.Classes
{
    public class StringListEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;  // opens as a modal dialog
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (svc != null)
            {
                using (Form f = new Form())
                {
                    f.Text = "Select Values";
                    f.Width = 300;
                    f.Height = 400;

                    ListBox lb = new ListBox
                    {
                        Dock = DockStyle.Fill,
                        SelectionMode = SelectionMode.MultiExtended
                    };

                    // Example items (replace with DB lookup if needed)
                    lb.Items.AddRange(new object[] { "E001", "E002", "E003", "E004" });

                    // Pre-select any already selected values
                    if (value is List<string> existingValues)
                    {
                        foreach (string val in existingValues)
                        {
                            int index = lb.Items.IndexOf(val);
                            if (index >= 0) lb.SetSelected(index, true);
                        }
                    }

                    f.Controls.Add(lb);

                    Button btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Dock = DockStyle.Bottom };
                    f.Controls.Add(btnOk);

                    if (svc.ShowDialog(f) == DialogResult.OK)
                    {
                        List<string> selected = new List<string>();
                        foreach (var item in lb.SelectedItems)
                            selected.Add(item.ToString());
                        return selected;
                    }
                }
            }
            return value;
        }
    }
}
