using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileListMaker
{
    class Program
    {
        private static string Path;
        static void Main(string[] args)
        {
            init();
            work();
            finish();
        }

        private static void init()
        {
            if(Directory.Exists(Directory.GetCurrentDirectory() + "/output"))
            {
                Console.WriteLine("FileListMaker - by Oxizone");
                Console.WriteLine("");
                Console.WriteLine("Please enter the path");
                Path = Console.ReadLine();
            }
            else
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/output");
                init();
            }
        }

        private static void work()
        {
            var count = Directory.GetFiles(Directory.GetCurrentDirectory() + "/output").Count();
            var xmlpath = Directory.GetCurrentDirectory() + "/output/output - " + count + ".xml";

            //Get Files To Write
            var files = Directory.GetFiles(Path, "*", SearchOption.AllDirectories);
            StreamWriter writer = new StreamWriter(xmlpath);
            writer.WriteLine("<configuration>");
            
            foreach(var path in files)
            {
                var test = path.Remove(0, Path.Length);
                writer.WriteLine("<path>"+ test +"</path>");
                Console.WriteLine(test + " - Added");
            }
            writer.WriteLine("</configuration>");
            writer.Close();
        }

        private static void finish()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Process is finished - Thanks !");
            Console.WriteLine("Oxizone");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
