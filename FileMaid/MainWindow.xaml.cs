using System.Windows;
using FileMaid.ViewModel;
using static System.Environment;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FileMaid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnFileOpen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (System.IO.Directory.Exists(txtFile.Text))
            {
                dialog.InitialDirectory = txtFile.Text;
            }
            var result = dialog.ShowDialog();
            switch (result)
            {
                case CommonFileDialogResult.Ok:
                    var file = dialog.FileName;
                    txtFile.Text = file;
                    txtFile.ToolTip = file;
                    var tmp = (MainVM)this.DataContext;
                    tmp.readRootCommand.Execute(this);

                    break;
                case CommonFileDialogResult.Cancel:
                default:
                    txtFile.Text = null;
                    txtFile.ToolTip = null;
                    break;
            }

        }
    }
}
