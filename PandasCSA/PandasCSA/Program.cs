using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PandasNet;

namespace PandasCSA
{
    class Program
    {
        static void Main(string[] args)
        {
            string csv_fn = @"";
            Pandas pd = new Pandas();
            DataFrame dataFrame = pd.read_csv(csv_fn);
        }
    }
}
