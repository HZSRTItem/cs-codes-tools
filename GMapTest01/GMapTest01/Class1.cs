/*------------------------------------------------------------------------------
 * File    : Class1
 * Time    : 2022/8/1 15:03:02
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[Class1]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMapTest01
{
    class Class1
    {
        public static ToolStripItem[] GetToolStripItems()
        {
            List<ToolStripItem> toolStripItems = new List<ToolStripItem>();
            ToolStripItem toolStripItem = new ToolStripMenuItem();
            toolStripItem.Name = "t1";
            toolStripItem.Text = "t1";
            toolStripItem.Click += ToolStripItem_Click;
            toolStripItems.Add(new ToolStripMenuItem());
            return toolStripItems.ToArray();
        }

        private static void ToolStripItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
    /*
     
CzechTuristOldMap
CzechHybridOldMap
CzechSatelliteOldMap
CzechOldMap
SpainMap
CloudMadeMap
TurkeyMap
MapBenderWMSdemoMap
LatviaMap
LithuaniaTOP50Map
LithuaniaHybridOldMap
LithuaniaHybridMap
LithuaniaOrtoFotoOldMap
LithuaniaOrtoFotoMap
Lithuania3dMap
CzechHistoryOldMap
LithuaniaReliefMap
CzechMap
CzechHybridMap
SwedenMap
ArcGIS_DarbAE_Q2_2011_NAVTQ_Eng_V5_Map
ArcGIS_World_Topo_Map
ArcGIS_World_Terrain_Base_Map
ArcGIS_World_Street_Map
ArcGIS_World_Shaded_Relief_Map
ArcGIS_World_Physical_Map
ArcGIS_Topo_US_2D_Map
ArcGIS_StreetMap_World_2D_Map
ArcGIS_ShadedRelief_World_2D_Map
ArcGIS_Imagery_World_2D_Map
CzechGeographicMap
CzechHistoryMap
CzechTuristWinterMap
CzechTuristMap
CzechSatelliteMap
YandexHybridMap
LithuaniaMap
GoogleMap
YahooHybridMap
YahooSatelliteMap
YahooMap
BingHybridMap
BingSatelliteMap
BingMap
YandexSatelliteMap
WikiMapiaMap
OpenStreetMapQuestHybrid
OpenStreetMapQuestSattelite
OpenStreetMapQuest
OpenCycleTransportMap
OpenCycleLandscapeMap
OpenCycleMap
OpenSeaMapHybrid
GoogleSatelliteMap
GoogleHybridMap
GoogleTerrainMap
YandexMap
OviTerrainMap
OviHybridMap
OviSatelliteMap
OviMap
NearHybridMap
NearSatelliteMap
NearMap
GoogleKoreaHybridMap
GoogleKoreaSatelliteMap
GoogleKoreaMap
GoogleChinaTerrainMap
GoogleChinaHybridMap
GoogleChinaSatelliteMap
GoogleChinaMap
OpenStreet4UMap
OpenStreetMap

     
     
     */
}
