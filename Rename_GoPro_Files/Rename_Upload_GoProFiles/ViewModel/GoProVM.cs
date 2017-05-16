using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using ClassLibrary_File_Interactions;
using System.IO;
using System.Threading;

namespace Rename_Upload_GoProFiles.ViewModel
{
    public class GoProVM : MainViewModel
    {
        #region Properties
        //public List<FileInfo> FoundItems { get; set; }
        private List<FileInfo> founditems;
        public List<FileInfo> FoundItems
        {
            get { return founditems; }
            set { founditems = value; RaisePropertyChanged("FoundItems"); }
        }


        private int numberofconverted;
        public int NumberOfConverted
        {
            get { return numberofconverted; }
            set { numberofconverted = value; RaisePropertyChanged("NumberofConverted"); }
        }

        private string messageleftcolumn;
        public string MessageLeftColumn
        {
            get { return messageleftcolumn; }
            set { messageleftcolumn = value; RaisePropertyChanged("MessageLeftColumn"); }
        }

        private GoPro_File_Interactions GPFI;
        private string FolderPath;



        #endregion

        //Constructor
        public GoProVM()
        {
            NumberOfConverted = 0;
            MessageLeftColumn = "";
            FolderPath = "";
            GPFI = new GoPro_File_Interactions();
        }

        #region ButtonCommands
        public ICommand OpenFolderCommand
        {
            get { return new RelayCommand(OpenFolder); }
        }

        public ICommand RenameFilesCommand
        {
            get { return new RelayCommand(RenameFiles); }
        }


        #endregion

        #region Methods
        //Open folder and get files
        private void OpenFolder()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                //We openen een folder en slaan het pad van deze folder op.
                DialogResult result = dialog.ShowDialog();
                FolderPath = dialog.SelectedPath;

                //We halen alle bestanden op uit deze map
                FoundItems = GPFI.GetGoProFilesFromFolder(FolderPath);//We tonen de gevonden bestanden in de ListView
            }
        }

        //De buttonclick opvangen en multithreading opstarten
        private void RenameFiles()
        {
            ThreadStart childref = new ThreadStart(RenameFilesMultiThreading);
            Thread childThread = new Thread(childref);
            childThread.Start();
        }

        //Mbv multithreading de progressbar opvullen.
        private void RenameFilesMultiThreading()
        {
            foreach(FileInfo file in FoundItems)
            {
                //Check if file was already renamed, clean up name before sending it back
                string name = GPFI.CheckAlreadyRenamed(file);

                NumberOfConverted++;

                GPFI.ChangeName(file, NumberOfConverted, name);
                //File.Move(file.FullName, FolderPath + "/Part" + NumberOfConverted + "_" + name);//Add File_number to each file in the right order.
                
                MessageLeftColumn = "Converted file " + NumberOfConverted;
            }

            MessageLeftColumn = "Done converting";
        }
        #endregion
    }
}
