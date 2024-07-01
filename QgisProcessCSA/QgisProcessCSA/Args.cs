/*------------------------------------------------------------------------------
 * File    : Args
 * Time    : 2022/7/29 8:05:31
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[Args]
 * 
 * 
"srt_qrastersampling [input_vector] [input_raster] [opt: -prefix] [opt: -o out shape file]          \n" +
"    input_vector: [vector:point] Point vector layer to use for sampling                            \n" +
"    input_raster: [raster] Raster layer to sample at the given point locations.                    \n" +
"    opt:-prefix: [string default:SAMPLE_] Prefix for the names of the added columns.               \n" +
"    opt:-o: [vector:point default:_rspl.shp] Specify the output layer containing the sampled values"
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QgisProcessCSA
{
    class Args
    {
        /// <summary>
        /// 程序名
        /// </summary>
        public string Name = null;
        /// <summary>
        /// 帮助信息
        /// </summary>
        public string HelpInfo = null;

        public Args() { }

        /// <summary>
        /// 获得参数的第一个参数数据
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns>第一个参数数据</returns>
        public string this[string name]
        {
            get { return Get(name); }
            set { AddArgData(name, value); }
        }

        /// <summary>
        /// 参数列表
        /// </summary>
        private Dictionary<string, Arg> ArgsD = new Dictionary<string, Arg>();

        /// <summary>
        /// 添加一个参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="n_max_arg">最大参数量</param>
        /// <param name="help_info">帮助信息</param>
        /// <param name="default_arg">默认参数</param>
        /// <returns>是否添加成功</returns>
        public bool AddArg(string name, int n_max_arg, string qgis_arg, string default_arg, string help_info)
        {
            ArgsD.Add(name, new Arg(n_max_arg, help_info, default_arg));
            ArgsD[name].QgisArg = qgis_arg;
            return true;
        }

        /// <summary>
        /// 添加一个参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="n_max_arg">最大参数量</param>
        /// <param name="help_info">帮助信息</param>
        /// <returns>是否添加成功</returns>
        public bool AddArg(string name, int n_max_arg, string qgis_arg, string help_info)
        {
            ArgsD.Add(name, new Arg(n_max_arg, help_info));
            ArgsD[name].QgisArg = qgis_arg;
            return true;
        }

        /// <summary>
        /// 添加参数的数据
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="info">参数数据</param>
        /// <returns></returns>
        public bool AddArgData(string name, string info)
        {
            ArgsD[name].Add(info);
            return true;
        }

        /// <summary>
        /// 获得帮助信息
        /// </summary>
        /// <returns>帮助信息</returns>
        public string GetHelpInfo()
        {
            string info = Name;
            foreach (KeyValuePair<string, Arg> item in ArgsD)
            {
                if (item.Value.AType == "")
                {
                    info += " [" + item.Key + "]";
                }
            }

            foreach (KeyValuePair<string, Arg> item in ArgsD)
            {
                if (item.Value.AType == "opt:")
                {
                    info += " [opt: " + item.Key + "]";
                }
            }

            info += "\nArgs:\n";
            foreach (KeyValuePair<string, Arg> item in ArgsD)
            {
                if (item.Value.AType == "")
                {
                    info += "    " + item.Value.GetHelp(item.Key) + "\n";
                }
            }
            foreach (KeyValuePair<string, Arg> item in ArgsD)
            {
                if (item.Value.AType == "opt:")
                {
                    info += "    " + item.Value.GetHelp(item.Key) + "\n";
                }
            }

            info += "\n";
            if (HelpInfo != null)
            {
                info += "Help:\n" + HelpInfo;
            }

            return info;
        }

        private string Get(string name)
        {
            if (ArgsD[name].ArgD.Length > 0)
            {
                return ArgsD[name].ArgD[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得Qgis的参数
        /// </summary>
        /// <returns></returns>
        public string GetQgisArgs()
        {
            string info = "";
            foreach (Arg item in ArgsD.Values)
            {
                info += item.GetQgisArgs();
            }
            return info;
        }
    }

    /// <summary>
    /// 一个参数
    /// </summary>
    class Arg
    {
        /// <summary>
        /// 参数类型
        /// </summary>
        public string AType = "";
        /// <summary>
        /// 一个参数中的数据的最大数量
        /// </summary>
        public int Number = 0;
        /// <summary>
        /// 参数的帮助信息
        /// </summary>
        public string HelpInfo = "";
        /// <summary>
        /// 参数数据
        /// </summary>
        public string[] ArgD = null;
        /// <summary>
        /// QGIS 参数输入
        /// </summary>
        public string QgisArg = "";

        public Arg()
        { }

        /// <summary>
        /// 构造一个参数信息
        /// </summary>
        /// <param name="n_max_arg">最大参数量</param>
        /// <param name="help_info">帮助信息</param>
        /// <param name="default_arg">默认参数</param>
        public Arg(int n_max_arg, string help_info, string default_arg)
        {
            HelpInfo = help_info;
            ArgD = new string[n_max_arg];
            Add(default_arg);
            AType = "opt:";
        }


        /// <summary>
        /// 构造一个参数信息
        /// </summary>
        /// <param name="n_max_arg">最大参数量</param>
        /// <param name="help_info">帮助信息</param>
        public Arg(int n_max_arg, string help_info)
        {
            HelpInfo = help_info;
            ArgD = new string[n_max_arg];
        }

        /// <summary>
        /// 添加一个参数数据
        /// </summary>
        /// <param name="arg">参数数据</param>
        /// <returns>是否大于设定的参数量</returns>
        public bool Add(string arg)
        {
            if (Number < ArgD.Length)
            {
                ArgD[Number++] = arg;
                return true;
            }
            else
            {
                ArgD[ArgD.Length - 1] = arg;
                return false;
            }
        }

        /// <summary>
        /// 获得参数帮助
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns>参数帮助</returns>
        public string GetHelp(string name)
        {
            string info = AType + name + ": " + HelpInfo.Replace('\n', ' ');
            return info;
        }

        /// <summary>
        /// 获得Qgis的参数
        /// </summary>
        /// <returns></returns>
        public string GetQgisArgs()
        {
            string info = "";
            for (int i = 0; i < Number; i++)
            {
                info += "\"" + QgisArg + "=" + ArgD[i] + "\" ";
            }
            return info;
        }
    }
}
