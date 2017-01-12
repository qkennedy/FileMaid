using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileMaid.Model;
namespace FileMaid.ViewModel
{
    public class FileDetailsVM
    {
        FileDetailModel model { get; set; }
        public FileDetailsVM(string filePath)
        {
            model = new FileDetailModel(filePath);
        }
        public string txtFileTitle {
            get
            {
                return model.info.Name;
            }
        }
        public string txtFilePath
        {
            get
            {
                return model.info.FullName;
            }
        }

        public string txtFileSize
        {
            get
            {
                return model.info.Length.ToString();
            }
        }
        public string txtFileAccDate
        {
            get
            {
                return model.info.LastAccessTime.ToShortTimeString();
            }
        }

    }
}
