using System;
using System.Collections.Generic;

namespace Files2Json
{
    public class FileModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public byte[] FileContent { get; set; }
    }
    
}
