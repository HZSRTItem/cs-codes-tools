using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ShowPointWFA
{
    public partial class MainForm : Form
    {
        public MainForm(ArgvsFmt argvsFmt)
        {
            InitializeComponent();
            colors[0] = Color.Black;
            colors[1] = Color.Green;
            colors[2] = Color.Red;
            colors[3] = Color.Blue;
            for (int i = 4; i < 256; i++)
            {
                colors[i] = GetRandomColor();
            }
            this.argvsFmt = argvsFmt;
            ClbCategory.Items.Clear();
            for (int i = 0; i < argvsFmt.NColumns; i++)
            {
                ClbCategory.Items.Add(argvsFmt.ColumnNames[i]);
            }

            SeriesNames = argvsFmt.CatetoryColumn.Distinct().ToList();
            ChartMain.Series.Clear();
            for (int i = 0; i < SeriesNames.Count; i++)
            {
                Series s = ChartMain.Series.Add(SeriesNames[i]);
                s.ChartType = SeriesChartType.Point;
                s.Color = colors[i + 1];
                s.MarkerSize = 5;
                s.MarkerStyle = MarkerStyle.Circle;
            }
            if(argvsFmt.NColumns == 1)
            {
                PlotColumn(argvsFmt.ColumnNames[0]);
            }
            else
            {
                PlotColumn(argvsFmt.ColumnNames[0], argvsFmt.ColumnNames[1]);
            }
        }

        ArgvsFmt argvsFmt;
        List<string> SeriesNames = null;
        Color[] colors = new Color[256];

        private void PlotColumn(string x_name, string y_name)
        {
            for (int i = 0; i < SeriesNames.Count; i++)
            {
                ChartMain.Series[i].Points.Clear();
            }
            int x_index = argvsFmt.GetIndex(x_name);
            int y_index = argvsFmt.GetIndex(y_name);
            for (int i = 0; i < argvsFmt.InData.Count; i++)
            {
                int n_series = SeriesNames.IndexOf(argvsFmt.CatetoryColumn[i]);
                double x = x_index == -1 ? i : argvsFmt.InData[i][x_index];
                double y = y_index == -1 ? i : argvsFmt.InData[i][y_index];
                ChartMain.Series[n_series].Points.AddXY(x, y);
            }
        }

        public Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            //  对于C#的随机数，没什么好说的
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
            //  为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            return Color.FromArgb(int_Red, int_Green, int_Blue);
        }

        private void PlotColumn(string y_name)
        {
            for (int i = 0; i < SeriesNames.Count; i++)
            {
                ChartMain.Series[i].Points.Clear();
            }
            int y_index = argvsFmt.GetIndex(y_name);
            for (int i = 0; i < argvsFmt.InData.Count; i++)
            {
                int n_series = SeriesNames.IndexOf(argvsFmt.CatetoryColumn[i]);
                double y = y_index == -1 ? i : argvsFmt.InData[i][y_index];
                ChartMain.Series[n_series].Points.AddXY(i, y);
            }
        }



        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string x_name = null;
            string y_name = null;
            for (int i = 0; i < ClbCategory.Items.Count; i++)
            {
                if(ClbCategory.GetItemChecked(i))
                {
                    if(x_name==null)
                    {
                        x_name = ClbCategory.Items[i].ToString();
                    }
                    else if (y_name == null)
                    {
                        y_name = ClbCategory.Items[i].ToString();
                    }
                }
            }
            if(x_name!=null & y_name != null)
            {
                PlotColumn(x_name, y_name);
            }
            else if(x_name!=null)
            {
                PlotColumn(x_name);
            }
            else
            {
                MessageBox.Show("Please select a plot column name");
            }
        }
    }
}
