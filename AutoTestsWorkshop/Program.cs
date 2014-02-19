using System;
using System.IO;

namespace AutoTestsWorkshop
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Usage();
                return -1;
            }
            string searchRoot = args[0];
            if (!Directory.Exists(searchRoot))
            {
                Console.WriteLine("Path '{0}' does not exist");
                return -1;
            }

            var filesService = new RealFilesService();
            var filesFinder = new FilesFinder(filesService);
            var sameFiles = filesFinder.FindWithSameContent(searchRoot);
            foreach (var sameFilesSet in sameFiles)
            {
                foreach (string fullPath in sameFilesSet)
                {
                    Console.WriteLine(fullPath);
                }
                Console.WriteLine();
            }
            return 0;
        }

        private static void Usage()
        {
            Console.WriteLine("fsame.exe rootDir");
            Console.WriteLine("Example:");
            Console.WriteLine("fsame.exe c:\\apps");
        }
    }
}
