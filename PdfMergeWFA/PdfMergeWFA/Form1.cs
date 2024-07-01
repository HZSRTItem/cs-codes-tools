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
using System.Diagnostics;

namespace PdfMergeWFA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ofd = new OpenFileDialog();
            ofd.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.Title = "添加PDF文件";
            ofd.Multiselect = true;
            sfd = new SaveFileDialog();
            sfd.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            sfd.Title = "导出PDF文件";
        }

        OpenFileDialog ofd = null;
        SaveFileDialog sfd = null;

        private void TsbtnAdd_Click(object sender, EventArgs e)
        {
            if (!(ofd.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            AddPDFFile(ofd.FileNames);

        }

        private void AddPDFFile(string[] pdf_fn)
        {
            foreach (string item in pdf_fn)
            {
                AddPDFFile(item);
            }
        }

        private void AddPDFFile(string pdf_fn)
        {
            int i_row = DgvPdfList.Rows.Add();
            DgvPdfList.Rows[i_row].Cells[0].Value = i_row + 1;
            DgvPdfList.Rows[i_row].Cells[1].Value = true;
            DgvPdfList.Rows[i_row].Cells[2].Value = Path.GetFileNameWithoutExtension(pdf_fn);
            DgvPdfList.Rows[i_row].Cells[3].Value = Path.GetDirectoryName(pdf_fn);
            DgvPdfList.Rows[i_row].Cells[4].Value = Path.GetFullPath(pdf_fn);
        }

        private void TsbtnUp_Click(object sender, EventArgs e)
        {
            if (DgvPdfList.SelectedCells.Count == 0)
            {
                return;
            }
            DataGridViewCell d = DgvPdfList.SelectedCells[0];
            int r = d.RowIndex;
            int c = d.ColumnIndex;
            if (d.RowIndex == 0 | d.RowIndex > DgvPdfList.RowCount - 2)
            {
                return;
            }

            DataGridViewRow dgvr = DgvPdfList.Rows[r - 1];//获取选中行的上一行
            DgvPdfList.Rows.RemoveAt(r - 1);//删除原选中行的上一行
            DgvPdfList.Rows.Insert(r, dgvr);//将选中行的上一行插入到选中行的后面

            DgvPdfList.CurrentCell = DgvPdfList.Rows[r - 1].Cells[c];
        }

        private void TsbtnDown_Click(object sender, EventArgs e)
        {
            if (DgvPdfList.SelectedCells.Count == 0)
            {
                return;
            }
            DataGridViewCell d = DgvPdfList.SelectedCells[0];
            int r = d.RowIndex;
            int c = d.ColumnIndex;
            if (d.RowIndex >= DgvPdfList.RowCount - 2)
            {
                return;
            }

            DataGridViewRow dgvr = DgvPdfList.Rows[r + 1];//获取选中行的上一行
            DgvPdfList.Rows.RemoveAt(r + 1);//删除原选中行的上一行
            DgvPdfList.Rows.Insert(r, dgvr);//将选中行的上一行插入到选中行的后面

            DgvPdfList.CurrentCell = DgvPdfList.Rows[r + 1].Cells[c];
        }

        private void TsmiAdd_Click(object sender, EventArgs e)
        {
            TsbtnAdd_Click(sender, e);
        }

        private void TsmiExport_Click(object sender, EventArgs e)
        {

        }

        private void TsbtnDelete_Click(object sender, EventArgs e)
        {
            if (DgvPdfList.SelectedCells.Count == 0)
            {
                return;
            }
            DataGridViewCell d = DgvPdfList.SelectedCells[0];

            if (d.RowIndex >= DgvPdfList.RowCount - 1)
            {
                return;
            }

            int r = d.RowIndex;
            int c = d.ColumnIndex;

            DgvPdfList.Rows.RemoveAt(r);//删除原选中行的上一行

            DgvPdfList.CurrentCell = DgvPdfList.Rows[r].Cells[c];
        }

        private void TsmiUp_Click(object sender, EventArgs e)
        {
            TsbtnUp_Click(sender, e);
        }

        private void TsmiDown_Click(object sender, EventArgs e)
        {
            TsbtnDown_Click(sender, e);
        }

        private void TsmiDelete_Click(object sender, EventArgs e)
        {
            TsbtnDelete_Click(sender, e);
        }

        private void TsbtnExport_Click(object sender, EventArgs e)
        {
            string out_pdf_fn = textBox2.Text;
            if (out_pdf_fn == "")
            {
                MessageBox.Show("Please select out put pdf file.");
                return;
            }

            string[] out_list = new string[DgvPdfList.Rows.Count - 1];


            for (int i = 0; i < DgvPdfList.Rows.Count - 1; i++)
            {
                string fn = DgvPdfList.Rows[i].Cells[4].Value.ToString();
                if (!File.Exists(fn))
                {
                    MessageBox.Show("Can not find file: " + fn + "\nPlease delete.");
                    return;
                }
                out_list[i] = fn;
            }

            if (!Directory.Exists(Path.GetDirectoryName(out_pdf_fn)))
            {
                MessageBox.Show("Can not find directory: " + Path.GetDirectoryName(out_pdf_fn) + "\nPlease make directory.");
                return;
            }

            if (File.Exists(out_pdf_fn))
            {
                if (MessageBox.Show("Whether to overwrite the file: " + out_pdf_fn, "Yes or No",
                     MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                {
                    return;
                }
            }

            string out_f_str = "";
            for (int i = 0; i < out_list.Length; i++)
            {
                out_f_str += string.Format("\"{0}\" ", out_list[i]);
            }

            string cmd_line = string.Format("{0} -o {1} {2} &exit", GetExe_pdfmerge(), addyh(out_pdf_fn), out_f_str);


            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    // 是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true; // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true; // 由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true; // 重定向标准错误输出
            p.StartInfo.CreateNoWindow = true; // 不显示程序窗口
            p.Start(); // 启动程序
            p.StandardInput.WriteLine(cmd_line); // 向cmd窗口发送输入信息
            p.StandardInput.AutoFlush = true;
            p.WaitForExit(); // 等待程序执行完退出进程
            p.Close();

            MessageBox.Show("Success save to " + out_pdf_fn);
        }

        private string GetExe_pdfmerge()
        {
            // "D:\CodeProjects\PyPkgs\PyPdfMerge\dist\pdfmerge\pdfmerge.exe"
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pdfmerge", "pdfmerge.exe");
        }

        private string addyh(string line)
        {
            return "\"" + line + "\"";
        }

        private void BtnOutFile_Click(object sender, EventArgs e)
        {
            if (!(sfd.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            textBox2.Text = sfd.FileName;
        }
    }

    /// <summary>
    /// 命令行运行
    /// </summary>
    class CmdRun
    {
        /// <summary>
        /// 命令行输出信息
        /// </summary>
        public static string OutInfo = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public static string ErrorInfo = "";

        /// <summary>
        /// 运行命令行信息
        /// </summary>
        /// <param name="command_line">命令</param>
        /// <returns>是否发生错误</returns>
        public static bool run(string command_line)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    // 是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true; // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true; // 由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true; // 重定向标准错误输出
            p.StartInfo.CreateNoWindow = true; // 不显示程序窗口
            p.Start(); // 启动程序
            string in_str = command_line;
            p.StandardInput.WriteLine(in_str + " &exit"); // 向cmd窗口发送输入信息
            p.StandardInput.AutoFlush = true;
            OutInfo = p.StandardOutput.ReadToEnd(); // 获取cmd窗口的输出信息
            ErrorInfo = p.StandardError.ReadToEnd();
            p.WaitForExit(); // 等待程序执行完退出进程
            p.Close();
            int i = 0;
            int n = 0;
            for (; i < OutInfo.Length; i++)
            {

                n += OutInfo[i] == '\n' ? 1 : 0;
                if (n == 4)
                {
                    break;
                }
            }
            OutInfo = OutInfo.Substring(i + 1);
            if (ErrorInfo != "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 运行命令，打印调试信息
        /// </summary>
        /// <param name="run_line">运行命令</param>
        /// <returns>错误代码</returns>
        public static int RunLine(string run_line)
        {
            DebugInfo.WriteLineDubeg("    runline: " + run_line);
            if (run(run_line))
            {
                DebugInfo.WriteLineDubeg("    outinfo: " + CmdRun.OutInfo);
                DebugInfo.WriteLineDubeg("  success");
                return 0;
            }
            else
            {
                DebugInfo.WriteLineDubeg("    outinfo: " + CmdRun.OutInfo);
                DebugInfo.WriteLineDubeg("    errorinfo: " + CmdRun.ErrorInfo);
                DebugInfo.WriteLineDubeg("  not success");
                return 1;
            }
        }
    }

    /// <summary>
    /// 调试信息
    /// </summary>
    class DebugInfo
    {
        /// <summary>
        /// 是否输出调试信息
        /// </summary>
        public static bool IsDebug = false;

        /// <summary>
        /// 输出调试信息
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLineDubeg(object info)
        {
            if (IsDebug)
            {
                Console.WriteLine(info);
            }
        }
    }

}
