using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace DrawingByCategoryWFA
{
    public partial class DBCForm : Form
    {
        public DBCForm(string points_file)
        {
            InitializeComponent();
            DcodePointFile(points_file);
        }

        /// <summary>
        /// 标签
        /// </summary>
        private List<int> DLabels = new List<int>();
        /// <summary>
        /// 标签名
        /// </summary>
        private List<string> DLabelsName = new List<string>();
        /// <summary>
        /// 特证名
        /// </summary>
        private string[] DFeaturesName = null;
        /// <summary>
        /// 每一个特征的数据
        /// </summary>
        private List<double[]> DFeatures = new List<double[]>();
        private double[] DFeatureMin = null;
        private double[] DFeatureMax = null;

        /// <summary>
        /// 添加一个label的名
        /// </summary>
        /// <param name="label_name">一个标签名</param>
        /// <returns>标签名的索引</returns>
        private int GetLabelName(string label_name)
        {
            int i = 0;
            for (; i < DLabelsName.Count; i++)
            {
                if(DLabelsName[i] == label_name)
                {
                    break;
                }
            }
            DLabels.Add(i);
            if (i== DLabelsName.Count)
            {
                DLabelsName.Add(label_name);
            }
            return i;
        }

        /// <summary>
        /// 解析点文件
        /// </summary>
        /// <param name="points_file"></param>
        private void DcodePointFile(string points_file)
        {
            StreamReader sr = new StreamReader(points_file);
            string line = sr.ReadLine();
            string[] lines;
            if (line.Trim() == "" | line == null)
            {
                throw new Exception("The first line of the data file is not empty");
            }
            DFeaturesName = line.Split('\t');
            for (int i = 0; i < DFeaturesName.Length; i++)
            {
                int n_feat = 2;
                for (int j = i + 1; j < DFeaturesName.Length; j++)
                {
                    if (DFeaturesName[i] == DFeaturesName[j])
                    {
                        DFeaturesName[i] += "_1";
                        DFeaturesName[j] += "_" + (n_feat++).ToString();
                    }
                }
            }

            int n_lines = 0;
            while (true)
            {
                line = sr.ReadLine();
                if (line == null)
                {
                    break;
                }
                line = line.Trim();
                if (line == "")
                {
                    continue;
                }
                lines = line.Split('\t');
                if (lines.Length != DFeaturesName.Length)
                {
                    throw new Exception(string.Format("The number of data in row {0} is not equal to the number of features, expect: {1}, acquired: {2}."
                        , n_lines + 1, DFeaturesName.Length, lines.Length));
                }
                GetLabelName(lines[0]);
                n_lines++;
                double[] line_d0 = new double[lines.Length - 1];
                for (int i = 0; i < line_d0.Length; i++)
                {
                    line_d0[i] = double.Parse(lines[i + 1]);
                }
                DFeatures.Add(line_d0);
            }
            TscbX.Items.AddRange(DFeaturesName);
            TscbX.Items.RemoveAt(0);
            TscbY.Items.AddRange(DFeaturesName);
            TscbY.Items.RemoveAt(0);

            TscbX.SelectedIndex = 1;
            TscbY.SelectedIndex = 2;
            DFeatureMin = DFeatures[0].ToArray();
            DFeatureMax = DFeatures[0].ToArray();
            for (int i = 0; i < DFeatures.Count; i++)
            {
                for (int j = 0; j < DFeatures[i].Length; j++)
                {
                    if(DFeatures[i][j] > DFeatureMax[j])
                    {
                        DFeatureMax[j] = DFeatures[i][j];
                    }

                    if (DFeatures[i][j] < DFeatureMin[j])
                    {
                        DFeatureMin[j] = DFeatures[i][j];
                    }
                }
            }
        }
        
        /// <summary>
        /// 对两个列画图
        /// </summary>
        /// <param name="x_index">X轴</param>
        /// <param name="y_index">y轴</param>
        public void Plot(int x_index, int y_index)
        {
            Mchart.Series.Clear();
            for (int i = 0; i < DLabelsName.Count; i++)
            {
                Series series0 = new Series();
                series0.LegendText = DLabelsName[i];
                series0.ChartType = SeriesChartType.Point;
                series0.MarkerColor = DColors[i];
                series0.MarkerStyle = MarkerStyle.Circle;
                Mchart.Series.Add(series0);
            }

            for (int i = 0; i < DLabels.Count; i++)
            {
                Mchart.Series[DLabels[i]].Points.AddXY(DFeatures[i][x_index], DFeatures[i][y_index]);
            }

            Mchart.ChartAreas[0].AxisX.Minimum = DFeatureMin[x_index];
            Mchart.ChartAreas[0].AxisX.Maximum = DFeatureMax[x_index];
            Mchart.ChartAreas[0].AxisY.Minimum = DFeatureMin[y_index];
            Mchart.ChartAreas[0].AxisY.Maximum = DFeatureMax[y_index];
        }

        private void Mchart_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                // 获得坐标
                double xValue = Mchart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                double yValue = Mchart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                TsslX.Text = string.Format("{0, -50:F8}", xValue);
                TsslY.Text = string.Format("{0, -50:F8}", yValue);

                // 移动时，焦点改变
                //if (IsDown)
                //{
                //    double dx = MousePosition.X - MouseP.X;
                //    double dy = MousePosition.Y - MouseP.Y;
                //    chart1.Location = new Point((int)(ChartP.X + dx), (int)(ChartP.Y + dy));
                //}

            }
            catch
            {
                // 出错不显示
                TsslX.Text = "             ";
                TsslY.Text = "             ";
                return;
            }
        }

        private void TsbtnPlot_Click(object sender, EventArgs e)
        {
            Plot(TscbX.SelectedIndex, TscbY.SelectedIndex);
        }


        private Color[] DColors = new Color[255]
        {
            Color.FromArgb(80,7,104),
            Color.FromArgb(65,186,79),
            Color.FromArgb(199,31,19),
            Color.FromArgb(234,45,162),
            Color.FromArgb(33,75,21),
            Color.FromArgb(174,51,156),
            Color.FromArgb(71,213,94),
            Color.FromArgb(249,231,99),
            Color.FromArgb(108,152,95),
            Color.FromArgb(128,123,250),
            Color.FromArgb(185,30,72),
            Color.FromArgb(210,167,123),
            Color.FromArgb(237,247,118),
            Color.FromArgb(202,47,218),
            Color.FromArgb(106,74,22),
            Color.FromArgb(182,3,28),
            Color.FromArgb(236,230,118),
            Color.FromArgb(22,123,33),
            Color.FromArgb(215,196,47),
            Color.FromArgb(22,107,38),
            Color.FromArgb(239,82,5),
            Color.FromArgb(5,193,203),
            Color.FromArgb(63,246,135),
            Color.FromArgb(41,191,158),
            Color.FromArgb(226,135,96),
            Color.FromArgb(65,168,150),
            Color.FromArgb(114,37,38),
            Color.FromArgb(17,30,90),
            Color.FromArgb(129,251,19),
            Color.FromArgb(183,97,67),
            Color.FromArgb(234,130,76),
            Color.FromArgb(178,237,55),
            Color.FromArgb(74,115,151),
            Color.FromArgb(29,226,150),
            Color.FromArgb(174,247,60),
            Color.FromArgb(100,176,224),
            Color.FromArgb(37,128,161),
            Color.FromArgb(1,163,140),
            Color.FromArgb(52,140,131),
            Color.FromArgb(115,3,244),
            Color.FromArgb(235,169,114),
            Color.FromArgb(79,31,42),
            Color.FromArgb(227,4,160),
            Color.FromArgb(246,252,133),
            Color.FromArgb(207,37,242),
            Color.FromArgb(72,242,86),
            Color.FromArgb(72,223,156),
            Color.FromArgb(205,43,124),
            Color.FromArgb(217,95,164),
            Color.FromArgb(84,187,201),
            Color.FromArgb(234,26,245),
            Color.FromArgb(80,24,193),
            Color.FromArgb(58,95,215),
            Color.FromArgb(192,165,187),
            Color.FromArgb(203,110,17),
            Color.FromArgb(20,229,72),
            Color.FromArgb(248,32,113),
            Color.FromArgb(78,164,71),
            Color.FromArgb(161,243,230),
            Color.FromArgb(54,87,10),
            Color.FromArgb(117,156,128),
            Color.FromArgb(60,248,81),
            Color.FromArgb(219,28,118),
            Color.FromArgb(88,253,21),
            Color.FromArgb(39,229,148),
            Color.FromArgb(91,221,186),
            Color.FromArgb(79,120,54),
            Color.FromArgb(121,31,99),
            Color.FromArgb(152,193,102),
            Color.FromArgb(137,97,20),
            Color.FromArgb(179,18,225),
            Color.FromArgb(226,98,84),
            Color.FromArgb(85,244,213),
            Color.FromArgb(136,231,155),
            Color.FromArgb(35,245,163),
            Color.FromArgb(61,213,113),
            Color.FromArgb(199,7,155),
            Color.FromArgb(98,228,70),
            Color.FromArgb(9,34,160),
            Color.FromArgb(162,93,158),
            Color.FromArgb(239,172,150),
            Color.FromArgb(124,135,120),
            Color.FromArgb(180,137,21),
            Color.FromArgb(244,30,157),
            Color.FromArgb(16,4,237),
            Color.FromArgb(9,83,29),
            Color.FromArgb(133,199,20),
            Color.FromArgb(68,208,236),
            Color.FromArgb(194,31,209),
            Color.FromArgb(81,104,102),
            Color.FromArgb(133,88,196),
            Color.FromArgb(205,205,94),
            Color.FromArgb(225,123,47),
            Color.FromArgb(51,201,107),
            Color.FromArgb(187,226,219),
            Color.FromArgb(103,226,126),
            Color.FromArgb(186,176,181),
            Color.FromArgb(172,144,154),
            Color.FromArgb(174,195,247),
            Color.FromArgb(170,190,143),
            Color.FromArgb(116,111,59),
            Color.FromArgb(101,32,95),
            Color.FromArgb(136,134,49),
            Color.FromArgb(26,48,50),
            Color.FromArgb(238,89,43),
            Color.FromArgb(141,165,136),
            Color.FromArgb(121,24,146),
            Color.FromArgb(31,10,59),
            Color.FromArgb(92,253,52),
            Color.FromArgb(199,99,144),
            Color.FromArgb(246,199,20),
            Color.FromArgb(122,228,231),
            Color.FromArgb(172,97,55),
            Color.FromArgb(207,233,114),
            Color.FromArgb(31,3,104),
            Color.FromArgb(67,70,114),
            Color.FromArgb(228,117,34),
            Color.FromArgb(240,185,184),
            Color.FromArgb(60,248,171),
            Color.FromArgb(254,239,204),
            Color.FromArgb(162,62,120),
            Color.FromArgb(0,115,24),
            Color.FromArgb(244,29,92),
            Color.FromArgb(101,90,231),
            Color.FromArgb(75,134,195),
            Color.FromArgb(230,130,113),
            Color.FromArgb(179,61,31),
            Color.FromArgb(93,56,208),
            Color.FromArgb(22,145,146),
            Color.FromArgb(79,238,24),
            Color.FromArgb(82,152,13),
            Color.FromArgb(209,111,38),
            Color.FromArgb(133,55,89),
            Color.FromArgb(92,185,105),
            Color.FromArgb(144,119,195),
            Color.FromArgb(169,137,191),
            Color.FromArgb(238,21,123),
            Color.FromArgb(50,95,21),
            Color.FromArgb(216,69,245),
            Color.FromArgb(208,131,119),
            Color.FromArgb(7,254,87),
            Color.FromArgb(174,154,202),
            Color.FromArgb(164,80,156),
            Color.FromArgb(151,30,86),
            Color.FromArgb(38,12,239),
            Color.FromArgb(235,155,113),
            Color.FromArgb(202,178,155),
            Color.FromArgb(114,104,116),
            Color.FromArgb(19,240,71),
            Color.FromArgb(20,193,123),
            Color.FromArgb(69,206,147),
            Color.FromArgb(135,65,132),
            Color.FromArgb(155,99,91),
            Color.FromArgb(55,163,186),
            Color.FromArgb(100,56,167),
            Color.FromArgb(156,8,227),
            Color.FromArgb(238,199,199),
            Color.FromArgb(37,177,39),
            Color.FromArgb(136,121,105),
            Color.FromArgb(248,120,203),
            Color.FromArgb(190,115,194),
            Color.FromArgb(73,59,161),
            Color.FromArgb(6,137,144),
            Color.FromArgb(203,17,121),
            Color.FromArgb(183,223,87),
            Color.FromArgb(34,53,130),
            Color.FromArgb(5,61,232),
            Color.FromArgb(134,45,228),
            Color.FromArgb(169,2,141),
            Color.FromArgb(149,103,20),
            Color.FromArgb(187,25,44),
            Color.FromArgb(65,84,141),
            Color.FromArgb(89,237,81),
            Color.FromArgb(203,67,168),
            Color.FromArgb(0,220,64),
            Color.FromArgb(252,189,251),
            Color.FromArgb(235,255,175),
            Color.FromArgb(24,116,214),
            Color.FromArgb(187,175,31),
            Color.FromArgb(150,187,10),
            Color.FromArgb(210,234,194),
            Color.FromArgb(180,184,145),
            Color.FromArgb(183,95,252),
            Color.FromArgb(40,198,11),
            Color.FromArgb(167,104,22),
            Color.FromArgb(95,27,104),
            Color.FromArgb(167,133,19),
            Color.FromArgb(88,93,102),
            Color.FromArgb(23,117,251),
            Color.FromArgb(199,84,130),
            Color.FromArgb(230,62,135),
            Color.FromArgb(236,23,69),
            Color.FromArgb(191,254,183),
            Color.FromArgb(36,182,249),
            Color.FromArgb(168,90,187),
            Color.FromArgb(152,73,202),
            Color.FromArgb(2,85,240),
            Color.FromArgb(194,143,141),
            Color.FromArgb(168,212,18),
            Color.FromArgb(173,190,62),
            Color.FromArgb(97,152,141),
            Color.FromArgb(178,8,122),
            Color.FromArgb(143,217,181),
            Color.FromArgb(169,209,11),
            Color.FromArgb(83,62,224),
            Color.FromArgb(176,193,247),
            Color.FromArgb(134,219,42),
            Color.FromArgb(124,152,157),
            Color.FromArgb(18,111,161),
            Color.FromArgb(78,8,109),
            Color.FromArgb(171,32,167),
            Color.FromArgb(174,165,233),
            Color.FromArgb(246,238,76),
            Color.FromArgb(228,226,79),
            Color.FromArgb(221,66,160),
            Color.FromArgb(61,151,111),
            Color.FromArgb(182,29,175),
            Color.FromArgb(64,213,195),
            Color.FromArgb(231,93,109),
            Color.FromArgb(174,215,47),
            Color.FromArgb(105,18,116),
            Color.FromArgb(219,101,107),
            Color.FromArgb(46,41,123),
            Color.FromArgb(129,177,45),
            Color.FromArgb(58,39,184),
            Color.FromArgb(235,113,78),
            Color.FromArgb(248,145,34),
            Color.FromArgb(12,23,249),
            Color.FromArgb(22,119,28),
            Color.FromArgb(76,61,163),
            Color.FromArgb(143,149,98),
            Color.FromArgb(174,251,146),
            Color.FromArgb(68,66,11),
            Color.FromArgb(152,222,88),
            Color.FromArgb(79,63,149),
            Color.FromArgb(45,103,144),
            Color.FromArgb(182,85,77),
            Color.FromArgb(89,13,18),
            Color.FromArgb(195,174,106),
            Color.FromArgb(73,236,3),
            Color.FromArgb(73,120,206),
            Color.FromArgb(132,56,6),
            Color.FromArgb(106,61,80),
            Color.FromArgb(180,18,245),
            Color.FromArgb(12,106,221),
            Color.FromArgb(81,166,223),
            Color.FromArgb(70,69,149),
            Color.FromArgb(234,152,19),
            Color.FromArgb(171,187,222),
            Color.FromArgb(182,227,176),
            Color.FromArgb(6,21,93),
            Color.FromArgb(226,122,169),
            Color.FromArgb(183,84,9),
            Color.FromArgb(80,87,251),
            Color.FromArgb(222,74,219)
        };


    }
}
