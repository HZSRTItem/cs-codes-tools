/*------------------------------------------------------------------------------
 * File    : CsvRW
 * Time    : 2023/1/7 13:02:47
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[CsvRW]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QgisJYBuildWFA
{
    class CsvRW
    {
        public DataTable dt = new DataTable();

        public void ReadCSV(string filename)//从csv读取数据返回table
        {
            dt.Clear();
            System.IO.StreamReader sr = new System.IO.StreamReader(filename);
            // 记录每次读取的一行记录
            string strLine = "";
            // 记录每行记录中的各字段内容
            string[] aryLine = null;
            string[] tableHead = null;
            // 标示列数
            int columnCount = 0;
            // 标示是否是读取的第一行
            bool IsFirst = true;
            // 逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    // 创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (aryLine != null && aryLine.Length > 0)
            {
                dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            }

            sr.Close();
        }

        public void SaveCSV(string filename)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(filename);
            sw.Write(dt.Columns[0].ColumnName.ToString());
            for (int i = 1; i < dt.Columns.Count; i++) // 写入列名
            {
                sw.Write(",");
                sw.Write(dt.Columns[i].ColumnName.ToString());
            }
            sw.Write("\n");
            for (int i = 0; i < dt.Rows.Count; i++) // 写入各行数据
            {
                string data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string str = dt.Rows[i][j].ToString();
                    // 替换英文冒号 英文冒号需要换成两个冒号
                    str = str.Replace("\"", "\"\"");
                    if (str.Contains(',') || str.Contains('"') || str.Contains('\r') || str.Contains('\n'))
                    {
                        // 含逗号 冒号 换行符的需要放到引号中
                        str = string.Format("\"{0}\"", str);
                    }
                    data += str;
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            sw.Close();
        }

        public string this[int row, int column]
        {
            set { dt.Rows[row][column] = value; }
            get { return dt.Rows[row][column].ToString(); }
        }
        public string this[int row, string column_name]
        {
            set { dt.Rows[row][column_name] = value; }
            get { return dt.Rows[row][column_name].ToString(); }
        }

        public string[] GetFieldNames()
        {
            string[] out_strs = new string[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++) // 写入列名
            {
                out_strs[i] = dt.Columns[i].ColumnName.ToString();
            }
            return out_strs;
        }

        public int NRows { get { return dt.Rows.Count; } }
        public int NColumn { get { return dt.Columns.Count; } }
        public bool IsKong { get { return NRows == 0 & NColumn == 0; } }
    }
}
