using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QgisJYBuildWFA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog ofd = new OpenFileDialog();
        SaveFileDialog sfd = new SaveFileDialog();
        CsvRW Csvrw = new CsvRW();
        CInfos CateInfos = new CInfos();
        ColorDialog color_d = new ColorDialog();
        SRTFileRW srw = new SRTFileRW();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnOpenCsvFile_Click(object sender, EventArgs e)
        {
            ofd.Title = "打开CSV文件";
            ofd.Filter = "Text CSV File (*.csv *.txt)|*.csv;*.txt";

            if (!(ofd.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            try
            {
                TxtCsvFile.Text = ofd.FileName;
                Csvrw.ReadCSV(ofd.FileName);
                string[] names = Csvrw.GetFieldNames();
                CbLField.Items.AddRange(names);
                CbLField.SelectedIndex = 0;
                CbBField.Items.AddRange(names);
                CbBField.SelectedIndex = 1;
                CbCategoryField.Items.AddRange(names);
                CbCategoryField.SelectedIndex = 2;
                CbSRTField.Items.AddRange(names);
                CbSRTField.SelectedIndex = 3;
                DgvRecords.DataSource = Csvrw.dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        private void BtnOpenQJYFile_Click(object sender, EventArgs e)
        {
            sfd.Title = "保存Qgis解译文件";
            sfd.Filter = "Text File (*.txt)|*.txt";

            if (!(sfd.ShowDialog() == DialogResult.OK))
            {
                return;
            }
            TxtQJYFile.Text = sfd.FileName;
        }

        private void BtnAddAll_Click(object sender, EventArgs e)
        {
            string column_name = CbCategoryField.Text.Trim();
            if (!Csvrw.IsKong)
            {
                for (int i = 0; i < Csvrw.NRows; i++)
                {
                    CateInfos.Add(Csvrw[i, column_name]);
                }
            }
            RenderCDGV();
        }

        private void RenderCDGV()
        {
            DgvCategoryInfo.Rows.Clear();
            for (int i = 0; i < CateInfos.Count; i++)
            {
                DgvCategoryInfo.Rows.Add();
                DgvCategoryInfo.Rows[i].Cells[0].Value = CateInfos[i].NCode;
                DgvCategoryInfo.Rows[i].Cells[1].Style.BackColor = CateInfos[i].CColor;
                DgvCategoryInfo.Rows[i].Cells[2].Value = CateInfos[i].Number;
                DgvCategoryInfo.Rows[i].Cells[3].Value = CateInfos[i].Name;
            }
        }

        private void DgvCategoryInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                color_d.Color = DgvCategoryInfo.Rows[e.RowIndex].Cells[1].Style.BackColor;
                color_d.ShowDialog();
                DgvCategoryInfo.Rows[e.RowIndex].Cells[1].Style.BackColor = color_d.Color;
                DgvCategoryInfo.Rows[e.RowIndex].Cells[1].Selected = false;
                CateInfos[DgvCategoryInfo.Rows[e.RowIndex].Cells[3].ToString()].CColor = color_d.Color;
            }
        }

        private void BtnCUp_Click(object sender, EventArgs e)
        {
            if (DgvCategoryInfo.SelectedCells.Count == 0)
            {
                return;
            }
            DataGridViewCell d = DgvCategoryInfo.SelectedCells[0];
            int r = d.RowIndex;
            int c = d.ColumnIndex;
            if (d.RowIndex == 0 | d.RowIndex > DgvCategoryInfo.RowCount - 2)
            {
                return;
            }
            CateInfos.ChangeByRowI(d.RowIndex, d.RowIndex - 1);
            RenderCDGV();
            DgvCategoryInfo.CurrentCell = DgvCategoryInfo.Rows[r - 1].Cells[c];
        }

        private void BtnCDown_Click(object sender, EventArgs e)
        {
            if (DgvCategoryInfo.SelectedCells.Count == 0)
            {
                return;
            }
            DataGridViewCell d = DgvCategoryInfo.SelectedCells[0];
            int r = d.RowIndex;
            int c = d.ColumnIndex;
            if (d.RowIndex >= DgvCategoryInfo.RowCount - 2)
            {
                return;
            }
            CateInfos.ChangeByRowI(d.RowIndex, d.RowIndex + 1);
            RenderCDGV();
            DgvCategoryInfo.CurrentCell = DgvCategoryInfo.Rows[r + 1].Cells[c];
        }

        private void TsbtnExportQinfoFile_Click(object sender, EventArgs e)
        {
            try
            {
                srw.initFromFile(TxtQJYFile.Text);
                srw.Open("w");
                srw.WriteLineMark("CategoryNames");
                for (int i = 0; i < CateInfos.Count; i++)
                {
                    srw.WriteLine(CateInfos[i].Name);
                }
                srw.WriteLineMark("CategoryColors");
                for (int i = 0; i < CateInfos.Count; i++)
                {
                    srw.WriteLine(string.Format("{0},{1},{2}",
                        CateInfos[i].CColor.R, CateInfos[i].CColor.G, CateInfos[i].CColor.B));
                }
                srw.WriteLineMark("Records");
                for (int i = 0; i < Csvrw.NRows; i++)
                {
                    string line = Csvrw[i, CbLField.Text] + ","
                        + Csvrw[i, CbBField.Text] + ","
                        //+ Csvrw[i, CbCategoryField.Text] + ","
                        + CateInfos[Csvrw[i, CbCategoryField.Text]].NCode + ","
                        + "0,"
                        + Csvrw[i, CbSRTField.Text];
                    srw.WriteLine(line);
                }
                srw.Close();
                MessageBox.Show("Save: " + TxtQJYFile.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);

            }
        }
    }

    public class CInfo
    {
        public string Name = "NOT_KNOW";
        public int NCode = 0;
        public int Number = 0;
        public Color CColor = Color.Black;
    }

    public class CInfos
    {
        List<CInfo> infos = new List<CInfo>(10);
        int i_current = 0;
        static Color[] colors =
        {
            Color.Black,
            Color.Red,
            Color.Green,
            Color.Yellow,
            Color.Blue,
            Color.DarkGray
        };
        static Random rd = new Random();

        public int Count { get { return infos.Count; } }

        public CInfos()
        {
            infos.Add(new CInfo());
        }

        public CInfo this[int i]
        {
            set { infos[i] = value; }
            get { return infos[i]; }
        }

        public CInfo this[string name]
        {
            get
            {
                for (int i = 0; i < infos.Count; i++)
                {
                    if (infos[i].Name == name)
                    {
                        return infos[i];
                    }
                }
                return new CInfo();
            }
        }

        public bool Add(string name)
        {
            if (IsIn(name))
            {
                infos[i_current].Number++;
                return true;
            }
            else
            {
                infos.Add(new CInfo
                {
                    Name = name,
                    NCode = getN(),
                    Number = 1,
                    CColor = getC()
                });
                return false;
            }
        }

        public bool IsIn(string name)
        {

            if (infos[i_current].Name == name)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < infos.Count; i++)
                {
                    if (infos[i].Name == name)
                    {
                        i_current = i;
                        return true;
                    }
                }
            }
            return false;
        }

        private int getN()
        {
            int ii = 1;
            while (true)
            {
                bool isf = false;
                for (int i = 0; i < infos.Count; i++)
                {
                    if (ii == infos[i].NCode)
                    {
                        isf = true;
                        break;
                    }
                }
                if (!isf)
                {
                    return ii;
                }
                ii++;
            }
        }

        private Color getC()
        {
            if (infos.Count < colors.Length)
            {
                return colors[infos.Count];
            }
            else
            {
                return Color.FromArgb(rd.Next(0, 255), rd.Next(0, 255), rd.Next(0, 255));
            }
        }

        public void ChangeByRowI(int i0, int i1)
        {
            string tmp = infos[i1].Name;
            infos[i1].Name = infos[i0].Name;
            infos[i0].Name = tmp;
            int i_tmp = infos[i1].Number;
            infos[i1].Number = infos[i0].Number;
            infos[i0].Number = i_tmp;
            Color c_tmp = infos[i1].CColor;
            infos[i1].CColor = infos[i0].CColor;
            infos[i0].CColor = c_tmp;
        }
    }
}
