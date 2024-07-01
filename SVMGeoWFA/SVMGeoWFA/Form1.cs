using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SVMGeoWFA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            SvmType_Cb.SelectedIndex = 0;
            KernelType_Cb.SelectedIndex = 2;
        }

        OpenFileDialog ofd = new OpenFileDialog();
        SVMDataSetList SVMDSList = new SVMDataSetList();
        SaveFileDialog sfd = new SaveFileDialog();
        ModTrainTestList ModTTList = new ModTrainTestList();
        SavePrjForm savePrjForm = new SavePrjForm();

        string prjDir = null;
        string prjName = null;

        private void TsbtnImportSvmData_Click(object sender, EventArgs e)
        {

            ofd.Filter = "Text File (*.csv,*.txt)|*.csv;*.txt";
            ofd.Title = "导入SVM格式的数据文件";

            if (!(ofd.ShowDialog() == DialogResult.OK))
            {
                return;
            }
            //try
            //{
            SVMDSList.AddFromSvmDataFile(ofd.FileName);
            RenderTVDataSet();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        private void RenderTVDataSet(int n = -1)
        {
            treeView1.Nodes["NodeDataSets"].Nodes.Clear();
            TscbScatterDS.Items.Clear();
            CbTestDS.Items.Clear();
            CbTrainDS.Items.Clear();
            for (int i = 0; i < SVMDSList.Count; i++)
            {
                treeView1.Nodes["NodeDataSets"].Nodes.Add(SVMDSList[i].Name);
                TscbScatterDS.Items.Add(SVMDSList[i].Name);
                TscbScatterDS.SelectedIndex = 0;
                CbTestDS.Items.Add(SVMDSList[i].Name);
                CbTestDS.SelectedIndex = 0;
                CbTrainDS.Items.Add(SVMDSList[i].Name);
                CbTrainDS.SelectedIndex = 0;
            }
            if (n < 0)
            {
                DgvData.DataSource = SVMDSList[SVMDSList.Count + n].DT;
                TstxtDataSetName.Text = SVMDSList[SVMDSList.Count + n].Name;
            }
            else
            {
                DgvData.DataSource = SVMDSList[n].DT;
                TstxtDataSetName.Text = SVMDSList[n].Name;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)  //单击鼠标左键才响应
            {
                if (e.Node.Level == 1)                               //判断子节点才响应
                {
                    //文件框中显示鼠标点击的节点名称
                    DgvData.DataSource = SVMDSList[e.Node.Text].DT;
                    TstxtDataSetName.Text = e.Node.Text;
                }
            }
        }

        private void TsbtnImportCSV_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Text File (*.csv,*.txt)|*.csv;*.txt";
            ofd.Title = "导入CSV格式的数据文件";

            if (!(ofd.ShowDialog() == DialogResult.OK))
            {
                return;
            }
            //try
            //{
            SVMDSList.AddFromCSVFile(ofd.FileName);
            RenderTVDataSet();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void DgvData_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void TsbtnExportSVMData_Click(object sender, EventArgs e)
        {
            sfd.Title = "保存数据为SVMData格式的文件";
            sfd.Filter = "Text File (*.txt)|*.txt";

            if (!(sfd.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            if (SVMDSList[TstxtDataSetName.Text].SaveToSvmDataFile(sfd.FileName))
            {
                MessageBox.Show("已经保存 " + sfd.FileName);
            }
        }

        private void TsbtnExportCSV_Click(object sender, EventArgs e)
        {
            sfd.Title = "保存数据为SVMData格式的文件";
            sfd.Filter = "Text File (*.csv,*.txt)|*.csv;*.txt";

            if (!(sfd.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            if (SVMDSList[TstxtDataSetName.Text].SaveToCsvFile(sfd.FileName))
            {
                MessageBox.Show("已经保存 " + sfd.FileName);
            }
        }

        private void TscbScatterDS_TextChanged(object sender, EventArgs e)
        {
            SVMDataSet ds = SVMDSList[TscbScatterDS.Text];
            TscbScatterX.Items.Clear();
            TscbScatterY.Items.Clear();
            TscbScatterC.Items.Clear();
            TscbScatterC.Items.Add(" ");
            for (int i = 0; i < ds.DT.Columns.Count; i++)
            {
                TscbScatterX.Items.Add(ds.DT.Columns[i].ColumnName);
                TscbScatterY.Items.Add(ds.DT.Columns[i].ColumnName);
                TscbScatterC.Items.Add(ds.DT.Columns[i].ColumnName);
                TscbScatterX.SelectedIndex = 0;
                TscbScatterY.SelectedIndex = 0;
                TscbScatterC.SelectedIndex = 0;
            }
        }

        private void TsbtnScatter_Click(object sender, EventArgs e)
        {
            SVMDataSet ds = SVMDSList[TscbScatterDS.Text];
            chart1.Series.Clear();
            if (TscbScatterC.Text == " ")
            {
                chart1.Series.Add("S1");
                chart1.Series[0].ChartType = SeriesChartType.Point;
                chart1.Series[0].MarkerColor = Color.Gray;
                chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
                for (int i = 0; i < ds.DT.Rows.Count; i++)
                {
                    chart1.Series[0].Points.AddXY(
                       double.Parse(ds.DT.Rows[i][TscbScatterX.Text].ToString()),
                       double.Parse(ds.DT.Rows[i][TscbScatterY.Text].ToString()));
                }
            }
            else
            {
                ColumnCateorys columnCateorys = new ColumnCateorys();
                for (int i = 0; i < ds.DT.Rows.Count; i++)
                {
                    columnCateorys.Add(ds.DT.Rows[i][TscbScatterC.Text].ToString());
                }
                for (int i = 0; i < columnCateorys.Count; i++)
                {
                    chart1.Series.Add(columnCateorys.GetNameByIndex(i));
                    chart1.Series[i].ChartType = SeriesChartType.Point;
                    chart1.Series[i].MarkerColor = SRTUtils.GetColor(i);
                    chart1.Series[i].MarkerStyle = MarkerStyle.Circle;
                    int[] indexs = columnCateorys.GetIndexByIndex(i);
                    for (int j = 0; j < indexs.Length; j++)
                    {
                        chart1.Series[i].Points.AddXY(
                           double.Parse(ds.DT.Rows[indexs[j]][TscbScatterX.Text].ToString()),
                           double.Parse(ds.DT.Rows[indexs[j]][TscbScatterY.Text].ToString()));
                    }
                }
            }
        }

        private void TsbtnScatterClear_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
        }

        private void CbTrainDS_TextChanged(object sender, EventArgs e)
        {
            if (CbTrainDS.Text == "")
            {
                return;
            }
            CbTrainLabelColumn.Items.Clear();
            SVMDataSet ds = SVMDSList[CbTrainDS.Text];
            string tt = "";
            for (int i = 0; i < ds.DT.Columns.Count; i++)
            {
                CbTrainLabelColumn.Items.Add(ds.DT.Columns[i].ColumnName);
                tt += ds.DT.Columns[i].ColumnName;
                tt += ",";
            }
            TxtTrainFeatColumn.Text = tt.Substring(0, tt.Length - 1);
            CbTrainLabelColumn.SelectedIndex = 0;

        }

        private void CbTestDS_TextChanged(object sender, EventArgs e)
        {
            if (CbTestDS.Text == "")
            {
                return;
            }
            CbTestLabelColumn.Items.Clear();
            SVMDataSet ds = SVMDSList[CbTestDS.Text];
            string tt = "";
            for (int i = 0; i < ds.DT.Columns.Count; i++)
            {
                CbTestLabelColumn.Items.Add(ds.DT.Columns[i].ColumnName);
                tt += ds.DT.Columns[i].ColumnName;
                tt += ",";
            }
            TxtTestFeatColumn.Text = tt.Substring(0, tt.Length - 1);
            CbTestLabelColumn.SelectedIndex = 0;
        }

        private void TsbtnStartTrain_Click(object sender, EventArgs e)
        {

            ModTrainTest modTrainTest = AddModel();

            // 添加参数
            modTrainTest.addTrainArg("-s", SvmType_Cb.SelectedIndex.ToString());
            modTrainTest.addTrainArg("-t", KernelType_Cb.SelectedIndex.ToString());
            modTrainTest.addTrainArg("-d", textBox1.Text);
            modTrainTest.addTrainArg("-g", textBox2.Text);
            modTrainTest.addTrainArg("-r", textBox3.Text);
            modTrainTest.addTrainArg("-c", textBox4.Text);
            modTrainTest.addTrainArg("-n", textBox5.Text);
            modTrainTest.addTrainArg("-p", textBox6.Text);
            modTrainTest.addTrainArg("-m", textBox15.Text);
            modTrainTest.addTrainArg("-e", textBox7.Text);
            modTrainTest.addTrainArg("-h", textBox16.Text);
            modTrainTest.addTrainArg("-b", textBox17.Text);
            modTrainTest.addTrainArg("-wi", textBox18.Text);
            modTrainTest.addTrainArg("-v", textBox19.Text);

            //string ss = "";
            //ss += "-s" + " " + SvmType_Cb.SelectedIndex.ToString() + "\n";
            //ss += "-t" + " " + KernelType_Cb.SelectedIndex.ToString() + "\n";
            //ss += "-d" + " " + textBox1.Text + "\n";
            //ss += "-g" + " " + textBox2.Text + "\n";
            //ss += "-r" + " " + textBox3.Text + "\n";
            //ss += "-c" + " " + textBox4.Text + "\n";
            //ss += "-n" + " " + textBox5.Text + "\n";
            //ss += "-p" + " " + textBox6.Text + "\n";
            //ss += "-m" + " " + textBox15.Text + "\n";
            //ss += "-e" + " " + textBox7.Text + "\n";
            //ss += "-h" + " " + textBox16.Text + "\n";
            //ss += "-b" + " " + textBox17.Text + "\n";
            //ss += "-wi" + " " + textBox18.Text + "\n";
            //ss += "-v" + " " + textBox19.Text + "\n";

            // 添加训练数据集
            modTrainTest.svmDS = SVMDSList[CbTrainDS.Text];
            string[] lines = TxtTrainFeatColumn.Text.Split(',');
            modTrainTest.clearTrainColumnNames();
            for (int i = 0; i < lines.Length; i++)
            {
                modTrainTest.addTrainColumnName(lines[i]);
            }
            modTrainTest.trainCateColumnName = CbTrainLabelColumn.Text;

            modTrainTest.train();

            TrbRun.Text = modTrainTest.trainLine;
        }

        ModTrainTest AddModel()
        {
            ModTrainTest modTrainTest = ModTTList.addOne(TxtModelName.Text, getModelDir());
            if (modTrainTest == null)
            {
                return ModTTList[TxtModelName.Text];
            }
            else
            {
                return modTrainTest;
            }
        }

        string getPrjDir()
        {
            if (prjDir == null)
            {
                savePrjForm.ShowDialog();
                prjDir = savePrjForm.getPrjDir();
                prjName = savePrjForm.getPrjName();
            }
            return prjDir;
        }

        public string getModelDir()
        {
            if (prjDir == null)
            {
                savePrjForm.ShowDialog();
                prjDir = savePrjForm.getPrjDir();
                prjName = savePrjForm.getPrjName();
            }
            return Path.Combine(prjDir, "Models");
        }

        private void BtnTrainFeatColumn_Click(object sender, EventArgs e)
        {
            SelectFeatForm selectFeatForm = new SelectFeatForm();
            SVMDataSet ds = SVMDSList[CbTrainDS.Text];
            for (int i = 0; i < ds.DT.Columns.Count; i++)
            {
                selectFeatForm.AddItemC(ds.DT.Columns[i].ColumnName);
            }
            selectFeatForm.setDSName("Train: " + ds.Name);
            selectFeatForm.ShowDialog();
            TxtTrainFeatColumn.Text = string.Join(",", selectFeatForm.getFeatArr());
        }

        private void BtnTestFeatColumn_Click(object sender, EventArgs e)
        {
            SelectFeatForm selectFeatForm = new SelectFeatForm();
            SVMDataSet ds = SVMDSList[CbTestDS.Text];
            for (int i = 0; i < ds.DT.Columns.Count; i++)
            {
                selectFeatForm.AddItemC(ds.DT.Columns[i].ColumnName);
            }
            selectFeatForm.setDSName("Train: " + ds.Name);
            selectFeatForm.ShowDialog();
            TxtTestFeatColumn.Text = string.Join(",", selectFeatForm.getFeatArr());
        }
    }
}
