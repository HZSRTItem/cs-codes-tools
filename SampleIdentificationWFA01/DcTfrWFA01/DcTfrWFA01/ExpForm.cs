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

namespace DcTfrWFA01
{
    public partial class ExpForm : Form
    {
        public ExpForm()
        {
            InitializeComponent();
            FileNameBiaoJi = Path.Combine(Directory.GetCurrentDirectory(), "cl_.txt");
            FileNameData = Path.Combine(Directory.GetCurrentDirectory(), "d_.npy");
            textBox1.Text = FileNameBiaoJi;
            textBox2.Text = FileNameData;
        }

        public string FileNameBiaoJi = "";
        public string FileNameData = "";
        public bool IsCal = false;

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Txt File (*.txt)|*.txt";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                FileNameBiaoJi = sfd.FileName;
                textBox1.Text = sfd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Numpy File (*.Npy)|*.npy";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileNameData = sfd.FileName;
                textBox2.Text = sfd.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IsCal = true;
            FileNameBiaoJi = textBox1.Text;
            FileNameData = textBox2.Text;
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
