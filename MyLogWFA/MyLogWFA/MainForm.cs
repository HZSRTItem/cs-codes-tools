using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MyLogWFA
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SetTxtTime();
            string log_file = GetLogFileName();
            OpenLogFile(log_file);
        }

        /// <summary>
        /// 打开log文件
        /// </summary>
        /// <param name="log_file"></param>
        private void OpenLogFile(string log_file)
        {
            if (File.Exists(log_file))
            {
                StreamReader sr = new StreamReader(log_file);
                TxtAbstrat.Text = sr.ReadLine();
                string line = sr.ReadLine();
                while (line != null)
                {
                    RtbBody.Text += line + "\n";
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            else
            {
                StreamWriter sw = new StreamWriter(log_file);
                sw.Write("\n");
                sw.Close();
            }
        }

        /// <summary>
        /// 默认日志文件路径
        /// </summary>
        private string DefaultLogDir = @"D:\log";
        /// <summary>
        /// 当前页面是否保存
        /// </summary>
        private bool IsSave = false;

        /// <summary>
        /// 打开当前日期的日志文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTime_Click(object sender, EventArgs e)
        {
            // 日志文件是否存在
            SetTxtTime();
            string log_file = GetLogFileName();
            if (log_file != "")
            {
                OpenLogFile(log_file);
                MessageBox.Show("已经显示");
                LblIsSave.Text = "已经保存";
            }
        }

        /// <summary>
        /// 设置日志时间
        /// </summary>
        /// <returns></returns>
        private bool SetTxtTime()
        {
            string dtstr = TxtTime.Text;
            DateTime dt;
            if (DateTime.TryParse(dtstr, out dt))
            {
                return true;
            }
            else
            {
                TxtTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                return false;
            }
        }

        /// <summary>
        /// 获得日志文件名
        /// </summary>
        /// <returns>日志文件名</returns>
        private string GetLogFileName()
        {
            if (Directory.Exists(TxtDir.Text))
            {
                if (DefaultLogDir == Path.Combine(TxtDir.Text, ""))
                {
                    SetTxtTime();
                    return Path.Combine(DefaultLogDir, TxtTime.Text + ".txt");
                }
                else
                {
                    if (MessageBox.Show("log文件与默认路径不同，是否更改？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        return Path.Combine(TxtDir.Text, TxtTime.Text + ".txt");
                    }
                    else
                    {
                        return Path.Combine(DefaultLogDir, TxtTime.Text + ".txt");
                    }
                }
            }
            else
            {
                MessageBox.Show("不存在该文件夹，请检查：" + TxtDir.Text);
                return "";
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            // 日志文件是否存在
            SetTxtTime();
            string log_file = GetLogFileName();
            if (log_file != "")
            {
                StreamWriter sw = new StreamWriter(log_file);
                sw.Write(TxtAbstrat.Text + "\n");
                sw.Write(RtbBody.Text);
                sw.Close();
                LblIsSave.Text = "已经保存";
            }
        }

        /// <summary>
        /// 摘要更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtAbstrat_TextChanged(object sender, EventArgs e)
        {
            IsSave = false;
            LblIsSave.Text = "尚未保存";
        }

        /// <summary>
        /// 主体内容更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RtbBody_TextChanged(object sender, EventArgs e)
        {
            LblIsSave.Text = "尚未保存";
            IsSave = false;
        }

        /// <summary>
        /// 时间文本框更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTime_TextChanged(object sender, EventArgs e)
        {
            LblIsSave.Text = "尚未保存";
            IsSave = false;
        }

        /// <summary>
        /// 日志文件夹更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtDir_TextChanged(object sender, EventArgs e)
        {
            LblIsSave.Text = "尚未保存";
            IsSave = false;
        }
    }
}
