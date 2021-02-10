using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Files2Json
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "/home/ziagham/amin/template";
            string sourcePath = Path.GetFullPath(path);

            string[] allfiles = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);

            List<FileModel> files = new List<FileModel>();

            foreach (var file in allfiles){
                FileInfo info = new FileInfo(file);
                string filePath = Path.GetFullPath(info.Directory.FullName);
                string destPath = "";
                if (filePath.StartsWith(sourcePath, StringComparison.CurrentCultureIgnoreCase))
                {
                    destPath = filePath.Substring(sourcePath.Length).TrimStart(Path.DirectorySeparatorChar);
                }


                files.Add(new FileModel{
                    FileName = info.Name,
                    FilePath = $"~/{destPath}",
                    FileContent = File.ReadAllBytes(info.FullName)
                });
            }

            string res = JsonSerializer.Serialize(files);
            File.WriteAllText("/home/ziagham/File2Json.json", res);
            return;
        }
    }
}
