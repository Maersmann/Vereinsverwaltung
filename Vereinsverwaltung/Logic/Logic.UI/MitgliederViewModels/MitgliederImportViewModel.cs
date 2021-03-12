using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Core.MitgliederCore;
using Vereinsverwaltung.Logic.Core.MitgliederCore.Models;
using Vereinsverwaltung.Logic.Messages.BaseMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.MitgliederViewModels
{
    public class MitgliederImportViewModel : ViewModelUebersicht<MitgliedImportModel>
    {
        public MitgliederImportViewModel()
        {
            Title = "Import Mitglieder";
            ImportCommand = new RelayCommand(() => ExecuteImportCommand());
            SaveCommand = new RelayCommand(() => ExecuteSaveCommand());
        }

        #region Bindings
        public  ICommand ImportCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        #endregion

        #region Commands
        private void ExecuteImportCommand()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                itemList = new ClassMitgliederImport().StartImport(openFileDialog.FileName);
                this.RaisePropertyChanged("ItemList");
            }
        }

        private void ExecuteSaveCommand()
        {
            new ClassMitgliederImport().Uebernahme(itemList);
            itemList.Clear();
            this.RaisePropertyChanged("ItemList");
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), StammdatenTypes.mitglied);
        }
        #endregion

    }
}
