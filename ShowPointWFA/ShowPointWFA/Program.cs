using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowPointWFA
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] argvs)
        {
            //argvs = new string[1]
            //{
            //    @"D:\code\cs\ShowPointWFA\ShowPointWFA\bin\Debug\t01.csv"
            //    //, "-c", "label"
            //    //, "-sep", "\\t"
            //};
            if (argvs.Length == 0)
            {
                MessageBox.Show(ArgvsFmt.Usage());
                return;
            }
            ArgvsFmt argvsFmt = new ArgvsFmt(argvs);
            if (argvsFmt.IsBuild)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(argvsFmt));
            }
        }
    }
}
