using System;
using System.Collections.Generic;

namespace Files2Json
{
    public class FileModel
    {
        public FileModel(string fileName, string filePath, byte[] fileContent)
        {
            FileName = fileName;
            FilePath = filePath;
            FileContent = fileContent;
        }

        public string FileName { get; }
        public string FilePath { get; }
        public byte[] FileContent { get; }
    }
    
}
