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

namespace SampleIdentifWFA01
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            ChangeCross(PanelH1, PanelV1, ImbShow.Width, ImbShow.Height, 3, 31);
        }

        #region 属性
        /// <summary>
        /// 当前是第几张照片
        /// </summary>
        private int NPic = 0;
        /// <summary>
        /// 颜色体系
        /// </summary>
        private List<Color> ClassesColor = new List<Color>();
        /// <summary>
        /// 项目文件名
        /// </summary>
        private string PrjFile = "";
        /// <summary>
        /// 是否开始播放
        /// </summary>
        private bool IsPlay = false;
        private string CSVFile = "";
        #endregion

        #region 处理事件
        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            ChangeCross(PanelH1, PanelV1, ImbShow.Width, ImbShow.Height, 3, 31);
        }

        /// <summary>
        /// 回车决定开始播放和结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtClasses_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                PlayOrNot();
                SampleNext(1);
            }

        }

        /// <summary>
        /// 左右像元移动键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtClasses_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                SampleNext(-1);
            }
            if (e.KeyCode == Keys.Right)
            {
                SampleNext(1);
            }
        }
        #endregion

        #region 点击事件
        /// <summary>
        /// 下一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNext_Click(object sender, EventArgs e)
        {
            SampleNext(1);
        }

        /// <summary>
        /// 按钮上一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrior_Click(object sender, EventArgs e)
        {
            SampleNext(-1);
        }

        /// <summary>
        /// 打开工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnOpenPrj_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "txt file|*.txt";
            //ofd.InitialDirectory = @"D:\CodeProjects\SampleIdentification\SampleIdentificationWFA01\test01";
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (!(ofd.ShowDialog() == DialogResult.OK))
            {
                return;
            }
            PrjFile = ofd.FileName;
            try
            {
                NPic = Samples.Open(ofd.FileName);
                RenderDgvClasses();
                RenderSample(NPic);
                Text = "样本解译工具 - v1.0 ";
            }
            catch (Exception ex)
            {
                TmrPlay.Stop();
                IsPlay = false;
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// 导入工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnExportPrj_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV File (*.csv)|*.csv";
            ofd.Multiselect = false;
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.Title = "CSV 文件";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            CSVFile = ofd.FileName;
            PrjFile = Path.GetFullPath(CSVFile);
            PrjFile = Path.Combine(Path.GetDirectoryName(PrjFile), Path.GetFileNameWithoutExtension(PrjFile) + "_SampleIdentify.txt");
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Txt File (*.txt)|*.txt";
            sfd.FileName = PrjFile;
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            sfd.Title = "保存项目文件";
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            PrjFile = sfd.FileName;
            Samples.BuildByCsv(ofd.FileName, "CLASS", "FILE");
            RenderDgvClasses();
            RenderSample(0);
            Text = "样本解译工具 - v1.0 ";
        }

        /// <summary>
        /// 计时播放点击开始和结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnTimerPlay_Click(object sender, EventArgs e)
        {
            if (Samples.Number == 0)
            {
                return;
            }

            PlayOrNot();
        }

        /// <summary>
        /// 转到第几个样本点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnGoto_Click(object sender, EventArgs e)
        {
            if (!IsPlay)
            {
                try
                {
                    int n = int.Parse(TstxtGoTo.Text);
                    if (n < 0 | n > Samples.Number)
                    {
                        MessageBox.Show("Sample number over range");
                    }
                    NPic = n - 1;
                    RenderSample(NPic);
                    Samples.Save(PrjFile, NPic, "SRT_WORK_NAME");
                    RenderDgvClasses();
                    TxtClasses.Focus();
                    TxtClasses.Select(0, TxtClasses.TextLength);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region 辅助函数

        /// <summary>
        /// 下一个
        /// </summary>
        private void SampleNext(int fangxiang)
        {
            if (Samples.Number == 0)
            {
                TmrPlay.Stop();
                IsPlay = false;
                return;
            }
            else if (NPic <= 0 & fangxiang == -1)
            {
                TmrPlay.Stop();
                IsPlay = false;
                MessageBox.Show("已经到第一个样本");
                NPic = Samples.Number - 1;
                RenderSample(NPic);
                return;
            }
            else if (NPic >= Samples.Number - 1 & fangxiang == 1)
            {
                TmrPlay.Stop();
                IsPlay = false;
                MessageBox.Show("已经到最后一个样本");
                RenderSample(0);
                NPic = 0;
                return;
            }
            else
            {
                try
                {
                    int n = int.Parse(TxtClasses.Text);
                    if (n >= Samples.CateoryNamesCounts.Count)
                    {
                        TmrPlay.Stop();
                        IsPlay = false;
                        MessageBox.Show("Error: wrong category number");
                        return;
                    }
                    Samples.Spls[NPic].ChangeClasses(n);
                    NPic += fangxiang;
                    RenderSample(NPic);
                    Samples.Save(PrjFile, NPic, "SRT_WORK_NAME");
                    RenderDgvClasses();
                    TxtClasses.Focus();
                    TxtClasses.Select(0, TxtClasses.TextLength);
                }
                catch (Exception ex)
                {
                    TmrPlay.Stop();
                    IsPlay = false;
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// 渲染类别表格
        /// </summary>
        private void RenderDgvClasses()
        {
            DgvClasses.Rows.Clear();
            for (int i = 0; i < Samples.CateoryNamesCounts.Count; i++)
            {
                int nrow = DgvClasses.Rows.Add();
                DgvClasses.Rows[nrow].Cells[0].Style.BackColor = ClassesColor[i];
                DgvClasses.Rows[nrow].Cells[1].Value = nrow;
                DgvClasses.Rows[nrow].Cells[2].Value = Samples.CateoryNames[i];
                DgvClasses.Rows[nrow].Cells[3].Value = Samples.CateoryCounts[i];
            }
            DgvClasses.Rows[0].Cells[0].Selected = false;
        }

        /// <summary>
        /// 渲染第N个样本
        /// </summary>
        /// <param name="n"></param>
        private void RenderSample(int n)
        {
            ImbShow.Image = Image.FromFile(Samples.Spls[n].ImFileName);
            TxtClasses.Text = Samples.Spls[n].NewCateory.ToString();
            TstxtSearch.Text = (n + 1).ToString() + " / " + Samples.Number.ToString();
            //TstbGoto.Text = (n + 1).ToString();
            PanelV1.BackColor = ClassesColor[Samples.Spls[n].NewCateory];
            PanelV2.BackColor = ClassesColor[Samples.Spls[n].NewCateory];
            PanelV3.BackColor = ClassesColor[Samples.Spls[n].NewCateory];
            PanelH1.BackColor = ClassesColor[Samples.Spls[n].NewCateory];
            PanelH2.BackColor = ClassesColor[Samples.Spls[n].NewCateory];
            PanelH3.BackColor = ClassesColor[Samples.Spls[n].NewCateory];
            DgvSplInfo.Rows.Clear();
            for (int i = 0; i < Samples.FieldNames.Length; i++)
            {
                int nrow = DgvSplInfo.Rows.Add();
                DgvSplInfo.Rows[nrow].Cells[0].Value = Samples.FieldNames[i];
                string info = Samples.Spls[n][i];
                DgvSplInfo.Rows[nrow].Cells[1].Value = Samples.Spls[n][i];
            }
            DgvSplInfo.Rows[0].Cells[0].Selected = false;
        }

        /// <summary>
        /// 是否播放
        /// </summary>
        private void PlayOrNot()
        {
            if (IsPlay)
            {
                IsPlay = false;
                TmrPlay.Stop();
            }
            else
            {
                IsPlay = true;
                TmrPlay.Start();
            }
        }
        #endregion

        /// <summary>
        /// 设置中心交叉线
        /// </summary>
        /// <param name="ph">横线</param>
        /// <param name="pv">竖线</param>
        /// <param name="c_width">控件宽</param>
        /// <param name="c_height">控件高</param>
        /// <param name="x0">初始竖向偏移</param>
        /// <param name="y0">初始横向偏移</param>
        private void ChangeCross(Panel ph, Panel pv, int c_width, int c_height, int x0, int y0)
        {
            ph.Width = c_width;
            pv.Height = c_height;
            ph.Location = new Point(x0, y0 + c_height / 2);
            pv.Location = new Point(x0 + c_width / 2, y0);
        }

        private bool ClearPrj()
        {
            return false;
        }
    }
}
