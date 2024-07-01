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
using System.Drawing.Drawing2D;
/*
             //RtbRunAdd("\n    sample number: " + (n + 1).ToString()
            //    + "\n    image: " + IndfImg.Imgs[n].ImFileName
            //    + "\n    original classes: " + Samples.CateoryNamesCounts[IndfImg.Imgs[n].Classes] + "\n");
            //PCenterIms.BackColor = ClassesColor[IndfImg.Imgs[n_pic].NewClasses];
 
 */
namespace SampleIdentificationWFA01
{
    public partial class SIMainForm : Form
    {
        public SIMainForm()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            ClassesColor.Add(Color.Black);
            ClassesColor.Add(Color.Green);
            ClassesColor.Add(Color.Red);
            ClassesColor.Add(Color.Blue);
            TmrPlay.Enabled = true;
            TmrPlay.Interval = 1000;
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
            if(ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            CSVFile = ofd.FileName;
            PrjFile = Path.GetFullPath(CSVFile);
            PrjFile = Path.Combine(Path.GetDirectoryName(PrjFile), Path.GetFileNameWithoutExtension(PrjFile)+ "_SampleIdentify.txt");
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Txt File (*.txt)|*.txt";
            sfd.FileName = PrjFile;
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            sfd.Title = "保存项目文件";
            if(sfd.ShowDialog()!= DialogResult.OK)
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
                    int n = int.Parse(TstbGoto.Text);
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
            TstxtSearch.Text = (n + 1).ToString() +" / "+ Samples.Number.ToString();
            //TstbGoto.Text = (n + 1).ToString();
            PClassColor.BackColor = ClassesColor[Samples.Spls[n].NewCateory];
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

        #region 其他事件
        /// <summary>
        /// 计时播放功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrPlay_Tick(object sender, EventArgs e)
        {
            SampleNext(1);
        }

        /// <summary>
        /// 绘制原始的像元图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImbShow_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = this.ImbShow;
            if (pic.Image == null)
                return;
            GraphicsState state = e.Graphics.Save();
            e.Graphics.SmoothingMode = SmoothingMode.None;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
            e.Graphics.Clear(this.ImbShow.BackColor);
            e.Graphics.DrawImage(pic.Image, new Rectangle(0, 0, pic.Width, pic.Height), new Rectangle(0, 0, pic.Image.Width, pic.Image.Height), GraphicsUnit.Pixel);
            //e.Graphics.DrawImage(pic.Image, new RectangleF(1, 1, pic.Width - 1, pic.Height - 1), new RectangleF(0, 0, pic.Image.Width, pic.Image.Height), GraphicsUnit.Pixel);
            e.Graphics.Restore(state);
        }
        #endregion


    }
}
