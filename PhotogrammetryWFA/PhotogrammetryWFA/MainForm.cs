using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PhotogrammetryWFA
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region 属性
        /// <summary>
        /// 单航带网概算对象
        /// </summary>
        SingleFlightNetwork Sfn;
        /// <summary>
        /// 计算报告
        /// </summary>
        private string Report = "";
        /// <summary>
        /// 文件读取对象
        /// </summary>
        private FileRead2Write F2r = new FileRead2Write();
        /// <summary>
        /// DEM计算对象
        /// </summary>
        private DEMInter demInter;
        /// <summary>
        /// 是否绘制DEM
        /// </summary>
        private bool IsDrawOriginDem = false;
        /// <summary>
        /// 是否绘制DEM
        /// </summary>
        private bool IsFlightCal = false;
        /// <summary>
        /// 是否绘制DEM
        /// </summary>
        private bool IsDrawFlight = false;

        #endregion

        #region 单航带网
        /// <summary>
        /// 读取单航带网数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFlightTxtClick(object sender, EventArgs e)
        {
            try
            {
                tcFightDem.SelectedIndex = 0;
                tcFlight.SelectedIndex = 0;

                // 清空之前的计算设置
                rtxreportFlight.Text = "";
                tcFlight.SelectedIndex = 0;
                chartFlightNetwork.Series[0].Points.Clear();
                chartFlightNetwork.Series[1].Points.Clear();
                chartFlightNetwork.Series[2].Points.Clear();
                dgvFlight.DataSource = null;
                IsFlightCal = false;
                IsDrawFlight = false;

                Sfn = new SingleFlightNetwork();
                DataTable dt = new DataTable();
                Sfn = F2r.ReadFlight(ref Report, ref dt);

                dgvFlight.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        /// <summary>
        /// 单航带网计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateFlightClick(object sender, EventArgs e)
        {

            try
            {
                tcFightDem.SelectedIndex = 0;
                if (Sfn != null)
                {
                    Sfn.Calculate();
                    Report += Sfn.Report;
                    rtxreportFlight.Text = Report;
                    IsFlightCal = true;
                }
                else
                {
                    MessageBox.Show("尚未导入数据");
                    return;
                }
                MessageBox.Show("计算完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
        }

        /// <summary>
        /// 航带点绘制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawSingleFlightClick(object sender, EventArgs e)
        {

            try
            {
                tcFightDem.SelectedIndex = 0;
                tcFlight.SelectedIndex = 1;
                if (IsDrawFlight)
                {
                    MessageBox.Show("已经绘制地面点坐标图");
                    return;
                }

                if (!IsFlightCal)
                {
                    MessageBox.Show("尚未计算不能绘图");
                    return;
                }

                for (int i = 0; i < Sfn.AllPoint.Length; i++)
                {
                    if (Sfn.AllPoint[i].Style == "像点")
                    {
                        chartFlightNetwork.Series[0].Points.AddXY(Sfn.AllPoint[i].Y, Sfn.AllPoint[i].X);
                        chartFlightNetwork.Series[0].Points[chartFlightNetwork.Series[0].Points.Count - 1].Label = Sfn.PhotoPointName[i];
                    }
                    else if (Sfn.AllPoint[i].Style == "控制点")
                    {
                        chartFlightNetwork.Series[2].Points.AddXY(Sfn.AllPoint[i].Y, Sfn.AllPoint[i].X);
                        chartFlightNetwork.Series[2].Points[chartFlightNetwork.Series[2].Points.Count - 1].Label = Sfn.PhotoPointName[i];

                    }
                    else
                    {
                        chartFlightNetwork.Series[1].Points.AddXY(Sfn.AllPoint[i].Y, Sfn.AllPoint[i].X);
                        chartFlightNetwork.Series[1].Points[chartFlightNetwork.Series[1].Points.Count - 1].Label = Sfn.PhotoPointName[i];

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void SaveReportClick(object sender, EventArgs e)
        {
            FileRead2Write.SaveReport(rtxreportFlight.Text);
        }

        private void 保存航带示意图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileRead2Write.SaveImage(chartFlightNetwork);
        }

        #endregion

        #region DEM内插
        /// <summary>
        /// 读取DEM数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenDEMTxtClick(object sender, EventArgs e)
        {

            try
            {
                tcFightDem.SelectedIndex = 1;

                // 清空
                tsbtnDemKong_Click(sender, e);
                chartDEM.Series[0].Points.Clear();
                dgvDEMOrignal.DataSource = null;
                IsDrawOriginDem = false;

                DataTable dt = new DataTable();
                dt.Columns.Add("点名");
                dt.Columns.Add("X(m)");
                dt.Columns.Add("Y(m)");
                dt.Columns.Add("Z(m)");
                List<double[]> datalist = F2r.ReadDEMData(dt);
                demInter = new DEMInter(datalist, datalist.Count, 6);
                dgvDEMOrignal.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


           
        }

        /// <summary>
        /// 计算内插高程点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateDEMClick(object sender, EventArgs e)
        {
            tcFightDem.SelectedIndex = 1;

            if(demInter == null)
            {
                MessageBox.Show("尚未导入数据");
                return;
            }

            try 
            {
                double x = double.Parse(tstxtInterX.Text);
                double y = double.Parse(tstxtInterY.Text);
                double Z = demInter.Calculate(x, y);
                int NDem = dgvDEMPoint.Rows.Add();
                dgvDEMPoint.Rows[NDem].Cells[0].Value = NDem.ToString();
                dgvDEMPoint.Rows[NDem].Cells[1].Value = x.ToString("F3");
                dgvDEMPoint.Rows[NDem].Cells[2].Value = y.ToString("F3");
                dgvDEMPoint.Rows[NDem].Cells[3].Value = Z.ToString("F3");

                // 绘图
                // 画原始DEM
                if (!IsDrawOriginDem)
                {
                    for (int i = 0; i < demInter.DataList.Count; i++)
                    {
                        chartDEM.Series[0].Points.AddXY(demInter.DataList[i][0], demInter.DataList[i][1]);
                        chartDEM.Series[0].Points[i].Label = demInter.DataList[i][2].ToString("F3") + "m";
                    }
                    IsDrawOriginDem = true;
                }

                chartDEM.Series[1].Points.AddXY(x, y);
                chartDEM.Series[1].Points[chartDEM.Series[1].Points.Count - 1].Label = 
                    dgvDEMPoint.Rows[dgvDEMPoint.Rows.Count-2].Cells[0].Value.ToString() + " : " + Z.ToString("F3") +"m";

                MessageBox.Show("已经计算出插值点高程" + Z.ToString("F3"));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 清空DEM已算数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDemKong_Click(object sender, EventArgs e)
        {
            tcFightDem.SelectedIndex = 1;

            chartDEM.Series[1].Points.Clear();
            dgvDEMPoint.Rows.Clear();
        }


        private void 保存DEM示意图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileRead2Write.SaveImage(chartDEM);
        }
        #endregion

        #region 图形操作

        /// <summary>
        /// 坐标显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XYMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Chart chart = sender as Chart;
                var area = chart.ChartAreas[0];
                double xValue = area.AxisX.PixelPositionToValue(e.X);
                double yValue = area.AxisY.PixelPositionToValue(e.Y);
                tsslX.Text = string.Format("{0:F3}m", yValue);
                tsslY.Text = string.Format("{0:F3}m", xValue);
            }
            catch
            {

            }
        }

        #endregion



        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本程序包括单航带计算和DEM内插", "帮助");
        }

        private void chartDEM_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Chart chart = sender as Chart;
                var area = chart.ChartAreas[0];
                double x = area.AxisX.PixelPositionToValue(e.X);
                double y = area.AxisY.PixelPositionToValue(e.Y);
                double Z = demInter.Calculate(x, y);
                int NDem = dgvDEMPoint.Rows.Add();
                dgvDEMPoint.Rows[NDem].Cells[0].Value = NDem.ToString();
                dgvDEMPoint.Rows[NDem].Cells[1].Value = x.ToString("F3");
                dgvDEMPoint.Rows[NDem].Cells[2].Value = y.ToString("F3");
                dgvDEMPoint.Rows[NDem].Cells[3].Value = Z.ToString("F3");
                chartDEM.Series[1].Points.AddXY(x, y);
                chartDEM.Series[1].Points[chartDEM.Series[1].Points.Count - 1].Label =
                    dgvDEMPoint.Rows[dgvDEMPoint.Rows.Count - 2].Cells[0].Value.ToString() + " : " + Z.ToString("F3") + "m";

            }
            catch
            {

            }
        }



    }
}
