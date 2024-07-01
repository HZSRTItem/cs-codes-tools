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
using SharpMap;
using SharpMap.Layers;
using SharpMap.Forms;
using GeoAPI.Geometries;
using System.Drawing.Drawing2D;
using SampleIdentifWFA01.Codes;
using GMap.NET.MapProviders;
using GMap.NET;

namespace SampleIdentifWFA01
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        public bool isbuild = false;

        /// <summary>
        /// 初始化参数
        /// </summary>
        private void Init()
        {
            OpenInitForm openInitForm = new OpenInitForm();
            openInitForm.ShowDialog();
            if (openInitForm.isbuild == false)
            {
                isbuild = false;
                return;
            }
            OSampleDT = openInitForm.OSampleDT;
            SampleDT.XmlSerializerSampleDT(OSampleDT.PrjXmlFileName, OSampleDT);

            ChangeCross(PanelH1, PanelV1, ImbShow.Width, ImbShow.Height, 3, 31);
            ChangeCross(PanelH2, PanelV2, MapBoxShow.Width, MapBoxShow.Height, 3, 31);
            ChangeCross(PanelH3, PanelV3, GMapControlShow.Width, GMapControlShow.Height, 3, 31);

            //指定地图缓存存放路径
            GMapControlShow.CacheLocation = OSampleDT.GMapCacheDir;
            //指定地图类型
            GMapControlShow.MapProvider = GMapProviders.GoogleChinaMap;
            //地图加载模式（有三种模式）
            GMapControlShow.Manager.Mode = AccessMode.ServerAndCache;
            //地图最小比例
            GMapControlShow.MinZoom = 1;
            //地图最大比例  
            GMapControlShow.MaxZoom = 18;
            //当前比例
            GMapControlShow.Zoom = 5;
            //不显示中心十字点
            GMapControlShow.ShowCenter = false;
            //默认显示地址 经，纬
            //GMapControlShow.Position = new PointLatLng(30.67, 104.06);//成都
            GMapControlShow.MouseWheelZoomType = MouseWheelZoomType.ViewCenter;

            // 添加高清影像
            TscbGmap.Items.Add("Bing Hybrid Map");
            TscbGmap.Items.Add("Bing Map");
            TscbGmap.Items.Add("Bing OS Map");
            TscbGmap.Items.Add("Bing Satellite Map");
            TscbGmap.Items.Add("Open Street Map");
            TscbGmap.Items.Add("Open Street Map Quest Satellite");
            TscbGmap.Items.Add("Google Map");
            TscbGmap.Items.Add("Google Satellite Map");

            // 添加属性信息
            int i = 0;
            for (; i < 4; i++)
            {
                DgvSplInfo.Rows.Add();
                DgvSplInfo.Rows[i].Cells[0].Value = OSampleDT.GetColumnName(i);
                DgvSplInfo.Rows[i].Cells[0].ReadOnly = true;
                DgvSplInfo.Rows[i].Cells[1].ReadOnly = true;
            }
            for (; i < OSampleDT.CountColumn; i++)
            {
                DgvSplInfo.Rows.Add();
                DgvSplInfo.Rows[i].Cells[0].Value = OSampleDT.GetColumnName(i);
                DgvSplInfo.Rows[i].Cells[0].ReadOnly = true;
            }
            RenderDgvClasses();

            // 当前渲染点
            int n = OSampleDT.NPic;

            // 单个视图:  存在就设置为图像，不存在就这样吧
            string png_file = OSampleDT.GetSingleImageFile(n);
            if (png_file != null)
            {
                ImbShow.Image = Image.FromFile(png_file);
            }

            // 高分辨率影像
            GMapControlShow.Position = new PointLatLng(OSampleDT.Y[n], OSampleDT.X[n]);
            // 信息表格
            for (i = 0; i < OSampleDT.CountColumn; i++)
            {
                string name = DgvSplInfo.Rows[i].Cells[0].Value.ToString();
                DgvSplInfo.Rows[i].Cells[1].Value = OSampleDT[name, n];
            }
            DgvSplInfo.Rows[0].Cells[0].Selected = false;
            // 当前样本类型
            TxtClasses.Text = OSampleDT.GetCategoryIndex(n);
            TstxtSearch.Text = (n + 1).ToString() + " / " + OSampleDT.CountRows.ToString();
            TstxtGoTo.Text = (n + 1).ToString();
            // 交叉线颜色
            PanelV1.BackColor = OSampleDT.GetCategoryColor(n);
            PanelV2.BackColor = OSampleDT.GetCategoryColor(n);
            PanelV3.BackColor = OSampleDT.GetCategoryColor(n);
            PanelH1.BackColor = OSampleDT.GetCategoryColor(n);
            PanelH2.BackColor = OSampleDT.GetCategoryColor(n);
            PanelH3.BackColor = OSampleDT.GetCategoryColor(n);

            TxtClasses.Focus();
            TxtClasses.Select(0, TxtClasses.TextLength);

            if (OSampleDT.ORemoteImageFile != null)
            {
                GdalRasterLayer gdalRasterLayer = new GdalRasterLayer("raster0", OSampleDT.ORemoteImageFile);
                MapBoxShow.Map.Layers.Add(gdalRasterLayer);
                MapBoxShow.Map.ZoomToExtents();
                //MapBoxShow.ActiveTool = MapBox.Tools.Pan;
                MapBoxShow.ZoomToPointer = false;
                MapBoxShow.Map.Zoom = 0.02;
                MapBoxShow.Refresh();
                MapBoxShow.Map.Center = new Coordinate(OSampleDT.X[n], OSampleDT.Y[n]);
                //MapBoxShow.Refresh();
            }
            TmrPlay.Interval = 1000;


            DgvClasses.Rows[0].Cells[0].Selected = false;
            isbuild = true;
        }

        #region 属性
        /// <summary>
        /// 是否开始播放
        /// </summary>
        private bool IsPlay = false;
        /// <summary>
        /// 样本对象
        /// </summary>
        private SampleDT OSampleDT;
        SaveFileDialog sfd = new SaveFileDialog();
        #endregion

        #region 处理事件
        /// <summary>
        /// 中心十字线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            ChangeCross(PanelH1, PanelV1, ImbShow.Width, ImbShow.Height, 3, 31);
        }

        /// <summary>
        /// 中心十字线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mapBox1_SizeChanged(object sender, EventArgs e)
        {
            ChangeCross(PanelH2, PanelV2, MapBoxShow.Width, MapBoxShow.Height, 3, 31);
        }

        /// <summary>
        /// 中心十字线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GMapControlShow_SizeChanged(object sender, EventArgs e)
        {
            ChangeCross(PanelH3, PanelV3, GMapControlShow.Width, GMapControlShow.Height, 3, 31);

        }

        /// <summary>
        /// 回车决定开始播放和结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtClasses_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                PlayOrNot();
                //SampleNext(1);
            }
        }

        /// <summary>
        /// 左右像元移动键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtClasses_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                SampleNext(-1);
            }
            if (e.KeyCode == Keys.Right)
            {
                SampleNext(1);
            }
        }

        /// <summary>
        /// 测试按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            BuildPrjForm buildPrjForm = new BuildPrjForm();
            buildPrjForm.ShowDialog();
        }

        /// <summary>
        /// 计时播放功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrPlay_Tick(object sender, EventArgs e)
        {
            SampleNext(1);
        }

        /// <summary>
        /// 绘制原始的像元图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImbShow_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = ImbShow;
            if (pic.Image == null)
                return;
            GraphicsState state = e.Graphics.Save();
            e.Graphics.SmoothingMode = SmoothingMode.None;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
            e.Graphics.Clear(ImbShow.BackColor);
            //e.Graphics.DrawImage(pic.Image, new Rectangle(0, 0, pic.Width, pic.Height), new Rectangle(0, 0, pic.Image.Width, pic.Image.Height), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(pic.Image, new RectangleF(1, 1, pic.Width - 1, pic.Height - 1), new RectangleF(0, 0, pic.Image.Width, pic.Image.Height), GraphicsUnit.Pixel);
            e.Graphics.Restore(state);
        }

        /// <summary>
        /// 选高分辨率的影像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TscbGmap_TextChanged(object sender, EventArgs e)
        {
            string text = TscbGmap.Text;
            switch (text)
            {
                case "Bing Hybrid Map":
                    GMapControlShow.MapProvider = GMapProviders.BingHybridMap;
                    break;

                case "Bing Map":
                    GMapControlShow.MapProvider = GMapProviders.BingMap;
                    break;

                case "Bing OS Map":
                    GMapControlShow.MapProvider = GMapProviders.BingOSMap;
                    break;

                case "Bing Satellite Map":
                    GMapControlShow.MapProvider = GMapProviders.BingSatelliteMap;
                    break;

                case "Open Street Map":
                    GMapControlShow.MapProvider = GMapProviders.OpenStreetMap;
                    break;

                case "Open Street Map Quest Satellite":
                    GMapControlShow.MapProvider = GMapProviders.OpenStreetMapQuestSatellite;
                    break;

                case "Google Map":
                    GMapControlShow.MapProvider = GMapProviders.GoogleMap;
                    break;

                case "Google Satellite Map":
                    GMapControlShow.MapProvider = GMapProviders.GoogleSatelliteMap;
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region 点击事件
        /// <summary>
        /// 下一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNext_Click(object sender, EventArgs e)
        {
            SampleNext(1);
        }

        /// <summary>
        /// 按钮上一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrior_Click(object sender, EventArgs e)
        {
            SampleNext(-1);
        }

        /// <summary>
        /// 计时播放点击开始和结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnTimerPlay_Click(object sender, EventArgs e)
        {
            if (OSampleDT.CountRows == 0)
            {
                return;
            }
            PlayOrNot();
        }

        /// <summary>
        /// 转到第几个样本点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnGoto_Click(object sender, EventArgs e)
        {
            if (!IsPlay)
            {
                try
                {
                    int n = int.Parse(TstxtGoTo.Text);
                    if (n < 0 | n > OSampleDT.CountRows)
                    {
                        MessageBox.Show($"样本数量错误，[0, {OSampleDT.CountRows}");
                    }
                    GoToSample(n - 1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void TsbtnSaveConstruction_Click(object sender, EventArgs e)
        {

        }

        private void TsmiLookPoints_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 导入样本数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnImportData_Click(object sender, EventArgs e)
        {

        }

        private void TsmiTfr2Png_Click(object sender, EventArgs e)
        {
            Tfr2PngForm tfr2PngForm = new Tfr2PngForm();
            tfr2PngForm.Show();
        }

        /// <summary>
        /// 保存工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiSaveConstruction_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 添加单张影像集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbtnAddSingleImage_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Directory.GetCurrentDirectory();

            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            OSampleDT.AddSingleImages(fbd.SelectedPath);
            SampleDT.XmlSerializerSampleDT(OSampleDT.PrjXmlFileName, OSampleDT);
            GoToSample(OSampleDT.NPic);
        }

        /// <summary>
        /// 属性表点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiLookAttrs_Click(object sender, EventArgs e)
        {
            AttrsForm attrsForm = new AttrsForm(OSampleDT.SplDataT);
            attrsForm.Show();
        }

        private void DgvClasses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                ColorDialog color_d = new ColorDialog();
                color_d.Color = DgvClasses.Rows[e.RowIndex].Cells[0].Style.BackColor;
                color_d.ShowDialog();
                DgvClasses.Rows[e.RowIndex].Cells[0].Style.BackColor = color_d.Color;
                DgvClasses.Rows[e.RowIndex].Cells[0].Selected = false;
            }
        }
        #endregion

        #region 辅助函数
        /// <summary>
        /// 下一个
        /// </summary>
        private void SampleNext(int fangxiang)
        {
            if (OSampleDT.CountRows == 0)
            {
                TmrPlay.Stop();
                IsPlay = false;
                return;
            }
            else if (OSampleDT.NPic <= 0 & fangxiang == -1)
            {
                TmrPlay.Stop();
                IsPlay = false;
                MessageBox.Show("已经到第一个样本");
                OSampleDT.NPic = OSampleDT.CountRows - 1;
                GoToSample(OSampleDT.NPic);
                return;
            }
            else if (OSampleDT.NPic >= OSampleDT.CountRows - 1 & fangxiang == 1)
            {
                TmrPlay.Stop();
                IsPlay = false;
                MessageBox.Show("已经到最后一个样本");
                GoToSample(0);
                OSampleDT.NPic = 0;
                return;
            }
            else
            {
                try
                {
                    GoToSample(OSampleDT.NPic + fangxiang);
                }
                catch (Exception ex)
                {
                    TmrPlay.Stop();
                    IsPlay = false;
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// 到第n个样本
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private bool GoToSample(int n)
        {
            // 修改前一个的信息
            int i_cate = int.Parse(TxtClasses.Text);
            if (i_cate >= OSampleDT.CateInfo.Count | i_cate < 0)
            {
                TmrPlay.Stop();
                IsPlay = false;
                MessageBox.Show("错误的类别编号", "错误");
                return false;
            }
            else
            {
                OSampleDT.ChangeClass(i_cate);
                // 保存上一个的信息
                for (int i = 4; i < DgvSplInfo.Rows.Count - 1; i++)
                {
                    string name = DgvSplInfo.Rows[i].Cells[0].Value.ToString();
                    OSampleDT[name, OSampleDT.NPic] = DgvSplInfo.Rows[i].Cells[1].Value.ToString();
                }
                // 渲染当前的信息
                // 单个视图:  存在就设置为图像，不存在就这样吧
                string png_file = OSampleDT.GetSingleImageFile(n);
                if (png_file != null)
                {
                    ImbShow.Image = Image.FromFile(png_file);
                }
                // 遥感总视图: 有的话就加进去，否则就不用加进去
                if (OSampleDT.ORemoteImageFile != null)
                {
                    MapBoxShow.Map.Center = new Coordinate(OSampleDT.X[n], OSampleDT.Y[n]);
                    MapBoxShow.Refresh();
                }
                // 高分辨率影像
                GMapControlShow.Position = new PointLatLng(OSampleDT.Y[n], OSampleDT.X[n]);
                // 信息表格
                for (int i = 0; i < OSampleDT.CountColumn; i++)
                {
                    string name = DgvSplInfo.Rows[i].Cells[0].Value.ToString();
                    DgvSplInfo.Rows[i].Cells[1].Value = OSampleDT[name, n];
                }
                DgvSplInfo.Rows[0].Cells[0].Selected = false;
                // 当前样本类型
                TxtClasses.Text = OSampleDT.GetCategoryIndex(n);
                TstxtSearch.Text = (n + 1).ToString() + " / " + OSampleDT.CountRows.ToString();
                TstxtGoTo.Text = (n + 1).ToString();
                // 交叉线颜色
                Color c = OSampleDT.GetCategoryColor(n);
                PanelV1.BackColor = c;
                PanelV2.BackColor = c;
                PanelV3.BackColor = c;
                PanelH1.BackColor = c;
                PanelH2.BackColor = c;
                PanelH3.BackColor = c;
                OSampleDT.NPic = n;
                for (int i = 0; i < DgvClasses.Rows.Count - 1; i++)
                {
                    DgvClasses.Rows[i].Cells[3].Value = OSampleDT.CateInfo[i].number;
                }
                TxtClasses.Focus();
                TxtClasses.Select(0, TxtClasses.TextLength);
                RenderDgvClasses();

                SampleDT.XmlSerializerSampleDT(OSampleDT.PrjXmlFileName, OSampleDT);
                return true;
            }
        }

        /// <summary>
        /// 渲染类别表格
        /// </summary>
        private void RenderDgvClasses()
        {
            for (int i = 0; i < DgvClasses.Rows.Count - 1; i++)
            {
                OSampleDT.SetCateInfoName(i, DgvClasses.Rows[i].Cells[2].Value.ToString());
                OSampleDT.SetCateInfoColor(i, DgvClasses.Rows[i].Cells[0].Style.BackColor);
            }
            DgvClasses.Rows.Clear();
            for (int i = 0; i < OSampleDT.CateInfo.Count; i++)
            {
                int nrow = DgvClasses.Rows.Add();
                DgvClasses.Rows[nrow].Cells[0].Style.BackColor = OSampleDT.CateInfo[i].color;
                DgvClasses.Rows[nrow].Cells[1].Value = i.ToString();
                DgvClasses.Rows[nrow].Cells[2].Value = OSampleDT.CateInfo[i].name;
                DgvClasses.Rows[nrow].Cells[3].Value = OSampleDT.CateInfo[i].number;
                DgvClasses.Rows[nrow].Cells[0].Selected = false;
                DgvClasses.Rows[nrow].Cells[1].Selected = false;
                DgvClasses.Rows[nrow].Cells[2].Selected = false;
                DgvClasses.Rows[nrow].Cells[3].Selected = false;
            }
        }

        /// <summary>
        /// 是否播放
        /// </summary>
        private void PlayOrNot()
        {
            if (IsPlay)
            {
                IsPlay = false;
                TmrPlay.Stop();
            }
            else
            {
                IsPlay = true;
                TmrPlay.Start();
            }
        }

        /// <summary>
        /// 设置中心交叉线
        /// </summary>
        /// <param name="ph">横线</param>
        /// <param name="pv">竖线</param>
        /// <param name="c_width">控件宽</param>
        /// <param name="c_height">控件高</param>
        /// <param name="x0">初始竖向偏移</param>
        /// <param name="y0">初始横向偏移</param>
        private void ChangeCross(Panel ph, Panel pv, int c_width, int c_height, int x0, int y0)
        {
            ph.Width = c_width;
            pv.Height = c_height;
            ph.Location = new Point(x0, y0 + c_height / 2);
            pv.Location = new Point(x0 + c_width / 2, y0);
        }

        #endregion

        private void TsbtnExportShapeFile_Click(object sender, EventArgs e)
        {
            sfd.Title = "保存为CSV文件";
            sfd.Filter = "CSV (*.csv)|*.csv";
            if (!(sfd.ShowDialog() == DialogResult.OK))
            {
                return;
            }

            OSampleDT.SaveCSV(sfd.FileName);
            MessageBox.Show("已经将将数据保存为CSV: " + sfd.FileName);

        }

        private void TsmiRasterToInterpret_Click(object sender, EventArgs e)
        {

        }
    }
}
