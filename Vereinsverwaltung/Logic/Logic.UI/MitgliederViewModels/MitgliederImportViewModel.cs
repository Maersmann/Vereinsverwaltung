using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vereinsverwaltung.Logic.Core.MitgliederCore;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.MitgliederViewModels
{
    public class MitgliederImportViewModel : ViewModelBasis
    {
        public MitgliederImportViewModel()
        {
            Title = "Import Mitglieder";
            ImportCommand = new RelayCommand(() => ExecuteImportCommand());
        }

        #region Bindings
       public  ICommand ImportCommand { get; set; }
        #endregion

        #region Commands
        private void ExecuteImportCommand()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                new MitgliederImport().Start(openFileDialog.FileName);
        }
        #endregion

    }
}
