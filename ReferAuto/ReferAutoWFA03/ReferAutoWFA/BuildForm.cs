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
namespace ReferAutoWFA
{
    public partial class BuildForm : Form
    {
        public BuildForm()
        {
            InitializeComponent();
            InitDir = Directory.GetCurrentDirectory();
            IsSelected = true;
        }

        /// <summary>
        /// 项目文件
        /// </summary>
        public string PrjFile = @"";
        /// <summary>
        /// 文献列表文件
        /// </summary>
        public string RefListFile = @"";
        /// <summary>
        /// 原始路径
        /// </summary>
        public string InitDir = @"";
        /// <summary>
        /// 是否选择成功
        /// </summary>
        public bool IsSelected = true;


        private void BtnPrjFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Txt File|*.txt";
            ofd.InitialDirectory = InitDir;

            if (!(ofd.ShowDialog() == DialogResult.OK))
            {
                return;
            }
            
            PrjFile = ofd.FileName;

            InitDir = Path.GetDirectoryName(ofd.FileName);
            TxtPrjFile.Text = PrjFile;
            TxtRefListFile.Text = RefListFile;
        }

        private void BtnRefListFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Txt File|*.txt";
            ofd.InitialDirectory = InitDir;

            if (!(ofd.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            RefListFile = ofd.FileName;
            InitDir = Path.GetDirectoryName(ofd.FileName);
            PrjFile = Path.Combine(InitDir, "ref.txt");
            
            TxtPrjFile.Text = PrjFile;
            TxtRefListFile.Text = RefListFile;
        }

        private void BtnRemAll_Click(object sender, EventArgs e)
        {
            IsSelected = false;
            Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if(File.Exists(RefListFile))
            {
                Close();
            }
            else
            {
                MessageBox.Show("参考文献文件不存在");
            }
        }
    }
}
