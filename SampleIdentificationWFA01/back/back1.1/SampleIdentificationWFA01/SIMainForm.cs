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

namespace SampleIdentificationWFA01
{
    public partial class SIMainForm : Form
    {
        public SIMainForm()
        {
            InitializeComponent();
            //DgvClasses.Rows.Add();
            //DgvClasses.Rows[0].Cells[0].Style.BackColor = Color.Red;
            //DgvClasses.Rows[0].Cells[1].Value = "草地";
            //DgvClasses.Rows.Add();
            //DgvClasses.Rows[1].Cells[0].Style.BackColor = Color.Green;
            //DgvClasses.Rows[1].Cells[1].Value = "不透水面";
            //DgvClasses.Rows.Add();
            //DgvClasses.Rows[2].Cells[0].Style.BackColor = Color.Blue;
            //DgvClasses.Rows[2].Cells[1].Value = "非不透水面";
            //PicList.Add(@"C:\Users\ASUS\Pictures\t01.jpg");
            //PicList.Add(@"C:\Users\ASUS\Pictures\驴子.jpg");
            //PicList.Add(@"C:\Users\ASUS\Pictures\图片1.png");
            //PicList.Add(@"C:\Users\ASUS\Pictures\图片2.jpg");
            //PicList.Add(@"C:\Users\ASUS\Pictures\图片3.jpg");
            //PicList.Add(@"C:\Users\ASUS\Pictures\图片4.jpg");
            //PicList.Add(@"C:\Users\ASUS\Pictures\图片5.jpg");
            //ImbShow.Image = Image.FromFile(PicList[n_pic]);
            ClassesColor.Add(Color.Black);
            ClassesColor.Add(Color.Green);
            ClassesColor.Add(Color.Red);
            ClassesColor.Add(Color.Blue);

            //g = ImbShow.CreateGraphics();//创建一个
            //g = panel1.CreateGraphics();//创建一个
        }

        private List<string> PicList = new List<string>();
        private int n_pic = 0;
        private List<Color> ClassesColor = new List<Color>();
        private string PrjFile = "";


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
        /// 下一个
        /// </summary>
        private void SampleNext(int fangxiang)
        {
            if (IndfImg.Imgs.Count == 0)
            {
                return;
            }
            else if (n_pic <= 0 & fangxiang == -1)
            {
                MessageBox.Show("已经到第一个样本");
                n_pic = IndfImg.Imgs.Count - 1;
                RenderSample(n_pic);
                return;
            }
            else if (n_pic >= IndfImg.Imgs.Count - 1 & fangxiang == 1)
            {
                MessageBox.Show("已经到最后一个样本");
                RenderSample(0);
                n_pic = 0;
                return;
            }
            else
            {
                try
                {
                    int n = int.Parse(TxtClasses.Text);
                    if (n >= IndfImg.NameClasses.Count)
                    {
                        MessageBox.Show("Error: wrong category number");
                        return;
                    }
                    RtbRunAdd(IndfImg.Imgs[n_pic].ChangeClasses(n));
                    n_pic += fangxiang;
                    RenderSample(n_pic);
                    IndfImg.SaveAll(PrjFile, n_pic);
                    RenderDgvClasses();
                    TxtClasses.Focus();
                    TxtClasses.Select(0, TxtClasses.TextLength);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

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
                n_pic = IndfImg.BuildIndfImg(ofd.FileName, "", "");
                RenderDgvClasses();
                RtbRunAdd("already export project " + IndfImg.WorkName);
                RenderSample(n_pic);
                Text = "样本解译工具 - v1.0 " + IndfImg.WorkName;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message);
            }

        }

        /// <summary>
        /// 导入工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnExportPrj_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            //fbd.SelectedPath = @"D:\CodeProjects\SampleIdentification\SampleIdentificationWFA01\test01";
            fbd.SelectedPath = Directory.GetCurrentDirectory();
            if (!(fbd.ShowDialog() == DialogResult.OK))
            {
                return;
            }
            // PrjFile
            IndfImg.BuildIndfImg(fbd.SelectedPath, "", "");
            RenderDgvClasses();
            RtbRunAdd("already export project " + IndfImg.WorkName);
            RenderSample(0);
            Text = "样本解译工具 - v1.0 " + IndfImg.WorkName;
        }

        /// <summary>
        /// 渲染类别表格
        /// </summary>
        private void RenderDgvClasses()
        {
            DgvClasses.Rows.Clear();
            for (int i = 0; i < IndfImg.NameClasses.Count; i++)
            {
                int nrow = DgvClasses.Rows.Add();
                DgvClasses.Rows[nrow].Cells[0].Style.BackColor = ClassesColor[i];
                DgvClasses.Rows[nrow].Cells[1].Value = nrow + 1;
                DgvClasses.Rows[nrow].Cells[2].Value = IndfImg.NameClasses[i];
                DgvClasses.Rows[nrow].Cells[3].Value = IndfImg.NumClasses[i];
            }
            RtbRunAdd(" * Category rendered");
            DgvClasses.Rows[0].Cells[0].Selected = false;
        }

        /// <summary>
        /// 渲染第N个样本
        /// </summary>
        /// <param name="n"></param>
        private void RenderSample(int n)
        {
            ImbShow.Image = Image.FromFile(IndfImg.Imgs[n].ImFileName);
            TxtClasses.Text = IndfImg.Imgs[n].NewClasses.ToString();
            TstxtSearch.Text = (n + 1).ToString() +" / "+ IndfImg.Imgs.Count.ToString();
            RtbRunAdd("\n    sample number: " + (n + 1).ToString()
                + "\n    image: " + IndfImg.Imgs[n].ImFileName
                + "\n    original classes: " + IndfImg.NameClasses[IndfImg.Imgs[n].Classes] + "\n");
            PCenterIms.BackColor = ClassesColor[IndfImg.Imgs[n_pic].NewClasses];
        }

        /// <summary>
        /// 添加运行信息
        /// </summary>
        /// <param name="info"></param>
        private void RtbRunAdd(string info)
        {
            RtbRun.Text += DateTime.Now.ToString("G") + " * " + info;
            RtbRun.Focus();//获取焦点
            RtbRun.Select(this.RtbRun.TextLength, 0);//光标定位到文本最后
            //RtbRun.ScrollToCaret();//滚动到光标处
        }

        /// <summary>
        /// Enter 下一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtClasses_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SampleNext(1);
            }
        }
    }
}
