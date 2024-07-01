/*------------------------------------------------------------------------------
 * File    : SVMDataSet
 * Time    : 2023/1/25 13:57:19
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[SVMDataSet]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SVMGeoWFA
{
    public class SVMDataSet
    {
        public DataTable DT = new DataTable();
        public string Name = "";
        public string DataFileName = "";

        public bool ReadFromSvmDataFile(string svm_data_file)
        {
            Name = Path.GetFileNameWithoutExtension(svm_data_file);
            StreamReader sr = new StreamReader(svm_data_file);
            string line = sr.ReadLine();
            DT.Columns.Add("_CATEGORY");
            while (line != null)
            {
                string[] lines = line.Split(' ');
                DataRow dr = DT.Rows.Add();
                dr["_CATEGORY"] = lines[0];
                for (int i = 1; i < lines.Length; i++)
                {
                    if (lines[i] == "")
                    {
                        break;
                    }
                    string[] datas = lines[i].Split(':');
                    int index0 = int.Parse(datas[0]);
                    double d = double.Parse(datas[1]);
                    if (index0 >= DT.Columns.Count)
                    {
                        for (int j = DT.Columns.Count; j <= index0; j++)
                        {
                            DT.Columns.Add();
                        }
                    }
                    dr[index0] = d;
                }
                for (int i = 0; i < DT.Columns.Count; i++)
                {
                    if (dr.IsNull(i))
                    {
                        dr[i] = 0;
                    }
                }
                line = sr.ReadLine();
            }
            sr.Close();
            DataFileName = svm_data_file;
            return true;
        }

        public bool ReadFromCSV(string csv_filename, bool is_header=true)
        {
            Name = Path.GetFileNameWithoutExtension(csv_filename);
            StreamReader sr = new StreamReader(csv_filename);
            string line = sr.ReadLine();
            string[] lines = line.Split(',');
            if (is_header)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    DT.Columns.Add(lines[i]);
                }
            }
            else
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    DT.Columns.Add();
                }
                DataRow dr = DT.Rows.Add();
                for (int i = 0; i < lines.Length; i++)
                {
                    dr[i] = lines[i];
                }
            }
            line = sr.ReadLine();
            while (line != null)
            {
                lines = line.Split(',');
                DataRow dr = DT.Rows.Add();
                for (int i = 0; i < DT.Columns.Count; i++)
                {
                    dr[i] = lines[i];
                }
                line = sr.ReadLine();
            }
            sr.Close();
            DataFileName = csv_filename;
            return true;
        }

        public bool SaveToSvmDataFile(string filename = "")
        {
            if (filename == "")
            {
                filename = DataFileName;
            }
            StreamWriter sw = new StreamWriter(filename);
            string c_columnname = "_CATEGORY";
            if (DT.Columns.IndexOf("_CATEGORY") <=0)
            {
                ExportToCsv_SelectCate_Form exportToCsv_SelectCate_Form = new ExportToCsv_SelectCate_Form();
                for (int i = 0; i < DT.Columns.Count; i++)
                {
                    exportToCsv_SelectCate_Form.AddItemC(DT.Columns[i].ColumnName);
                }
                exportToCsv_SelectCate_Form.ShowDialog();
                if (exportToCsv_SelectCate_Form.select_name == "")
                {
                    return false;
                }
                else
                {
                    c_columnname = exportToCsv_SelectCate_Form.select_name;
                }
            }
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataRow dr = DT.Rows[i];
                sw.Write(dr[c_columnname]);
                int ii = 0;
                for (int j = 0; j < DT.Columns.Count; j++)
                {
                    if(DT.Columns[j].ColumnName != c_columnname)
                    {
                        sw.Write(" ");
                        sw.Write(++ii);
                        sw.Write(":");
                        sw.Write(dr[j]);
                    }
                }

                sw.Write("\n");
            }
            sw.Close();
            return true;
        }

        public bool SaveToCsvFile(string filename = "")
        {
            if (filename == "")
            {
                filename = DataFileName;
            }
            StreamWriter sw = new StreamWriter(filename);
            sw.Write(DT.Columns[0].ColumnName);
            for (int i =1; i < DT.Columns.Count; i++)
            {
                sw.Write(',');
                sw.Write(DT.Columns[i].ColumnName);
            }
            sw.Write("\n");
            string c_columnname = "_CATEGORY";
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataRow dr = DT.Rows[i];
                sw.Write(dr[0]);
                for (int j = 1; j < DT.Columns.Count; j++)
                {
                    sw.Write(',');
                    sw.Write(dr[j]);
                }
                sw.Write("\n");
            }
            sw.Close();
            return true;
        }
    }

    public class SVMDataSetList
    {
        public List<SVMDataSet> DS = new List<SVMDataSet>(5);
        public int Count
        {
            get { return DS.Count; }
        }

        public SVMDataSet this[int i]
        {
            get { return DS[i]; }
        }

        public SVMDataSet this[string name]
        {
            get { return GetByName(name); }
        }

        public SVMDataSet GetByName(string name)
        {
            for (int i = 0; i < DS.Count; i++)
            {
                if (DS[i].Name == name)
                {
                    return DS[i];
                }
            }
            throw new Exception("Can not find svm data set by name:" + name);
        }

        public bool AddFromSvmDataFile(string svm_data_file)
        {
            SVMDataSet sVMDataSet = new SVMDataSet();
            sVMDataSet.ReadFromSvmDataFile(svm_data_file);
            ChangeChongName(sVMDataSet);
            DS.Add(sVMDataSet);
            return true;
        }

        public bool AddFromCSVFile(string csv_filename)
        {
            SVMDataSet sVMDataSet = new SVMDataSet();
            sVMDataSet.ReadFromCSV(csv_filename);
            ChangeChongName(sVMDataSet);
            DS.Add(sVMDataSet);
            return true;
        }

        private void ChangeChongName(SVMDataSet sVMDataSet)
        {
            for (int i = 0; i < DS.Count; i++)
            {
                if (sVMDataSet.Name == DS[i].Name)
                {
                    string[] ss = DS[i].Name.Split('_');
                    double t = 1;
                    try
                    {
                        t = double.Parse(ss[ss.Length - 1]);
                        t = t + 1;
                        sVMDataSet.Name = "";
                        for (int j = 0; j < ss.Length - 1; j++)
                        {
                            sVMDataSet.Name += ss[j];
                            sVMDataSet.Name += "_";

                        }
                        sVMDataSet.Name += t.ToString();
                    }
                    catch
                    {
                        sVMDataSet.Name = "";
                        for (int j = 0; j < ss.Length; j++)
                        {
                            sVMDataSet.Name += ss[j];
                            sVMDataSet.Name += "_";

                        }
                        sVMDataSet.Name += t.ToString();
                    }

                }
            }
        }
    }
}
