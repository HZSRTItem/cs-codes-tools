/*------------------------------------------------------------------------------
 * File    : WriteAllFile
 * Time    : 2022/4/18 16:25:45
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[WriteAllFile] 写入所有的文件信息
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace FileManagementCSA
{
    class WriteAllFileInfo
    {
        /// <summary>
        /// 输出文件数据流
        /// </summary>
        private static StreamWriter OutSW = null;
        /// <summary>
        /// 文件的数量
        /// </summary>
        private static int NFiles = 0;
        /// <summary>
        /// 文件大小
        /// </summary>
        private static double FilesSize = 0;

        /// <summary>
        /// 获得文件夹下所有文件的信息
        /// </summary>
        /// <param name="look_dir">查看的文件夹</param>
        /// <param name="out_csv_file">输出的txt文件</param>
        /// <returns>文件的数量</returns>
        public static int Fit(string look_dir, string out_excel_file)
        {
            DateTime dt = DateTime.Now;
            string out_csv_file = Path.Combine(Path.GetDirectoryName(out_excel_file), Path.GetFileNameWithoutExtension(out_excel_file) + ".csv");
            OutSW = new StreamWriter(out_csv_file, false, Encoding.UTF8);
            OutSW.WriteLine(string.Format("File Name\tCreate Time\tChanged Time\tExtension\tSize (MB)\tPath"));
            Director(look_dir);
            OutSW.Close();
            Csv2Excel(out_csv_file, out_excel_file, 6);
            File.Delete(out_csv_file);
            Console.WriteLine("\nLook Dir: " + look_dir);
            Console.WriteLine("Info File: " + out_excel_file);
            Console.WriteLine("Number of Files: " + NFiles.ToString());
            Console.WriteLine("Files Size (MB): " + FilesSize.ToString());
            OutSW = null;
            NFiles = 0;
            FilesSize = 0;
            Console.WriteLine("Using Time: " + (DateTime.Now - dt).ToString());
            return NFiles;
        }

        private static void Director(string in_dir)
        {
            Console.WriteLine("look> " + in_dir);
            FileInfo[] fsinfos;
            DirectoryInfo[] directoryInfos;

            try
            {
                DirectoryInfo d = new DirectoryInfo(in_dir);
                fsinfos = d.GetFiles();
                directoryInfos = d.GetDirectories();
            }
            catch (Exception ex)
            {
                Console.WriteLine("    " + ex.Message);
                return;
            }

            foreach (FileInfo item in fsinfos)
            {
                try
                {
                    double dd = (double)(item.Length) / 1024.0 / 1024.0;
                    string ss = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}"
                        , item.Name
                        , item.CreationTime
                        , item.LastWriteTime
                        , item.Extension
                        , dd
                        , item.DirectoryName);
                    OutSW.WriteLine(ss);
                    NFiles++;
                    FilesSize += dd;
                    Console.WriteLine("    " + item.Name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("    " + ex.Message);
                }
            }

            foreach (DirectoryInfo item in directoryInfos)
            {
                if (@"D:\$RECYCLE.BIN" != item.FullName & @"D:\System Volume Information" != item.FullName)
                {
                    Director(item.FullName);
                }
            }
        }

        private static void Csv2Excel(string out_csv_file, string out_excel_file, int n)
        {
            // 读取数据
            string[] lines = File.ReadAllLines(out_csv_file);
            object[,] objectData = new object[lines.Length, n];
            string[] tempArr;
            for (int i = 0; i < lines.Length; i++)
            {
                tempArr = lines[i].Split('\t');
                for (int k = 0; k < n; k++)
                {
                    objectData[i, k] = tempArr[k];
                }
            }
            // 初始化
            object missing = System.Reflection.Missing.Value;
            Excel.ApplicationClass app = new Excel.ApplicationClass();
            app.Application.Workbooks.Add(true);
            Excel.Workbook book = (Excel.Workbook)app.ActiveWorkbook;
            Excel.Worksheet sheet = (Excel.Worksheet)book.ActiveSheet;
            Excel.Range r = sheet.get_Range("A1");
            // 写入数据
            r = r.get_Resize(lines.Length, n);
            r.Value2 = objectData;
            r.EntireColumn.AutoFit();
            // 保存excel文件 关闭文件 退出excel
            book.SaveCopyAs(out_excel_file);
            book.Close(false, missing, missing);
            app.Quit();
        }

    }
}
