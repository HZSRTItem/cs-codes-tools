/*------------------------------------------------------------------------------
 * File    : HZRecord
 * Time    : 2023/3/10 20:40:51
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2023, ZhengHan. All rights reserved.
 * Desc    : class[HZRecord]
------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SRTFmtArg;

namespace HZRecordCSA
{
    public class HZRecordManager
    {
        HZRecordCollection recordCollection = null;
        SRTArgCollection argCollection = null;


        public HZRecordManager()
        {
            InitSRTArgs();

            recordCollection = new HZRecordCollection();
            recordCollection.Read("test1.xml");
            Console.WriteLine("Init:\n");
            recordCollection.Show();

        }

        public void Test()
        {
            recordCollection.Save("test1.xml");
        }

        private void InitSRTArgs()
        {
            argCollection = new SRTArgCollection();

            argCollection.Name = "srt_hzr";
            argCollection.Description = "Record manager for every time";

            argCollection.Add("add", help_info: "add a record to collection");
            argCollection.Add("delete", help_info: "delete a record in collection");
            argCollection.Add("show", help_info: "show a record from collection");
            argCollection.Add("help", help_info: "show a record from collection", arg_type:SRTArgType.Bool);
        }

        public void Run(string[] args)
        {
            if (args.Length == 0)
            {
                Usage();
                return;
            }

            if (args[0] == "--h")
            {
                Usage();
            }
            else if (args[0] == "add")
            {
                Add(args);
            }
            else if (args[0] == "delete")
            {
                Delete(args);
            }
            else if (args[0] == "show")
            {
                Show(args);
            }
        }

        public void Add(string[] args)
        {
            SRTArgCollection add_args_collection = new SRTArgCollection();
            add_args_collection.Add("timeid");
        }

        public void Delete(string[] args)
        {

        }

        public void Show(string[] args)
        {

        }

        private void Usage()
        {
            Console.WriteLine(argCollection.Usage());
        }

    }

    public class HZRecordCollection
    {
        List<HZRecord> records = null;
        XmlSerializer ser = new XmlSerializer(typeof(List<HZRecord>));

        public HZRecordCollection()
        {
            records = new List<HZRecord>(9);
            records.Add(new HZRecord() { TimeID = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), RecordMarkDown = "record test 1" });
            records.Add(new HZRecord() { TimeID = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), RecordMarkDown = "record test 1" });
            records.Add(new HZRecord() { TimeID = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), RecordMarkDown = "record test 1" });
            records.Add(new HZRecord() { TimeID = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), RecordMarkDown = "record test 1" });
            records.Add(new HZRecord() { TimeID = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), RecordMarkDown = "record test 1" });
        }

        public void Show()
        {
            for (int i = 0; i < records.Count; i++)
            {
                Console.WriteLine(records[i].ToString());
                Console.WriteLine();
            }
        }

        public void Save(string save_fn)
        {
            StreamWriter sw = new StreamWriter(save_fn);
            ser.Serialize(sw, records);
            sw.Close();
        }

        public void Read(string read_fn)
        {
            StreamReader sr = new StreamReader(read_fn);
            List<HZRecord> records_r = (List<HZRecord>)ser.Deserialize(sr);
            sr.Close();
            if (records == null)
            {
                records = new List<HZRecord>(records_r.Count + 6);
            }
            records.AddRange(records_r);
        }

    }

    public class HZRecord
    {
        /// <summary>
        /// Internal variable TimeID:
        ///     everyone record for input time
        /// </summary>
        private string m_TimeID = null;

        /// <summary>
        /// Property TimeID:
        ///     everyone record for input time
        /// </summary>
        public string TimeID
        {
            get { return GetTimeID(); }
            set { SetTimeID(value); }
        }

        /// <summary>
        /// Set TimeID:
        ///     everyone record for input time
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetTimeID(string v_timeid)
        {
            m_TimeID = v_timeid;
        }

        /// <summary>
        /// Get TimeID:
        ///     everyone record for input time
        /// </summary>
        private string GetTimeID()
        {
            return m_TimeID;
        }

        /// <summary>
        /// Internal variable RecordMarkDown:
        ///     record of markdown format
        /// </summary>
        private string m_RecordMarkDown = null;

        /// <summary>
        /// Property RecordMarkDown:
        ///     record of markdown format
        /// </summary>
        public string RecordMarkDown
        {
            get { return GetRecordMarkDown(); }
            set { SetRecordMarkDown(value); }
        }

        /// <summary>
        /// Set RecordMarkDown:
        ///     record of markdown format
        /// </summary>
        /// <param name="v_count">external incoming variable</param>
        private void SetRecordMarkDown(string v_recordmarkdown)
        {
            m_RecordMarkDown = v_recordmarkdown;
        }

        /// <summary>
        /// Get RecordMarkDown:
        ///     record of markdown format
        /// </summary>
        private string GetRecordMarkDown()
        {
            return m_RecordMarkDown;
        }

        public override string ToString()
        {
            string record_str = m_RecordMarkDown;
            if (record_str.Contains("\n"))
            {
                record_str = "\n" + record_str;
            }
            string line = string.Format(
                "TimeID: {0}\n" +
                "Record: {1}"
                , m_TimeID, record_str);
            return line;
        }
    }
}
