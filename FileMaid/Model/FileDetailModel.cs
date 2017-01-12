using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace FileMaid.Model
{
    public class FileDetailModel
    {
        public string filePath;
        public FileMetaDataModel meta { get; set; }
        public FileInfo info
        {
            get
            {
                if (!File.Exists(filePath))
                {
                    //return FNF exception
                    return null;
                }
                else
                {
                    return new FileInfo(filePath);
                }
            }
        }
        public FileDetailModel(string path)
        {
            filePath = path;
            ReadFileMetaData(path);
        }
        private void ReadFileMetaData(string name)
        {
            FileMetaDataModel mod = new FileMetaDataModel(name);
        }

    }
}
