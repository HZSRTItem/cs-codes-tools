/*------------------------------------------------------------------------------
 * File    : SrtFileFmt
 * Time    : 2022/5/14 14:38:41
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[SrtFileFmt]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SrtFileFmtCSA
{
    /// <summary>
    /// 文件的打开方式
    /// </summary>
    enum SrtFileOpenOpts
    {
        /// <summary>
        /// 只读取数据
        /// </summary>
        Read,
        /// <summary>
        /// 清空文件，写入数据
        /// </summary>
        Write,
        /// <summary>
        /// 追加写数据
        /// </summary>
        Append
    }

    /// <summary>
    /// 格式化文件的方式
    /// </summary>
    class SrtFileFmt
    {
        /// <summary>
        /// 当前工作文件
        /// </summary>
        public string WorkFile = "";
        /// <summary>
        /// 标识
        /// </summary>
        public List<string> Marks = new List<string>();
        /// <summary>
        /// 打开文件选项
        /// </summary>
        private SrtFileOpenOpts _SrtFileOpenOpts;
        /// <summary>
        /// 当前写入文件的文件流
        /// </summary>
        private StreamReader InStream = null;
        /// <summary>
        /// 当前写出的文件流
        /// </summary>
        private StreamWriter OutStream = null;
        /// <summary>
        /// 当前活动的标识
        /// </summary>
        private string ActivityMark = null;
        /// <summary>
        /// 是否为当前活动的标记
        /// </summary>
        private bool IsActivityMark = false;
        
        /// <summary>
        /// 新建一个格式化文件的文档
        /// </summary>
        /// <param name="workFile"></param>
        public SrtFileFmt(string workFile)
        {
            WorkFile = workFile;
            
        }

        /// <summary>
        /// 初始化文件
        /// </summary>
        private void Init()
        {
            if (File.Exists(WorkFile))
            {
                StreamReader streamReader = new StreamReader(WorkFile);
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    if (line.Length > 1)
                    {
                        if (line[0] == '>')
                        {
                            string mark = line.Substring(1).Trim();
                            if (!Marks.Contains(mark))
                            {
                                Marks.Add(mark);
                            }
                        }
                    }
                    line = streamReader.ReadLine();
                }
                streamReader.Close();
            }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="srtFileOpenOpts">打开方式</param>
        /// <returns>是否打开成功</returns>
        public bool Open(SrtFileOpenOpts srtFileOpenOpts)
        {
            _SrtFileOpenOpts = srtFileOpenOpts;
            if (_SrtFileOpenOpts == SrtFileOpenOpts.Read)
            {
                InStream = new StreamReader(WorkFile);
                if (InStream == null)
                {
                    return false;
                }
                Init();
            }
            else if (_SrtFileOpenOpts == SrtFileOpenOpts.Append)
            {
                Init();
                OutStream = new StreamWriter(WorkFile, true);
                if (OutStream == null)
                {
                    return false;
                }
            }
            else
            {
                OutStream = new StreamWriter(WorkFile);
                if (OutStream == null)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 关闭文件
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            Marks.Clear();
            Marks = null;
            Marks = new List<string>();
            IsActivityMark = false;
            ActivityMark = null;
            InStream?.Close();
            InStream = null;
            OutStream?.Close();
            OutStream = null;
            return true;
        }

        int iii = 0;
        /// <summary>
        /// 在当前的活动 Mark 中获得一行信息
        /// </summary>
        /// <returns>没有的时候返回null</returns>
        public string GetLine()
        {
           
            string line = InStream.ReadLine();
            while (line != null)
            {
                iii++;
                if (string.IsNullOrWhiteSpace(line))
                {
                    line = InStream.ReadLine();
                    continue;
                }
                IsMarkEq(line);
                if (IsActivityMark)
                {
                    if (line.Length > 1)
                    {
                        if (line[0] == ' ')
                        {
                            return line.Trim();
                        }
                    }
                }
                line = InStream.ReadLine();
            }
            return null;
        }

        /// <summary>
        /// 是否为当前的活动的标记
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool IsMarkEq(string line)
        {
            if (line.Length > 1)
            {
                if (line[0] == '>')
                {
                    if (line.Substring(1).Trim() == ActivityMark)
                    {
                        IsActivityMark = true;
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return false;
        }

        /// <summary>
        /// 写入一行帮助信息
        /// </summary>
        /// <param name="info">帮助信息，结尾不加换行符</param>
        /// <returns>是否可以添加</returns>
        public bool WriteHelpInfo(string info)
        {
            if (_SrtFileOpenOpts == SrtFileOpenOpts.Read)
            {
                return false;
            }
            OutStream.Write("# ");
            OutStream.WriteLine(info);
            return true;
        }

        /// <summary>
        /// 设置当前活动标识
        /// </summary>
        /// <param name="mark"></param>
        /// <returns></returns>
        public bool SetActivityMark(string mark)
        {
            if (Marks.Contains(mark))
            {
                if (mark != ActivityMark)
                {
                    if(OutStream != null)
                    {
                        OutStream.Write("> ");
                        OutStream.WriteLine(mark);
                    }
                }
                ActivityMark = mark;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据当前mark写入数据，写入后，当前mark改为输入的mark
        /// </summary>
        /// <param name="mark">标识</param>
        /// <param name="info">数据</param>
        /// <returns>是否存在这个mark</returns>
        public bool WriteLine(string mark, string info)
        {
            if(mark == ActivityMark)
            {
                OutStream.Write(" ");
                OutStream.WriteLine(info);
                return true;
            }
            else
            {
                if (SetActivityMark(mark))
                {
                    OutStream.Write(" ");
                    OutStream.WriteLine(info);
                    return true;
                }
                else
                {
                    OutStream.Write("> ");
                    OutStream.WriteLine(mark);
                    OutStream.Write(" ");
                    OutStream.WriteLine(info);
                    Marks.Add(mark);
                    ActivityMark = mark;
                    return false;
                }
            }
        }

        /// <summary>
        /// 根据当前的mark写入数据
        /// </summary>
        /// <param name="info">信息</param>
        /// <returns>是否可以写入信息</returns>
        public bool WriteLine(string info)
        {
            if(Marks.Count == 0)
            {
                return false;
            }
            else
            {
                OutStream.Write(" ");
                OutStream.WriteLine(info);
                return true;
            }
        }
    }
}
