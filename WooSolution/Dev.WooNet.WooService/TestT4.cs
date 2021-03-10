using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WooService
{
    public class TestT4
    {
        private static string folderpath = Directory.GetCurrentDirectory().Replace("Dev.WooNet.UserWebAPI", "") + @"Dev.WooNet.Model\Models"; //@"E:\WooCode\WooSolution\Dev.WooNet.Model\Models";
        public static void GetFiles()
        {
            DirectoryInfo TheFolder = new DirectoryInfo(folderpath);
            foreach (FileInfo wfile in TheFolder.GetFiles())
            {
                string fname = wfile.Name.Replace(".cs", "Service");
            }
               
        }
       
    }
}
