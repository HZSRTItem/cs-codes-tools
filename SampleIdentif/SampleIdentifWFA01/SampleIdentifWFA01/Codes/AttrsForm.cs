using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleIdentifWFA01
{
    public partial class AttrsForm : Form
    {
        public AttrsForm(DataTable dataTable)
        {
            InitializeComponent();
            dataGridView1.DataSource = dataTable;
        }
    }
}
