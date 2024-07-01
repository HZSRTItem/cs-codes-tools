/*------------------------------------------------------------------------------
 * File    : Samples_V2
 * Time    : 2022/8/3 10:28:54
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[Samples_V2]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gdal = OSGeo.GDAL;
using ogr = OSGeo.OGR;
using  OSGeo.OGR;
using osr = OSGeo.OSR;


namespace SampleIdentifWFA01
{
    class Samples_V2
    {
        #region 公共静态
        /// <summary>
        /// 使用 csv file 构建
        /// </summary>
        /// <param name="csv_file">csv file</param>
        /// <returns>建立的样本</returns>
        public static Samples_V2 BuildByCsv(string csv_file)
        {
            return OGRBuild(csv_file, "CSV");
        }

        /// <summary>
        /// 使用 shape file 构建
        /// </summary>
        /// <param name="shape_file">shape file</param>
        /// <returns>建立的样本</returns>
        public static Samples_V2 BuildByShapeFile(string shape_file)
        {
            return OGRBuild(shape_file, "ESRI Shapefile");
        }
        #endregion

        #region 私有静态
        /// <summary>
        /// 使用OGR文件构建
        /// </summary>
        /// <param name="in_vector_file">输入矢量文件</param>
        /// <param name="drive_name">矢量文件类型</param>
        /// <returns></returns>
        private static Samples_V2 OGRBuild(string in_vector_file, string drive_name)
        {
            Samples_V2 samples_V2 = new Samples_V2();
            ogr.Driver driver = ogr.Ogr.GetDriverByName(drive_name);
            ogr.DataSource ds = driver.Open(in_vector_file, 1);
            if (ds == null)
            {
                return null;
            }
            ogr.Layer o_layer = ds.GetLayerByIndex(1);
            if (o_layer == null)
            {
                ds.Dispose();
                return null;
            }
            // 获得属性信息
            samples_V2.SRS = o_layer.GetSpatialRef();
            ogr.FeatureDefn t_defn = o_layer.GetLayerDefn();
            int i_field_count = t_defn.GetFieldCount();
            for (int i = 0; i < i_field_count; i++)
            {
                ogr.FieldDefn o_field = t_defn.GetFieldDefn(samples_V2.NAttrsNames);
                samples_V2.AddAttrsName(o_field.GetNameRef());
            }
            long n_feat = o_layer.GetFeatureCount(0);
            for (long i = 0; i < n_feat; i++)
            {
                AddASample(samples_V2, o_layer, i, drive_name);
            }
            ds.Dispose();
            return samples_V2;
        }

        /// <summary>
        /// 添加一个样本
        /// </summary>
        /// <param name="samples_V2">样本</param>
        /// <param name="o_layer">矢量图层</param>
        /// <param name="i">第几个样本</param>
        /// <param name="drive_name">驱动的名称</param>
        private static void AddASample(Samples_V2 samples_V2, Layer o_layer, long i, string drive_name)
        {
            if (drive_name == "CSV")
            {
                // 获得数据信息
                ogr.Feature oFeature = o_layer.GetFeature(i);
                OneSample_V2 oneSample_V2 = new OneSample_V2();
                for (int j = 0; j < samples_V2.NAttrsNames; j++)
                {
                    string info = oFeature.GetFieldAsString(samples_V2.AttrsNames[j]);
                    if (!oneSample_V2.SetAttrByName(samples_V2.AttrsNames[j], info))
                    {
                        oneSample_V2.AddAttr(info);
                    }
                }
                samples_V2.Spls.Add(oneSample_V2);
            }
            else if (drive_name == "ESRI Shapefile")
            {
                // 获得数据信息
                ogr.Feature oFeature = o_layer.GetFeature(i);
                ogr.Geometry geometry = oFeature.GetGeometryRef();
                OneSample_V2 oneSample_V2 = new OneSample_V2();
                for (int j = 0; j < samples_V2.NAttrsNames; j++)
                {
                    string info = oFeature.GetFieldAsString(samples_V2.AttrsNames[j]);
                    if (!oneSample_V2.SetAttrByName(samples_V2.AttrsNames[j], info))
                    {
                        oneSample_V2.AddAttr(info);
                    }
                }
                samples_V2.Spls.Add(oneSample_V2);
                oneSample_V2.X = geometry.GetX(0);
                oneSample_V2.Y = geometry.GetY(0);
            }
            else
            {
                throw new Exception("not support this stype: " + drive_name);
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 字段的数量
        /// </summary>
        public int NAttrs
        {
            get { return NAttrsNames; }
        }

        /// <summary>
        /// 样本的数量
        /// </summary>
        public int NSample
        {
            get { return Spls.Count; }
        }
        #endregion

        #region 私有属性
        /// <summary>
        /// 属性名
        /// </summary>
        private string[] AttrsNames = new string[15];
        /// <summary>
        /// 属性名数量
        /// </summary>
        private int NAttrsNames = 0;
        /// <summary>
        /// 所有样本
        /// </summary>
        private List<OneSample_V2> Spls = new List<OneSample_V2>();
        /// <summary>
        /// 投影信息
        /// </summary>
        private osr.SpatialReference SRS = null;
        /// <summary>
        /// 类别
        /// </summary>
        private Category Categorys = new Category();
        #endregion

        #region 公共方法
        /// <summary>
        /// 获得一个样本对象
        /// </summary>
        /// <param name="i">第几个</param>
        /// <returns></returns>
        public OneSample_V2 this[int i]
        {
            get { return Spls[i]; }
        }

        /// <summary>
        /// 找到第i个样本的属性名对应的属性值
        /// </summary>
        /// <param name="i">第i个样本</param>
        /// <param name="field_name">字段名</param>
        /// <returns>字段属性值</returns>
        public string this[int i, string field_name]
        {
            get { return Get(i, field_name); }
        }

        /// <summary>
        /// 找到第i个样本的第i_field个属性对应的属性值
        /// </summary>
        /// <param name="i">第i个样本</param>
        /// <param name="i_field">第i_field个属性</param>
        /// <returns>属性值</returns>
        public string this[int i, int i_field]
        {
            get { return Spls[i][i_field]; }
        }

        /// <summary>
        /// 获得属性的名
        /// </summary>
        /// <param name="i">第i个</param>
        /// <returns>属性的名</returns>
        public string GetFieldNameByIndex(int i)
        {
            return AttrsNames[i];
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 添加一个属性名
        /// </summary>
        /// <param name="name">属性名</param>
        private void AddAttrsName(string name)
        {
            if (NAttrsNames < AttrsNames.Length)
            {
                AttrsNames[NAttrsNames] = name;
            }
            else
            {
                string[] ss = AttrsNames;
                AttrsNames = new string[ss.Length + 5];
                for (int i = 0; i < ss.Length; i++)
                {
                    AttrsNames[i] = ss[i];
                }
                AttrsNames[ss.Length] = name;
            }
            NAttrsNames++;
        }

        private string Get(int i, string field_name)
        {
            for (int j = 0; j < NAttrsNames; j++)
            {
                if (field_name == AttrsNames[j])
                {
                    return Spls[i][j];
                }
            }
            throw new Exception("Not find field: " + field_name);
        }
        #endregion
    }

    /// <summary>
    /// 一个样本
    /// </summary>
    class OneSample_V2
    {
        /// <summary>
        /// 新建一个样本
        /// </summary>
        public OneSample_V2() { }

        /// <summary>
        /// X 坐标
        /// </summary>
        public double X = 0;
        /// <summary>
        /// Y 坐标
        /// </summary>
        public double Y = 0;
        /// <summary>
        /// 解译图
        /// </summary>
        public string ImFile = null;
        /// <summary>
        /// 类别
        /// </summary>
        public int CategoryIndex = 0;
        /// <summary>
        /// SRT
        /// </summary>
        public int SRT = 0;

        /// <summary>
        /// 当前记录的属性
        /// </summary>
        private int NAttrs = 0;
        /// <summary>
        /// 属性信息
        /// </summary>
        private string[] Attrs = new string[16];

        /// <summary>
        /// 属性
        /// </summary>
        /// <param name="i">第几个属性</param>
        /// <returns></returns>
        public string this[int i]
        {
            set { SetAttr(i, value); }
            get { return GetAttr(i); }
        }

        /// <summary>
        /// 添加一个属性
        /// </summary>
        /// <param name="info"></param>
        public void AddAttr(string info)
        {
            if (NAttrs < Attrs.Length)
            {
                Attrs[NAttrs] = info;
            }
            else
            {
                string[] ss = Attrs;
                Attrs = new string[ss.Length + 5];
                for (int i = 0; i < ss.Length; i++)
                {
                    Attrs[i] = ss[i];
                }
                Attrs[ss.Length] = info;
            }
            NAttrs++;
        }

        /// <summary>
        /// 获得属性
        /// </summary>
        /// <param name="i">第几个</param>
        /// <returns>属性</returns>
        private string GetAttr(int i)
        {
            if (i < 0)
            {
                i += NAttrs;
            }
            return Attrs[i];
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="i">第几个</param>
        /// <param name="value">属性值</param>
        private void SetAttr(int i, string value)
        {
            if (i < 0)
            {
                i += NAttrs;
            }
            Attrs[i] = value;
        }

        /// <summary>
        /// 添加多个属性
        /// </summary>
        /// <param name="infos"></param>
        public void AddAttr(string[] infos)
        {
            for (int i = 0; i < infos.Length; i++)
            {
                AddAttr(infos[i]);
            }
        }

        /// <summary>
        /// 使用属性名设置信息
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="info">属性信息</param>
        /// <returns>是否可以设置</returns>
        public bool SetAttrByName(string name, string info)
        {
            name = name.ToLower();
            switch (name)
            {
                case "x":
                    X = double.Parse(info);
                    break;

                case "y":
                    Y = double.Parse(info);
                    break;

                case "imfile":
                    ImFile = info;
                    break;

                case "category":
                    CategoryIndex = int.Parse(info);
                    break;

                case "srt":
                    SRT = int.Parse(info);
                    break;

                default:
                    return false;
            }
            return true;
        }
    }

    class Category
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        private string[] Name = new string[16];
        /// <summary>
        /// 类别的数量
        /// </summary>
        private int[] Count = new int[16];
        /// <summary>
        /// 当前类别的数量
        /// </summary>
        private int Number = 0;

        /// <summary>
        /// 获得类别的数量
        /// </summary>
        /// <returns>类别数量</returns>
        public int GetNumber()
        {
            return Number;
        }

        /// <summary>
        /// 添加一个类别名
        /// </summary>
        /// <param name="name">类别名</param>
        /// <returns>类别号</returns>
        public int Add(string name)
        {
            for (int i = 0; i < Number; i++)
            {
                if (name == Name[i])
                {
                    int n = 1;
                    for (int j = 0; j < Number; j++)
                    {
                        string name0 = name + "_" + n.ToString();
                        if (name0 == Name[i])
                        {
                            n++;
                            j = 0;
                        }
                    }
                    name = name + "_" + n.ToString();
                    break;
                }
            }

            if (Number < Name.Length)
            {
                Name[Number] = name;
            }
            else
            {
                string[] ss = Name;
                Name = new string[Number + 5];
                for (int i = 0; i < Number; i++)
                {
                    Name[i] = ss[i];
                }
                Name[Number] = name;
                int[] ii = Count;
                Count = new int[Number + 5];
                for (int i = 0; i < Number; i++)
                {
                    Count[i] = ii[i];
                }
                Count[Number] = 0;
            }

            Number++;
            return Number;
        }

        /// <summary>
        /// 类别编号获得类别名
        /// </summary>
        /// <param name="i">类别编号</param>
        /// <returns>类别名</returns>
        public string this[int i]
        {
            set { Name[i - 1] = value; }
            get { return Name[i]; }
        }

        /// <summary>
        /// 类别名获得索引
        /// </summary>
        /// <param name="name">类别名</param>
        /// <returns>索引</returns>
        public int this[string name]
        {
            get { return Array.IndexOf(Name, name); }
        }

        /// <summary>
        /// 使用类别名添加的数量
        /// </summary>
        /// <param name="name">类别名</param>
        /// <param name="n">数量</param>
        /// <returns>是否存在改字段</returns>
        public bool AddCount(string name, int n)
        {
            for (int i = 0; i < Number; i++)
            {
                if (name == Name[i])
                {
                    Count[i] += n;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 使用类别编号添加的数量
        /// </summary>
        /// <param name="i">类别编号</param>
        /// <param name="n">数量</param>
        /// <returns>是否存在该类别编号</returns>
        public bool AddCount(int i, int n)
        {
            if (i < 0)
            {
                i += Number;
            }
            else
            {
                i--;
            }
            if (i > 0 | i < Number)
            {
                Count[i] += n;
            }
            else
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 使用类别名添加的数量
        /// </summary>
        /// <param name="name">类别名</param>
        /// <returns>是否存在改字段</returns>
        public bool AddCount(string name)
        {
            return AddCount(name, 1);
        }

        /// <summary>
        /// 使用类别编号添加的数量
        /// </summary>
        /// <param name="i">类别编号</param>
        /// <returns>是否存在该类别编号</returns>
        public bool AddCount(int i)
        {
            return AddCount(i, 1);
        }
    }

}
