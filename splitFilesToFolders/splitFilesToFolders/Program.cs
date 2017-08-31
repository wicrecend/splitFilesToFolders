using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace splitFilesToFolders
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = GetMyValue();
            string filePattern = "*.jpg";
            if (!string.IsNullOrEmpty(pattern.Trim())) filePattern = pattern.Trim();
            //CreateFiles(filePattern);
            SplitFilesToFoldersFnc(filePattern);
            Console.ReadKey();
        }

        private static string GetMyValue()
        {
            string htmlCode = string.Empty;
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("http://blog.csdn.net/yangzhenping/article/category/6540363");
            }
            String regex = "#.*#";
            MatchCollection coll = Regex.Matches(htmlCode, regex);
            String result = coll[1].Groups[0].Value;
            return result.Replace("#", string.Empty);
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

        private static void DeleteFiles(string filePattern)
        {
            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory(), filePattern))
            {
                if(File.Exists(file)) File.Delete(file);
            }
        }

        private static void SplitFilesToFoldersFnc(string filePattern)
        {
            foreach(var file in Directory.GetFiles(Directory.GetCurrentDirectory(), filePattern))
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
