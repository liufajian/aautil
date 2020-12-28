using ScintillaNET;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AAUtil.WinForm.Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbFolder.Text = AppContext.BaseDirectory;
            TreeConfigFiles(tbFolder.Text);
            InitSyntaxColoring(scintilla1);
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = tbFolder.Text;
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            tbFolder.Text = folderBrowserDialog1.SelectedPath;
            TreeConfigFiles(tbFolder.Text);
        }

        private void TreeConfigFiles(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder) || !Directory.Exists(folder))
            {
                MessageBox.Show("目录不存在");
            }
            else
            {
                treeView1.Nodes.Clear();

                var tnods = Directory.EnumerateFiles(folder)
                    .Where(file => file.ToLower().EndsWith("config") || file.ToLower().EndsWith("xml"))
                    .Select(n => new TreeNode(Path.GetFileName(n))
                    {
                        Tag = n,
                        ImageKey = "book.png",
                        SelectedImageKey = "book_edit.png"
                    }).ToArray();

                treeView1.Nodes.AddRange(tnods);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            scintilla1.Text = File.ReadAllText(e.Node.Tag.ToString());
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
        }

        private void InitSyntaxColoring(Scintilla scintilla)
        {
            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 10;
            scintilla.Styles[Style.Default].BackColor = IntToColor(0xCCE8CF);
            scintilla.Styles[Style.Default].ForeColor = Color.Blue;
            scintilla.StyleClearAll();

            //无用
            scintilla.Styles[Style.Xml.Asp].ForeColor = Color.Gray;
            scintilla.Styles[Style.Xml.AspAt].ForeColor = Color.Gray;
            scintilla.Styles[Style.Xml.XcComment].ForeColor = Color.Gray;
            scintilla.Styles[Style.Xml.Default].ForeColor = Color.Blue;
            scintilla.Styles[Style.Xml.Script].ForeColor = Color.Gray;
            //
            scintilla.Styles[Style.Xml.XmlStart].ForeColor = IntToColor(0x808080);
            scintilla.Styles[Style.Xml.XmlEnd].ForeColor = IntToColor(0x808080);
            scintilla.Styles[Style.Xml.CData].ForeColor = IntToColor(0x808080);
            scintilla.Styles[Style.Xml.Question].ForeColor = IntToColor(0x808080);
            //属性
            scintilla.Styles[Style.Xml.Attribute].ForeColor = Color.Red;
            scintilla.Styles[Style.Xml.AttributeUnknown].ForeColor = Color.Red;
            //注释
            scintilla.Styles[Style.Xml.Comment].ForeColor = IntToColor(0x006400);
            //字符串
            scintilla.Styles[Style.Xml.DoubleString].ForeColor = Color.Blue;
            //
            scintilla.Styles[Style.Xml.SingleString].ForeColor = Color.Blue;
            //
            scintilla.Styles[Style.Xml.Number].ForeColor = Color.Blue;
            //其它
            scintilla.Styles[Style.Xml.Other].ForeColor = Color.Blue;
            scintilla.Styles[Style.Xml.Value].ForeColor = Color.Blue;

            //
            scintilla.Styles[Style.Xml.Entity].ForeColor = IntToColor(0xA31515);
            scintilla.Styles[Style.Xml.Tag].ForeColor = IntToColor(0xA31515);
            scintilla.Styles[Style.Xml.TagUnknown].ForeColor = IntToColor(0xA31515);

            scintilla.Lexer = Lexer.Xml;
        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        private void tbFolder_Click(object sender, EventArgs e)
        {
            tbFolder.Select(0, tbFolder.Text.Length);
        }

        private void tbFolder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == ' ')
            {
                e.Handled = true;
                TreeConfigFiles(tbFolder.Text);
            }
        }
    }
}
