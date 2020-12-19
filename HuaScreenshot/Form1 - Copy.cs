using PuppeteerSharp;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HuaScreenshot
{
    public partial class Form2 : Form
    {
        public delegate void MessageBoxHandler();

        Browser browser { get; set; }
        Page page { get; set; }

        public Form2()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(invokeScreenshot);
        }

        private async void invokeScreenshot()
        {
            int widthBefore = page.Viewport.Width;
            int heightBefore = page.Viewport.Height;

            this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t分辨率：this.comboBox1.SelectedItem\r\n";
            this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();

            switch (this.comboBox1.SelectedItem)
            {
                case "7680x4320":
                    await page.SetViewportAsync(new ViewPortOptions
                    {
                        Width = 4320,
                        Height = 7680
                    });
                    break;
                case "3840x2160":
                    await page.SetViewportAsync(new ViewPortOptions
                    {
                        Width = 3840,
                        Height = 2160
                    });
                    break;
                case "2560x1440":
                    await page.SetViewportAsync(new ViewPortOptions
                    {
                        Width = 2560,
                        Height = 1440
                    });
                    break;
                case "1920x1080 (实验性的)":
                    await page.SetViewportAsync(new ViewPortOptions
                    {
                        Width = 1920,
                        Height = 1080
                    });
                    break;
                case "1600x900 (实验性的)":
                    await page.SetViewportAsync(new ViewPortOptions
                    {
                        Width = 1600,
                        Height = 900
                    });
                    break;
                case "1366x768 (实验性的)":
                    await page.SetViewportAsync(new ViewPortOptions
                    {
                        Width = 1366,
                        Height = 768
                    });
                    break;
                case "1280x720 (实验性的)":
                    await page.SetViewportAsync(new ViewPortOptions
                    {
                        Width = 1280,
                        Height = 720
                    });
                    break;
                default:
                    await page.SetViewportAsync(new ViewPortOptions
                    {
                        Width = 4320,
                        Height = 7680
                    });
                    break;
            }

            this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t缩放Flash：" + this.numericUpDown1.Value + "倍\r\n";
            this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
            await page.EvaluateExpressionAsync("document.getElementsByTagName('embed')[0].Zoom(100)");
            await page.EvaluateExpressionAsync("document.getElementsByTagName('embed')[0].Zoom(" + (1 / numericUpDown1.Value * 100) + ")");

            System.Threading.Thread.Sleep(5000);
            this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t等待Flash缩放完成！\r\n";
            this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();


            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "保存截图";
            dialog.Filter = "PNG图片 (*.png) | *.png";

            this.Invoke(new MessageBoxHandler(delegate ()
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        page.ScreenshotAsync(dialog.FileName);
                        this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t" + dialog.FileName + "保存成功！\r\n";
                        this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                    }
                    catch (Exception ee)
                    {
                        this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t保存失败！" + ee.Message + "\r\n";
                        this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                    }
                }
            }));

            this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t分辨率还原\r\n";
            this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();

            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = 1920,
                Height = 1080
            });
        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t请选择谷歌内核的浏览器位置\r\n";
            this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "谷歌内核浏览器启动程序 (*.exe) | *.exe";
            openFileDialog.Title = "选择要启用调试模式的浏览器";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFileDialog openFileDialog2 = new OpenFileDialog();
                openFileDialog2.Filter = "谷歌内核浏览器启动程序 (*.exe) | *.exe";
                openFileDialog2.Title = "选择要启用调试模式的浏览器";

                await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
                browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    ExecutablePath = "E:\\Data\\ChromeCore\\ChromeCore.exe",
                    Headless = false
                });
                page = await browser.NewPageAsync();
                await page.GoToAsync("http://hua.61.com/Client.swf");
            }
        }
    }
}
