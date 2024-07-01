using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ShapeShowWFA
{
    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // NewPoint.shp
            //args = new string[1] { "NewPoint.shp" };
            //SSMainForm sSMainForm = new SSMainForm(args);
            //Application.Run(sSMainForm);

            if (IsShow(args))
                Application.Run(new SSMainForm(args));
        }

        private static bool IsShow(string[] args)
        {
            if (args.Length == 0)
            {
                return false;
            }

            if (!(Path.GetExtension(args[0]) == ".shp"))
            {
                MessageBox.Show("file is not shape file -- \n" + args[0]);
                return false;
            }

            if (!File.Exists(args[0]))
            {
                MessageBox.Show("file is not exists\n" + args[0]);
                return false;
            }

            string file_name = Path.GetFullPath(args[0]);
            string info = GetShapeInfo(file_name);
            string[] lines = info.Split('\n');
            if (lines[4].Trim() == "FAILURE:")
            {
                MessageBox.Show(info);
                return false;
            }

            // Geometry
            int i_line = 10;
            string Geometry = lines[i_line].Split(':')[1].Trim();
            if (Geometry != "Point")
            {
                MessageBox.Show("shape type should point not " + Geometry);
                return false; 
            }

            return true;
        }

        private static string GetShapeInfo(string shp_file)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    // 是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true; // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true; // 由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true; // 重定向标准错误输出
            p.StartInfo.CreateNoWindow = true; // 不显示程序窗口
            p.Start(); // 启动程序
            string in_str = "ogrinfo -al " + shp_file;
            p.StandardInput.WriteLine(in_str + " &exit"); // 向cmd窗口发送输入信息
            p.StandardInput.AutoFlush = true;
            string output = p.StandardOutput.ReadToEnd(); // 获取cmd窗口的输出信息
            p.WaitForExit(); // 等待程序执行完退出进程
            p.Close();
            return output;
        }

    }
}
