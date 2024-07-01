/*------------------------------------------------------------------------------
 * File    : SRTNPOI
 * Time    : 2023/8/3 20:59:15
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[SRTNPOI]
------------------------------------------------------------------------------*/

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteratureReadingCSA
{
    public class SRTNPOI
    {
        /// <summary>
        /// 将DataTable(DataSet)导出到Execl文档
        /// </summary>
        /// <param name="dataSet">传入一个DataSet</param>
        /// <param name="Outpath">导出路径（可以不加扩展名，不加默认为.xls）</param>
        /// <returns>返回一个Bool类型的值，表示是否导出成功</returns>
        /// True表示导出成功，Flase表示导出失败
        public static bool DataSetToExcel(DataSet dataSet, string Outpath)
        {
            bool result = false;
            try
            {
                if (dataSet == null || dataSet.Tables == null || dataSet.Tables.Count == 0 || string.IsNullOrEmpty(Outpath))
                    throw new Exception("输入的DataSet或路径异常");
                int sheetIndex = 0;
                //根据输出路径的扩展名判断workbook的实例类型
                IWorkbook workbook = null;
                string pathExtensionName = Outpath.Trim().Substring(Outpath.Length - 5);
                if (pathExtensionName.Contains(".xlsx"))
                {
                    workbook = new XSSFWorkbook();
                }
                else if (pathExtensionName.Contains(".xls"))
                {
                    workbook = new HSSFWorkbook();
                }
                else
                {
                    Outpath = Outpath.Trim() + ".xls";
                    workbook = new HSSFWorkbook();
                }
                //将DataSet导出为Excel
                foreach (DataTable dt in dataSet.Tables)
                {
                    sheetIndex++;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ISheet sheet = workbook.CreateSheet(string.IsNullOrEmpty(dt.TableName) ? ("sheet" + sheetIndex) : dt.TableName);//创建一个名称为Sheet0的表
                        int rowCount = dt.Rows.Count;//行数
                        int columnCount = dt.Columns.Count;//列数

                        //设置列头
                        IRow row = sheet.CreateRow(0);//excel第一行设为列头
                        for (int c = 0; c < columnCount; c++)
                        {
                            ICell cell = row.CreateCell(c);
                            cell.SetCellValue(dt.Columns[c].ColumnName);
                        }

                        //设置每行每列的单元格,
                        for (int i = 0; i < rowCount; i++)
                        {
                            row = sheet.CreateRow(i + 1);
                            for (int j = 0; j < columnCount; j++)
                            {
                                ICell cell = row.CreateCell(j);//excel第二行开始写入数据
                                cell.SetCellValue(dt.Rows[i][j].ToString());
                            }
                        }
                    }
                }
                //向outPath输出数据
                using (FileStream fs = File.OpenWrite(Outpath))
                {
                    workbook.Write(fs);//向打开的这个xls文件中写入数据
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool DataTableToExcel(DataTable dt, string excel_fn)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            int rowCount = dt.Rows.Count; 
            int columnCount = dt.Columns.Count;

            IRow row = sheet.CreateRow(0); 
            for (int c = 0; c < columnCount; c++)
            {
                ICell cell = row.CreateCell(c);
                cell.SetCellValue(dt.Columns[c].ColumnName);
            }

            for (int i = 0; i < rowCount; i++)
            {
                row = sheet.CreateRow(i + 1);
                for (int j = 0; j < columnCount; j++)
                {
                    ICell cell = row.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            using (FileStream fs = File.OpenWrite(excel_fn))
            {
                workbook.Write(fs);
            }

            return true;
        }
    }


}

