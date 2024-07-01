/*------------------------------------------------------------------------------
 * File    : CodeFileManager
 * Time    : 2023/2/22 18:59:17
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[CodeFileManager]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRTFmtArg;

namespace CodeFileManagerCSA_V2
{
    public class CodeFileManager
    {

        CodeFiles codeFiles = new CodeFiles();

        public CodeFileManager()
        {

        }

        public void Add(string[] args)
        {
            SRTArgCollection sarg_coll = new SRTArgCollection();

        }

        public void Find(string[] args)
        {

        }

        public void Load(string[] args)
        {

        }

        public void Update(string[] args)
        {

        }
    }

    public class CodeFiles
    {
        Dictionary<string, CodeFile> codeFiles = new Dictionary<string, CodeFile>(CONST_VAR.N_CODE_FILES);
        Dictionary<string, CodeFileType> codeFileTypes = new Dictionary<string, CodeFileType>(6);

        public CodeFiles()
        {
            CodeFileType codeFileType = new CodeFileType("CodeFileType.Name: cpp\nCodeFileType.Suffix: .h, .cpp");
            codeFileTypes.Add(codeFileType.Name, codeFileType);
            codeFileType = new CodeFileType("CodeFileType.Name: c\nCodeFileType.Suffix: .h, .c");
            codeFileTypes.Add(codeFileType.Name, codeFileType);
            codeFileType = new CodeFileType("CodeFileType.Name: python\nCodeFileType.Suffix: .py");
            codeFileTypes.Add(codeFileType.Name, codeFileType);
            codeFileType = new CodeFileType("CodeFileType.Name: matlab\nCodeFileType.Suffix: .m");
            codeFileTypes.Add(codeFileType.Name, codeFileType);
            codeFileType = new CodeFileType("CodeFileType.Name: cs\nCodeFileType.Suffix: .cs");
            codeFileTypes.Add(codeFileType.Name, codeFileType);
            codeFileType = new CodeFileType("CodeFileType.Name: java\nCodeFileType.Suffix: .java");
            codeFileTypes.Add(codeFileType.Name, codeFileType);
            codeFileType = new CodeFileType("CodeFileType.Name: javascript\nCodeFileType.Suffix: .js");
            codeFileTypes.Add(codeFileType.Name, codeFileType);
            codeFileType = new CodeFileType("CodeFileType.Name: html\nCodeFileType.Suffix: .html");
            codeFileTypes.Add(codeFileType.Name, codeFileType);
            codeFileType = new CodeFileType("CodeFileType.Name: xml\nCodeFileType.Suffix: .xml");
            codeFileTypes.Add(codeFileType.Name, codeFileType);
            codeFileType = new CodeFileType("CodeFileType.Name: css\nCodeFileType.Suffix: .css");
            codeFileTypes.Add(codeFileType.Name, codeFileType);

            foreach (CodeFileType item in codeFileTypes.Values)
            {
                string dir = item.GetDir();
                DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                FileInfo[] fileInfos = directoryInfo.GetFiles();
                for (int i = 0; i < fileInfos.Length; i++)
                {
                    FileInfo fileInfo = fileInfos[i];
                    foreach (string ext in item.Extension)
                    {
                        if (fileInfo.Extension == ext)
                        {
                            string in_fn = fileInfo.Name;
                            string fn;
                            DateTime dtime;
                            this.SplitFnDt(in_fn, out fn, out dtime);
                            if (!codeFiles.Keys.Contains(fn))
                            {
                                CodeFile codeFile = new CodeFile();
                                codeFile.CFType = item;
                                codeFile.FileName = fn;
                                codeFiles.Add(fn, codeFile);
                            }
                            codeFiles[fn].Add(dtime);
                        }
                    }
                }
            }
        }

        private void SplitFnDt(string in_fn, out string fn, out DateTime dtime)
        {
            // 20230223204048
            // 2023年2月22日20:40:48
            string dt1 = in_fn.Substring(in_fn.Length - 14);
            dtime = DateTime.ParseExact(dt1, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
            fn = in_fn.Substring(0, in_fn.Length - 15);
        }

    }

    public class CodeFile
    {
        List<DateTime> dateTimes = new List<DateTime>();


        public CodeFile()
        {


        }

        public bool Add(string code_fn, bool is_init = false)
        {
            if (is_init)
            {

            }
            return true;
        }

        public bool Add(DateTime dtime)
        {
            dateTimes.Add(dtime);
            return true;
        }

        public bool CopyToBase(string o_fn)
        {
            return true;
        }

        #region prop
        /// <summary>
        /// Internal variable CFType:
        ///     code file type
        /// </summary>
        private CodeFileType m_CFType = null;

        /// <summary>
        /// Property CFType:
        ///     code file type
        /// </summary>
        public CodeFileType CFType
        {
            get { return GetCFType(); }
            set { SetCFType(value); }
        }

        /// <summary>
        /// Set CFType:
        ///     code file type
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetCFType(CodeFileType v_cftype)
        {
            m_CFType = v_cftype;
        }

        /// <summary>
        /// Get CFType:
        ///     code file type
        /// </summary>
        private CodeFileType GetCFType()
        {
            return m_CFType;
        }

        /// <summary>
        /// Internal variable FileName:
        ///     code file name
        /// </summary>
        private string m_FileName = null;

        /// <summary>
        /// Property FileName:
        ///     code file name
        /// </summary>
        public string FileName
        {
            get { return GetFileName(); }
            set { SetFileName(value); }
        }

        /// <summary>
        /// Set FileName:
        ///     code file name
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetFileName(string v_filename)
        {
            m_FileName = v_filename;
        }

        /// <summary>
        /// Get FileName:
        ///     code file name
        /// </summary>
        private string GetFileName()
        {
            return m_FileName;
        }
        #endregion

    }

    public class CodeFileType
    {

        public CodeFileType()
        {
            m_Name = "temp";
            m_Extension = null;
            MakeDir();
        }

        public CodeFileType(string name, params string[] suffixs)
        {
            m_Name = name;
            m_Extension = suffixs;
            MakeDir();
        }

        public CodeFileType(string info)
        {
            string[] infos = info.Split('\n');
            infos[0] = infos[0].Trim();
            string[] infos1 = infos[0].Split(':');
            infos1[1] = infos1[1].Trim();
            m_Name = infos1[1];
            if (infos.Length >= 2)
            {
                infos[1] = infos[1].Trim();
                string[] infos2 = infos[1].Split(':');
                infos2[1] = infos2[1].Trim();
                string[] info21 = infos2[1].Split(',');
                for (int i = 0; i < info21.Length; i++)
                {
                    info21[i] = info21[i].Trim();
                }
                m_Extension = info21;
            }
            MakeDir();
        }

        public override string ToString()
        {
            string outstr = "CodeFileType.Name: " + m_Name;
            if (m_Name != null)
            {
                outstr += "\n";
                outstr += "CodeFileType.Suffix:";
                outstr += string.Join(", ", m_Extension);
            }
            return outstr;
        }

        public bool MakeDir()
        {
            string dir = GetDir();
            if (Directory.Exists(dir))
            {
                return true;
            }
            else
            {
                Directory.CreateDirectory(dir);
                return false;
            }
        }

        public string GetDir()
        {
            return Path.Combine(CONST_VAR.CODE_FILE_DIR, m_Name);
        }

        /// <summary>
        /// Internal variable Name:
        ///     Name of code file type
        /// </summary>
        private string m_Name = "temp";

        /// <summary>
        /// Property Name:
        ///     Name of code file type
        /// </summary>
        public string Name
        {
            get { return GetName(); }
            set { SetName(value); }
        }

        /// <summary>
        /// Set Name:
        ///     Name of code file type
        /// </summary>
        /// <param name="v_name">external incoming variable</param>
        private void SetName(string v_name)
        {
            m_Name = v_name;
        }

        /// <summary>
        /// Get Name:
        ///     Name of code file type
        /// </summary>
        private string GetName()
        {
            return m_Name;
        }

        /// <summary>
        /// Internal variable Extension:
        ///     Extension for this code file type
        /// </summary>
        private string[] m_Extension = null;

        /// <summary>
        /// Property Extension:
        ///     Extension for this code file type
        /// </summary>
        public string[] Extension
        {
            get { return GetExtension(); }
            set { SetExtension(value); }
        }

        /// <summary>
        /// Set Extension:
        ///     Extension for this code file type
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetExtension(string[] v_extension)
        {
            m_Extension = v_extension;
        }

        /// <summary>
        /// Get Extension:
        ///     Extension for this code file type
        /// </summary>
        private string[] GetExtension()
        {
            return m_Extension;
        }

    }

    public class CONST_VAR
    {
        /// <summary>
        /// save code file dir
        /// </summary>
        public static string CODE_FILE_DIR = @"D:\Utils\CodeFiles";

        /// <summary>
        /// number code files
        /// </summary>
        public static int N_CODE_FILES = 100;
    }
}
