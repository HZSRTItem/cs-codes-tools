using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace SampleIdentifWFA01
{
    public partial class Tfr2PngForm : Form
    {
        public Tfr2PngForm()
        {
            InitializeComponent();
            bool t = Init();
            if (!t)
            {
                Close();
            }
        }

        XmlSerializer xmlSearializer = new XmlSerializer(typeof(List<TfrFeatInfo>));
        OpenFileDialog ofd = new OpenFileDialog();
        FolderBrowserDialog fbd = new FolderBrowserDialog();


        public bool Init()
        {
            if (!CmdRun.run(UseExes.srt_tfrinfo()))
            {
                MessageBox.Show("找不到文件：srt_tfrinfo.exe", "提示");
                return false;
            }
            if (!CmdRun.run(UseExes.srt_tfr2png()))
            {
                MessageBox.Show("找不到文件：srt_tfr2png.exe", "提示");
                return false;
            }
            fbd.SelectedPath = Directory.GetCurrentDirectory();
            TxtSaveDir.Text = Directory.GetCurrentDirectory();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            return true;
        }

        #region 事件
        private void BtnTfrFilesAdd_Click(object sender, EventArgs e)
        {
            ofd.Title = "Tfrcrd Files";
            ofd.Filter = "Tfrcrd File (*.tfrecord)|*.tfrecord";
            ofd.Multiselect = true;

            if (!(ofd.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            for (int i = 0; i < ofd.FileNames.Length; i++)
            {
                AddTfrFile(ofd.FileNames[i]);
            }
        }

        private void BtnTfrFilesDelete_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection select_row = DgvTfrFiles.SelectedRows;
            for (int i = 0; i < select_row.Count; i++)
            {
                DgvTfrFiles.Rows.Remove(select_row[i]);
            }
            if (DgvTfrFiles.Rows.Count == 0)
            {
                ClearAll();
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            // 检查数据格式
            if (DgvTfrFiles.Rows.Count == 0)
            {
                return;
            }

            string[] lines = TxtImageSize.Text.Split(',');
            try
            {
                int.Parse(lines[0]);
                int.Parse(lines[1]);
            }
            catch
            {
                MessageBox.Show("清输入正确的图像大小 `int,int`: width,height");
                return;
            }

            if (!CheckStringNumber(TxtBMin.Text))
            {
                MessageBox.Show("请检查 B min: " + TxtBMin.Text);
            }
            if (!CheckStringNumber(TxtGMin.Text))
            {
                MessageBox.Show("请检查 G min: " + TxtGMin.Text);
            }
            if (!CheckStringNumber(TxtRMin.Text))
            {
                MessageBox.Show("请检查 R min: " + TxtRMin.Text);
            }
            if (!CheckStringNumber(TxtBMax.Text))
            {
                MessageBox.Show("请检查 B max: " + TxtBMax.Text);
            }
            if (!CheckStringNumber(TxtGMax.Text))
            {
                MessageBox.Show("请检查 G max: " + TxtGMax.Text);
            }
            if (!CheckStringNumber(TxtRMax.Text))
            {
                MessageBox.Show("请检查 R max: " + TxtRMax.Text);
            }

            string info = "";
            if (TxtBMin.Text != "")
            {
                info += " -bdmin " + TxtBMin.Text;
            }
            if (TxtGMin.Text != "")
            {
                info += " -gdmin " + TxtGMin.Text;
            }
            if (TxtRMin.Text != "")
            {
                info += " -rdmin " + TxtRMin.Text;
            }
            if (TxtBMax.Text != "")
            {
                info += " -bdmax " + TxtBMax.Text;
            }
            if (TxtGMax.Text != "")
            {
                info += " -gdmax " + TxtGMax.Text;
            }
            if (TxtRMax.Text != "")
            {
                info += " -rdmax " + TxtRMax.Text;
            }
            string run_line = "";
            if (CbSrt.Text != " ")
            {
                info += " -srt " + CbSrt.Text;
            }
            for (int i = 0; i < DgvTfrFiles.Rows.Count - 1; i++)
            {
                run_line += UseExes.srt_tfr2png(DgvTfrFiles.Rows[i].Cells[0].Value.ToString(),
                    "-size", utils.add_yh(TxtImageSize.Text),
                    "-b", CbBName.Text,
                    "-g", CbGName.Text,
                    "-r", CbRName.Text,
                    "-dir", TxtSaveDir.Text, info);
                run_line += "\n";
            }
            RtbRun.Text = run_line;
        }

        private void BtnTfrFilesUp_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection select_row = DgvTfrFiles.SelectedRows;
            if (select_row.Count == 0)
            {
                return;
            }
            for (int i = 0; i < DgvTfrFiles.Rows.Count; i++)
            {
                if (DgvTfrFiles.Rows[i] == select_row[0])
                {
                    if (i == 0)
                    {
                        return;
                    }
                    for (int j = 0; j < DgvTfrFiles.Rows[i].Cells.Count; j++)
                    {
                        var v = DgvTfrFiles.Rows[i].Cells[j].Value;
                        DgvTfrFiles.Rows[i].Cells[j].Value = DgvTfrFiles.Rows[i - 1].Cells[j].Value;
                        DgvTfrFiles.Rows[i - 1].Cells[j].Value = v;
                    }
                    DgvTfrFiles.Rows[i - 1].Selected = true;
                    DgvTfrFiles.Rows[i].Selected = false;
                    break;
                }
            }
        }

        private void BtnTfrFilesDown_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection select_row = DgvTfrFiles.SelectedRows;
            if (select_row.Count == 0)
            {
                return;
            }
            for (int i = 0; i < DgvTfrFiles.Rows.Count; i++)
            {
                if (DgvTfrFiles.Rows[i] == select_row[0])
                {
                    if (i == DgvTfrFiles.Rows.Count - 2)
                    {
                        return;
                    }
                    for (int j = 0; j < DgvTfrFiles.Rows[i].Cells.Count; j++)
                    {
                        var v = DgvTfrFiles.Rows[i].Cells[j].Value;
                        DgvTfrFiles.Rows[i].Cells[j].Value = DgvTfrFiles.Rows[i + 1].Cells[j].Value;
                        DgvTfrFiles.Rows[i + 1].Cells[j].Value = v;
                    }
                    DgvTfrFiles.Rows[i + 1].Selected = true;
                    DgvTfrFiles.Rows[i].Selected = false;
                    break;
                }
            }
        }

        private void BtnRemoveAll_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        #endregion

        #region 辅助函数

        /// <summary>
        /// 添加一个tfr文件
        /// </summary>
        /// <param name="tfr_file"></param>
        /// <returns></returns>
        private bool AddTfrFile(string tfr_file)
        {
            if (CmdRun.run(UseExes.srt_tfrinfo(tfr_file, "--xml")))
            {
                List<TfrFeatInfo> tfrFeatInfos;
                using (StringReader sr = new StringReader(CmdRun.OutInfo))
                {
                    tfrFeatInfos = xmlSearializer.Deserialize(sr) as List<TfrFeatInfo>;
                }
                if (DgvTfrFiles.Rows.Count == 1)
                {
                    int i0 = DgvTfrFiles.Rows.Add();
                    DgvTfrFiles.Rows[i0].Cells[0].Value = tfr_file;
                    for (int i = 0; i < tfrFeatInfos.Count; i++)
                    {
                        int i_rows = DgvTfrInfo.Rows.Add();
                        DgvTfrInfo.Rows[i_rows].Cells[0].Value = i + 1;
                        DgvTfrInfo.Rows[i_rows].Cells[1].Value = tfrFeatInfos[i].Name;
                        DgvTfrInfo.Rows[i_rows].Cells[2].Value = tfrFeatInfos[i].Number;
                        DgvTfrInfo.Rows[i_rows].Cells[3].Value = tfrFeatInfos[i].Dtype;
                        DgvTfrInfo.Rows[i_rows].Cells[4].Value = tfrFeatInfos[i].Infos;
                        if (tfrFeatInfos[i].Number == 1)
                        {
                            CbSrt.Items.Add(tfrFeatInfos[i].Name);
                        }
                        else
                        {
                            CbBName.Items.Add(tfrFeatInfos[i].Name);
                            CbGName.Items.Add(tfrFeatInfos[i].Name);
                            CbRName.Items.Add(tfrFeatInfos[i].Name);
                            if (CbBName.Text == "")
                            {
                                CbBName.Text = tfrFeatInfos[i].Name;
                            }
                            else if (CbGName.Text == "")
                            {
                                CbGName.Text = tfrFeatInfos[i].Name;
                            }
                            else if (CbRName.Text == "")
                            {
                                CbRName.Text = tfrFeatInfos[i].Name;
                            }

                            if (TxtImageSize.Text == "")
                            {
                                int n0 = (int)(Math.Sqrt(tfrFeatInfos[i].Number));
                                if (n0 * n0 == tfrFeatInfos[i].Number)
                                {
                                    TxtImageSize.Text = string.Format("{0}, {1}", n0, n0);
                                }
                                else
                                {
                                    TxtImageSize.Text = string.Format("{0}, {1}", tfrFeatInfos[i].Number, 1);
                                }
                            }
                        }
                    }
                    CbSrt.Items.Add(" ");
                    CbSrt.Text = " ";
                }
                else
                {
                    for (int i = 0; i < tfrFeatInfos.Count; i++)
                    {
                        bool is_have = false;
                        for (int j = 0; j < DgvTfrInfo.Rows.Count; j++)
                        {
                            if (DgvTfrInfo.Rows[i].Cells[1].Value.ToString() == tfrFeatInfos[i].Name &
                                int.Parse(DgvTfrInfo.Rows[i].Cells[2].Value.ToString()) == tfrFeatInfos[i].Number &
                                DgvTfrInfo.Rows[i].Cells[3].Value.ToString() == tfrFeatInfos[i].Dtype)
                            {
                                is_have = true;
                                break;
                            }
                        }
                        if (!is_have)
                        {
                            MessageBox.Show("与之前的文件的信息不同 " + tfr_file);
                            return false;
                        }
                    }
                    int i0 = DgvTfrFiles.Rows.Add();
                    DgvTfrFiles.Rows[i0].Cells[0].Value = tfr_file;
                }
            }
            else
            {
                MessageBox.Show("无法读取文件：" + tfr_file);
            }

            return true;
        }

        private bool ClearAll()
        {
            DgvTfrFiles.Rows.Clear();
            DgvTfrInfo.Rows.Clear();
            TxtBMax.Text = "";
            TxtGMax.Text = "";
            TxtRMax.Text = "";
            TxtBMin.Text = "";
            TxtGMin.Text = "";
            TxtRMin.Text = "";
            TxtImageSize.Text = "";
            CbBName.Items.Clear();
            CbGName.Items.Clear();
            CbRName.Items.Clear();
            CbBName.Text = "";
            CbGName.Text = "";
            CbRName.Text = "";
            CbSrt.Items.Clear();
            CbSrt.Text = "";
            RtbRun.Clear();
            TxtSaveDir.Text = "";
            return true;
        }

        private bool CheckStringNumber(string ss)
        {
            if (ss == "")
            {
                return true;
            }
            try
            {
                double.Parse(ss);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        private void BtnSaveDir_Click(object sender, EventArgs e)
        {
            fbd.ShowDialog();
            TxtSaveDir.Text = fbd.SelectedPath;
        }
    }

    /// <summary>
    /// tfr 文件的每一个feature的信息
    /// </summary>
    public class TfrFeatInfo
    {
        /// <summary>
        /// 特征名
        /// </summary>
        public string Name = "";
        /// <summary>
        /// 特征的数据类型
        /// </summary>
        public string Dtype = "";
        /// <summary>
        /// 特征的数据的数量
        /// </summary>
        public int Number = 0;
        /// <summary>
        /// 简要信息
        /// </summary>
        public string Infos = "";
    }

    /// <summary>
    /// 使用到的命令行程序
    /// </summary>
    class UseExes
    {
        /// <summary>
        /// gdal - ogr2ogr
        /// </summary>
        public static string ogr2ogr = @"D:\SpecialProjects\ogr2ogrtest\tt\Library\bin\ogr2ogr.exe";
        /// <summary>
        /// gdal - gdalinfo
        /// </summary>
        public static string gdalinfo = @"D:\SpecialProjects\ogr2ogrtest\tt\Library\bin\gdalinfo.exe";
        /// <summary>
        /// srt - tfrinfo
        /// </summary>
        public static string srt_tfrinfo(params string[] args)
        {
            string info = @"srt_tfrinfo";
            for (int i = 0; i < args.Length; i++)
            {
                info += " " + args[i];
            }
            return info;
        }

        /// <summary>
        /// srt - tfr2png
        /// </summary>
        public static string srt_tfr2png(params string[] args)
        {
            string info = @"srt_tfr2png";
            for (int i = 0; i < args.Length; i++)
            {
                info += " " + args[i];
            }
            return info;
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


    /// <summary>
    /// 一些函数
    /// </summary>
    class utils
    {
        /// <summary>
        /// 添加引号
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string add_yh(string info)
        {
            return "\"" + info + "\"";
        }

        public static string remove_yh(string info)
        {
            if (info == "")
            {
                return "";
            }
            else
            {
                if (info[0] == '"')
                {
                    info = info.Remove(0, 1);
                }

                if (info.Length >= 2)
                {
                    if (info[info.Length - 1] == '"')
                    {
                        info = info.Remove(info.Length - 1);
                    }
                }

                return info;
            }
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

}
