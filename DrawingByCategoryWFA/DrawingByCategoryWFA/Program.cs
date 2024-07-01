using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DrawingByCategoryWFA
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] argvs)
        {
            if (argvs.Length != 0)
            {
                if (File.Exists(argvs[0]))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    DBCForm form1 = new DBCForm(argvs[0]);
                    form1.Plot(0, 1);
                    Application.Run(form1);
                }
            }
        }
    }
}
