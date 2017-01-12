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
    public class MainVM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<FileDetailsVM> details { get; set; }

       

        public MainVM()
        {
            this.rootFolder = "C:/TestFolder";
            this.details = new ObservableCollection<FileDetailsVM>();
        }

       
        /// <summary>
        /// This is the folder currently chosen as root in MainWindow
        /// </summary>
        private string _rootFolder { get; set; }
        public string rootFolder {
          
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

        #region ICommands
        private bool _canreadRoot { get; set; } = true;
        private ICommand _readRootCommand;
        public ICommand readRootCommand
        {
            get
            {
                return _readRootCommand ?? (_readRootCommand = new CommandHandler(() => ReadRoot(), _canreadRoot));
            }
        }
        #endregion

        #region Stuff that should be in Model
        private void ReadRoot()
        {
            ReadTopFiles(rootFolder);
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
        #endregion


    }
}
