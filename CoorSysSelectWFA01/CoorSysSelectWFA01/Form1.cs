using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CoorSysSelectWFA01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CoorSysForm sIMainForm = new CoorSysForm();
            sIMainForm.Show();

            //for (int i=0; i<8000; i++)
            //{
            //    //进程的名称
            //    string fileName = "cmd.exe";
            //    //测试参数
            //    string para = @"D:\CodeProjects\SampleIdentification\CoorSysSelect\CoorSysSelectWFA01\ss\projinfo.exe epsg:4326";
            //    Process p = new Process(); //声明
            //    p.StartInfo.CreateNoWindow = true;         // 不创建新窗口    
            //    p.StartInfo.UseShellExecute = false;       // 不启用shell启动进程  
            //    p.StartInfo.RedirectStandardInput = true;  // 重定向输入    
            //    p.StartInfo.RedirectStandardOutput = true; // 重定向标准输出    
            //    p.StartInfo.RedirectStandardError = true;  // 重定向错误输出  
            //    p.StartInfo.FileName = fileName;
            //    p.Start();
            //    p.StandardInput.WriteLine(para + " &exit");
            //    p.StandardInput.AutoFlush = true;
            //    p.StandardInput.Close();
            //    //p.StandardOutput.ReadToEnd();
            //    // p.StandardError.ReadToEnd()
            //    string output = p.StandardOutput.ReadToEnd();
            //    //  p.OutputDataReceived += new DataReceivedEventHandler(processOutputDataReceived);
            //    p.WaitForExit();//参数单位毫秒，在指定时间内没有执行完则强制结束，不填写则无限等待
            //    p.Close();
            //    richTextBox1.Text += i.ToString();
            //    richTextBox1.Text += " ";
            //}
        }
    }
}
