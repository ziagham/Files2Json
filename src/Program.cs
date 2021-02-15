using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Files2Json
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(args[0]);

            if (Directory.Exists(path)){
                var generaror = new GeneratorFiles(path);
                generaror.GenerateJson();
            }
            else
                Console.WriteLine("path doesn't exist");
            
            return;
        }
    }
}
