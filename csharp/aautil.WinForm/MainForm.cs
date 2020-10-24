using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace aautil.WinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void menuJsonView_Click(object sender, EventArgs e)
        {
            var form = MdiChildren.OfType<EPocalipse.Json.JsonView.MainForm>().FirstOrDefault()
               ?? new EPocalipse.Json.JsonView.MainForm { MdiParent = this };
            form.Show();
            form.BringToFront();
        }

        private void menuScintilla_Click(object sender, EventArgs e)
        {
            var form = MdiChildren.OfType<YLConfigTool.Form1>().FirstOrDefault()
               ?? new YLConfigTool.Form1 { MdiParent = this };
            form.Show();
            form.BringToFront();
        }
    }
}
