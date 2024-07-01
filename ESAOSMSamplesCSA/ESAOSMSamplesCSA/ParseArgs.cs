/*------------------------------------------------------------------------------
 * File    : ParseArgs
 * Time    : 2022/4/30 18:38:56
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[ParseArgs]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace ESAOSMSamplesCSA
{
    
    class ParseArgs
    {
        public enum AType
        {
            ConstNumber,
            NonConstNumber,
            YesNo
        }
        public class OneParam
        {
            
            /// <summary>
            /// 由用户定义的数量
            ///   -1: 不定参数
            ///    0: 无参数 Yes/No
            ///   >0: 确定的参数的数量
            /// </summary>
            public int Number = 0;
            /// <summary>
            /// 参数的内容
            /// </summary>
            private List<string> Infos = new List<string>();
            /// <summary>
            /// 参数的帮助信息
            /// </summary>
            public string HelpInfo = "";
            /// <summary>
            /// 是否已经获得参数
            /// </summary>
            public bool IsStart = false;

            /// <summary>
            /// 构造一个参数对象
            /// 由用户定义的数量
            ///   -1: 不定参数
            ///    0: 无参数 Yes/No
            ///   >0: 确定的参数的数量
            /// </summary>
            /// <param name="n"></param>
            public OneParam(int n)
            {
                Number = n;
            }

            /// <summary>
            /// 构造一个参数对象
            /// </summary>
            /// <param name="n">参数数量</param>
            /// <param name="help_info">帮助信息</param>
            public OneParam(int n, string help_info)
            {
                HelpInfo = help_info;
                Number = n;
            }

            /// <summary>
            /// 添加一个参数
            /// </summary>
            /// <param name="info">参数</param>
            /// <returns></returns>
            public bool Add(string info)
            {
                IsStart = true;
                if (Number == -1)
                {
                    Infos.Add(info);
                    return true;
                }
                else
                {
                    if (Number == 0)
                    {
                        Infos.Add("");
                        return true;
                    }
                    else if (Number > Infos.Count)
                    {
                        Infos.Add(info);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            /// <summary>
            /// 获得当前输入的参数的数量
            /// </summary>
            /// <returns></returns>
            public int GetInputNumber()
            {
                return Infos.Count;
            }

            private int N_Now = 0;
            /// <summary>
            /// 可以迭代获得每一个info
            /// </summary>
            /// <returns></returns>
            public string GetInfoIterate()
            {
                if (N_Now == Infos.Count)
                {
                    N_Now = 0;
                }
                return Infos[N_Now++];
            }


        }

        /// <summary>
        /// 构造一个空的解析对象
        /// </summary>
        /// <param name="args"></param>
        public ParseArgs(string name_info, string desc_info, string license_info) 
        {
            NameInfo = name_info;
            DescInfo = desc_info;
            LicenseInfo = license_info;
        }

        /// <summary>
        /// 名
        /// </summary>
        private string NameInfo = "";
        /// <summary>
        /// 描述信息
        /// </summary>
        private string DescInfo = "";
        /// <summary>
        /// 归属信息
        /// </summary>
        private string LicenseInfo = "";

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="args">参数</param>
        public void Fit(string[] args)
        {
            string[] param_names = RequiredParams.Keys.ToArray();
            int argc = args.Length;
            int n_req = 0;
            for (int i = 0; i < argc; i++)
            {
                if (OptionalParams.ContainsKey(args[i]))
                {
                    if (OptionalParams[args[i]].Number == 0)
                    {
                        OptionalParams[args[i]].Add(args[i]);
                    }
                    else
                    {
                        if (i < argc - 1)
                        {
                            OptionalParams[args[i]].Add(args[i+1]);
                        }
                        else
                        {
                            throw new Exception("Can not find " + args[i] + " info");
                        }
                    }
                }
                else
                {
                    if (n_req == param_names.Length)
                    {
                        OtherParam.Add(args[i]);
                    }
                    else
                    {
                        RequiredParams[param_names[n_req++]].Add(args[i]);
                    }
                }
            }
        }

        /// <summary>
        /// 可选参数列表
        /// </summary>
        public Dictionary<string, OneParam> OptionalParams = new Dictionary<string, OneParam>();
        /// <summary>
        /// 必选参数列表
        /// </summary>
        public Dictionary<string, OneParam> RequiredParams = new Dictionary<string, OneParam>();
        /// <summary>
        /// 其他参数
        /// </summary>
        public List<string> OtherParam = new List<string>();

        /// <summary>
        /// 添加一个可选参数
        /// </summary>
        /// <param name="info">参数信息</param>
        /// <param name="n">参数的数量</param>
        public void AddOptional(string info, int n)
        {
            OptionalParams.Add(info, new OneParam(n));
        }

        /// <summary>
        /// 添加一个可选参数
        /// </summary>
        /// <param name="info">参数信息</param>
        /// <param name="n">参数的数量</param>
        /// <param name="help_info">参数的数量</param>
        public void AddOptional(string info, int n, string help_info)
        {
            OptionalParams.Add(info, new OneParam(n, help_info));
        }

        /// <summary>
        /// 添加一个可选参数
        /// </summary>
        /// <param name="info">参数信息</param>
        /// <param name="help_info">参数的数量</param>
        public void AddOptional(string info, string help_info)
        {
            OptionalParams.Add(info, new OneParam(1, help_info));
        }

        /// <summary>
        /// 添加一个必选参数
        /// </summary>
        /// <param name="info"></param>
        public void AddRequired(string info)
        {
            RequiredParams.Add(info, new OneParam(1));
        }

        /// <summary>
        /// 添加一个必选参数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="help_info"></param>
        public void AddRequired(string info, string help_info)
        {
            RequiredParams.Add(info, new OneParam(1, help_info));
        }

        /// <summary>
        /// 获得用户输入信息
        /// </summary>
        /// <returns></returns>
        public string UsageInfo()
        {
            string info = "> " + NameInfo ;
            string[] param_name = OptionalParams.Keys.ToArray();
            for (int i = 0; i < param_name.Length; i++)
            {
                info += " [" + param_name[i] + "]";
            }
            param_name = RequiredParams.Keys.ToArray();
            for (int i = 0; i < param_name.Length; i++)
            {
                info += " " + param_name[i];
            }
            info += "\n";
            info += "@ description: \n    " + DescInfo.Replace("\n", "\n    ");
            info += "\n@ optional params: \n";
            param_name = OptionalParams.Keys.ToArray();
            for (int i = 0; i < param_name.Length; i++)
            {
                info += string.Format("    {0,-16} : {1}\n", param_name[i], OptionalParams[param_name[i]].HelpInfo.Replace("\n", " "));
            }
            info += "@ required params: \n";
            param_name = RequiredParams.Keys.ToArray();
            for (int i = 0; i < param_name.Length; i++)
            {
                info += string.Format("    {0,-16} : {1}\n", param_name[i], RequiredParams[param_name[i]].HelpInfo.Replace("\n", " "));
            }
            info += LicenseInfo;
            return info;
        }

        public bool IsFileCorrect(string key, string houzui, ref string out_file)
        {
            // 找到这个文件在参数中的位置
            if (RequiredParams.ContainsKey(key))
            {
                out_file = RequiredParams[key].IsStart ? RequiredParams[key].GetInfoIterate() : "";
            }
            else if (OptionalParams.ContainsKey(key))
            {
                out_file = OptionalParams[key].IsStart ? OptionalParams[key].GetInfoIterate() : "";
            }
            else
            {
                return false;
            }
            if (out_file == "")
            {
                return false;
            }
            if (File.Exists(out_file))
            {
                if (Path.GetExtension(out_file) == houzui)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool IsOutFile(string key, string houzui, ref string out_file)
        {
            string file_name = "";
            if (RequiredParams.ContainsKey(key))
            {
                file_name = RequiredParams[key].IsStart ? RequiredParams[key].GetInfoIterate() : "";
            }
            else if (OptionalParams.ContainsKey(key))
            {
                file_name = OptionalParams[key].IsStart ? OptionalParams[key].GetInfoIterate() : "";
            }
            else
            {
                return false;
            }
            if(file_name=="")
            { 
                return false;
            }

            if(Path.GetDirectoryName(file_name) == "")
            {
                if (Path.GetExtension(file_name) == houzui)
                {
                    out_file = file_name;
                    return true;
                }
                else
                {
                    out_file = Path.GetFileNameWithoutExtension(file_name) + houzui;
                    return false;
                }
            }

            if(Directory.Exists(Path.GetDirectoryName(file_name)))
            {
                if (Path.GetExtension(file_name) == houzui)
                {
                    out_file = file_name;
                    return true;
                }
                else
                {
                    out_file = Path.Combine(Path.GetDirectoryName(file_name), Path.GetFileNameWithoutExtension(file_name) + houzui);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
