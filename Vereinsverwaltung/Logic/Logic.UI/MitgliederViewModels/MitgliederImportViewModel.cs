using Data.Model.MitgliederModels;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.UI.BaseViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.MitgliederViewModels
{
    public class MitgliederImportViewModel : ViewModelUebersicht<MitgliederImportModel>
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
            // Todo: Request
            /*
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                itemList = new ClassMitgliederImport(history).StartImport(openFileDialog.FileName);
                this.RaisePropertyChanged("ItemList");
            }
            */
        }

        private void ExecuteSaveCommand()
        {
            //Todo: Request
            /*
            new ClassMitgliederImport(history).Uebernahme(itemList);   
            itemList.Clear();
            this.RaisePropertyChanged("ItemList");
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), StammdatenTypes.mitglied);
            */
        }
        #endregion

    }
}
