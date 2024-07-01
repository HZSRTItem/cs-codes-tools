using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataNormalizationWFA
{
    public partial class progressForm : Form
    {
        public progressForm()
        {
            InitializeComponent();
        }
        public void AddProgress(int n)
        {
            progressBar1.Value = n;
            label1.Text = progressBar1.Value.ToString() + "%";
            label1.Refresh();
        }
    }
}
