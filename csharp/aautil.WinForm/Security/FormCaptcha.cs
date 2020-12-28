using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace AAUtil.WinForm.Security
{
    public partial class FormCaptcha : Form
    {
        public FormCaptcha()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = Captcha.GenerateCaptchaImage(150, 50, "454534");
            var ms = new System.IO.MemoryStream(result.CaptchaByteData);
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new MSCaptcha.CaptchaImage().RenderImage();
        }
    }
}
