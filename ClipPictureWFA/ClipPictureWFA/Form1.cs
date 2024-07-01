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

namespace ClipPictureWFA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            ShangKuang.Height = 1;
            XiaKuang.Height = 1;
            ZuoKuang.Width = 1;
            YouKuang.Width = 1;
            X1Kuang = pictureBox1.Width - 5;
            Y1Kuang = pictureBox1.Height - 5;
            SetKuangLoca(X0Kuang, Y0Kuang, X1Kuang, Y1Kuang);
            //SetKuangLoca(5, 5, 100, 200);
        }

        private int X0Kuang = 5;
        private int Y0Kuang = 5;
        private int X1Kuang = 100;
        private int Y1Kuang = 200;

        private bool SetKuangLoca(int x0, int y0, int x1, int y1)
        {
            if (x1 > x0 & y1 > y0)
            {
                ShangKuang.Location = new Point(x0, y0);
                ShangKuang.Width = x1 - x0;
                ZuoKuang.Location = new Point(x0, y0);
                ZuoKuang.Height = y1 - y0;
                XiaKuang.Location = new Point(x0, y1);
                XiaKuang.Width = x1 - x0;
                YouKuang.Location = new Point(x1, y0);
                YouKuang.Height = y1 - y0;
                return true;
            }
            return false;
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (PicFiles.Count == 0)
            {
                return;
            }

            if (e.Delta > 0)
            {
                RenderI(NCurrent - 1);
            }
            else
            {
                RenderI(NCurrent + 1);
            }
        }

        private OpenFileDialog ofd = new OpenFileDialog();
        private FolderBrowserDialog fbd = new FolderBrowserDialog();
        private string InDir = "";
        private string OutDir = "";
        private List<string> PicFiles = new List<string>(100);
        private int NCurrent = -1;

        private bool BuildByDir(string in_dir)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(in_dir);
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            for (int i = 0; i < fileInfos.Length; i++)
            {
                string ext = fileInfos[i].Extension.ToLower();
                if (ext == ".png" | ext == ".jpg")
                {
                    PicFiles.Add(fileInfos[i].FullName);
                }
            }
            if (PicFiles.Count != 0)
            {
                for (int i = 0; i < PicFiles.Count; i++)
                {
                    int ir = DgvFiles.Rows.Add();
                    DgvFiles.Rows[ir].Cells[0].Value = ir + 1;
                    DgvFiles.Rows[ir].Cells[1].Value = Path.GetFileNameWithoutExtension(PicFiles[ir]);
                    DgvFiles.Rows[ir].Cells[0].Style.BackColor = Color.White;
                    DgvFiles.Rows[ir].Cells[1].Style.BackColor = Color.White;
                }
                NCurrent = 0;
                RenderI(0);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void TsbtnBuild_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = Directory.GetCurrentDirectory();
            if (!(fbd.ShowDialog() == DialogResult.OK))
            {
                return;
            }
            if (BuildByDir(fbd.SelectedPath))
            {
                InDir = fbd.SelectedPath;
                OutDir = Path.Combine(InDir, "imgs");
                TxtInDir.Text = InDir;
                TxtOutDir.Text = OutDir;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private bool RenderI(int i_pic)
        {
            if (i_pic < 0 | i_pic >= PicFiles.Count)
            {
                return false;
            }
            pictureBox1.Image = Image.FromFile(PicFiles[i_pic]);
            DgvFiles.Rows[i_pic].Cells[0].Style.BackColor = Color.DarkGray;
            DgvFiles.Rows[i_pic].Cells[1].Style.BackColor = Color.DarkGray;
            if (NCurrent != i_pic & NCurrent != -1)
            {
                DgvFiles.Rows[NCurrent].Cells[0].Style.BackColor = Color.White;
                DgvFiles.Rows[NCurrent].Cells[1].Style.BackColor = Color.White;
            }
            NCurrent = i_pic;
            return true;
        }

        private void BtnQian_Click(object sender, EventArgs e)
        {
            RenderI(NCurrent - 1);
        }

        private void BtnHou_Click(object sender, EventArgs e)
        {
            RenderI(NCurrent + 1);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (toolStripComboBox1.Text == "左上角")
            {
                if (e.X < X1Kuang & e.Y < Y1Kuang)
                {
                    X0Kuang = e.X;
                    Y0Kuang = e.Y;
                }
            }
            else if (toolStripComboBox1.Text == "右下角")
            {
                if (e.X > X0Kuang & e.Y > Y0Kuang)
                {
                    X1Kuang = e.X;
                    Y1Kuang = e.Y;
                }
            }

            SetKuangLoca(X0Kuang, Y0Kuang, X1Kuang, Y1Kuang);
        }


    }
}
