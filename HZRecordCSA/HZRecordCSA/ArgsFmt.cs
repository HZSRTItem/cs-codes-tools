/*------------------------------------------------------------------------------
 * File    : ArgsFmt
 * Time    : 2022/5/9 18:49:14
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[ArgsFmt]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtOpenCSA
{
    class ArgsFmt
    {
        List<string[]> Args = new List<string[]>();
        Dictionary<string, string> ArgsInfo = new Dictionary<string, string>();
        List<string> OArgs = new List<string>();
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Descinfos = "";

        /// <summary>
        /// 添加一个信息
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="info">参数信息</param>
        public void AddArgs(string name, string info)
        {
            ArgsInfo.Add(name, info);
        }

        /// <summary>
        /// 格式化args
        /// </summary>
        /// <param name="args"></param>
        public void Fit(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if(args[i].Length < 2)
                {
                    OArgs.Add(args[i]);
                }
                else
                {
                    if(args[i][0] == '-' & args[i][1] == '-')
                    {
                        Args.Add(new string[2] { args[i], " " });
                    }
                    else if(args[i][0] == '-' & i< args.Length-1)
                    {
                        Args.Add(new string[2] { args[i], args[i + 1] });
                    }
                    else if(args[i][0] == '-')
                    {
                        Args.Add(new string[2] { args[i], " " });
                    }
                    else
                    {
                        OArgs.Add(args[i]);
                    }
                }
            }

        }

        /// <summary>
        /// 通过名字获得参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns>参数内容</returns>
        public string GetArgOne(string name)
        {
            for (int i = 0; i < Args.Count; i++)
            {
                if(Args[i][0] == name)
                {
                    return Args[i][1];
                }
            }
            return "";
        }

        /// <summary>
        /// 获得第N个单个参数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public string GetArgOne(int n)
        {
            if (OArgs.Count==0)
            {
                return "";
            }
            return n > OArgs.Count ? "" : OArgs[n];
        }

        /// <summary>
        /// 获得无标记参数第一个
        /// </summary>
        /// <returns></returns>
        public string GetArgOne()
        {
            return GetArgOne(0);
        }

        /// <summary>
        /// 获得所有标记为 name 的参数内容
        /// </summary>
        /// <param name="name">标记</param>
        /// <returns></returns>
        public string[] GetArgs(string name)
        {
            List<string> argss = new List<string>();
            for (int i = 0; i < Args.Count; i++)
            {
                if (Args[i][0] == name)
                {
                    argss.Add(Args[i][1]);
                }
            }
            return argss.ToArray();
        }

        /// <summary>
        /// 获得所有的无标记参数
        /// </summary>
        /// <returns></returns>
        public string[] GetArgs()
        {
            return OArgs.ToArray();
        }

        /// <summary>
        /// 获得帮助信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetHelpInfo(string info)
        {
            info = info.Trim() + "\n";
            info += "description:\n    ";
            info += Descinfos.Trim().Replace("\n", " ") + "\n";
            info += "args:\n";
            foreach (string item in ArgsInfo.Keys)
            {
                info += "    " + item + ": " + ArgsInfo[item] + "\n";
            }
            return info;
        }
    }
}
