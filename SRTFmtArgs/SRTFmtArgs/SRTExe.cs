/*------------------------------------------------------------------------------
 * File    : SRTFmtArgs
 * Time    : 2023/2/11 9:06:46
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[SRTFmtArgs]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRTFmtArg
{
    public class SRTArgCollection
    {
        private Dictionary<string, SRTArg> Args = new Dictionary<string, SRTArg>(16);

        public bool FmtArgs(string[] args)
        {
            List<string> args_list = new List<string>(args);
            foreach (string item in Args.Keys)
            {
                SRTArg arg = Args[item];
                if (arg.ArgType == SRTArgType.Bool)
                {
                    arg.FmtArg(args_list);
                }
            }
            foreach (string item in Args.Keys)
            {
                SRTArg arg = Args[item];
                if (arg.ArgType == SRTArgType.MarkInfo)
                {
                    arg.FmtArg(args_list);
                }
            }
            foreach (string item in Args.Keys)
            {
                SRTArg arg = Args[item];
                if (arg.ArgType == SRTArgType.Location)
                {
                    arg.FmtArg(args_list);
                }
            }
            return true;
        }

        /// <summary>
        /// get a arg by name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>arg</returns>
        public SRTArg this[string name]
        {
            get { return Args[name]; }
        }

        public string Usage()
        {
            string usage = m_Name + " ";
            if (Args.Count != 0)
            {
                foreach (string item in Args.Keys)
                {
                    SRTArg arg = Args[item];
                    if (arg.ArgType == SRTArgType.Location)
                    {
                        usage += arg.GetArgDes() + " ";
                    }
                }
                foreach (string item in Args.Keys)
                {
                    SRTArg arg = Args[item];
                    if (arg.ArgType == SRTArgType.MarkInfo)
                    {
                        usage += arg.GetArgDes() + " ";
                    }
                }
                foreach (string item in Args.Keys)
                {
                    SRTArg arg = Args[item];
                    if (arg.ArgType == SRTArgType.Bool)
                    {
                        usage += arg.GetArgDes() + " ";
                    }
                }
            }
            usage += "\n";
            if (m_Description != null)
            {
                usage += "@Description: \n    " + m_Description + "\n";
            }
            if (Args.Count != 0)
            {
                usage += "@Args: \n";
                foreach (string item in Args.Keys)
                {
                    SRTArg arg = Args[item];
                    if (arg.ArgType == SRTArgType.Location)
                    {
                        usage += "    " + arg.GetHelp();
                    }
                }
                foreach (string item in Args.Keys)
                {
                    SRTArg arg = Args[item];
                    if (arg.ArgType == SRTArgType.MarkInfo)
                    {
                        usage += "    " + arg.GetHelp();
                    }
                }
                foreach (string item in Args.Keys)
                {
                    SRTArg arg = Args[item];
                    if (arg.ArgType == SRTArgType.Bool)
                    {
                        usage += "    " + arg.GetHelp();
                    }
                }
            }
            usage += "(C)Copyright 2023, ZhengHan. All rights reserved.\n";
            return usage;
        }

        /// <summary>
        /// add an arg
        /// </summary>
        /// <param name="arg">an arg</param>
        public void Add(SRTArg arg)
        {
            Args.Add(arg.Name, arg);
        }

        public void Add(string name, string help_info = "", string mark_name = ""
            , int max_number = 1, SRTArgType arg_type = SRTArgType.Location
            , bool arg_bool = false, bool is_optional = false)
        {
            Add(new SRTArg(name, help_info, mark_name, max_number, arg_type, arg_bool, is_optional));
        }

        /// <summary>
        /// Internal variable Description:
        ///     Descriptive information of the procedure
        /// </summary>
        private string m_Description = null;

        /// <summary>
        /// Property Description:
        ///     Descriptive information of the procedure
        /// </summary>
        public string Description
        {
            get { return GetDescription(); }
            set { SetDescription(value); }
        }

        /// <summary>
        /// Set Description:
        ///     Descriptive information of the procedure
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetDescription(string v_description)
        {
            m_Description = v_description;
        }

        /// <summary>
        /// Get Description:
        ///     Descriptive information of the procedure
        /// </summary>
        private string GetDescription()
        {
            return m_Description;
        }

        /// <summary>
        /// Internal variable Name:
        ///     Program name
        /// </summary>
        private string m_Name = null;

        /// <summary>
        /// Property Name:
        ///     Program name
        /// </summary>
        public string Name
        {
            get { return GetName(); }
            set { SetName(value); }
        }

        /// <summary>
        /// Set Name:
        ///     Program name
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetName(string v_name)
        {
            m_Name = v_name;
        }

        /// <summary>
        /// Get Name:
        ///     Program name
        /// </summary>
        private string GetName()
        {
            return m_Name;
        }

    }

    public enum SRTArgType
    {
        None,
        Location,
        MarkInfo,
        Bool
    }

    public class SRTArg
    {
        #region 外部属性
        /// <summary>
        /// Internal variable Name:
        ///     arg name for index
        /// </summary>
        private string m_Name = null;

        /// <summary>
        /// Property Name:
        ///     arg name for index
        /// </summary>
        public string Name
        {
            get { return GetName(); }
            set { SetName(value); }
        }

        /// <summary>
        /// Set Name:
        ///     arg name for index
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetName(string v_name)
        {
            m_Name = v_name;
        }

        /// <summary>
        /// Get Name:
        ///     arg name for index
        /// </summary>
        private string GetName()
        {
            return m_Name;
        }

        /// <summary>
        /// Internal variable MarkName:
        ///     arg command line identifier
        /// </summary>
        private string m_MarkName = null;

        /// <summary>
        /// Property MarkName:
        ///     arg command line identifier
        /// </summary>
        public string MarkName
        {
            get { return GetMarkName(); }
            set { SetMarkName(value); }
        }

        /// <summary>
        /// Set MarkName:
        ///     arg command line identifier
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetMarkName(string v_markname)
        {
            if (v_markname == "")
            {
                v_markname = m_Name.ToLower();
            }
            if (m_ArgType == SRTArgType.MarkInfo)
            {
                m_MarkName = "-" + v_markname;
            }
            else if (m_ArgType == SRTArgType.Bool)
            {
                m_MarkName = "--" + v_markname;
            }
            else
            {
                m_MarkName = v_markname;
            }

        }

        /// <summary>
        /// Get MarkName:
        ///     arg command line identifier
        /// </summary>
        private string GetMarkName()
        {
            return m_MarkName;
        }

        /// <summary>
        /// Internal variable ArgType:
        ///     type of arg
        /// </summary>
        private SRTArgType m_ArgType = SRTArgType.None;

        /// <summary>
        /// Property ArgType:
        ///     type of arg
        /// </summary>
        public SRTArgType ArgType
        {
            get { return GetArgType(); }
            set { SetArgType(value); }
        }

        /// <summary>
        /// Set ArgType:
        ///     type of arg
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetArgType(SRTArgType v_argtype)
        {
            m_ArgType = v_argtype;
        }

        /// <summary>
        /// Get ArgType:
        ///     type of arg
        /// </summary>
        private SRTArgType GetArgType()
        {
            return m_ArgType;
        }

        /// <summary>
        /// Internal variable MaxNumber:
        ///     the max number of this arg
        /// </summary>
        private int m_MaxNumber = 0;

        /// <summary>
        /// Property MaxNumber:
        ///     the max number of this arg
        /// </summary>
        public int MaxNumber
        {
            get { return GetMaxNumber(); }
            set { SetMaxNumber(value); }
        }

        /// <summary>
        /// Set MaxNumber:
        ///     the max number of this arg
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetMaxNumber(int v_maxnumber)
        {
            if (m_MaxNumber < v_maxnumber)
            {
                string[] ss = new string[v_maxnumber];
                for (int i = 0; i < count; i++)
                {
                    ss[i] = data[i];
                }
                data = ss;
            }
            else
            {
                if (count > v_maxnumber)
                {
                    count = v_maxnumber;
                }
            }
            m_MaxNumber = v_maxnumber;
        }

        /// <summary>
        /// Get MaxNumber:
        ///     the max number of this arg
        /// </summary>
        private int GetMaxNumber()
        {
            return m_MaxNumber;
        }

        /// <summary>
        /// Internal variable HelpInfo:
        ///     help info for this arg
        /// </summary>
        private string m_HelpInfo = null;

        /// <summary>
        /// Property HelpInfo:
        ///     help info for this arg
        /// </summary>
        public string HelpInfo
        {
            get { return GetHelpInfo(); }
            set { SetHelpInfo(value); }
        }

        /// <summary>
        /// Set HelpInfo:
        ///     help info for this arg
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetHelpInfo(string v_helpinfo)
        {
            m_HelpInfo = v_helpinfo;
        }

        /// <summary>
        /// Get HelpInfo:
        ///     help info for this arg
        /// </summary>
        private string GetHelpInfo()
        {
            return m_HelpInfo;
        }

        /// <summary>
        /// number of arg items
        /// </summary>
        private int count;

        /// <summary>
        /// number of arg items
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Internal variable IsOptional:
        ///     Is this arg optional
        /// </summary>
        private bool m_IsOptional = false;

        /// <summary>
        /// Property IsOptional:
        ///     Is this arg optional
        /// </summary>
        public bool IsOptional
        {
            get { return GetIsOptional(); }
            set { SetIsOptional(value); }
        }

        /// <summary>
        /// Set IsOptional:
        ///     Is this arg optional
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetIsOptional(bool v_isoptional)
        {
            m_IsOptional = v_isoptional;
        }

        /// <summary>
        /// Get IsOptional:
        ///     Is this arg optional
        /// </summary>
        private bool GetIsOptional()
        {
            return m_IsOptional;
        }
        #endregion

        public bool ArgBool { get { return argBool; } set { argBool = value; } }

        private string[] data = null;
        private bool argBool = false;

        /// <summary>
        /// srt arg format
        /// </summary>
        public SRTArg()
        {
            Init();
        }

        /// <summary>
        /// Build an SRT arg
        /// </summary>
        /// <param name="name">arg name for index</param>
        /// <param name="help_info">help info for show</param>
        /// <param name="mark_name">mark for cmd line</param>
        /// <param name="max_number">max number of arg</param>
        /// <param name="arg_type"> as SRTArgType</param>
        public SRTArg(string name, string help_info = "", string mark_name = ""
            , int max_number = 1, SRTArgType arg_type = SRTArgType.Location
            , bool arg_bool = false, bool is_optional = false)
        {
            SetName(name);
            SetMaxNumber(max_number);
            SetArgType(arg_type);
            SetHelpInfo(help_info);
            SetMarkName(mark_name);
            SetIsOptional(is_optional);
            argBool = false;
            count = 0;
        }

        public void FmtArg(List<string> args, int start = 0, int end = -1)
        {
            if (start < 0)
            {
                start = 0;
            }
            if (end < 0)
            {
                end = args.Count + end;
            }

            if (m_ArgType == SRTArgType.Location)
            {
                for (int i = start; i < end + 1; i++)
                {
                    if (Add(args[i]))
                    {
                        args.RemoveAt(i);
                        i--;
                        end--;
                    }
                }
            }
            else if (m_ArgType == SRTArgType.MarkInfo)
            {
                for (int i = start; i < end + 1; i++)
                {
                    if (args[i] == m_MarkName & i < end)
                    {
                        if (Add(args[i + 1]))
                        {
                            args.RemoveAt(i);
                            args.RemoveAt(i);
                            i--;
                            end -= 2;
                        }
                    }
                }
            }
            else if (m_ArgType == SRTArgType.Bool)
            {
                for (int i = start; i < end + 1; i++)
                {
                    if (args[i] == m_MarkName)
                    {
                        argBool = true;
                        args.RemoveAt(i);
                        i--;
                        end--;
                    }
                }
            }
        }

        public bool Add(string info)
        {
            if (count < m_MaxNumber)
            {
                data[count] = info;
                count++;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Init()
        {
            m_MaxNumber = 1;
            data = new string[m_MaxNumber];
            m_Name = "";
            m_HelpInfo = "";
            m_MarkName = "";
            m_ArgType = SRTArgType.None;
            count = 0;
            argBool = false;
        }

        /// <summary>
        /// get arg of index. 
        /// set arg of index. 
        /// can use index=-1 for end of this index = index + count.
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>info</returns>
        public string this[int index]
        {
            get { return Get(index); }
            set { Set(index, value); }
        }

        public string Get(int index = 0)
        {
            if (index < 0)
            {
                index += count;
            }
            return data[index];
        }

        public void Set(string info)
        {
            Set(0, info);
        }

        public void Set(int index, string info)
        {
            if (index < 0)
            {
                index += count;
            }
            data[index] = info;
        }

        public string GetHelp()
        {
            string help = "";
            if (m_IsOptional)
            {
                help += "[opt]";
            }

            if (m_ArgType == SRTArgType.Location)
            {
                string t1 = "";
                if (m_MaxNumber != 1)
                {
                    t1 = " nmax=" + m_MaxNumber;
                }
                help += string.Format("{0}:{1} {2}\n", m_Name, t1, m_HelpInfo);
            }
            else if (m_ArgType == SRTArgType.MarkInfo)
            {
                string t1 = "";
                if (m_MaxNumber != 1)
                {
                    t1 = " nmax=" + m_MaxNumber;
                }
                help += string.Format("{0}:{1}{2} {3}\n", m_Name, m_MarkName, t1, m_HelpInfo);
            }
            else if (m_ArgType == SRTArgType.Bool)
            {
                help += string.Format("{0}:{1} default={2} {3}\n", m_Name, m_MarkName, argBool, m_HelpInfo);
            }

            return help;
        }

        public string GetArgDes()
        {
            string help = "";
            if (m_IsOptional)
            {
                help += "opt:";
            }

            if (m_ArgType == SRTArgType.Location)
            {
                if (m_MaxNumber != 1)
                {
                    help += string.Format("{0} {1}", m_Name, m_MaxNumber);
                }
                else
                {
                    help += m_Name;
                }
            }
            else if (m_ArgType == SRTArgType.MarkInfo)
            {
                string t1 = "";
                if (m_MaxNumber != 1)
                {
                    help += string.Format("{0}:{1} {2}", m_MarkName, m_Name, m_MaxNumber);
                }
                else
                {
                    help += string.Format("{0}:{1}", m_MarkName, m_Name);
                }
            }
            else if (m_ArgType == SRTArgType.Bool)
            {
                help += string.Format("{0}:{1}", m_MarkName, m_Name); ;
            }
            if (help.Contains(" "))
            {
                help = "[" + help + "]";
            }
            return help;
        }
    }
}
