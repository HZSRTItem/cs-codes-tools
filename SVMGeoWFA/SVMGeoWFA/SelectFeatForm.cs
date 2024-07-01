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
    public partial class SelectFeatForm : Form
    {
        public SelectFeatForm()
        {
            InitializeComponent();
        }

        List<string> selectList = new List<string>(10);

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    selectList.Add(checkedListBox1.Items[i].ToString());
                }
            }
            Close();
        }

        public string[] getFeatArr()
        {
            return selectList.ToArray();
        }

        public bool AddItemC(string column_name)
        {
            checkedListBox1.Items.Add(column_name);
            checkedListBox1.SetItemChecked(checkedListBox1.Items.Count - 1, true);
            return true;
        }

        public void setDSName(string name)
        {
            textBox1.Text = name;
        }
    }
}
