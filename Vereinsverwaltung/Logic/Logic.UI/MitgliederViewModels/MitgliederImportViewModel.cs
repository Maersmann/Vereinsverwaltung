using Data.Model.ImportModel;
using Data.Model.MitgliederModels;
using Data.Types;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Core.ImportHelper;
using Logic.Messages.BaseMessages;
using Logic.Messages.UtilMessages;
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
        MitgliedImportHistoryModel data;
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
                    Messenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Mitglieder werden importiert" }, "MitgliederImport");
                    await new ImportHelper().PostFile( openFileDialog.FileName ).ContinueWith(async task =>
                    {                      
                        if (task.Result.IsSuccessStatusCode)
                        {
                            data = await task.Result.Content.ReadAsAsync<MitgliedImportHistoryModel>();
                            itemList = data.Importlist;
                            this.RaisePropertyChanged("ItemList");
                        }
                        else
                        {
                            SendExceptionMessage("Import-Datei konnte nicht eingelesen werden");
                            return;
                        }
                    });
                    Messenger.Default.Send(new CloseLoadingViewMessage(), "MitgliederImport");
                }
            }         
        }

        private async void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                Messenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Mitglieder werden gespeichert" }, "MitgliederImport");
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/Import/Mitglieder/Save", data);
                Messenger.Default.Send(new CloseLoadingViewMessage(), "MitgliederImport");

                if (resp.IsSuccessStatusCode)
                {
                    itemList.Clear();
                    RaisePropertyChanged("ItemList");
                    Messenger.Default.Send(new AktualisiereViewMessage(), StammdatenTypes.mitglied);
                }
                else
                {
                    SendExceptionMessage("Mitglieder konnten nicht gespeichert werden.");
                    return;
                }
            }
        }
        #endregion

    }
}
