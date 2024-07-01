using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SVMGeoWFA
{
    public partial class ExportToCsv_SelectCate_Form : Form
    {
        public ExportToCsv_SelectCate_Form()
        {
            InitializeComponent();
            
        }

        public bool AddItemC(string column_name)
        {
            comboBox1.Items.Add(column_name);
            comboBox1.SelectedIndex = 0;
            return true;
        }

        public string select_name = "";

        private void button2_Click(object sender, EventArgs e)
        {
            select_name = comboBox1.Text;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
