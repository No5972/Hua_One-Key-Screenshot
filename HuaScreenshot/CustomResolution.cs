using System;
using System.Windows.Forms;

namespace HuaScreenshot
{
    public partial class CustomResolution : Form
    {
        public int thisHeight { get; set; }
        public int thisWidth { get; set; }

        public CustomResolution()
        {
            InitializeComponent();
        }

        public CustomResolution(int width, int height)
        {
            InitializeComponent();
            this.textBox1.Value = width;
            this.textBox2.Value = height;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.thisWidth = (int)this.textBox1.Value;
            this.thisHeight = (int)this.textBox2.Value;
            this.DialogResult = DialogResult.OK;
        }

        private void CustomResolution_Load(object sender, EventArgs e)
        {
            this.textBox1.Select();
        }


    }
}
