using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace FileMaid.Model
{
    public class FileDetailModel
    {
        public string filePath;
        public int timeGroup { get; set; }
        public FileMetaDataModel meta { get; set; }
        public FileInfo info
        {
            get
            {
                if (!File.Exists(filePath))
                {
                    //return FNF exception?
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
        public void moveFile(string path)
        {
            path = path + info.Name;
            info.MoveTo(path);
        }
        private string _checkSum;
        public string checkSum
        {
            get
            {
                if (_checkSum == "")
                {
                    calcCheckSum();
                }
                return _checkSum;
            }
            set
            {
                _checkSum = value;
            }
        }
        public void calcCheckSum()
        {
            using(MD5 md5 = MD5.Create())
            {
                using (var stream = new BufferedStream(File.OpenRead(filePath), 1200000))
                {
                    byte[] checksum = md5.ComputeHash(stream);
                    checkSum = BitConverter.ToString(checksum).Replace("-", String.Empty);
                }
            }
            
        }

    }
}
