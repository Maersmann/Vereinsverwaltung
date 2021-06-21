using Data.Model.MitgliederModels;
using Data.Types;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Core.ImportHelper;
using Logic.Messages.BaseMessages;
using Logic.UI.BaseViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Handlers;
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
        private async void ExecuteImportCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    await new ImportHelper().PostFile( openFileDialog.FileName ).ContinueWith(async task =>
                    { 
                        if (task.Result.IsSuccessStatusCode)
                        {
                            itemList = await task.Result.Content.ReadAsAsync<ObservableCollection<MitgliederImportModel>>();
                            this.RaisePropertyChanged("ItemList");
                        }
                           
                    });
                }
            }         
        }

        private async void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp2 = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/Import/Mitglieder/Save", itemList);


                if (resp2.IsSuccessStatusCode)
                {
                    itemList.Clear();
                    this.RaisePropertyChanged("ItemList");
                    Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), StammdatenTypes.mitglied);
                }
                else
                {
                    SendExceptionMessage("Fehler Import/Save"+  Environment.NewLine + resp2.StatusCode);
                    return;
                }
            }
        }
        #endregion

    }
}
