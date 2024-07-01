using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET;
using SharpMap.Forms;
using SharpMap.Layers;

namespace GMapTest01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MapBox mapBox1 = new MapBox();
            mapBox1.Dock = DockStyle.Fill;
            tabPage2.Controls.Add(mapBox1);
            //VectorLayer vlay = new VectorLayer("States");
            GdalRasterLayer gdalRasterLayer = new GdalRasterLayer("raster0", @"E:\ImageData\Shadow\Imd\sj_qd_esa1.tif");
            mapBox1.Map.Layers.Add(gdalRasterLayer);
            mapBox1.Map.ZoomToExtents();
            mapBox1.Refresh();
            mapBox1.ActiveTool = MapBox.Tools.Pan;
        }

        private void t02ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.EmptyProvider;
            gMapControl1.Manager.Mode = AccessMode.ServerOnly; // 设置从服务器获取地图数据
            gMapControl1.MinZoom = 2; // 最小比例
            gMapControl1.MaxZoom = 16; // 最大比例
            gMapControl1.Zoom = 100; // 当前比例
            gMapControl1.ShowCenter = true; // 显示中心十字标记
            gMapControl1.DragButton = MouseButtons.Left; // 左键拖拽地图
            gMapControl1.MouseWheelZoomType = MouseWheelZoomType.ViewCenter;
            gMapControl1.Position = new PointLatLng(35.963687, 120.176016);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            gMapControl1.MapProvider = GoogleSatelliteMapProvider.Instance;
            gMapControl1.Manager.Mode = AccessMode.ServerOnly; // 设置从服务器获取地图数据
            gMapControl1.MinZoom = 2; // 最小比例
            gMapControl1.MaxZoom = 16; // 最大比例
            gMapControl1.Zoom = 100; // 当前比例
            gMapControl1.ShowCenter = true; // 显示中心十字标记
            gMapControl1.DragButton = MouseButtons.Left; // 左键拖拽地图
            gMapControl1.MouseWheelZoomType = MouseWheelZoomType.ViewCenter;
            gMapControl1.Position = new PointLatLng(35.963687, 120.176016);
            // ttToolStripMenuItem.DropDownItems.AddRange()
            ToolStripMenuItem tt;

            tt = new ToolStripMenuItem() { Text = "CzechTuristOldMap" }; tt.Click += CzechTuristOldMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "CzechHybridOldMap" }; tt.Click += CzechHybridOldMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "CzechSatelliteOldMap" }; tt.Click += CzechSatelliteOldMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "CzechOldMap" }; tt.Click += CzechOldMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "SpainMap" }; tt.Click += SpainMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "CloudMadeMap" }; tt.Click += CloudMadeMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "TurkeyMap" }; tt.Click += TurkeyMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "MapBenderWMSdemoMap" }; tt.Click += MapBenderWMSdemoMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "LatviaMap" }; tt.Click += LatviaMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "LithuaniaTOP50Map" }; tt.Click += LithuaniaTOP50Map_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "LithuaniaHybridOldMap" }; tt.Click += LithuaniaHybridOldMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "LithuaniaHybridMap" }; tt.Click += LithuaniaHybridMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "LithuaniaOrtoFotoOldMap" }; tt.Click += LithuaniaOrtoFotoOldMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "LithuaniaOrtoFotoMap" }; tt.Click += LithuaniaOrtoFotoMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "Lithuania3dMap" }; tt.Click += Lithuania3dMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "CzechHistoryOldMap" }; tt.Click += CzechHistoryOldMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "LithuaniaReliefMap" }; tt.Click += LithuaniaReliefMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "CzechMap" }; tt.Click += CzechMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "CzechHybridMap" }; tt.Click += CzechHybridMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "SwedenMap" }; tt.Click += SwedenMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "ArcGIS_DarbAE_Q2_2011_NAVTQ_Eng_V5_Map" }; tt.Click += ArcGIS_DarbAE_Q2_2011_NAVTQ_Eng_V5_Map_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "ArcGIS_World_Topo_Map" }; tt.Click += ArcGIS_World_Topo_Map_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "ArcGIS_World_Terrain_Base_Map" }; tt.Click += ArcGIS_World_Terrain_Base_Map_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "ArcGIS_World_Street_Map" }; tt.Click += ArcGIS_World_Street_Map_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "ArcGIS_World_Shaded_Relief_Map" }; tt.Click += ArcGIS_World_Shaded_Relief_Map_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "ArcGIS_World_Physical_Map" }; tt.Click += ArcGIS_World_Physical_Map_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "ArcGIS_Topo_US_2D_Map" }; tt.Click += ArcGIS_Topo_US_2D_Map_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "ArcGIS_StreetMap_World_2D_Map" }; tt.Click += ArcGIS_StreetMap_World_2D_Map_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "ArcGIS_ShadedRelief_World_2D_Map" }; tt.Click += ArcGIS_ShadedRelief_World_2D_Map_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "ArcGIS_Imagery_World_2D_Map" }; tt.Click += ArcGIS_Imagery_World_2D_Map_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "CzechGeographicMap" }; tt.Click += CzechGeographicMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "CzechHistoryMap" }; tt.Click += CzechHistoryMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "CzechTuristWinterMap" }; tt.Click += CzechTuristWinterMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "CzechTuristMap" }; tt.Click += CzechTuristMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "CzechSatelliteMap" }; tt.Click += CzechSatelliteMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "YandexHybridMap" }; tt.Click += YandexHybridMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "LithuaniaMap" }; tt.Click += LithuaniaMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "GoogleMap" }; tt.Click += GoogleMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "YahooHybridMap" }; tt.Click += YahooHybridMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "YahooSatelliteMap" }; tt.Click += YahooSatelliteMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "YahooMap" }; tt.Click += YahooMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "BingHybridMap" }; tt.Click += BingHybridMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "BingSatelliteMap" }; tt.Click += BingSatelliteMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "BingMap" }; tt.Click += BingMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "YandexSatelliteMap" }; tt.Click += YandexSatelliteMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "WikiMapiaMap" }; tt.Click += WikiMapiaMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "OpenStreetMapQuestHybrid" }; tt.Click += OpenStreetMapQuestHybrid_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "OpenStreetMapQuestSattelite" }; tt.Click += OpenStreetMapQuestSattelite_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "OpenStreetMapQuest" }; tt.Click += OpenStreetMapQuest_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "OpenCycleTransportMap" }; tt.Click += OpenCycleTransportMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "OpenCycleLandscapeMap" }; tt.Click += OpenCycleLandscapeMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "OpenCycleMap" }; tt.Click += OpenCycleMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "OpenSeaMapHybrid" }; tt.Click += OpenSeaMapHybrid_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "GoogleSatelliteMap" }; tt.Click += GoogleSatelliteMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "GoogleHybridMap" }; tt.Click += GoogleHybridMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "GoogleTerrainMap" }; tt.Click += GoogleTerrainMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "YandexMap" }; tt.Click += YandexMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "OviTerrainMap" }; tt.Click += OviTerrainMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "OviHybridMap" }; tt.Click += OviHybridMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "OviSatelliteMap" }; tt.Click += OviSatelliteMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "OviMap" }; tt.Click += OviMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "NearHybridMap" }; tt.Click += NearHybridMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "NearSatelliteMap" }; tt.Click += NearSatelliteMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "NearMap" }; tt.Click += NearMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "GoogleKoreaHybridMap" }; tt.Click += GoogleKoreaHybridMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "GoogleKoreaSatelliteMap" }; tt.Click += GoogleKoreaSatelliteMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "GoogleKoreaMap" }; tt.Click += GoogleKoreaMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "GoogleChinaTerrainMap" }; tt.Click += GoogleChinaTerrainMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "GoogleChinaHybridMap" }; tt.Click += GoogleChinaHybridMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "GoogleChinaSatelliteMap" }; tt.Click += GoogleChinaSatelliteMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "GoogleChinaMap" }; tt.Click += GoogleChinaMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "OpenStreet4UMap" }; tt.Click += OpenStreet4UMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);
            tt = new ToolStripMenuItem() { Text = "OpenStreetMap" }; tt.Click += OpenStreetMap_Click; ttToolStripMenuItem.DropDownItems.Add(tt);

        }


        private void t01ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.BingSatelliteMap;
        }


        private void CzechTuristOldMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.CzechTuristOldMap; textBox1.Text = "CzechTuristOldMap"; }
        private void CzechHybridOldMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.CzechHybridOldMap; textBox1.Text = "CzechHybridOldMap"; }
        private void CzechSatelliteOldMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.CzechSatelliteOldMap; textBox1.Text = "CzechSatelliteOldMap"; }
        private void CzechOldMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.CzechOldMap; textBox1.Text = "CzechOldMap"; }
        private void SpainMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.SpainMap; textBox1.Text = "SpainMap"; }
        private void CloudMadeMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.CloudMadeMap; textBox1.Text = "CloudMadeMap"; }
        private void TurkeyMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.TurkeyMap; textBox1.Text = "TurkeyMap"; }
        private void MapBenderWMSdemoMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.MapBenderWMSdemoMap; textBox1.Text = "MapBenderWMSdemoMap"; }
        private void LatviaMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.LatviaMap; textBox1.Text = "LatviaMap"; }
        private void LithuaniaTOP50Map_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.LithuaniaTOP50Map; textBox1.Text = "LithuaniaTOP50Map"; }
        private void LithuaniaHybridOldMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.LithuaniaHybridOldMap; textBox1.Text = "LithuaniaHybridOldMap"; }
        private void LithuaniaHybridMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.LithuaniaHybridMap; textBox1.Text = "LithuaniaHybridMap"; }
        private void LithuaniaOrtoFotoOldMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.LithuaniaOrtoFotoOldMap; textBox1.Text = "LithuaniaOrtoFotoOldMap"; }
        private void LithuaniaOrtoFotoMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.LithuaniaOrtoFotoMap; textBox1.Text = "LithuaniaOrtoFotoMap"; }
        private void Lithuania3dMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.Lithuania3dMap; textBox1.Text = "Lithuania3dMap"; }
        private void CzechHistoryOldMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.CzechHistoryOldMap; textBox1.Text = "CzechHistoryOldMap"; }
        private void LithuaniaReliefMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.LithuaniaReliefMap; textBox1.Text = "LithuaniaReliefMap"; }
        private void CzechMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.CzechMap; textBox1.Text = "CzechMap"; }
        private void CzechHybridMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.CzechHybridMap; textBox1.Text = "CzechHybridMap"; }
        private void SwedenMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.SwedenMap; textBox1.Text = "SwedenMap"; }
        private void ArcGIS_DarbAE_Q2_2011_NAVTQ_Eng_V5_Map_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.ArcGIS_DarbAE_Q2_2011_NAVTQ_Eng_V5_Map; textBox1.Text = "ArcGIS_DarbAE_Q2_2011_NAVTQ_Eng_V5_Map"; }
        private void ArcGIS_World_Topo_Map_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.ArcGIS_World_Topo_Map; textBox1.Text = "ArcGIS_World_Topo_Map"; }
        private void ArcGIS_World_Terrain_Base_Map_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.ArcGIS_World_Terrain_Base_Map; textBox1.Text = "ArcGIS_World_Terrain_Base_Map"; }
        private void ArcGIS_World_Street_Map_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.ArcGIS_World_Street_Map; textBox1.Text = "ArcGIS_World_Street_Map"; }
        private void ArcGIS_World_Shaded_Relief_Map_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.ArcGIS_World_Shaded_Relief_Map; textBox1.Text = "ArcGIS_World_Shaded_Relief_Map"; }
        private void ArcGIS_World_Physical_Map_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.ArcGIS_World_Physical_Map; textBox1.Text = "ArcGIS_World_Physical_Map"; }
        private void ArcGIS_Topo_US_2D_Map_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.ArcGIS_Topo_US_2D_Map; textBox1.Text = "ArcGIS_Topo_US_2D_Map"; }
        private void ArcGIS_StreetMap_World_2D_Map_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.ArcGIS_StreetMap_World_2D_Map; textBox1.Text = "ArcGIS_StreetMap_World_2D_Map"; }
        private void ArcGIS_ShadedRelief_World_2D_Map_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.ArcGIS_ShadedRelief_World_2D_Map; textBox1.Text = "ArcGIS_ShadedRelief_World_2D_Map"; }
        private void ArcGIS_Imagery_World_2D_Map_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.ArcGIS_Imagery_World_2D_Map; textBox1.Text = "ArcGIS_Imagery_World_2D_Map"; }
        private void CzechGeographicMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.CzechGeographicMap; textBox1.Text = "CzechGeographicMap"; }
        private void CzechHistoryMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.CzechHistoryMap; textBox1.Text = "CzechHistoryMap"; }
        private void CzechTuristWinterMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.CzechTuristWinterMap; textBox1.Text = "CzechTuristWinterMap"; }
        private void CzechTuristMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.CzechTuristMap; textBox1.Text = "CzechTuristMap"; }
        private void CzechSatelliteMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.CzechSatelliteMap; textBox1.Text = "CzechSatelliteMap"; }
        private void YandexHybridMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.YandexHybridMap; textBox1.Text = "YandexHybridMap"; }
        private void LithuaniaMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.LithuaniaMap; textBox1.Text = "LithuaniaMap"; }
        private void GoogleMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.GoogleMap; textBox1.Text = "GoogleMap"; }
        private void YahooHybridMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.YahooHybridMap; textBox1.Text = "YahooHybridMap"; }
        private void YahooSatelliteMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.YahooSatelliteMap; textBox1.Text = "YahooSatelliteMap"; }
        private void YahooMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.YahooMap; textBox1.Text = "YahooMap"; }
        private void BingHybridMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.BingHybridMap; textBox1.Text = "BingHybridMap"; }
        private void BingSatelliteMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.BingSatelliteMap; textBox1.Text = "BingSatelliteMap"; }
        private void BingMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.BingMap; textBox1.Text = "BingMap"; }
        private void YandexSatelliteMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.YandexSatelliteMap; textBox1.Text = "YandexSatelliteMap"; }
        private void WikiMapiaMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.WikiMapiaMap; textBox1.Text = "WikiMapiaMap"; }
        private void OpenStreetMapQuestHybrid_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.OpenStreetMapQuestHybrid; textBox1.Text = "OpenStreetMapQuestHybrid"; }
        private void OpenStreetMapQuestSattelite_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.OpenStreetMapQuestSattelite; textBox1.Text = "OpenStreetMapQuestSattelite"; }
        private void OpenStreetMapQuest_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.OpenStreetMapQuest; textBox1.Text = "OpenStreetMapQuest"; }
        private void OpenCycleTransportMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.OpenCycleTransportMap; textBox1.Text = "OpenCycleTransportMap"; }
        private void OpenCycleLandscapeMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.OpenCycleLandscapeMap; textBox1.Text = "OpenCycleLandscapeMap"; }
        private void OpenCycleMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.OpenCycleMap; textBox1.Text = "OpenCycleMap"; }
        private void OpenSeaMapHybrid_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.OpenSeaMapHybrid; textBox1.Text = "OpenSeaMapHybrid"; }
        private void GoogleSatelliteMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.GoogleSatelliteMap; textBox1.Text = "GoogleSatelliteMap"; }
        private void GoogleHybridMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.GoogleHybridMap; textBox1.Text = "GoogleHybridMap"; }
        private void GoogleTerrainMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.GoogleTerrainMap; textBox1.Text = "GoogleTerrainMap"; }
        private void YandexMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.YandexMap; textBox1.Text = "YandexMap"; }
        private void OviTerrainMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.OviTerrainMap; textBox1.Text = "OviTerrainMap"; }
        private void OviHybridMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.OviHybridMap; textBox1.Text = "OviHybridMap"; }
        private void OviSatelliteMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.OviSatelliteMap; textBox1.Text = "OviSatelliteMap"; }
        private void OviMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.OviMap; textBox1.Text = "OviMap"; }
        private void NearHybridMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.NearHybridMap; textBox1.Text = "NearHybridMap"; }
        private void NearSatelliteMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.NearSatelliteMap; textBox1.Text = "NearSatelliteMap"; }
        private void NearMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.NearMap; textBox1.Text = "NearMap"; }
        private void GoogleKoreaHybridMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.GoogleKoreaHybridMap; textBox1.Text = "GoogleKoreaHybridMap"; }
        private void GoogleKoreaSatelliteMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.GoogleKoreaSatelliteMap; textBox1.Text = "GoogleKoreaSatelliteMap"; }
        private void GoogleKoreaMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.GoogleKoreaMap; textBox1.Text = "GoogleKoreaMap"; }
        private void GoogleChinaTerrainMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.GoogleChinaTerrainMap; textBox1.Text = "GoogleChinaTerrainMap"; }
        private void GoogleChinaHybridMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.GoogleChinaHybridMap; textBox1.Text = "GoogleChinaHybridMap"; }
        private void GoogleChinaSatelliteMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.GoogleChinaSatelliteMap; textBox1.Text = "GoogleChinaSatelliteMap"; }
        private void GoogleChinaMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.GoogleChinaMap; textBox1.Text = "GoogleChinaMap"; }
        private void OpenStreet4UMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.OpenStreet4UMap; textBox1.Text = "OpenStreet4UMap"; }
        private void OpenStreetMap_Click(object sender, EventArgs e) { gMapControl1.MapProvider = GMapProviders.OpenStreetMap; textBox1.Text = "OpenStreetMap"; }


    }
}
