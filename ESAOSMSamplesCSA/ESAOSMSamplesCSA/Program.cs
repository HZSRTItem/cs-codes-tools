using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SrtGeo;
using System.IO;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ESAOSMSamplesCSA
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    for (int i = 0; i < args.Length; i++)
        //    {
        //        Console.WriteLine(string.Format("{0, 3}: {1}", i + 1, args[i]));
        //    }
        //}

        static void Main(string[] args)
        {
            DateTime dateTime = DateTime.Now;
            DebugInfo.IsDebug = true;
            //args = new string[2] { @"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\6\mpoly.shp", "--debug" };
            //string[] roads = new string[2] { @"D:\GraduationProject\Framework\1Sample\1GetOriginalSpl\Data\gba_spl01\reprj\reprj_cc_gis_osm_railways_free_1.shp", "--debug" };
            //string[] railway = new string[2] { @"D:\GraduationProject\Framework\1Sample\1GetOriginalSpl\Data\gba_spl01\reprj\reprj_cc_gis_osm_roads_free_1.shp", "--debug" };
            //OutExe.ShapeToPoint(args);
            //int n = ShapeTools.Reprojection(
            //    @"D:\SpecialProjects\ogr2ogrtest\d0426\t_merge.shp"
            //    , @"D:\SpecialProjects\ogr2ogrtest\d0426\t_merge_re.shp"
            //    , "+proj=tmerc +lat_0=0 +lon_0=111 +k=1 +x_0=500000 +y_0=0 +ellps=GRS80 +units=m +no_defs +type=crs"
            //    );
            // ogr2ogr -overwrite -clipsrc mian01.shp line_t0.shp New_Shapefile.shp
            //int n = ShapeTools.WarpByShapeFile(
            //    @"D:\SpecialProjects\ogr2ogrtest\d0424\New_Shapefile.shp"
            //    , @"D:\SpecialProjects\ogr2ogrtest\d0426\line_t0.shp"
            //    , @"D:\SpecialProjects\ogr2ogrtest\d0424\mian01.shp"
            //    );
            //ShapeGeometry shapeGeometry = ShapeGeometry.FormatWKT("MULTILINESTRING ((113.761799528585 23.3330158256619,113.775662155144 23.1280469901164),(113.775662155144 23.1280469901164,113.80380218808 22.7119765031402,113.862026476278 22.7273243280629),(113.862026476278 22.7273243280629,114.070693758174 22.7823286743366),(114.108582330027 22.6190325732829,113.805447832887 22.4433173223983),(113.689448627853 22.4593157830791,113.862026476278 22.7273243280629),(113.862026476278 22.7273243280629,114.024803296348 22.9801121516562,114.024803296348 22.9801121516562),(113.913716100449 23.2035553686519,113.775662155144 23.1280469901164),(113.775662155144 23.1280469901164,113.055206932681 22.7339951900368),(112.932217298942 22.6667262119589,112.812535268993 22.6012663223392))");
            //ShapeGeometry shapeGeometry1 = ShapeGeometry.FormatWKT(
            //    //"               LINESTRING (113.985830955026 23.5578748907129,113.514697675871 22.658438630509,112.80799775714 21.9267543712162,112.49390890437 22.3550573522656)"
            //    );
            //ShapeGeometry shapeGeometry2 = ShapeGeometry.FormatWKT(
            //    "POINT (115.035895816332                          21.7971359049326)"
            //    );

            //ShapeInfo shapeInfo = new ShapeInfo(@"D:\GraduationProject\Framework\1Sample\1GetOriginalSpl\Data\gba_spl01\reprj\reprj_cc_gis_osm_railways_free_1.shp");
            //ShapeInfo shapeInfo7 = new ShapeInfo(@"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\6\point.shp");
            //ShapeInfo shapeInfo5 = new ShapeInfo(@"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\6\mpoint.shp");
            //ShapeInfo shapeInfo2 = new ShapeInfo(@"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\6\line.shp");
            //ShapeInfo shapeInfo4 = new ShapeInfo(@"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\6\mline.shp");
            //ShapeInfo shapeInfo8 = new ShapeInfo(@"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\6\poly.shp");
            //ShapeInfo shapeInfo6 = new ShapeInfo(@"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\6\mpoly.shp");
            //ShapeInfo shapeInfo3 = new ShapeInfo(@"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\6\mian01.shp");
            //shapeInfo.SaveToShapeFile(@"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\6\mpoly2.shp");
            //shapeInfo7.SaveToShapeFile(@"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\6\point1.shp");
            //int n = ShapeExeTools.WarpByShapeFile(
            //    @"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\6\poly.shp"
            //    , @"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\6\mpoly.shp"
            //    , @"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\6\mian01.shp"
            //    );
            // 0.0008983153
            //ShapeInfo shapeInfo1 = shapeInfo.ToPoint();
            //shapeInfo1.SaveToShapeFile(@"mpoly_p1.shp");

            //ShapeInfo shapeInfo = new ShapeInfo(@"D:\GraduationProject\Ablation\Temp\osm_roadbuild\build_tp1.shp");
            //ShapeInfo shapeInfo1 = shapeInfo.UniformSpace(0.0008983153);
            //shapeInfo1.SaveToShapeFile(@"D:\GraduationProject\Ablation\Temp\osm_roadbuild\build_tp1_ufs1.shp");


            //ShapeInfo shapeInfo = new ShapeInfo(@"D:\GraduationProject\Ablation\Temp\osm_roadbuild\railways_tp1.shp");
            //ShapeInfo shapeInfo1 = shapeInfo.UniformSpace(0.0008983153);
            //shapeInfo1.SaveToShapeFile(@"D:\GraduationProject\Ablation\Temp\osm_roadbuild\railways_tp1_ufs1.shp");



            ShapeInfo shapeInfo = new ShapeInfo(@"D:\GraduationProject\Ablation\Temp\osm_roadbuild\roads_tp2.shp");
            ShapeInfo shapeInfo1 = shapeInfo.UniformSpace(0.0008983153);
            shapeInfo1.SaveToShapeFile(@"D:\GraduationProject\Ablation\Temp\osm_roadbuild\roads_tp2_ufs1.shp");



            //ShapeInfo shapeInfo = new ShapeInfo(@"D:\GraduationProject\Ablation\Temp\osm_roadbuild\build_tp1.shp");
            //ShapeInfo shapeInfo1 = shapeInfo.UniformSpace(0.0008983153);
            //shapeInfo1.SaveToShapeFile(@"D:\GraduationProject\Ablation\Temp\osm_roadbuild\build_tp1_ufs1.shp");



            //ShapeInfo shapeInfo2 = shapeInfo1.UniformSpace(0.0008983153);
            //shapeInfo2.SaveToShapeFile(@"mpoly_p2.shp");
            //string jsonText = File.ReadAllText(@"D:\CodeProjects\Samples\ESAOSMSamplesCSA\temp\t01.json");
            //JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
            //JToken bands = jo["bands"];
            //int i = bands.Count();
            //JToken jToken_0 = bands[0];
            //string info = jToken_0.ToString();
            //string jToken_band0 = jToken_0["type"].ToString();

            //t1();
            //GC.Collect();
            Console.WriteLine(DateTime.Now - dateTime);
            Console.ReadLine();
        }

        static void t1()
        {
            ShapeInfo shapeInfo = new ShapeInfo(@"D:\GraduationProject\Framework\1Sample\1GetOriginalSpl\Data\gba_spl01\reprj\reprj_cc_gis_osm_railways_free_1.shp");
            ShapeInfo shapeInfo1 = shapeInfo.ToPoint();
            shapeInfo1.SaveToShapeFile(@"mpoly_p1.shp");
            shapeInfo1 = null; 
            //ShapeInfo shapeInfo2 = shapeInfo1.UniformSpace(0.0008983153);
            //shapeInfo2.SaveToShapeFile(@"mpoly_p2.shp");

        }
    }
}
