using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace splitFilesToFolders
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateFiles();
            SplitFilesToFoldersFnc();
            Console.ReadKey();
        }

        private static void CreateFiles()
        {
            //foreach(var files in Directory.GetFiles(Directory.GetCurrentDirectory()))
            for(int i=1; i<1000;i++)
            {
                string filename = string.Format("文件名 {0}.jpg", i.ToString());
                File.AppendAllText(filename, "test content");
            }
        }

        private static void DeleteFiles()
        {
            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.jpg"))
            {
                if(File.Exists(file)) File.Delete(file);
            }
        }

        private static void SplitFilesToFoldersFnc()
        {
            foreach(var file in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.jpg"))
            {
                //files -> folder
                // 0-9    -> 1
                // 10-19  -> 2
                // 20-29  -> 3
                // 90-99  -> 10
                // 100-109 -> 11
                string resultString = Regex.Match(file, @"\d+").Value;
                string foldername = getFolderName(resultString);
                if (!Directory.Exists(foldername)) Directory.CreateDirectory(foldername);
                File.Copy(file, Path.Combine(Directory.GetCurrentDirectory(), foldername, Path.GetFileName(file)));
                File.Delete(file);
                Console.WriteLine(resultString+"->"+foldername);
            }
        }

        private static string getFolderName(string fileNumber)
        {
            int num = Convert.ToInt32(fileNumber);
            return (num / 10 + 1).ToString();
        }
    }
}
