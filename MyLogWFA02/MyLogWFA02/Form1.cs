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

namespace MyLogWFA02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TxtDir.Text = DefaultLogDir;
            OpenLogFile(GetLogFileName(DateTime.Now));
        }

        /// <summary>
        /// 默认日志文件路径
        /// </summary>
        private string DefaultLogDir = @"D:\log";
        /// <summary>
        /// 当前页面是否保存
        /// </summary>
        private bool IsSave = false;

        private DateTime CurrentDateTime = DateTime.Now;

        #region 私有方法
        /// <summary>
        /// 获得日志文件名
        /// </summary>
        /// <returns>日志文件名</returns>
        private string GetLogFileName(DateTime dateTime)
        {
            string log_file = dateTime.ToString("yyyy-MM-dd") + ".txt";
            if (DefaultLogDir == Path.Combine(TxtDir.Text, ""))
            {
                return Path.Combine(DefaultLogDir, log_file);
            }
            else
            {
                if (MessageBox.Show("log文件与默认路径不同，是否更改？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    return Path.Combine(TxtDir.Text, log_file);
                }
                else
                {
                    return Path.Combine(DefaultLogDir, log_file);
                }
            }
        }

        /// <summary>
        /// 打开log文件
        /// </summary>
        /// <param name="log_file"></param>
        private bool OpenLogFile(string log_file)
        {
            if (File.Exists(log_file))
            {
                StreamReader sr = new StreamReader(log_file);
                TxtAbstrat.Text = sr.ReadLine();
                string tt = "";
                string line = sr.ReadLine();
                while (line != null)
                {
                    tt += line + "\n";
                    line = sr.ReadLine();
                }
                RtbBody.Text = tt;
                sr.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 渲染这一天
        /// </summary>
        /// <param name="current_time">当前这一天的时间</param>
        /// <param name="next_time">下一个天的时间</param>
        /// <returns>是不是存在下一个天的日志文件</returns>
        private bool RenderDay(DateTime current_time, DateTime next_time)
        {
            // 判断是否可以渲染
            string current_log_file = GetLogFileName(current_time);
            // 保存当前的日志文件
            SaveLogFile(current_log_file);
            string next_log_file = GetLogFileName(next_time);
            DtpLog.Value = next_time;
            CurrentDateTime = next_time;
            if (File.Exists(next_log_file))
            {
                // 打开日志文件
                OpenLogFile(next_log_file);
                return true;
            }
            else
            {
                TxtAbstrat.Text = "";
                RtbBody.Text = "";
                return false;
            }
        }

        /// <summary>
        /// 保存log文件
        /// </summary>
        /// <param name="log_file"></param>
        /// <returns></returns>
        private bool SaveLogFile(string log_file)
        {
            try
            {
                StreamWriter sw = new StreamWriter(log_file);
                sw.Write(TxtAbstrat.Text + "\n");
                sw.Write(RtbBody.Text);
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 信息更改
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
        /// 日志文件夹更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtDir_TextChanged(object sender, EventArgs e)
        {
            LblIsSave.Text = "尚未保存";
            IsSave = false;
        }

        /// <summary>
        /// 关闭窗口时候保存文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLogFile(GetLogFileName(DtpLog.Value));
        }
        #endregion

        #region 点击事件
        /// <summary>
        /// 选择保存的文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectDir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("不支持更改日志文件保存的位置");
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            SaveLogFile(GetLogFileName(DtpLog.Value));
            IsSave = true;
            LblIsSave.Text = "已经保存";
        }

        /// <summary>
        /// 上一天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            DateTime qianyitian = CurrentDateTime.AddDays(-1);
            if(!RenderDay(CurrentDateTime, qianyitian))
            {
                TxtAbstrat.Text = "";
                RtbBody.Text = "";
            }
        }

        /// <summary>
        /// 下一天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNext_Click(object sender, EventArgs e)
        {
            DateTime xiayitian = CurrentDateTime.AddDays(1);
            if (!RenderDay(CurrentDateTime, xiayitian))
            {
                TxtAbstrat.Text = "";
                RtbBody.Text = "";
            }
        }


        #endregion

        private void DtpLog_ValueChanged(object sender, EventArgs e)
        {
            RenderDay(CurrentDateTime, DtpLog.Value);
        }
    }
}
