using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileCompareCSA
{

    class Program
    {
        public static string dirname1 = null;
        public static string dirname2 = null;
        public static string dirname3 = null;

        static void Usage()
        {
            Console.WriteLine("srt_filecmp 1 2 to");
        }

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Not get enough args.");
                Usage();
                return;
            }

            // Create two identical or different temporary folders
            // on a local drive and change these file paths.  

            dirname1 = @"D:\RemoteShadow\Dissertation";
            dirname2 = @"F:\ASDEShadow_T1\Dissertation";
            //dirname3 = @"F:\ASDEShadow_T1\Temp\Dissertation";

            dirname1 = args[0];
            dirname2 = args[1];
            if (args.Length >= 3)
            {
                dirname3 = args[2];
            }

            DirectoryInfo dir1 = new DirectoryInfo(dirname1);
            DirectoryInfo dir2 = new DirectoryInfo(dirname2);

            // Take a snapshot of the file system.  
            IEnumerable<FileInfo> list1 = dir1.GetFiles("*.*", SearchOption.AllDirectories);
            IEnumerable<FileInfo> list2 = dir2.GetFiles("*.*", SearchOption.AllDirectories);

            List<FileInfo> fileInfos1 = list1.ToList();
            List<FileInfo> fileInfos2 = list2.ToList();


            //FileCompareFuncs.fileNameEqual(list1.First(), list2.First());
            FileInfo[] sameFiles = findSameFile(fileInfos1, fileInfos2);
            FileInfo[] files1 = findFiles1(fileInfos1, fileInfos2);
            FileInfo[] files2 = findFiles2(fileInfos1, fileInfos2);
            FileInfo[] fileInfos3 = fileInfos1.ToArray();
            fileInfos1.AddRange(fileInfos2);
            FileInfo[] notSameFiles = fileInfos1.ToArray();

            for (int i = 0; i < sameFiles.Length; i++)
            {
                Console.WriteLine("{0}, {1}", "sameFiles", sameFiles[i].FullName);
            }

            for (int i = 0; i < notSameFiles.Length; i++)
            {
                Console.WriteLine("{0}, {1}", "notSameFiles", notSameFiles[i].FullName);
            }
            
            for (int i = 0; i < files1.Length; i++)
            {
                Console.WriteLine("{0}, {1}", "files1", files1[i].FullName);
            }

            for (int i = 0; i < files2.Length; i++)
            {
                Console.WriteLine("{0}, {1}", "files2", files2[i].FullName);
            }

            if (dirname3 != null)
            {
                Console.Write("Whether copy to new folder? [y/n]: ");
                char is_y = Console.ReadLine()[0];
                if (is_y == 'y')
                {
                    copyFiles(sameFiles, dirname3);
                    copyFiles(files1, dirname3);
                    copyFiles(files2, dirname3);
                    copyFiles(fileInfos3, dirname3);
                    saveToCSVFile(fileInfos3, fileInfos2.ToArray(), Path.Combine(dirname3, "filecmp.csv"));
                }
            }

            //Console.ReadKey();
        }

        static FileInfo[] findSameFile(List<FileInfo> fileInfos1, List<FileInfo> fileInfos2)
        {
            List<FileInfo> fileInfos = new List<FileInfo>(fileInfos1.Count);

            for (int i = 0; i < fileInfos1.Count; i++)
            {
                for (int j = 0; j < fileInfos2.Count; j++)
                {
                    if (FileCompareFuncs.fileEqual(fileInfos1[i], fileInfos2[j]))
                    {
                        fileInfos.Add(fileInfos1[i]);
                        fileInfos1.RemoveAt(i);
                        i--;
                        fileInfos2.RemoveAt(j);
                        break;
                    }
                }
            }

            return fileInfos.ToArray();
        }

        static FileInfo[] findFiles1(List<FileInfo> fileInfos1, List<FileInfo> fileInfos2)
        {
            List<FileInfo> fileInfos = new List<FileInfo>(fileInfos1.Count);
            bool is_find;

            for (int i = 0; i < fileInfos1.Count; i++)
            {
                is_find = true;
                for (int j = 0; j < fileInfos2.Count; j++)
                {
                    if (FileCompareFuncs.fileNameEqual(fileInfos1[i], fileInfos2[j]))
                    {
                        is_find = false;
                        break;
                    }
                }

                if (is_find)
                {
                    fileInfos.Add(fileInfos1[i]);
                    fileInfos1.RemoveAt(i);
                    i--;
                }
            }

            return fileInfos.ToArray();
        }

        static FileInfo[] findFiles2(List<FileInfo> fileInfos1, List<FileInfo> fileInfos2)
        {
            return findFiles1(fileInfos2, fileInfos1);
        }

        static void copyFiles(FileInfo[] fileInfos, string to_d)
        {
            for (int i = 0; i < fileInfos.Length; i++)
            {
                string to_f = FileCompareFuncs.fileNameto(fileInfos[i]);
                to_f = to_d + to_f;
                Console.WriteLine("{0}\n  -> {1}", fileInfos[i].FullName, to_f);
                if (!Directory.Exists(Path.GetDirectoryName(to_f)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(to_f));
                }
                fileInfos[i].CopyTo(to_f, true);
            }
        }

        static void saveToCSVFile(FileInfo[] files1, FileInfo[] files2, string to_f)
        {
            List<FileInfo> fileInfos1 = new List<FileInfo>(files1);
            List<FileInfo> fileInfos2 = new List<FileInfo>(files2);
            StreamWriter sw = new StreamWriter(to_f);
            sw.WriteLine("File1, File2");
            for (int i = 0; i < fileInfos1.Count; i++)
            {
                for (int j = 0; j < fileInfos2.Count; j++)
                {
                    if (FileCompareFuncs.fileNameEqual(fileInfos1[i], fileInfos2[j]))
                    {
                        sw.WriteLine("{0}, {1}", fileInfos1[i].FullName, fileInfos2[j].FullName);
                        fileInfos1.RemoveAt(i);
                        i--;
                        fileInfos2.RemoveAt(j);
                        break;
                    }
                }
            }
            sw.Close();
        }
    }



    // This implementation defines a very simple comparison  
    // between two FileInfo objects. It only compares the name  
    // of the files being compared and their length in bytes.  
    class FileCompare : System.Collections.Generic.IEqualityComparer<FileInfo>
    {
        public FileCompare() { }

        public bool Equals(FileInfo f1, FileInfo f2)
        {
            return (FileCompareFuncs.fileNameEqual(f1, f2) && f1.Length == f2.Length);
        }

        // Return a hash that reflects the comparison criteria. According to the
        // rules for IEqualityComparer<T>, if Equals is true, then the hash codes must  
        // also be equal. Because equality as defined here is a simple value equality, not  
        // reference identity, it is possible that two or more objects will produce the same  
        // hash code.  
        public int GetHashCode(FileInfo fi)
        {
            return FileCompareFuncs.fileNameHash(fi);
        }
    }

    // This implementation defines a very simple comparison  
    // between two FileInfo objects. It only compares the name  
    // of the files being compared and their length in bytes.  
    class FileCompare2 : System.Collections.Generic.IEqualityComparer<FileInfo>
    {
        public FileCompare2() { }

        public bool Equals(FileInfo f1, FileInfo f2)
        {
            return FileCompareFuncs.fileNameEqual(f1, f2);
        }

        // Return a hash that reflects the comparison criteria. According to the
        // rules for IEqualityComparer<T>, if Equals is true, then the hash codes must  
        // also be equal. Because equality as defined here is a simple value equality, not  
        // reference identity, it is possible that two or more objects will produce the same  
        // hash code.  
        public int GetHashCode(FileInfo fi)
        {
            return FileCompareFuncs.fileNameHash(fi);
        }
    }

    // This implementation defines a very simple comparison  
    // between two FileInfo objects. It only compares the name  
    // of the files being compared and their length in bytes.  
    class FileCompare3 : System.Collections.Generic.IEqualityComparer<FileInfo>
    {
        public FileCompare3() { }

        public bool Equals(FileInfo f1, FileInfo f2)
        {
            return (FileCompareFuncs.fileNameEqual(f1, f2) && f1.Length != f2.Length);
        }

        // Return a hash that reflects the comparison criteria. According to the
        // rules for IEqualityComparer<T>, if Equals is true, then the hash codes must  
        // also be equal. Because equality as defined here is a simple value equality, not  
        // reference identity, it is possible that two or more objects will produce the same  
        // hash code.  
        public int GetHashCode(FileInfo fi)
        {
            return FileCompareFuncs.fileNameHash(fi);
        }
    }

    class FileCompareFuncs
    {
        public static bool fileNameEqual(FileInfo f1, FileInfo f2)
        {
            string fn1 = null;
            string fn2 = null;
            if (f1.FullName.Contains(Program.dirname1))
            {
                fn1 = f1.FullName.Substring(Program.dirname1.Length);
                fn2 = f2.FullName.Substring(Program.dirname2.Length);
            }
            else
            {
                fn1 = f1.FullName.Substring(Program.dirname2.Length);
                fn2 = f2.FullName.Substring(Program.dirname1.Length);
            }

            return fn1 == fn2;
        }

        public static bool fileEqual(FileInfo f1, FileInfo f2)
        {
            return (fileNameEqual(f1, f2) && f1.Length == f2.Length);
        }

        public static int fileNameHash(FileInfo f)
        {
            string s = fileNameto(f);
            return s.GetHashCode();
        }

        public static string fileNameto(FileInfo f)
        {
            string s;
            if (f.FullName.Contains(Program.dirname1))
            {
                s = f.FullName.Substring(Program.dirname1.Length);
            }
            else
            {
                s = f.FullName.Substring(Program.dirname2.Length);
            }

            return s;
        }
    }

}
