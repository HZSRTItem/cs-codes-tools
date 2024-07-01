/*------------------------------------------------------------------------------
 * File    : CodeFileManager_v1
 * Time    : 2023/2/22 20:56:37
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[CodeFileManager_v1]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRTFmtArg;

namespace CodeFileManager_V1
{
    public class CodeFileManager
    {

        CodeFiles CodeFilesColl = new CodeFiles();

        public CodeFileManager()
        {

        }

        public void Add(string[] args)
        {
            SRTArgCollection sarg_coll = new SRTArgCollection();
            sarg_coll.Name = "srt_cfm add";
            sarg_coll.Description = "Add a code file name";
            sarg_coll.Add("code_file", "input code file");
            sarg_coll.Add("filename", help_info: "as the added file name, the default is the input code file name", arg_type: SRTArgType.MarkInfo, mark_name: "i");
            sarg_coll.Add("help", help_info: "get help info for add", arg_type: SRTArgType.Bool, mark_name: "h");
            sarg_coll.FmtArgs(args, start: 1);

            if (sarg_coll["help"].ArgBool)
            {
                Console.WriteLine(sarg_coll.Usage());
                return;
            }

            if (sarg_coll["code_file"][0] == null)
            {
                Console.WriteLine("Can not find input code file");
                Console.WriteLine(sarg_coll.Usage());
                return;
            }

            string code_fn = sarg_coll["code_file"][0];
            string filename = sarg_coll["filename"].Get();
            if (filename == null)
            {
                filename = code_fn;
            }
            else
            {
                if (Path.GetExtension(filename) == "")
                {
                    filename = filename + Path.GetExtension(code_fn);
                }
            }

            if (CodeFilesColl[filename] == null)
            {
                if (File.Exists(code_fn))
                {
                    DateTime dtime = DateTime.Now;
                    CodeFilesColl.Add(code_fn, dtime, filename);
                    CodeFilesColl[filename].Show();
                    Console.WriteLine("Success add to " + CodeFilesColl[filename].GetBaseFileName());
                }
                else
                {
                    Console.WriteLine("Can not find file: " + code_fn);
                }

            }
            else
            {
                //CodeFilesColl[code_fn].Show();
                Console.WriteLine("Code file exist, please use `upsate`: " + CodeFilesColl[filename].FileName);
            }
        }

        public void Find(string[] args)
        {
            SRTArgCollection sarg_coll = new SRTArgCollection();
            sarg_coll.Name = "srt_cfm find";
            sarg_coll.Description = "Find a code file and show file info";
            sarg_coll.Add("name_info", help_info: "input code file name");
            sarg_coll.Add("extension", help_info: "codes file extension", arg_type: SRTArgType.MarkInfo, mark_name: "ext");
            sarg_coll.Add("findnumber", help_info: "find codes file number", arg_type: SRTArgType.MarkInfo, mark_name: "n");
            sarg_coll.Add("all", help_info: "get all code file", arg_type: SRTArgType.Bool, mark_name: "a");
            sarg_coll.Add("help", help_info: "get help info for find", arg_type: SRTArgType.Bool, mark_name: "h");
            sarg_coll.FmtArgs(args, start: 1);

            if (sarg_coll["help"].ArgBool)
            {
                Console.WriteLine(sarg_coll.Usage());
                return;
            }

            if (sarg_coll["all"].ArgBool)
            {
                if (!CodeFilesColl.Show())
                {
                    Console.WriteLine("Can not find code file.");
                }
                return;
            }

            string code_fn = sarg_coll["name_info"][0];
            if (code_fn == null)
            {
                code_fn = "";
            }
            if (code_fn == null)
            {
                Console.WriteLine("Can not get arg name_info.");
                Console.WriteLine(sarg_coll.Usage());
            }
            else
            {
                if (CodeFilesColl[code_fn] != null)
                {
                    CodeFilesColl[code_fn].Show();
                    return;
                }

                string ext_filter = sarg_coll["extension"][0];
                if (ext_filter == null)
                {
                    ext_filter = "";
                }
                int findnumber = 10;
                if (sarg_coll["findnumber"][0] != null)
                {
                    try
                    {
                        findnumber = int.Parse(sarg_coll["findnumber"][0]);
                    }
                    catch
                    {
                        Console.WriteLine("Warning: Can not fotmat `findnumber` as `{0}`.", sarg_coll["findnumber"][0]);
                    }
                }
                CodeFile[] codeFiles1 = CodeFilesColl.Find(code_fn, ext_filter, findnumber);
                if (!CodeFiles.Show(codeFiles1))
                {
                    Console.WriteLine("Can not find code file by info: " + code_fn);
                }
            }
        }

        public void Load(string[] args)
        {
            SRTArgCollection sarg_coll = new SRTArgCollection();
            sarg_coll.Name = "srt_cfm load";
            sarg_coll.Description = "Load a code file into a folder";
            sarg_coll.Add("input_code_file", "input code file");
            sarg_coll.Add("output_code_file", "output code file", arg_type: SRTArgType.MarkInfo, mark_name: "o");
            sarg_coll.Add("end", help_info: "load the last version of the code file", arg_type: SRTArgType.Bool);
            sarg_coll.Add("yes", help_info: "Join or not", arg_type: SRTArgType.Bool, mark_name: "y");
            sarg_coll.Add("help", help_info: "get help info for add", arg_type: SRTArgType.Bool, mark_name: "h");
            sarg_coll.FmtArgs(args, start: 1);

            if (sarg_coll["help"].ArgBool)
            {
                Console.WriteLine(sarg_coll.Usage());
                return;
            }

            if (sarg_coll["input_code_file"][0] == null)
            {
                Console.WriteLine("Can not find input code file");
                Console.WriteLine(sarg_coll.Usage());
                return;
            }

            string output_fn = sarg_coll["output_code_file"][0];
            string input_code_fn = sarg_coll["input_code_file"][0];

            if (CodeFilesColl[input_code_fn] == null)
            {
                Console.WriteLine("Can not find in code base for input code file: {0}", input_code_fn);
                return;
            }
            else
            {
                if (output_fn == null)
                {
                    output_fn = CodeFilesColl[input_code_fn].FileName;
                }
                CodeFilesColl[input_code_fn].Show();
                int dt_index = -1;
                string line;
                if (!sarg_coll["end"].ArgBool)
                {
                    Console.Write("Please input select code file version[N]: ");
                    line = Console.ReadLine();
                    try
                    {
                        dt_index = int.Parse(line) - 1;
                        Console.WriteLine("Code file version: {0}", CodeFilesColl[input_code_fn][dt_index].ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return;
                    }
                }
                char y_n = 'n';
                if (sarg_coll["yes"].ArgBool)
                {
                    y_n = 'y';
                }
                else
                {
                    Console.Write("Whether to load this code file? [y/n]: ");
                    line = Console.ReadLine();
                    if (line.Length >= 1)
                    {
                        y_n = line[0];
                    }
                }

                if (y_n == 'y')
                {
                    if (CodeFilesColl[input_code_fn].LoadTo(output_fn, dt_index))
                    {
                        Console.WriteLine("Success load to {0}.", output_fn);
                    }
                    else
                    {
                        Console.WriteLine("Not load code file: {0}.", CodeFilesColl[input_code_fn].FileName);
                    }
                }
                else
                {
                    Console.WriteLine("Not load code file: {0}.", CodeFilesColl[input_code_fn].FileName);
                }
            }
        }

        public void Update(string[] args)
        {
            SRTArgCollection sarg_coll = new SRTArgCollection();
            sarg_coll.Name = "srt_cfm update";
            sarg_coll.Description = "Update a file in the code base";
            sarg_coll.Add("code_file", "input code file");
            sarg_coll.Add("filename", help_info: "as the update file name, the default is the input code file name", arg_type: SRTArgType.MarkInfo, mark_name: "i");
            sarg_coll.Add("yes", help_info: "Join or not", arg_type: SRTArgType.Bool, mark_name: "y");
            sarg_coll.Add("help", help_info: "get help info for add", arg_type: SRTArgType.Bool, mark_name: "h");
            sarg_coll.FmtArgs(args, start: 1);

            if (sarg_coll["help"].ArgBool)
            {
                Console.WriteLine(sarg_coll.Usage());
                return;
            }

            if (sarg_coll["code_file"][0] == null)
            {
                Console.WriteLine("Can not find input code file");
                Console.WriteLine(sarg_coll.Usage());
                return;
            }

            string code_fn = sarg_coll["code_file"][0];
            string filename = sarg_coll["filename"].Get();
            if (filename == null)
            {
                filename = code_fn;
            }

            if (CodeFilesColl[filename] == null)
            {
                Console.WriteLine("Can not find in code base for input code file: {0}.", filename);
                return;
            }
            else
            {
                CodeFilesColl[filename].Show();
                char y_n = 'n';
                if (sarg_coll["yes"].ArgBool)
                {
                    y_n = 'y';
                }
                else
                {
                    Console.Write("Whether to update this code file? [y/n]: ");
                    string line = Console.ReadLine();
                    if (line.Length >= 1)
                    {
                        y_n = line[0];
                    }
                }
                if (y_n == 'y')
                {
                    DateTime dtime = DateTime.Now;
                    CodeFilesColl.Add(code_fn, dtime, filename);
                    CodeFilesColl[filename].Show();
                    Console.WriteLine("Success update to {0}.", CodeFilesColl[filename].FileName);
                }
                else
                {
                    Console.WriteLine("Not update code file: {0}.", CodeFilesColl[filename].FileName);
                }
            }
        }
    }

    public class CodeFiles
    {
        Dictionary<string, CodeFile> CFilesDict = new Dictionary<string, CodeFile>(CONST_VAR.N_CODE_FILES);

        public CodeFile this[string fn]
        {
            get { return GetByFileName(fn); }
        }

        public CodeFiles()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(CONST_VAR.CODE_FILE_DIR);
            FileInfo[] fileInfos = directoryInfo.GetFiles();

            for (int i = 0; i < fileInfos.Length; i++)
            {
                FileInfo fileInfo = fileInfos[i];
                string in_fn = Path.GetFileName(fileInfo.Name);
                string fn;
                DateTime dtime;
                this.SplitFnDt(in_fn, out fn, out dtime);
                if (!CFilesDict.Keys.Contains(fn))
                {
                    CodeFile codeFile = new CodeFile();
                    codeFile.FileName = fn;
                    codeFile.Extension = fileInfo.Extension;
                    CFilesDict.Add(fn, codeFile);
                }
                CFilesDict[fn].Add(dtime);
            }
        }

        public bool Add(string o_fn, DateTime dtime, string filename = "")
        {
            string fn = Path.GetFileName(o_fn);
            if (filename != "")
            {
                fn = Path.GetFileName(filename);
            }
            bool ret = false;
            if (!CFilesDict.Keys.Contains(fn))
            {
                CodeFile codeFile = new CodeFile();
                codeFile.FileName = fn;
                codeFile.Extension = Path.GetExtension(o_fn);
                CFilesDict.Add(fn, codeFile);
                ret = true;
            }
            CFilesDict[fn].Add(dtime);
            CFilesDict[fn].CopyToBase(o_fn, dtime);
            return ret;
        }

        public CodeFile GetByFileName(string o_fn)
        {
            string fn = Path.GetFileName(o_fn);
            if (CFilesDict.ContainsKey(fn))
            {
                return CFilesDict[fn];
            }
            else
            {
                return null;
            }
        }

        public CodeFile[] Find(string name_info, string ext_filter = "", int find_number = 9)
        {
            CodeFile[] out_cfs = new CodeFile[find_number];
            double[] DoubleArr = new double[CFilesDict.Count];
            string[] StrArr = new string[CFilesDict.Count];
            int i = 0;
            foreach (string item in CFilesDict.Keys)
            {
                CodeFile cf = CFilesDict[item];
                StrArr[i] = item;
                if (ext_filter == "")
                {
                    DoubleArr[i] = cf.NameSimilarity(name_info);
                }
                else
                {
                    if (ext_filter == cf.Extension)
                    {
                        DoubleArr[i] = cf.NameSimilarity(name_info);
                    }
                    else
                    {
                        DoubleArr[i] = -1;
                    }
                }
                i++;
            }
            List<double> d_list = new List<double>(DoubleArr);
            int n = DoubleArr.Length;
            if (n > find_number)
            {
                n = find_number;
            }
            for (int j = 0; j < n; j++)
            {
                double n_max = d_list.Max();
                if (n_max == -1)
                {
                    break;
                }
                int n_select = d_list.IndexOf(n_max);
                out_cfs[j] = CFilesDict[StrArr[n_select]];
                double t = d_list[0];
                d_list[n_select] = -1;
            }
            return out_cfs;
        }

        private void SplitFnDt(string in_fn, out string fn, out DateTime dtime)
        {
            // 20230223204048
            // 2023年2月22日20:40:48
            string file_exten = Path.GetExtension(in_fn);
            in_fn = Path.GetFileNameWithoutExtension(in_fn);
            string dt1 = in_fn.Substring(in_fn.Length - 14);
            dtime = DateTime.ParseExact(dt1, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
            fn = in_fn.Substring(0, in_fn.Length - 15) + file_exten;
        }

        public static bool Show(CodeFile[] cfs)
        {
            int n_cfs = 0;
            int m_line1 = 11;
            int m_line2 = 12;
            for (n_cfs = 0; n_cfs < cfs.Length; n_cfs++)
            {
                if (cfs[n_cfs] == null)
                {
                    break;
                }
                if (cfs[n_cfs].FileName.Length > m_line1)
                {
                    m_line1 = cfs[n_cfs].FileName.Length;
                }
                if (cfs[n_cfs].Extension.Length > m_line2)
                {
                    m_line2 = cfs[n_cfs].Extension.Length;
                }
            }
            if (n_cfs == 0)
            {
                return false;
            }
            Console.WriteLine("Code files are as follows:\n");
            string fmt1 = "  {0, " + 5.ToString() + "}" +
                          " {1, " + m_line1.ToString() + "}" +
                          " {2, -" + m_line2.ToString() + "}" +
                          " {3, -6}";
            Console.WriteLine(fmt1, "No.", "-FileName-", "-Extension-", "-Version-");
            for (int i = 0; i < n_cfs; i++)
            {
                Console.WriteLine(fmt1, i + 1, Path.GetFileNameWithoutExtension(cfs[i].FileName), cfs[i].Extension, cfs[i].CountDT);
            }
            return true;
        }

        public bool Show()
        {
            CodeFile[] cfs = CFilesDict.Values.ToArray();
            return Show(cfs);
        }
    }

    public class CodeFile
    {
        List<DateTime> dateTimes = new List<DateTime>();

        public DateTime this[int index]
        {
            get { return GetDT(index); }
        }

        public DateTime GetDT(int index)
        {
            if (index < 0)
            {
                index = dateTimes.Count + index;
            }
            if (index < 0 | index >= dateTimes.Count)
            {
                throw new Exception("dateTimes index out");
            }
            else
            {
                return dateTimes[index];
            }
        }

        public int CountDT
        {
            get { return dateTimes.Count; }
        }

        public CodeFile()
        {
            m_Extension = "";
            m_FileName = "";
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

        public bool CopyToBase(string o_fn, DateTime dtime)
        {
            if (File.Exists(o_fn))
            {
                FileInfo fileInfo = new FileInfo(o_fn);
                string to_filename = Path.GetFileNameWithoutExtension(m_FileName) + "_" + dtime.ToString("yyyyMMddHHmmss") + m_Extension;
                fileInfo.CopyTo(Path.Combine(CONST_VAR.CODE_FILE_DIR, to_filename), true);
                return true;
            }
            else
            {
                return true;
            }
        }

        public bool CopyToBase(string o_fn)
        {
            DateTime dtime = DateTime.Now;
            return CopyToBase(o_fn, dtime);
        }

        public void Show()
        {
            Console.WriteLine("FileName  : {0}", m_FileName);
            Console.WriteLine("Extension : {0}", m_Extension);
            Console.WriteLine("Version   : [{0}]", dateTimes.Count);
            for (int i = 0; i < dateTimes.Count; i++)
            {
                Console.WriteLine("  - {0, 2}: " + dateTimes[i].ToString("yyyy-MM-dd HH:mm:ss"), i + 1);
            }
        }

        public string GetBaseFileName()
        {
            return Path.Combine(CONST_VAR.CODE_FILE_DIR, m_FileName);
        }

        public double NameSimilarity(string fn)
        {
            double Kq = 2;
            double Kr = 1;
            double Ks = 1;

            char[] ss = m_FileName.ToCharArray();
            char[] st = fn.ToCharArray();

            //获取交集数量
            int q = ss.Intersect(st).Count();
            int s = ss.Length - q;
            int r = st.Length - q;

            return Kq * q / (Kq * q + Kr * r + Ks * s);
        }

        public bool LoadTo(string to_fn, int dt_index = -1)
        {
            if (dt_index < 0)
            {
                dt_index = dateTimes.Count + dt_index;
            }


            if (File.Exists(to_fn))
            {
                Console.WriteLine("Code file exist: " + to_fn);
                Console.Write("Whether to overwrite this code file? [y/n]: ");
                string line = Console.ReadLine();
                char y_n = 'n';
                if (line.Length >= 1)
                {
                    y_n = line[0];
                }
                if (y_n == 'y')
                {
                    try
                    {
                        File.Delete(to_fn);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            string to_dir = Path.GetDirectoryName(to_fn);
            if (Directory.Exists(to_dir))
            {
                Console.WriteLine("Can not find to dir: " + to_dir);
                return false;
            }

            try
            {
                string save_filename = Path.GetFileNameWithoutExtension(m_FileName) + "_" + dateTimes[dt_index].ToString("yyyyMMddHHmmss") + m_Extension;
                string save_fn = Path.Combine(CONST_VAR.CODE_FILE_DIR, save_filename);
                FileInfo fileInfo = new FileInfo(save_fn);
                fileInfo.CopyTo(to_fn, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            return true;
        }

        public bool LoadTo(int dt_index = -1)
        {
            string to_fn = Path.Combine(Directory.GetCurrentDirectory(), m_FileName);
            return LoadTo(to_fn, dt_index);
        }

        #region prop
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

        /// <summary>
        /// Internal variable Extension:
        ///     Extension of this file
        /// </summary>
        private string m_Extension = null;

        /// <summary>
        /// Property Extension:
        ///     Extension of this file
        /// </summary>
        public string Extension
        {
            get { return GetExtension(); }
            set { SetExtension(value); }
        }

        /// <summary>
        /// Set Extension:
        ///     Extension of this file
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetExtension(string v_extension)
        {
            m_Extension = v_extension;
        }

        /// <summary>
        /// Get Extension:
        ///     Extension of this file
        /// </summary>
        private string GetExtension()
        {
            return m_Extension;
        }
        #endregion

    }

    public class CONST_VAR
    {
        /// <summary>
        /// save code file dir
        /// </summary>
        public static string CODE_FILE_DIR = @"D:\Utils\CodeFiles\Temp";

        /// <summary>
        /// number code files
        /// </summary>
        public static int N_CODE_FILES = 100;
    }
}
