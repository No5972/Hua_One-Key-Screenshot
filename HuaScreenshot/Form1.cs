using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HuaScreenshot
{
    public partial class Form1 : Form
    {
        public delegate void MessageBoxHandler();
        public string browserPath = "";

        public Form1()
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
            ChromeOptions options = new ChromeOptions();
            options.DebuggerAddress = "127.0.0.1:9222";
            this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t查找浏览器\r\n";
            this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
            ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;//关闭cmd窗口
            try
            {
                ChromeDriver driver = new ChromeDriver(driverService, options, TimeSpan.FromSeconds(7));
                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t已找到浏览器：" + driver.SessionId + "\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();

                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t开启设备模拟：" + this.comboBox1.SelectedItem + "\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                Dictionary<string, object> emulationParams = new Dictionary<string, object>();
                emulationParams.Add("mobile", false);
                switch (this.comboBox1.SelectedItem)
                {
                    case "4320x7680":
                        emulationParams.Add("width", 4320); emulationParams.Add("height", 7680);
                        break;
                    case "2160x3840":
                        emulationParams.Add("width", 2160); emulationParams.Add("height", 3840);
                        break;
                    case "1440x2560":
                        emulationParams.Add("width", 1440); emulationParams.Add("height", 2560);
                        break;
                    case "1080x1920 (实验性的)":
                        emulationParams.Add("width", 1080); emulationParams.Add("height", 1920);
                        break;
                    case "900x1600 (实验性的)":
                        emulationParams.Add("width", 900); emulationParams.Add("height", 1600);
                        break;
                    case "768x1366 (实验性的)":
                        emulationParams.Add("width", 768); emulationParams.Add("height", 1366);
                        break;
                    case "720x1280 (实验性的)":
                        emulationParams.Add("width", 720); emulationParams.Add("height", 1280);
                        break;
                    default:
                        emulationParams.Add("width", 4320); emulationParams.Add("height", 7680);
                        break;
                }

                emulationParams.Add("deviceScaleFactor", 1);
                driver.ExecuteChromeCommand("Emulation.setDeviceMetricsOverride", emulationParams);
                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t已开启设备模拟\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();

                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t缩放Flash：" + this.numericUpDown1.Value + "倍\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                try
                {
                    driver.ExecuteScript("document.getElementsByTagName('embed')[0].Zoom(100)");
                    driver.ExecuteScript("document.getElementsByTagName('embed')[0].Zoom(" + (1 / numericUpDown1.Value * 100) + ")");
                }
                catch (WebDriverException e)
                {
                    MessageBox.Show("错误：没有打开小花仙页面，请在浏览器打开http://hua.61.com/play.shtml", "小花仙 一键截图", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t关闭设备模拟\r\n";
                    this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                    driver.ExecuteChromeCommand("Emulation.clearDeviceMetricsOverride", new Dictionary<string, object>());
                    this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t已关闭设备模拟\r\n";
                    this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                    return;
                }
                System.Threading.Thread.Sleep(3000);
                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t已缩放Flash\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t调整Flash视野：(" + this.numericUpDown2.Value + "," + this.numericUpDown3.Value + ")\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                driver.ExecuteScript("document.getElementsByTagName('embed')[0].Pan(" + numericUpDown2.Value + "," + numericUpDown3.Value + ",1)");
                System.Threading.Thread.Sleep(2000);
                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t已调整Flash视野\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();

                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t执行截图，请稍候！\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                Dictionary<string, object> captureParam = new Dictionary<string, object>();
                captureParam.Add("fromSurface", true);
                object screenshotObject = driver.ExecuteChromeCommandWithResult("Page.captureScreenshot", captureParam);
                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t已完成执行截图\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();

                Dictionary<string, object> screenshotResult = screenshotObject as Dictionary<string, object>;
                string screenshotData = screenshotResult["data"] as string;

                Screenshot screenshot = new Screenshot(screenshotData);

                string savePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".png";

                try
                {
                    File.WriteAllBytes(savePath, screenshot.AsByteArray);
                    this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t" + savePath + "保存成功！\r\n";
                    this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                }
                catch (Exception ee)
                {
                    this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t保存失败！" + ee.Message + "\r\n";
                    this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                }

                /*
                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t请选择保存路径\r\n";
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
                            File.WriteAllBytes(dialog.FileName, screenshot.AsByteArray);
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
                */


                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t关闭设备模拟\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                driver.ExecuteChromeCommand("Emulation.clearDeviceMetricsOverride", new Dictionary<string, object>());
                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t已关闭设备模拟\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\tFlash缩放还原\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                driver.ExecuteScript("document.getElementsByTagName('embed')[0].Zoom(500)");
                this.textBox1.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "\t已完成Flash缩放还原\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length; this.textBox1.ScrollToCaret();
                driver.Dispose();
            } catch (WebDriverException wde)
            {
                MessageBox.Show("未能找到浏览器实例或者已经打开的浏览器实例已被关闭，请先点击启动浏览器。", "小花仙 - 一键截图", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.button1.Enabled = true;
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
                    Process.Start(openFileDialog.FileName, "http://hua.61.com/play.shtml --remote-debugging-port=9222  --user-data-dir=\"D:\\selenium\\AutomationProfile\"");
                }
                else if (this.useInternalFlash.Checked)
                {
                    if (!File.Exists(this.browserPath + "\\User Data\\PepperFlash\\pepflashplayer.dll"))
                    {
                        MessageBox.Show("所选浏览器的目录下未能找到Flash的组件DLL，Flash可能无法正常加载，建议使用手动指定Flash的模式。（留意一下PepperFlash文件夹是否放在了浏览器的某个子目录下）", "小花仙 - 一键截图", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    Process.Start(openFileDialog.FileName, "http://hua.61.com/play.shtml --remote-debugging-port=9222  --user-data-dir=\"D:\\selenium\\AutomationProfile\"  --ppapi-flash-path=\"" + this.browserPath + "\\User Data\\PepperFlash\\pepflashplayer.dll\" --ppapi-flash-version=99.0.0.999");
                } 
                else if (this.useSpecificFlash.Checked)
                {
                    if (File.Exists(this.textBox2.Text) && this.textBox2.Text.EndsWith(".dll"))
                    {
                        Process.Start(openFileDialog.FileName, "http://hua.61.com/play.shtml --remote-debugging-port=9222  --user-data-dir=\"D:\\selenium\\AutomationProfile\"  --ppapi-flash-path=\"" + this.textBox2.Text + "\" --ppapi-flash-version=99.0.0.999");
                    } else
                    {
                        MessageBox.Show("错误：指定位置没有找到可用的DLL文件！请点击右侧的...按钮选择正确的pepflashplayer.dll文件，如果无法找到此文件可到网络上搜索下载PPAPI Flash Player绿色版。", "小花仙 - 一键截图", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            KillProcess("chromedriver.exe");
            Application.Exit();
        }

        private void useSpecificFlash_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                this.textBox2.Enabled = true;
                this.button4.Enabled = true;
            } else
            {
                this.textBox2.Enabled = false;
                this.button4.Enabled = false;
            }
        }

        public static void KillProcess(string strProcessesByName)//关闭线程
        {
            foreach (Process p in Process.GetProcesses())//GetProcessesByName(strProcessesByName))
            {
                if (p.ProcessName.ToUpper().Contains("你要关闭的进程名字"))
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
