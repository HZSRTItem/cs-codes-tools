using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoorSysSelectWFA01
{
    public partial class CoorSysForm : Form
    {
        public CoorSysForm()
        {
            InitializeComponent();
            for (int i = 0; i < CoorSysInfo.PROJCS.Length / 2; i++)
            {
                int irow = dataGridView1.Rows.Add();
                dataGridView1.Rows[irow].Cells[0].Value = CoorSysInfo.PROJCS[i, 0];
                dataGridView1.Rows[irow].Cells[1].Value = CoorSysInfo.PROJCS[i, 1];
            }
        }
    }
}
