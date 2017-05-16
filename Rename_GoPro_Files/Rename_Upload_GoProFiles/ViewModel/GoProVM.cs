using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;

namespace Rename_Upload_GoProFiles.ViewModel
{
    public class GoProVM : MainViewModel
    {
        #region Properties
        public List<string> FoundItems { get; set; }
        #endregion

        //Constructor
        public GoProVM()
        {

        }

        #region ButtonCommands
        public ICommand OpenFolderCommand
        {
            get { return new RelayCommand(OpenFolder); }
        }


        #endregion

        #region Methods
        private void OpenFolder()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                //We openen een folder en slaan het pad van deze folder op.
                DialogResult result = dialog.ShowDialog();
                string folderPath = dialog.SelectedPath;

                //We halen alle bestanden op uit deze map
            }
        }
        #endregion

        //public event PropertyChangedEventHandler PropertyChanged;

        //public void OnPropertyChanged(string name)
        //{
        //    if (this.PropertyChanged != null)
        //        this.PropertyChanged(this, new PropertyChangedEventArgs(name));

        //}

    }
}
