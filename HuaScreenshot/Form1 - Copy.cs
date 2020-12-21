using PuppeteerSharp;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HuaScreenshot
{
    public partial class Form2 : Form
    {
        public delegate void MessageBoxHandler();
        public string browserPath = "";

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
            this.button1.Enabled = false;
            if (page == null) {
                MessageBox.Show("错误：未能找到浏览器实例，请先点击启动浏览器。", "小花仙 - 一键截图", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.button1.Enabled = true;
                return;
            }

            if (page.IsClosed)
            {
                MessageBox.Show("错误：已打开的浏览器实例已经被关闭，请先点击启动浏览器。", "小花仙 - 一键截图", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.button1.Enabled = true;
                return;
            }

            try
            {
                int widthBefore = page.Viewport.Width;
                int heightBefore = page.Viewport.Height;

                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t分辨率：" + this.comboBox1.SelectedItem + "\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();

                switch (this.comboBox1.SelectedItem)
                {
                    case "4320x7680":
                        await page.SetViewportAsync(new ViewPortOptions
                        {
                            Width = 4320,
                            Height = 7680
                        });
                        break;
                    case "2160x3840":
                        await page.SetViewportAsync(new ViewPortOptions
                        {
                            Width = 2160,
                            Height = 3840
                        });
                        break;
                    case "1440x2560":
                        await page.SetViewportAsync(new ViewPortOptions
                        {
                            Width = 1440,
                            Height = 2560
                        });
                        break;
                    case "1080x1920 (实验性的)":
                        await page.SetViewportAsync(new ViewPortOptions
                        {
                            Width = 1080,
                            Height = 1920
                        });
                        break;
                    case "900x1600 (实验性的)":
                        await page.SetViewportAsync(new ViewPortOptions
                        {
                            Width = 900,
                            Height = 1600
                        });
                        break;
                    case "768x1366 (实验性的)":
                        await page.SetViewportAsync(new ViewPortOptions
                        {
                            Width = 768,
                            Height = 1366
                        });
                        break;
                    case "720x1280 (实验性的)":
                        await page.SetViewportAsync(new ViewPortOptions
                        {
                            Width = 720,
                            Height = 1280
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
                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t调整Flash视野：(" + this.numericUpDown2.Value + "," + this.numericUpDown3.Value + ")\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();

                new Task(() => page.EvaluateExpressionAsync(@"document.getElementsByTagName('embed')[0].Zoom(500);
                document.getElementsByTagName('embed')[0].Zoom(" + (1 / numericUpDown1.Value * 100) + @");
                document.getElementsByTagName('embed')[0].Pan(" + numericUpDown2.Value + "," + numericUpDown3.Value + ",1)")).RunSynchronously();

                System.Threading.Thread.Sleep(5000);
                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t等待Flash缩放完成！\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();

                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t执行截图，请稍候！\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                string savePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".png";

                await page.ScreenshotAsync(savePath);
                System.Threading.Thread.Sleep(5000);
                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t" + savePath + "保存成功！\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();

                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t分辨率还原\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();


                await page.SetViewportAsync(new ViewPortOptions
                {
                    Width = widthBefore,
                    Height = heightBefore
                });


                await page.EvaluateExpressionAsync("document.getElementsByTagName('embed')[0].Zoom(1000)");
            } catch (Exception e)
            {
                MessageBox.Show("截图过程中发生了未知错误：" + e.Message + "\n" + e.StackTrace, "小花仙 - 一键截图", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.button1.Enabled = true;
            }
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
                this.browserPath = openFileDialog.FileName.Substring(0, openFileDialog.FileName.LastIndexOf("\\"));

                if (this.useNoArgs.Checked)
                {
                    browser = await Puppeteer.LaunchAsync(new LaunchOptions
                    {
                        ExecutablePath = openFileDialog.FileName,
                        Headless = false,
                        DefaultViewport =
                        {
                            Width = 1067,
                            Height = 600
                        }
                    });
                    page = await browser.NewPageAsync();
                    await page.GoToAsync("http://hua.61.com/play.shtml");
                }
                else if (this.useInternalFlash.Checked)
                {
                    if (!File.Exists(this.browserPath + "\\User Data\\PepperFlash\\pepflashplayer.dll"))
                    {
                        MessageBox.Show("所选浏览器的目录下未能找到Flash的组件DLL，Flash可能无法正常加载，建议使用手动指定Flash的模式。（留意一下PepperFlash文件夹是否放在了浏览器的某个子目录下）", "小花仙 - 一键截图", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    // await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
                    browser = await Puppeteer.LaunchAsync(new LaunchOptions
                    {
                        ExecutablePath = openFileDialog.FileName,
                        Headless = false,
                        Args = new string[]{ 
                            "--ppapi-flash-path=\"" + this.browserPath + "\\User Data\\PepperFlash\\pepflashplayer.dll\"",
                            "--ppapi-flash-version=99.0.0.999"
                        },
                        DefaultViewport =
                        {
                            Width = 1067,
                            Height = 600
                        }
                    });
                    page = await browser.NewPageAsync();
                    await page.GoToAsync("http://hua.61.com/play.shtml");
                }
                else if (this.useSpecificFlash.Checked)
                {
                    if (File.Exists(this.textBox2.Text) && this.textBox2.Text.EndsWith(".dll"))
                    {
                        // await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
                        browser = await Puppeteer.LaunchAsync(new LaunchOptions
                        {
                            ExecutablePath = openFileDialog.FileName,
                            Headless = false,
                            Args = new string[]{
                                "--ppapi-flash-path=\"" + this.textBox2.Text,
                                "--ppapi-flash-version=99.0.0.999"
                            },
                            DefaultViewport =
                            {
                                Width = 1067,
                                Height = 600
                            }
                        });
                        page = await browser.NewPageAsync();
                        await page.GoToAsync("http://hua.61.com/play.shtml");
                    }
                    else
                    {
                        MessageBox.Show("错误：指定位置没有找到可用的DLL文件！请点击右侧的...按钮选择正确的pepflashplayer.dll文件，如果无法找到此文件可到网络上搜索下载PPAPI Flash Player绿色版。", "小花仙 - 一键截图", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            KillProcess("CHROMEDRIVER");
            Application.Exit();
        }

        private void useSpecificFlash_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                this.textBox2.Enabled = true;
                this.button4.Enabled = true;
            }
            else
            {
                this.textBox2.Enabled = false;
                this.button4.Enabled = false;
            }
        }

        public static void KillProcess(string strProcessesByName)//关闭线程
        {
            foreach (Process p in Process.GetProcesses())//GetProcessesByName(strProcessesByName))
            {
                if (p.ProcessName.ToUpper().Contains(strProcessesByName))
                {
                    try
                    {
                        p.Kill();
                        p.WaitForExit(); // possibly with a timeout
                    }
                    catch (Win32Exception e)
                    {
                        MessageBox.Show(e.Message.ToString());   // process was terminating or can't be terminated - deal with it
                    }
                    catch (InvalidOperationException e)
                    {
                        MessageBox.Show(e.Message.ToString()); // process has already exited - might be able to let this one go
                    }
                }
            }
        }
    }
}
