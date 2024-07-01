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
    public partial class OpenInitForm : Form
    {
        public OpenInitForm()
        {
            InitializeComponent();


        }

        //public string PrjDirName;
        //public string PrjName;
        //public string ORemoteImageFile;
        //public string PrjTxtName;
        public SampleDT OSampleDT;
        public bool isbuild = false;

        private void button2_Click(object sender, EventArgs e)
        {
            // 获得工程文件和路径
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog.Filter = "Txt File|*.txt";
            saveFileDialog.Title = "请选择保存的工程文件";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string prj_name = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
            string prj_dir_name = Path.Combine(Path.GetDirectoryName(saveFileDialog.FileName), prj_name);
            // 创建工程文件
            if (Directory.Exists(prj_dir_name))
            {
                MessageBox.Show("已经存在该项目" + prj_name, "提示");
                return;
            }
            // 获得工程参数
            BuildPrjForm buildPrjForm = new BuildPrjForm();
            buildPrjForm.ShowDialog();
            if (buildPrjForm.isbuild == false)
            {
                return;
            }
            OSampleDT = buildPrjForm.OSampleDT;
            OSampleDT.PrjDirName = prj_dir_name;
            Directory.CreateDirectory(OSampleDT.PrjDirName);
            OSampleDT.PrjName = prj_name;
            OSampleDT.PrjTxtName = Path.Combine(prj_dir_name, prj_name + ".txt");
            OSampleDT.ORemoteImageFile =  buildPrjForm.ORemoteImageFile;
            OSampleDT.GMapCacheDir = Path.Combine(OSampleDT.PrjDirName, "GMapCache");
            Directory.CreateDirectory(OSampleDT.GMapCacheDir);
            string dt_dir = Path.Combine(OSampleDT.PrjDirName, "Data");
            OSampleDT.DataDir = dt_dir;
            Directory.CreateDirectory(dt_dir);
            OSampleDT.Save();
            isbuild = true;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Txt File|*.txt";
            ofd.Multiselect = false;
            if(ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            OSampleDT = new SampleDT(ofd.FileName);
            if(!OSampleDT.is_f_build)
            {
                OSampleDT = null;
                return;
            }
            isbuild = true;
            Close();

        }
    }
}
