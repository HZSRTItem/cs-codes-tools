using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRTReadWriteCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            SRTInfoFileRW sRTInfoFileRW = new SRTInfoFileRW();
            sRTInfoFileRW.initFromFile(@"D:\SpecialProjects\SRTReadWrite\t01.srti");
            Dictionary<string, SRTInfo> d = sRTInfoFileRW.readAsDict();
            sRTInfoFileRW.saveAsDict(d);
            double t = 0;
        }
    }
}
