using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using FileMaid.Model;

namespace FileMaid.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<FileDetailsVM> details { get; set; }
        public ObservableCollection<FileDetailsVM> selectedFiles { get; set; }
        public List<FileDetailsVM> excludedFiles { get; set; } = new List<FileDetailsVM>();
        /// <summary>
        /// This is the folder currently chosen as root in MainWindow
        /// </summary>
        private string _rootFolder { get; set; }
        public string rootFolder
        {

            set
            {
                if (_rootFolder != value)
                {
                    _rootFolder = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("rootFolder"));
                    }
                }
            }
            get
            {
                return _rootFolder;
            }
        }

        private string _keyword { get; set; } = "";
        public string keyword
        {

            set
            {
                if (_keyword != value)
                {
                    _keyword = value;
                    updateSelFiles();
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("keyword"));
                    }
                }
            }
            get
            {
                return _keyword;
            }
        }

        private string _fileExt { get; set; } = "";
        public string fileExt
        {

            set
            {
                if (_fileExt != value)
                {
                    _fileExt = value;
                    updateSelFiles();
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("fileExt"));
                    }
                }
            }
            get
            {
                return _fileExt;
            }
        }

        private string _newFolderTitle { get; set; } = "";
        public string newFolderTitle
        {

            set
            {
                if (_newFolderTitle != value)
                {
                    _newFolderTitle = value;
                    updateSelFiles();
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("newFolderTitle"));
                    }
                }
            }
            get
            {
                return _newFolderTitle;
            }
        }


        private DateTime _startDate { get; set; } = DateTime.Now;
        public DateTime startDate
        {

            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    updateSelFiles();
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("startDate"));
                    }
                }
            }
            get
            {
                return _startDate;
            }
        }
        private DateTime _endDate { get; set; } = DateTime.Now;
        public DateTime endDate
        {

            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    updateSelFiles();
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("endDate"));
                    }
                }
            }
            get
            {
                return _endDate;
            }
        }
        private bool _delDuplicates{ get; set; } = false;
        public bool delDuplicates
        {

            set
            {
                if (_delDuplicates != value)
                {
                    _delDuplicates = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("delDuplicates"));
                    }
                }
            }
            get
            {
                return _delDuplicates;
            }
        }

        public MainVM()
        {
            this.rootFolder = "C:/TestFolder";
            this.details = new ObservableCollection<FileDetailsVM>();
            this.selectedFiles = new ObservableCollection<FileDetailsVM>();
        }




        #region ICommands
        private bool _canreadRoot { get; set; } = true;
        private ICommand _readRootCommand;
        public ICommand readRootCommand
        {
            get
            {
                return _readRootCommand ?? (_readRootCommand = new CommandHandler(() => readRoot(), _canreadRoot));
            }
        }

        private bool _canMoveFiles
        {
            get
            {
                return true;
            }
        }
        private ICommand _moveFilesCommand;
        public ICommand moveFilesCommand
        {
            get
            {
                return _moveFilesCommand ?? (_moveFilesCommand = new CommandHandler(() => moveFiles(), _canMoveFiles));
            }
        }
        #endregion

        #region Stuff that should be in Model
        private void readRoot()
        {
            ReadTopFiles(rootFolder);
        }
        private void moveFiles()
        {
            string newPath = rootFolder + "/" + newFolderTitle + "/";
            Directory.CreateDirectory(newPath);
            foreach(FileDetailsVM f in selectedFiles)
            {
                f.model.calcCheckSum();
            }
            if (delDuplicates)
            {
                deleteDuplicates();
            }
            
            foreach (FileDetailsVM f in selectedFiles)
            {
                if (!excludedFiles.Contains(f))
                {
                    f.model.moveFile(newPath);
                }
            }
            selectedFiles.Clear();
            excludedFiles.Clear();
            readRoot();
        }
        private void deleteDuplicates()
        {
            List<FileDetailsVM> tbd = new List<FileDetailsVM>();
            foreach (FileDetailsVM f in selectedFiles)
            {
                Console.Out.WriteLine(f.txtFileTitle + ", " + f.model.checkSum);
                if (!tbd.Contains(f))
                {
                    var query = selectedFiles.Where(x => x.model.checkSum == f.model.checkSum);
                    foreach (FileDetailsVM g in query)
                    {
                        if (g != f)
                        {
                            tbd.Add(g);
                        }
                        
                    }
                }
            }
            foreach (FileDetailsVM f in tbd)
            {
                selectedFiles.Remove(f);
                f.model.info.Delete();
            }
        }
        private void ReadTopFiles(string root)
        {
            List<string> list = DirectoryParser.getTopFiles(root);
            foreach (string s in list)
            {
                if (File.Exists(s))
                {
                    details.Add(new FileDetailsVM(s));
                }
            }
        }
        private void updateSelFiles()
        {
            List<FileDetailsVM> list1, list2 = new List<FileDetailsVM>();
            list1 = selectByKeyword();
            list2 = selectByExt();
            selectedFiles.Clear();
            var tmp = list1.Intersect(list2);
            foreach(FileDetailsVM f in tmp)
            {
                selectedFiles.Add(f);
            }
        }
        /// <summary>
        /// Going to have this be a LINQ select that changes selected files, should eventually move the full list to a model,
        /// have the observable collection be changeable by this 
        /// </summary>
        /// <param name="keyword"> This is the key word to search for</param>
        private List<FileDetailsVM> selectByKeyword()
        {
            var query = details.Where(x => x.txtFileTitle.Contains(keyword));
            List<FileDetailsVM> list = new List<FileDetailsVM>();
            foreach(FileDetailsVM f in query)
            {
                list.Add(f);
            }
            return list;
        }
        private List<FileDetailsVM> selectByExt()
        {
            var query = details.Where(x => x.txtFileExt.Contains(fileExt));
            List<FileDetailsVM> list = new List<FileDetailsVM>();
            foreach (FileDetailsVM f in query)
            {
                list.Add(f);
            }
            return list;
        }
        private List<FileDetailsVM> selectByDate()
        {
            var query = details.Where(x => IsBetween(x.model.info.LastAccessTime));
            List<FileDetailsVM> list = new List<FileDetailsVM>();
            foreach (FileDetailsVM f in query)
            {
                list.Add(f);
            }
            return list;
        }
        private bool IsBetween(DateTime date)
        {
            return date.CompareTo(endDate) <= 0 && date.CompareTo(startDate) >= 0;
        }
        #endregion
        public void ToggleSelected(FileDetailsVM det)
        {
            if (excludedFiles.Contains(det))
            {
                excludedFiles.Remove(det);
            }
            else
            {
                excludedFiles.Add(det);
            }
        }

    }
}
