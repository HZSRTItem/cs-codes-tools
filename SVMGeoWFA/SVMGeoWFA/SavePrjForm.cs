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

namespace SVMGeoWFA
{
    public partial class SavePrjForm : Form
    {
        public SavePrjForm()
        {
            InitializeComponent();
            textBox1.Text = "SVMGeo";
            textBox2.Text = Directory.GetCurrentDirectory();
        }

        string prjName = null;
        string prjDir = null;

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "选择工程文件夹";
            fbd.ShowNewFolderButton = true;
            
            if (!(fbd.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            textBox2.Text = fbd.SelectedPath;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            prjName = textBox1.Text;
            prjDir = Path.Combine( textBox2.Text, prjName);
            if (!Directory.Exists(prjDir))
            {
                Directory.CreateDirectory(prjDir);
            }
            string dataDir = Path.Combine(prjDir, "Data");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
            dataDir = Path.Combine(prjDir, "Models");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
            dataDir = Path.Combine(prjDir, "Logs");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
            Close();
        }

        public string getPrjDir()
        {
            return prjDir;
        }

        public string getPrjName()
        {
            return prjName;
        }
    }
}
