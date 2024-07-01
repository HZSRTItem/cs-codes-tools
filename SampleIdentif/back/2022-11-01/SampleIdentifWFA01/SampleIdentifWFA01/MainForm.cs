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
            Tfr2PngForm tfr2PngForm = new Tfr2PngForm();
            tfr2PngForm.ShowDialog();
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
            sampleDT = openInitForm.OSampleDT;
            ChangeCross(PanelH1, PanelV1, ImbShow.Width, ImbShow.Height, 3, 31);
            ChangeCross(PanelH2, PanelV2, MapBoxShow.Width, MapBoxShow.Height, 3, 31);
            ChangeCross(PanelH3, PanelV3, GMapControlShow.Width, GMapControlShow.Height, 3, 31);

            //指定地图缓存存放路径
            GMapControlShow.CacheLocation = Path.Combine(sampleDT.PrjDirName, "GMapCache");
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
            //左键拖拽地图
            //GMapControlShow.DragButton = System.Windows.Forms.MouseButtons.Left;
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
                DgvSplInfo.Rows[i].Cells[0].Value = sampleDT.GetColumnName(i);
                DgvSplInfo.Rows[i].Cells[0].ReadOnly = true;
                DgvSplInfo.Rows[i].Cells[1].ReadOnly = true;
            }
            for (; i < sampleDT.CountColumn; i++)
            {
                DgvSplInfo.Rows.Add();
                DgvSplInfo.Rows[i].Cells[0].Value = sampleDT.GetColumnName(i);
                DgvSplInfo.Rows[i].Cells[0].ReadOnly = true;
            }

            RenderDgvClasses();
            //GoToSample(0);

            int n = sampleDT.NPic;
            // 单个视图:  存在就设置为图像，不存在就这样吧
            ImbShow.Image = Image.FromFile(sampleDT.GetSingleImageFile(n));

            // 高分辨率影像
            GMapControlShow.Position = new PointLatLng(sampleDT.Y[n], sampleDT.X[n]);
            // 信息表格
            for (i = 0; i < sampleDT.CountColumn; i++)
            {
                string name = DgvSplInfo.Rows[i].Cells[0].Value.ToString();
                DgvSplInfo.Rows[i].Cells[1].Value = sampleDT[name, n];
            }
            DgvSplInfo.Rows[0].Cells[0].Selected = false;
            // 当前样本类型
            TxtClasses.Text = sampleDT.GetCategoryIndex(n);
            TstxtSearch.Text = (n + 1).ToString() + " / " + sampleDT.CountRows.ToString();
            TstxtGoTo.Text = (n + 1).ToString();
            // 交叉线颜色
            PanelV1.BackColor = sampleDT.GetCategoryColor(n);
            PanelV2.BackColor = sampleDT.GetCategoryColor(n);
            PanelV3.BackColor = sampleDT.GetCategoryColor(n);
            PanelH1.BackColor = sampleDT.GetCategoryColor(n);
            PanelH2.BackColor = sampleDT.GetCategoryColor(n);
            PanelH3.BackColor = sampleDT.GetCategoryColor(n);
            sampleDT.NPic = n;

            TxtClasses.Focus();
            TxtClasses.Select(0, TxtClasses.TextLength);

            if (sampleDT.ORemoteImageFile != null)
            {
                GdalRasterLayer gdalRasterLayer = new GdalRasterLayer("raster0", sampleDT.ORemoteImageFile);
                MapBoxShow.Map.Layers.Add(gdalRasterLayer);
                MapBoxShow.Map.ZoomToExtents();
                //MapBoxShow.ActiveTool = MapBox.Tools.Pan;
                MapBoxShow.ZoomToPointer = false;
                MapBoxShow.Map.Zoom = 0.02;
                MapBoxShow.Refresh();
                MapBoxShow.Map.Center = new Coordinate(sampleDT.X[n], sampleDT.Y[n]);
                //MapBoxShow.Refresh();
            }

            // 遥感总视图: 有的话就加进去，否则就不用加进去
            //if (sampleDT.ORemoteImageFile != null)
            //{
            //    MapBoxShow.Map.Center = new Coordinate(sampleDT.Y[n], sampleDT.X[n]);
            //    MapBoxShow.Refresh();
            //}

            isbuild = true;
        }

        #region 属性
        /// <summary>
        /// 是否开始播放
        /// </summary>
        private bool IsPlay = false;
        /// <summary>
        /// 影像渲染对象
        /// </summary>
        private SharpMapRender m_SharpMapRender;
        private SampleDT sampleDT;
        #endregion

        #region 处理事件
        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            ChangeCross(PanelH1, PanelV1, ImbShow.Width, ImbShow.Height, 3, 31);
        }

        private void mapBox1_SizeChanged(object sender, EventArgs e)
        {
            ChangeCross(PanelH2, PanelV2, MapBoxShow.Width, MapBoxShow.Height, 3, 31);
        }

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
                SampleNext(1);
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
            if (sampleDT.CountRows == 0)
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
                    if (n < 0 | n > sampleDT.CountRows)
                    {
                        MessageBox.Show($"样本数量错误，[0, {sampleDT.CountRows}");
                    }
                    GoToSample(n);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region 辅助函数

        /// <summary>
        /// 下一个
        /// </summary>
        private void SampleNext(int fangxiang)
        {
            if (sampleDT.CountRows == 0)
            {
                TmrPlay.Stop();
                IsPlay = false;
                return;
            }
            else if (sampleDT.NPic <= 0 & fangxiang == -1)
            {
                TmrPlay.Stop();
                IsPlay = false;
                MessageBox.Show("已经到第一个样本");
                sampleDT.NPic = sampleDT.CountRows - 1;
                GoToSample(sampleDT.NPic);
                return;
            }
            else if (sampleDT.NPic >= sampleDT.CountRows - 1 & fangxiang == 1)
            {
                TmrPlay.Stop();
                IsPlay = false;
                MessageBox.Show("已经到最后一个样本");
                GoToSample(0);
                sampleDT.NPic = 0;
                return;
            }
            else
            {
                try
                {
                    GoToSample(sampleDT.NPic + fangxiang);
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
            if (i_cate >= sampleDT.CateInfo.Count | i_cate < 0)
            {
                TmrPlay.Stop();
                IsPlay = false;
                MessageBox.Show("错误的类别编号", "错误");
                return false;
            }
            else
            {
                sampleDT.ChangeClass(i_cate);
                // 保存上一个的信息
                for (int i = 4; i < sampleDT.CountColumn; i++)
                {
                    string name = DgvSplInfo.Rows[i].Cells[0].Value.ToString();
                    sampleDT[name, sampleDT.NPic] = DgvSplInfo.Rows[i].Cells[1].Value.ToString();
                }
                // 渲染当前的信息

                // 单个视图:  存在就设置为图像，不存在就这样吧
                ImbShow.Image = Image.FromFile(sampleDT.GetSingleImageFile(n));
                // 遥感总视图: 有的话就加进去，否则就不用加进去
                if (sampleDT.ORemoteImageFile != null)
                {
                    MapBoxShow.Map.Center = new Coordinate(sampleDT.X[n], sampleDT.Y[n]);
                    MapBoxShow.Refresh();
                }
                // 高分辨率影像
                GMapControlShow.Position = new PointLatLng(sampleDT.Y[n], sampleDT.X[n]);
                // 信息表格
                for (int i = 0; i < sampleDT.CountColumn; i++)
                {
                    string name = DgvSplInfo.Rows[i].Cells[0].Value.ToString();
                    DgvSplInfo.Rows[i].Cells[1].Value = sampleDT[name, n];
                }
                DgvSplInfo.Rows[0].Cells[0].Selected = false;
                // 当前样本类型
                TxtClasses.Text = sampleDT.GetCategoryIndex(n);
                TstxtSearch.Text = (n + 1).ToString() + " / " + sampleDT.CountRows.ToString();
                TstxtGoTo.Text = (n + 1).ToString();
                // 交叉线颜色
                Color c = sampleDT.GetCategoryColor(n);
                PanelV1.BackColor = c;
                PanelV2.BackColor = c;
                PanelV3.BackColor = c;
                PanelH1.BackColor = c;
                PanelH2.BackColor = c;
                PanelH3.BackColor = c;
                sampleDT.NPic = n;

                sampleDT.Save();
                for (int i = 0; i < DgvClasses.Rows.Count - 1; i++)
                {
                    DgvClasses.Rows[i].Cells[3].Value = sampleDT.CateInfo[i].number;
                }
                TxtClasses.Focus();
                TxtClasses.Select(0, TxtClasses.TextLength);
                return true;
            }
        }

        /// <summary>
        /// 渲染类别表格
        /// </summary>
        private void RenderDgvClasses()
        {
            DgvClasses.Rows.Clear();
            for (int i = 0; i < sampleDT.CateInfo.Count; i++)
            {
                int nrow = DgvClasses.Rows.Add();
                DgvClasses.Rows[nrow].Cells[0].Style.BackColor = sampleDT.CateInfo[i].color;
                DgvClasses.Rows[nrow].Cells[1].Value = i.ToString();
                DgvClasses.Rows[nrow].Cells[2].Value = sampleDT.CateInfo[i].name;
                DgvClasses.Rows[nrow].Cells[3].Value = sampleDT.CateInfo[i].number;
            }
            DgvClasses.Rows[0].Cells[0].Selected = false;
        }

        /// <summary>
        /// 渲染第N个样本
        /// </summary>
        /// <param name="n"></param>
        private void RenderSample(int n)
        {
            // 单个视图:  存在就设置为图像，不存在就这样吧
            ImbShow.Image = Image.FromFile(sampleDT.GetSingleImageFile(n));
            // 遥感总视图: 有的话就加进去，否则就不用加进去
            if (sampleDT.ORemoteImageFile != null)
            {
                MapBoxShow.Map.Center = new Coordinate(sampleDT.Y[n], sampleDT.X[n]);
                MapBoxShow.Refresh();
            }
            // 高分辨率影像
            GMapControlShow.Position = new PointLatLng(sampleDT.Y[n], sampleDT.X[n]);
            // 信息表格
            for (int i = 0; i < sampleDT.CountColumn; i++)
            {
                string name = DgvSplInfo.Rows[i].Cells[0].Value.ToString();
                DgvSplInfo.Rows[i].Cells[1].Value = sampleDT[name, n];
            }
            DgvSplInfo.Rows[0].Cells[0].Selected = false;
            // 当前样本类型
            TxtClasses.Text = sampleDT.GetCategoryIndex(n);
            TstxtSearch.Text = (n + 1).ToString() + " / " + sampleDT.CountRows.ToString();
            TstxtGoTo.Text = (n + 1).ToString();
            // 交叉线颜色
            PanelV1.BackColor = sampleDT.GetCategoryColor(n);
            PanelV2.BackColor = sampleDT.GetCategoryColor(n);
            PanelV3.BackColor = sampleDT.GetCategoryColor(n);
            PanelH1.BackColor = sampleDT.GetCategoryColor(n);
            PanelH2.BackColor = sampleDT.GetCategoryColor(n);
            PanelH3.BackColor = sampleDT.GetCategoryColor(n);
            sampleDT.NPic = n;
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
        #endregion

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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            BuildPrjForm buildPrjForm = new BuildPrjForm();
            buildPrjForm.ShowDialog();
        }

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

        private void TsbtnSaveConstruction_Click(object sender, EventArgs e)
        {

        }

        private void TsmiLookPoints_Click(object sender, EventArgs e)
        {

        }

        private void TsbtnImportData_Click(object sender, EventArgs e)
        {

        }
    }
}
