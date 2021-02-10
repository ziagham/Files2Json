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
            Console.Write("Please enter a path: ");
            string path = Console.ReadLine();

            string sourcePath = Path.GetFullPath(path);

            string[] allfiles = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);

            List<FileModel> files = new List<FileModel>();

            Parallel.ForEach(allfiles, (file) =>
            {
                FileInfo info = new FileInfo(file);
                string filePath = Path.GetFullPath(info.Directory.FullName);
                string destPath = "";
                if (filePath.StartsWith(sourcePath, StringComparison.CurrentCultureIgnoreCase))
                {
                    destPath = filePath.Substring(sourcePath.Length).TrimStart(Path.DirectorySeparatorChar);
                }

                var model = new FileModel{
                    FileName = info.Name,
                    FilePath = $"~/{destPath}",
                    FileContent = File.ReadAllBytes(info.FullName)
                };

                lock (files)
                {    
                    files.Add(model);
                }

            });

            string res = JsonSerializer.Serialize(files);
            File.WriteAllText(System.IO.Path.Combine(path, "File2Json.json"), res);
            return;
        }
    }
}
