using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tensorflow;
using System.IO;
using System.Diagnostics;
using Google.Protobuf.Collections;
using Google.Protobuf;

namespace DcTfrWFA01
{
    public partial class DTMainForm : Form
    {
        public DTMainForm()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// tfr 文件的每一个feature的信息
        /// </summary>
        class TfrFeatInfo
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
            public int NEle = 0;
            /// <summary>
            /// 简要信息
            /// </summary>
            public string AInfos = "";
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

        }

        /// <summary>
        /// 所有文件的信息
        /// </summary>
        Dictionary<string, TfrFeatInfo[]> TfrFileInfos = new Dictionary<string, TfrFeatInfo[]>();
        List<string> TfrFileNames = new List<string>();

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            UseDir(Directory.GetCurrentDirectory());
        }

        /// <summary>
        /// 使用文件初始化
        /// </summary>
        /// <param name="in_dir"></param>
        private void UseDir(string in_dir)
        {
            Text = in_dir;
            TfrFileInfos.Clear();
            string[] files = GetDirFiles(in_dir, ".tfrecord");
            if (files.Length == 0)
            {
                MessageBox.Show("Not find tfrecord file in this dir");
                return;
            }
            // 获得信息
            for (int i = 0; i < files.Length; i++)
            {
                TfrFileInfos.Add(Path.GetFileNameWithoutExtension(files[i]), GetTfrInfo(files[i]));
                TfrFileNames.Add(files[i]);
            }
            string[] names = TfrFileInfos.Keys.ToArray();
            TscbTfrFileList.Items.AddRange(names);
            TscbTfrFileList.Text = names[0];
            RenderDgvOneTfrInfo(names[0]);
            RenderClbAllTfrFile();
        }

        /// <summary>
        /// 渲染所有文件
        /// </summary>
        private void RenderClbAllTfrFile()
        {
            ClbAllTfrFile.Items.Clear();
            string[] names = TfrFileInfos.Keys.ToArray();
            ClbAllTfrFile.Items.AddRange(names);
            QuanXuan(ClbAllTfrFile);
        }

        /// <summary>
        /// 渲染一个tfr文件表格
        /// </summary>
        /// <param name="tfr_name">文件名</param>
        private void RenderDgvOneTfrInfo(string tfr_name)
        {
            DgvOneTfrInfo.Rows.Clear();
            for (int i = 0; i < TfrFileInfos[tfr_name].Length; i++)
            {
                TfrFeatInfo tfrInfo = TfrFileInfos[tfr_name][i];
                int irows = DgvOneTfrInfo.Rows.Add();
                DgvOneTfrInfo.Rows[i].Cells[0].Value = i + 1;
                DgvOneTfrInfo.Rows[i].Cells[1].Value = tfrInfo.Name;
                DgvOneTfrInfo.Rows[i].Cells[2].Value = tfrInfo.NEle;
                DgvOneTfrInfo.Rows[i].Cells[3].Value = tfrInfo.Dtype;
                DgvOneTfrInfo.Rows[i].Cells[4].Value = tfrInfo.AInfos;
            }
        }

        /// <summary>
        /// 获得一个文件的所有信息
        /// </summary>
        /// <param name="tfrecord_file">tfrecord文件</param>
        /// <returns></returns>
        private TfrFeatInfo[] GetTfrInfo(string tfrecord_file)
        {
            // Tfrecord 读取器
            TFRecordReader tr = new TFRecordReader(File.OpenRead(tfrecord_file), true);
            // 读取一个Example缓存
            byte[] readbytes = tr.Read();
            if (readbytes == null) return null;
            // 解析为一个 Example
            Example readexample = Example.Parser.ParseFrom(readbytes);
            // 获得每一个 Feature 名
            string[] feat_names = new string[readexample.Features.Feature.Keys.Count];
            TfrFeatInfo[] tfrInfos = new TfrFeatInfo[readexample.Features.Feature.Keys.Count];
            int i_feat_names = 0;
            foreach (string item in readexample.Features.Feature.Keys)
            {
                tfrInfos[i_feat_names] = new TfrFeatInfo();
                tfrInfos[i_feat_names].Name = item;
                feat_names[i_feat_names++] = item;
            }
            // 解析数据
            readexample = Example.Parser.ParseFrom(readbytes);
            for (int i = 0; i < feat_names.Length; i++)
            {
                Feature feat = readexample.Features.Feature[feat_names[i]];
                if (feat.BytesList != null)
                {
                    tfrInfos[i].Dtype = "byte_list";
                    tfrInfos[i].NEle = feat.BytesList.Value.Count;
                    tfrInfos[i].AInfos = feat.BytesList.Value.ToString();
                }
                else if (feat.FloatList != null)
                {
                    tfrInfos[i].Dtype = "float_list";
                    tfrInfos[i].NEle = feat.FloatList.Value.Count;
                    NewMethod1(ref tfrInfos, i, feat);
                }
                else if (feat.Int64List != null)
                {
                    tfrInfos[i].Dtype = "int_list";
                    tfrInfos[i].NEle = feat.Int64List.Value.Count;
                    NewMethod1(ref tfrInfos, i, feat);
                }
                else
                {
                    continue;
                }
            }
            tr.Dispose();
            return tfrInfos;

            static void NewMethod1(ref TfrFeatInfo[] tfrInfos, int i, Feature feat)
            {
                var tt = feat.FloatList.Value.ToArray();
                string ss = "[";
                if (tfrInfos[i].NEle < 6)
                {
                    int j = 0;
                    for (; j < tt.Length-1; j++)
                    {
                        ss += tt[j].ToString() + ", ";
                    }
                    ss += tt[j].ToString() + "]";
                }
                else
                {
                    int j = 0;
                    for (; j < 3; j++)
                    {
                        ss += tt[j].ToString() + ", ";
                    }
                    ss += "... ";
                    for (j = tt.Length - 4; j < tt.Length-1; j++)
                    {
                        ss += tt[j].ToString() + ", ";
                    }
                    ss += tt[j].ToString() + "]";
                }
                tfrInfos[i].AInfos = ss;
            }
        }

        /// <summary>
        /// 获得文件夹下的所有的指定拓展名的文件
        /// </summary>
        /// <param name="dir_name">文件夹</param>
        /// <param name="ext">拓展名</param>
        /// <returns></returns>
        private string[] GetDirFiles(string dir_name, string ext)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(dir_name);
            FileInfo[] files = directoryInfo.GetFiles();
            var files0 = from fileinfo in files where fileinfo.Extension == ext select fileinfo.FullName;
            return files0.ToArray();
        }

        /// <summary>
        /// 渲染实例表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TscbTfrFileList_TextChanged(object sender, EventArgs e)
        {
            RenderDgvOneTfrInfo(TscbTfrFileList.Text);
        }

        /// <summary>
        /// 计算选中的文件的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnCalTong_Click(object sender, EventArgs e)
        {
            ClbClData.Items.Clear();
            ClbExpData.Items.Clear();
            // 找到选择的文件的特征
            List<TfrFeatInfo[]> tfrFeatInfos = new List<TfrFeatInfo[]>();
            for (int i = 0; i < ClbAllTfrFile.Items.Count; i++)
            {
                if(ClbAllTfrFile.GetItemChecked(i))
                {
                    tfrFeatInfos.Add(TfrFileInfos[ClbAllTfrFile.Items[i].ToString()]);
                }
            }
            if (tfrFeatInfos.Count == 0)
            {
                return;
            }
            // 计算每个特征的数量
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
            for (int i = 0; i < tfrFeatInfos.Count; i++)
            {
                for (int j = 0; j < tfrFeatInfos[i].Length; j++)
                {
                    if (!keyValuePairs.ContainsKey(tfrFeatInfos[i][j].Name))
                    {
                        keyValuePairs.Add(tfrFeatInfos[i][j].Name, 1);
                    }
                    else
                    {
                        keyValuePairs[tfrFeatInfos[i][j].Name]++;
                    }
                }
            }
            // 只有都包含的特征才会保留
            List<string> feat_select_list = new List<string>();
            foreach (string item in keyValuePairs.Keys)
            {
                if(keyValuePairs[item] == tfrFeatInfos.Count)
                {
                    feat_select_list.Add(item);
                }
            }
            // 计算每个文件的的数据的数量是否相同，只有相同的才会保留
            int[,] n_feat_select = new int[tfrFeatInfos.Count, feat_select_list.Count];
            for (int i = 0; i < feat_select_list.Count; i++) // 遍历每一个特征
            {
                for (int j = 0; j < tfrFeatInfos.Count; j++) // 遍历每一个文件
                {
                    for (int k = 0; k < tfrFeatInfos[j].Length; k++) // 找到那个特征
                    {
                        if(tfrFeatInfos[j][k].Name == feat_select_list[i])
                        {
                            n_feat_select[j, i] = tfrFeatInfos[j][k].NEle;
                        }
                    }
                }
            }
            for (int i = 0; i < feat_select_list.Count; i++)
            {
                for (int j = 0; j < tfrFeatInfos.Count; j++)
                {
                    if (n_feat_select[0, i] != n_feat_select[j, i])
                    {
                        feat_select_list[i] = "";
                        break;
                    }
                }
            }
            for (int i = 0; i < feat_select_list.Count; i++)
            {
                if(feat_select_list[i] == "")
                {
                    feat_select_list.RemoveAt(i--);
                }
            }
            // 分组 数量为1的分为一组
            // 数量为其他的分为另一组
            List<int> feat_count = new List<int>();
            for (int i = 0; i < feat_select_list.Count; i++)
            {
                for (int j = 0; j < tfrFeatInfos.Count; j++) // 遍历每一个文件
                {
                    for (int k = 0; k < tfrFeatInfos[j].Length; k++) // 找到那个特征
                    {
                        if (tfrFeatInfos[j][k].Name == feat_select_list[i])
                        {
                            feat_count.Add(tfrFeatInfos[j][k].NEle);
                        }
                    }
                    if (feat_count.Count == i + 1)
                    {
                        break;
                    }
                }
            }
            for (int i = 0; i < feat_count.Count; i++)
            {
                if(feat_count[i] == 1)
                {
                    ClbClData.Items.Add(feat_select_list[i] + ": 1");
                }
                else
                {
                    ClbExpData.Items.Add(feat_select_list[i] + ": " + feat_count[i].ToString());
                }
            }
            QuanXuan(ClbClData);
            QuanXuan(ClbExpData);
        }

        private static void QuanXuan(CheckedListBox checkedListBox)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SetItemChecked(i, true);
            }
        }

        private static void Shang(CheckedListBox checkedListBox)
        {
            int i_select = checkedListBox.SelectedIndex;
            if (!(i_select == 0 | i_select == -1))
            {
                var item_i_checked = checkedListBox.Items[i_select];
                bool c = checkedListBox.GetItemChecked(i_select);
                checkedListBox.Items.RemoveAt(i_select);
                checkedListBox.Items.Insert(i_select - 1, item_i_checked);
                checkedListBox.SetSelected(i_select - 1, true);
                checkedListBox.SetItemChecked(i_select - 1, c);
            }
        }

        private static void Xia(CheckedListBox checkedListBox)
        {
            int i_select = checkedListBox.SelectedIndex;
            if (!(i_select == checkedListBox.Items.Count - 1 | i_select == -1))
            {
                var item_i_checked = checkedListBox.Items[i_select];
                bool c = checkedListBox.GetItemChecked(i_select);
                checkedListBox.Items.RemoveAt(i_select);
                checkedListBox.Items.Insert(i_select + 1, item_i_checked);
                checkedListBox.SetSelected(i_select + 1, true);
                checkedListBox.SetItemChecked(i_select + 1, c);

            }
        }

        private void BtnClShang_Click(object sender, EventArgs e)
        {
            // 上
            Shang(ClbClData);
        }

        private void BtnExpShang_Click(object sender, EventArgs e)
        {
            Shang(ClbExpData);
        }

        private void BtnClXia_Click(object sender, EventArgs e)
        {
            Xia(ClbClData);
        }

        private void BtnExpXia_Click(object sender, EventArgs e)
        {
            Xia(ClbExpData);
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnExp_Click(object sender, EventArgs e)
        {
            int m = 0;
            int n = 0;
            try
            {
                string[] lines = TstbExpSize.Text.Split(',');
                m = int.Parse(lines[0].Trim());
                n = int.Parse(lines[1].Trim());
            }
            catch
            {
                MessageBox.Show("Wrong size of exported data");
                return;
            }

            List<string> biaojishuju = new List<string>();
            for (int i = 0; i < ClbClData.Items.Count; i++)
            {
                if(ClbClData.GetItemChecked(i))
                {
                    int n_one;
                    biaojishuju.Add(NewMethod0(ClbClData.Items[i].ToString(), out n_one));
                }
            }

            List<string> outshuju = new List<string>();
            List<int> n_outshuju = new List<int>();
            for (int i = 0; i < ClbExpData.Items.Count; i++)
            {
                if(ClbExpData.GetItemChecked(i))
                {
                    int n_one;
                    outshuju.Add(NewMethod0(ClbExpData.Items[i].ToString(), out n_one));
                    n_outshuju.Add(n_one);
                }
            }
            if(outshuju.Count==0)
            {
                MessageBox.Show("Please select exported data");
                return;
            }
            if (n_outshuju.Sum() * 1.0 / n_outshuju[0] != n_outshuju.Count)
            {
                MessageBox.Show("Selected data are not the same size");
                return;
            }
            if (m * n != n_outshuju[0])
            {
                MessageBox.Show("The set data size is different from the original data size");
                return;
            }

            ExpForm expForm = new ExpForm();
            expForm.ShowDialog();
            if(expForm.IsCal)
            {
                
                string run_info = "> Files\n";
                for (int i = 0; i < ClbAllTfrFile.Items.Count; i++)
                {
                    if (ClbAllTfrFile.GetItemChecked(i))
                    {
                        run_info += " " + TfrFileNames[i] + "\n";
                    }
                }
                run_info += "> marks\n";
                for (int i = 0; i < biaojishuju.Count; i++)
                {
                    run_info += " " + biaojishuju[i] + "\n";
                }
                run_info += "> expdatas\n";
                for (int i = 0; i < outshuju.Count; i++)
                {
                    run_info += " " + outshuju[i] + "\t" + TstbExpSize.Text + "\n";
                }
                run_info += "> clfile\n";
                run_info += " " + expForm.FileNameBiaoJi + "\n";
                run_info += "> dfile\n";
                run_info += " " + expForm.FileNameData + "\n";
                RtbRunInfo.Text = run_info;
                RtbRunInfo.Text += @"C:\Users\ASUS\anaconda3\envs\tf2\python.exe D:\code\lib\srt_dctfr\tfr2npy.py dctfr_temp.txt"+"\n";
                StreamWriter streamWriter = new StreamWriter(@"dctfr_temp.txt");
                streamWriter.WriteLine(run_info);
                streamWriter.Close();
            }

            static string NewMethod0(string line, out int n_one)
            {
                string[] lines = line.Split(':');
                string v_str = lines[0];
                for (int j = 1; j < lines.Length - 1; j++)
                {
                    v_str += ":" + lines[j] ;
                }
                n_one = int.Parse(lines[lines.Length - 1].Trim());
                return v_str;
            }
        }

        private void TscbTfrFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RenderDgvOneTfrInfo(TscbTfrFileList.Text);
        }

        private void TsbtnRun_Click(object sender, EventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter(@"D:\Temp\dctfr_temp.txt");
            streamWriter.Write(RtbRunInfo.Text);
            streamWriter.Close();
            // "D:\code\lib\srt_dctfr\tfr2npy.py"
            CmdRun.run(@"C:\Users\ASUS\anaconda3\envs\tf2\python.exe D:\code\lib\srt_dctfr\tfr2npy.py D:\Temp\dctfr_temp.txt");
            RtbRunInfo.Text += CmdRun.OutInfo + "\n";
            RtbRunInfo.Text += CmdRun.ErrorInfo + "\n";
        }
    }
}
