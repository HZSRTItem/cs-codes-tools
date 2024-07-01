using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteratureReadingCSA
{
    class Program
    {
        static void Main(string[] args)
        {

            LiteratureRun literatureRun = new LiteratureRun();
            literatureRun.Run(args);
            //literatureCollection.readFromMarkDown(@"D:\code\cs\LiteratureReadingCSA\LiteratureReadingCSA\bin\Debug\test3.md");
            //literatureCollection.saveToMarkDown("test3_2.md");
            //literatureCollection.saveToExcel("test3_3.xlsx");
            //Console.Read();
        }

    }
}
