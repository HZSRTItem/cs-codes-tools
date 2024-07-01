using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ShapeShowWFA
{
    public partial class SSMainForm : Form
    {
        public SSMainForm()
        {
            InitializeComponent();

        }

        private string[] InArgs = null;
        private string LayerName = "";
        private string Geometry = "";
        private int FeatureCount = 0;
        private double[] Extent = new double[4];
        private double[] X;
        private double[] Y;

        public SSMainForm(string[] args)
        {
            InitializeComponent();
            InArgs = args;
            Init();
        }

        private void Init()
        {
            // 获得shape数据
            string info = GetShapeInfo(InArgs[0]);
            string[] lines = info.Split('\n');
            int i_line = 0;

            // 读取图层名 7 
            i_line = 7;
            LayerName = lines[i_line].Split(':')[1].Trim();
            Text = LayerName;

            // Feature Count
            i_line = 11;
            FeatureCount = int.Parse(lines[i_line].Split(':')[1].Trim());
            X = new double[FeatureCount];
            Y = new double[FeatureCount];
            string line = "", line0 = "";
            int i_count = 0;
            string[] liness;

            for (int i = i_line; i < lines.Length; i++)
            {
                line = lines[i];

                if (line.Length < 9)
                {
                    continue;
                }

                if (line.Substring(0, 8) == "  POINT ")
                {
                    line0 = line.Substring(9, line.Length - 11);
                    liness = line0.Split(' ');
                    X[i_count] = double.Parse(liness[0]);
                    Y[i_count] = double.Parse(liness[1]);
                    i_count++;
                }
            }

            TstxtNFeatures.Text = FeatureCount.ToString();

            for (int i = 0; i < FeatureCount; i++)
            {
                chart1.Series[0].Points.AddXY(X[i], Y[i]);
            }

            chart1.ChartAreas[0].AxisX.Minimum = X.Min();
            chart1.ChartAreas[0].AxisY.Minimum = Y.Min();
            chart1.ChartAreas[0].AxisX.Maximum = X.Max();
            chart1.ChartAreas[0].AxisY.Maximum = Y.Max();

        }

        /*
            // 获得shape数据
            string info = GetShapeInfo(InArgs[0]);
            string[] lines = info.Split('\n');
            int i_line = 0;

            // 读取图层名 7 
            i_line = 7;
            LayerName = lines[i_line].Split(':')[1].Trim();
            Text = LayerName;

            // Geometry
            i_line = 10;
            Geometry = lines[i_line].Split(':')[1].Trim();

            // Feature Count
            i_line = 11;
            FeatureCount = int.Parse(lines[i_line].Split(':')[1].Trim());

            // Extent
            i_line = 12;
            string line = lines[i_line].Split(':')[1].Trim();
            string[] lines0 = line.Split('-');
            lines0
         */

        private void toolStripButton1_Click(object sender, EventArgs e)
        {


        }

        public static string GetShapeInfo(string shp_file)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    // 是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true; // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true; // 由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true; // 重定向标准错误输出
            p.StartInfo.CreateNoWindow = true; // 不显示程序窗口
            p.Start(); // 启动程序
            string in_str = "ogrinfo -al " + shp_file;
            p.StandardInput.WriteLine(in_str + " &exit"); // 向cmd窗口发送输入信息
            p.StandardInput.AutoFlush = true;
            string output = p.StandardOutput.ReadToEnd(); // 获取cmd窗口的输出信息
            p.WaitForExit(); // 等待程序执行完退出进程
            p.Close();
            return output;
        }

        // 图形缩放点击次数
        private int CliclNumber = 0;
        // 鼠标是否点下
        private bool IsDown = false;
        // 鼠标焦点
        private PointF MouseP;
        // 表格焦点
        private PointF ChartP;

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                // 获得坐标
                double xValue = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                double yValue = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                TsslX.Text = string.Format("{0, -50:F8}", yValue);
                TsslY.Text = string.Format("{0, -50:F8}", xValue);

                // 移动时，焦点改变
                if (IsDown)
                {
                    double dx = MousePosition.X - MouseP.X;
                    double dy = MousePosition.Y - MouseP.Y;
                    chart1.Location = new Point((int)(ChartP.X + dx), (int)(ChartP.Y + dy));
                }

            }
            catch
            {
                // 出错不显示
                TsslX.Text = "             ";
                TsslY.Text = "             ";
                return;
            }
        }
    }

    class GeoLayer
    {
        public int NFeature = 0;
        public int ShapeType = 0;

        public double[] PointFeatures;
        public List<double[]> LineFeatures = new List<double[]>();
        public List<double[]> PolygonFeatures = new List<double[]>();

        public List<string> IntFieldName = new List<string>();
        public List<int[]> IntField = new List<int[]>();

        public List<string> DoubleFieldName = new List<string>();
        public List<double[]> DoubleField = new List<double[]>();

        public List<string> StringFieldName = new List<string>();
        public List<string[]> StringField = new List<string[]>(); 

        public GeoLayer(string shp_type, int n_feature)
        {
            switch (shp_type)
            {
                case "Point":
                    ShapeType = 1;
                    break;

                case "Line String":
                    ShapeType = 2;
                    break;

                case "Polygon":
                    ShapeType = 3;
                    break;

                default:
                    ShapeType = 0;
                    break;
            }

            NFeature = n_feature;
        }
    }
}
