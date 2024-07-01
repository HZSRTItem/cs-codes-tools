/*------------------------------------------------------------------------------
 * File    : BackFile
 * Time    : 2022/10/10 18:41:13
 * Author  : Zheng Han 
 * Contact : hzsongrentou1580@gmail.com
 * License : (C)Copyright 2022, ZhengHan. All rights reserved.
 * Desc    : class[BackFile]
 * 
 * 
 * (base) C:\Users\ASUS>conda install numpy
Collecting package metadata (current_repodata.json): done
Solving environment: done



The
Out[2]: 

## Package Plan ##

  environment location: E:\miniconda3

  added / updated specs:
    - numpy


The following packages will be downloaded:

    package                    |            build
    ---------------------------|-----------------
    certifi-2022.9.24          |   py39haa95532_0         154 KB  https://mirrors.tuna.tsinghua.edu.cn/anaconda/pkgs/main
    conda-22.9.0               |   py39haa95532_0         888 KB  https://mirrors.tuna.tsinghua.edu.cn/anaconda/pkgs/main
    ------------------------------------------------------------
                                           Total:         1.0 MB

The following NEW packages will be INSTALLED:

  blas               anaconda/pkgs/main/win-64::blas-1.0-mkl
  intel-openmp       anaconda/pkgs/main/win-64::intel-openmp-2021.4.0-haa95532_3556
  mkl                anaconda/pkgs/main/win-64::mkl-2021.4.0-haa95532_640
  mkl-service        anaconda/pkgs/main/win-64::mkl-service-2.4.0-py39h2bbff1b_0
  mkl_fft            anaconda/pkgs/main/win-64::mkl_fft-1.3.1-py39h277e83a_0
  mkl_random         anaconda/pkgs/main/win-64::mkl_random-1.2.2-py39hf11a4ad_0
  numpy              anaconda/pkgs/main/win-64::numpy-1.23.1-py39h7a0a035_0
  numpy-base         anaconda/pkgs/main/win-64::numpy-base-1.23.1-py39hca35cd5_0

The following packages will be UPDATED:

  certifi                          2022.9.14-py39haa95532_0 --> 2022.9.24-py39haa95532_0
  conda                               4.14.0-py39haa95532_0 --> 22.9.0-py39haa95532_0


Proceed ([y]/n)?

------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace BackFileCSA
{
    class SRTBackFile
    {
        /// <summary>
        /// 备份的文件的位置
        /// </summary>
        private string BackDir = null;

        public SRTBackFile()
        {
            BackDir = Directory.GetCurrentDirectory();
            BackDir = Path.Combine(BackDir, "back");
        }

        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="file_name">文件名</param>
        /// <param name="b_y">是否询问</param>
        /// <returns>备份是否成功</returns>
        public bool Back(string file_name, bool b_y = true, string back_dir = null)
        {
            if (back_dir == null)
            {
                back_dir = BackDir;
            }

            if (!Directory.Exists(back_dir))
            {
                Console.WriteLine("Back folder does not exist:");
                Console.WriteLine("  - " + BackDir);
                if (!UUtils.IsYN("\nWhether to create back folder?"))
                {
                    return false;
                }
                try
                {
                    Directory.CreateDirectory(back_dir);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }

            // 检查文件是否存在
            if (!File.Exists(file_name))
            {
                Console.WriteLine("The following file was not found:");
                Console.WriteLine("  - " + file_name);
                return false;
            }

            string back_file_name = UUtils.FileNameAddSuffix(file_name, "_" + DateTime.Now.ToString("yyMMddHHmmss"));
            back_file_name = Path.Combine(back_dir, Path.GetFileName(back_file_name));

            // 备份写入的文件检查是否存在
            if (File.Exists(back_file_name))
            {
                Console.WriteLine("Back file already exists:");
                Console.WriteLine("  - " + back_file_name);
                if (!UUtils.IsYN("\nWhether to overwrite back file?"))
                {
                    return false;
                }
            }

            FileInfo fileInfo = new FileInfo(file_name);

            if (b_y)
            {
                fileInfo.CopyTo(back_file_name, true);
                Console.WriteLine("  - " + back_file_name);
                return true;
            }

            Console.WriteLine("The following file will be backed up:");
            Console.WriteLine("  ** " + file_name);
            Console.WriteLine("  -> " + back_file_name);
            Console.WriteLine("  Name: {0}", fileInfo.Name);
            Console.WriteLine("  Time: {0}", fileInfo.LastWriteTime.ToString("u"));
            Console.WriteLine("  Type: {0}", fileInfo.Extension);
            Console.WriteLine("  Size: {0}", fileInfo.Length * 1.0 / 1024 / 1024);

            if (!UUtils.IsYN("\nProcess?"))
            {
                return false;
            }

            fileInfo.CopyTo(back_file_name, true);
            return true;
        }
    }
}
