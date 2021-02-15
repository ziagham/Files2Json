using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace Files2Json
{
    public class GeneratorFiles
    {
        private readonly string _path;

        public GeneratorFiles(string path)
        {
            _path = path;
        }

        public void GenerateJson()
        {
            string sourcePath = Path.GetFullPath(_path);
            //Console.WriteLine(sourcePath);

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

                var model = new FileModel(info.Name, $"~/{destPath}", File.ReadAllBytes(info.FullName));

                lock (files)
                {    
                    files.Add(model);
                }
                
            });

            string res = JsonSerializer.Serialize(files);
            File.WriteAllText(System.IO.Path.Combine(_path, "File2Json.json"), res);
        }
    }
    
}
