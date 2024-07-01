/*------------------------------------------------------------------------------
 * File    : ArgvsFmt
 * Time    : 2022/7/6 12:35:17
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[ArgvsFmt]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ShowPointWFA
{
    public class ArgvsFmt
    {
        public bool IsBuild = false;

        /// <summary>
        /// 输入文件名
        /// </summary>
        public string InFileName = null;
        /// <summary>
        /// 数据分隔符
        /// </summary>
        public char Delimiter = ',';
        /// <summary>
        /// 第一行是不是列名
        /// </summary>
        public bool IsHeader = true;
        /// <summary>
        /// 类别列名
        /// </summary>
        public string CategoryColumnName = null;
        /// <summary>
        /// 列名
        /// </summary>
        public string[] ColumnNames = null;
        /// <summary>
        /// 数据
        /// </summary>
        public List<double[]> InData = new List<double[]>();
        /// <summary>
        /// 列数
        /// </summary>
        public int NColumns = 0;
        /// <summary>
        /// 类别列
        /// </summary>
        public List<string> CatetoryColumn = new List<string>();

        public static string Usage()
        {
            string out0 = "srt_showpoint [in file] [opt: -sep=','] [opt: -c=null] [opt: --h=true]\n";
            out0 += "    in file: input file name\n";
            out0 += "    opt: -sep=',': delimiter\n";
            out0 += "    opt: -c=null: category column name\n";
            out0 += "    opt: --h=true: is the first row a column name\n";
            out0 += "(C)Copyright 2022, ZhengHan. All rights reserved.\n";
            return out0;
        }

        public ArgvsFmt(string[] argvs)
        {
            for (int i = 0; i < argvs.Length; i++)
            {
                if (argvs[i] == "-sep" & i < argvs.Length - 1)
                {
                    if (argvs[i + 1] == "\\t")
                    {
                        Delimiter = '\t';
                    }
                    else
                    {
                        Delimiter = argvs[i + 1][0];
                    }
                    i++;
                }
                else if (argvs[i] == "-c" & i < argvs.Length - 1)
                {
                    CategoryColumnName = argvs[i + 1];
                    i++;
                }
                else if (argvs[i] == "--h")
                {
                    IsHeader = false;
                }
                else if (InFileName == null)
                {
                    InFileName = argvs[i];
                }
            }

            try
            {
                StreamReader sr = new StreamReader(InFileName);
                string line = sr.ReadLine();
                int n_cateory_column = -1;
                if (IsHeader)
                {
                    ColumnNames = line.Split(Delimiter);
                    NColumns = ColumnNames.Length;
                    line = sr.ReadLine();
                    if (CategoryColumnName != null)
                    {
                        n_cateory_column = GetIndex(CategoryColumnName);
                        if (n_cateory_column == -1)
                        {
                            throw new Exception("not find category column name in column names");
                        }
                        ColumnNames = ColumnNames.Where(val => val != CategoryColumnName).ToArray();
                    }
                    NColumns = ColumnNames.Length;
                }
                while (line != null)
                {
                    if (line == "")
                    {
                        continue;
                    }
                    string[] lines = line.Split(Delimiter);
                    if (IsHeader & CategoryColumnName != null)
                    {
                        CatetoryColumn.Add(lines[n_cateory_column]);
                        lines = lines.Where((val, idx) => idx != n_cateory_column).ToArray();
                    }
                    else
                    {
                        CatetoryColumn.Add("0");
                    }
                    InData.Add(lines.Select(double.Parse).ToArray());
                    line = sr.ReadLine();
                }

                if (InData.Count == 0)
                {
                    throw new Exception("not find data in file " + InFileName);
                }
                else
                {
                    if (NColumns == 0)
                    {
                        NColumns = InData[0].Length;
                        ColumnNames = new string[NColumns];
                        for (int i = 0; i < NColumns; i++)
                        {
                            ColumnNames[i] = "Column " + (i + 1).ToString();
                        }
                    }
                }
                sr.Close();

                if (CategoryColumnName != null)
                {

                }
                IsBuild = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }

        public int GetIndex(string column_name)
        {
            for (int i = 0; i < NColumns; i++)
            {
                if (column_name == ColumnNames[i])
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
