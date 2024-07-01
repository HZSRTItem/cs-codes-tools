/*------------------------------------------------------------------------------
 * File    :
 * Time    : 2022-11-29 19:4:12
 * Author  : Zheng Han
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    :
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.OleDb;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace TableToMdCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] tt = { "-ft", "txt", "- sep", "\t", @"D:\SpecialProjects\Yan1Course\dilixinxililun\delszy\to_mdtable.txt" };
            //args = tt;
            //Init(tt);
            if (args.Length == 0)
            {
                Console.WriteLine(Usage());
            }
            Init(args);

            Console.WriteLine("(C)Copyright 2022, ZhengHan. All rights reserved.");
        }

        static string Usage()
        {
            string line = "srt_table2md [opt:in_file] [opt:-ft csv|excel|txt] [opt:-sep default:`,`]\n" +
                "    [opt:-sheet_name]\n" +
                "    - [opt:-ft csv|excel|txt]: in_file type\n" +
                "    - [opt:-sep default:\\t]: data separator `\\space|\\t|,|...`\n" +
                "    - [opt:-sheet_name]: excel file sheet name\n";
            return line;
        }

        static bool Init(string[] args)
        {
            string f_type = null;
            char sep = ' ';
            string in_file = null;
            string sheet_name = null;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-ft" & i < args.Length - 1)
                {
                    f_type = args[i + 1];
                    i++;
                }
                else if (args[i] == "-sep" & i < args.Length - 1)
                {
                    if(args[i + 1] == "\\space")
                    {
                        sep = ' ';
                    }
                    else if (args[i + 1] == "\\t")
                    {
                        sep = '\t';
                    }
                    else
                    {
                        sep = args[i + 1][0];
                    }
                    i++;

                }
                else if (args[i] == "-sheet_name" & i < args.Length - 1)
                {
                    sheet_name = args[i + 1];
                    i++;
                }
                else
                {
                    in_file = args[i];
                }
            }
            try
            {
                if (f_type != null)
                {
                    if (in_file == null)
                    {
                        Console.WriteLine("Can not find in_file");
                        return false;
                    }

                    if (!File.Exists(in_file))
                    {
                        Console.WriteLine("Can not exist file. " + in_file);
                        return false;
                    }

                    if (f_type == "csv")
                    {
                        return FmtCsv(in_file);
                    }
                    else if (f_type == "excel")
                    {
                        return FmtExcel(in_file, sheet_name);
                    }
                    else if (f_type == "txt")
                    {
                        return FmtTxt(in_file, sep);
                    }
                    else
                    {
                        Console.WriteLine("Can not decode file type. " + f_type);
                        return false;
                    }
                }
                else
                {
                    Usage();
                    Console.WriteLine("sep = `{0}`", sep);
                    Console.WriteLine("input `--exit` to end");
                    string lines = "";
                    int i = 0;
                    while (true)
                    {
                        Console.Write("{0, 5}> ", ++i);
                        string line = Console.ReadLine();
                        if(line.Trim() == "--exit")
                        {
                            break;
                        }
                        else
                        {
                            lines += line + "\n";
                        }
                    }
                    Console.WriteLine("To markdown table: \n");
                    DataTable dt = FmtD(lines, sep);
                    dcodeDt(dt);
                    Console.WriteLine(" ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }

            return true;
        }

        static bool FmtCsv(string csv_file)
        {

            DataTable dt = CsvToDataTable(csv_file);
            dcodeDt(dt);
            return true;
        }

        static bool FmtTxt(string csv_file, char sep = ' ')
        {
            DataTable dt = FmtD(File.ReadAllText(csv_file), sep);
            dcodeDt(dt);
            return true;
        }

        static DataTable CsvToDataTable(string csv_file)
        {
            return FmtD(File.ReadAllText(csv_file));
        }

        static DataTable FmtD(string d, char sep = ',')
        {
            string[] lines = d.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
            }
            DataTable dt = new DataTable();
            string[] line_s = lines[0].Split(sep);
            int n_col = 0;
            for (int i = 0; i < line_s.Length; i++)
            {
                dt.Columns.Add("C" + i);
                ++n_col;
            }
            for (int i = 0; i < lines.Length; i++)
            {
                if(lines[i] == "")
                {
                    continue;
                }
                line_s = lines[i].Split(sep);
                dt.Rows.Add();
                for (int j = 0; j < n_col; j++)
                {
                    if (j < line_s.Length)
                    {
                        dt.Rows[i][j] = line_s[j];
                    }
                    else
                    {
                        dt.Rows[i][j] = " ";
                    }
                }
            }
            return dt;
        }

        static bool FmtExcel(string excel_file, string sheet_name)
        {
            DataTable dt = ExcelToDataTable(excel_file, sheet_name, true);
            dcodeDt(dt);
            return true;
        }

        static void dcodeDt(DataTable dt)
        {
            string line = "";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                line += "|:---:";
            }
            line += "|";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string ss = "| ";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ss += dt.Rows[i][j].ToString();
                    ss += " | ";
                }
                Console.WriteLine(ss);
                if (i == 0)
                {
                    Console.WriteLine(line);
                }
            }
        }

        static DataTable ExcelToDataTable(string excel_file, string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;

            XSSFWorkbook workbook0 = null;
            HSSFWorkbook workbook1 = null;
            FileStream fs = new FileStream(excel_file, FileMode.Open, FileAccess.Read);
            if (excel_file.IndexOf(".xlsx") > 0) // 2007版本
            {
                workbook0 = new XSSFWorkbook(fs);
                if (sheetName != null)
                {
                    sheet = workbook0.GetSheet(sheetName);
                }
                else
                {
                    sheet = workbook0.GetSheetAt(0);
                }

            }
            else if (excel_file.IndexOf(".xls") > 0) // 2003版本
            {
                workbook1 = new HSSFWorkbook(fs);
                if (sheetName != null)
                {
                    sheet = workbook1.GetSheet(sheetName);
                }
                else
                {
                    sheet = workbook1.GetSheetAt(0);
                }
            }

            if (sheet != null)
            {
                IRow firstRow = sheet.GetRow(0);
                int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                if (isFirstRowColumn)
                {
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        DataColumn column = new DataColumn(firstRow.GetCell(i).StringCellValue);
                        data.Columns.Add(column);
                    }
                    startRow = sheet.FirstRowNum + 1;
                }
                else
                {
                    startRow = sheet.FirstRowNum;
                }
                //最后一列的标号
                int rowCount = sheet.LastRowNum;
                for (int i = startRow; i <= rowCount; ++i)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue; //没有数据的行默认是null　　　　　　　

                    DataRow dataRow = data.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {
                        if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                            dataRow[j] = row.GetCell(j).ToString();
                    }
                    data.Rows.Add(dataRow);
                }
            }
            else
            {
                throw new Exception(string.Format("Can not find sheet:\"{0}\" in file:{1}", sheetName, excel_file));
            }
            return data;
        }

    }
}
