using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Xml;

namespace CURLCSA
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            SRTRecordRun run = new SRTRecordRun();
            await run.runAsync(args);
            Console.Read();


        }


    }
}



