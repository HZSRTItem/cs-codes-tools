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

namespace ScienceDirectReferenceWFA
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private string RefFileName = "";
        private string InitPath = Directory.GetCurrentDirectory();

        private void BtnRefPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.InitialDirectory = InitPath;
            ofd.Filter = "txt file (*.txt)|*.txt";

            if (!(ofd.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            RefFileName = ofd.FileName;
            TxtRefPath.Text = RefFileName;
            InitPath = Path.GetDirectoryName(RefFileName);
            TxtJsonSavePath.Text = GetSavePath();
        }

        private string GetSavePath()
        {
            DirectoryInfo root = new DirectoryInfo(InitPath);
            FileInfo[] files = root.GetFiles();
            string saveFileName = "n.json";
            int ifile = 1;
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo finfo = files[i];
                string filename = Path.GetFileNameWithoutExtension(finfo.Name);
                if (finfo.Name == saveFileName)
                {
                    saveFileName = "n_" + ifile.ToString() + ".json";
                    i = 0;
                    ifile++;
                }
            }
            saveFileName = Path.Combine(InitPath, saveFileName);
            return saveFileName;
        }

        /// <summary>
        ///     "title": "",
        ///     "author": "",
        ///     "periodical": "",
        ///     "volume": "",
        ///     "time":"",
        ///     "pages":"",
        ///     "issn": "",
        ///     "doi": "https://doi.org/",
        ///     "abstract": "",
        ///     "keywords": "",
        ///     "innovation": "",
        ///     "read_time": "",
        ///     "write": ""
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCal_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(RefFileName);
            string jsonstr = "{\n    "; 
            string line = sr.ReadLine();

            // 作者
            jsonstr += "\"author\": \"";
            jsonstr += line.Substring(0, line.Length - 1);
            jsonstr += "\", ";

            // 题目
            line = sr.ReadLine();
            jsonstr += "\n    "; 
            jsonstr += "\"title\": \"";
            jsonstr += line.Substring(0, line.Length - 1);
            jsonstr += "\", ";

            // 期刊
            line = sr.ReadLine();
            jsonstr += "\n    "; 
            jsonstr += "\"periodical\": \"";
            jsonstr += line.Substring(0, line.Length - 1);
            jsonstr += "\", ";

            // 卷名
            line = sr.ReadLine();
            jsonstr += "\n    "; 
            jsonstr += "\"volume\": \"";
            jsonstr += line.Substring(7, line.Length - 8);
            jsonstr += "\", ";

            // 时间
            line = sr.ReadLine();
            jsonstr += "\n    "; 
            jsonstr += "\"time\": \"";
            jsonstr += line.Substring(0, line.Length - 1);
            jsonstr += "\", ";

            // 页码
            line = sr.ReadLine();
            jsonstr += "\n    "; 
            jsonstr += "\"pages\": \"";
            jsonstr += line.Substring(0, line.Length - 1);
            jsonstr += "\", ";

            // ISSN
            line = sr.ReadLine();
            jsonstr += "\n    "; 
            jsonstr += "\"ISSN\": \"";
            jsonstr += line.Substring(5, line.Length - 6);
            jsonstr += "\", ";

            // DOI
            line = sr.ReadLine();
            jsonstr += "\n    "; 
            jsonstr += "\"doi\": \"";
            jsonstr += line;
            jsonstr += "\", ";

            line = sr.ReadLine();
            
            // 摘要
            line = sr.ReadLine();
            jsonstr += "\n    "; 
            jsonstr += "\"abstract\": \"";
            jsonstr += line.Substring(10);
            jsonstr += "\", ";

            // 关键词
            line = sr.ReadLine();
            jsonstr += "\n    "; 
            jsonstr += "\"keywords\": \"";
            jsonstr += line.Substring(10);
            jsonstr += "\", ";

            jsonstr += "\n    "; 
            jsonstr += "\"innovation\": \"\",";
            jsonstr += "\n    "; 
            jsonstr += "\"read_time\": \"\",";
            jsonstr += "\n    "; 
            jsonstr += "\"write\": \"\"";
            jsonstr += "\n}";

            sr.Close();

            StreamWriter sw = new StreamWriter(TxtJsonSavePath.Text);
            sw.Write(jsonstr);
            sw.Close();
            MessageBox.Show("已经保存");
        }

        private void BtnQK_Click(object sender, EventArgs e)
        {
            TxtRefPath.Text = "";
            TxtJsonSavePath.Text = "";
            RefFileName = "";
        }

        private void BtnSavePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = InitPath;
            sfd.Filter = "json file (*.json)|*.json";

            if (!(sfd.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            TxtJsonSavePath.Text = sfd.FileName;
        }
    }
}
