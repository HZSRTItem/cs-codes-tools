/*------------------------------------------------------------------------------
 * File    : RasterSample
 * Time    : 2022/4/28 19:53:48
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[RasterSample]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtGeo
{
    class RasterSample
    {
        public RasterSample(string raster_file, int n_sample)
        {
            //// 检查栅格的数据类型
            //// gdallocationinfo 采样
            //string line = UseExes.gdalinfo + " -json" + raster_file;
            //bool check_raster = false;
            //if (CmdRun.RunLine(line) == 0)
            //{
            //    string info_json = CmdRun.OutInfo;
            //    JObject jo = (JObject)JsonConvert.DeserializeObject(info_json);
            //    JToken jToken_bands = jo["bands"];
            //    if (jToken_bands == null)
            //    {
            //        throw new Exception("Raster Format Error: not find info bands");
            //    }
            //    else
            //    {
            //        if (jToken_bands.Count() == 1)
            //        {
            //            JToken jToken_band0 = jToken_bands[0]["type"];
            //            if (jToken_band0 == null)
            //            {
            //                throw new Exception("Raster Format Error: not find info type");
            //            }
            //            else
            //            {
            //                string isint = jToken_band0.ToString();
            //                // "Byte" 
            //                if (isint == "Byte")
            //                {
            //                    check_raster = true;
            //                }
            //                else
            //                {
            //                    throw new Exception("Error: raster data type have to byte");
            //                }
            //            }
            //        }
            //        else
            //        {
            //            throw new Exception("Error: number of raster have to one 1");
            //        }
            //    }
            //}
            //else
            //{
            //    throw new Exception("CMD RUN ERROR: " + CmdRun.ErrorInfo);
            //}

        }
    }
}
