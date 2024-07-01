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

        public SampleDT OSampleDT;
        public bool isbuild = false;

        private void button2_Click(object sender, EventArgs e)
        {
            // 获得工程文件和路径
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            sfd.Filter = "XML File (*.xml)|*.xml";
            sfd.Title = "请选择保存的工程文件";
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            //string prj_name = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
            //string prj_dir_name = Path.Combine(Path.GetDirectoryName(saveFileDialog.FileName), prj_name);
            //// 创建工程文件
            //if (Directory.Exists(prj_dir_name))
            //{
            //    MessageBox.Show("已经存在该项目" + prj_name, "提示");
            //    return;
            //}

            // 获得工程参数
            BuildPrjForm buildPrjForm = new BuildPrjForm();
            buildPrjForm.ShowDialog();
            if (!buildPrjForm.isbuild)
            {
                return;
            }

            OSampleDT = buildPrjForm.OSampleDT;
            OSampleDT.GMapCacheDir = @"D:\CodeProjects\CSGeo\SampleIdentif\GMapCacheDir";
            OSampleDT.PrjXmlFileName = sfd.FileName;

            //OSampleDT.PrjDirName = prj_dir_name;
            //Directory.CreateDirectory(OSampleDT.PrjDirName);
            //OSampleDT.PrjName = prj_name;
            //OSampleDT.PrjTxtName = Path.Combine(prj_dir_name, prj_name + ".txt");
            //OSampleDT.GMapCacheDir = Path.Combine(OSampleDT.PrjDirName, "GMapCache");
            //Directory.CreateDirectory(OSampleDT.GMapCacheDir);
            //string dt_dir = Path.Combine(OSampleDT.PrjDirName, "Data");
            //OSampleDT.DataDir = dt_dir;
            //Directory.CreateDirectory(dt_dir);

            isbuild = true;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML File (*.xml)|*.xml";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            try
            {
                OSampleDT = SampleDT.SampleDTSerializerXml(ofd.FileName);
                isbuild = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("未能序列化XML文件为SampleDT. \nFile: " + ofd.FileName + "\nError: " + ex.Message);
            }
        }
    }
}
