using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.ViewModels;
using Data.Model.ImportModel;
using Data.Model.MitgliederModels;
using Data.Model.SchnurrschiessenModels;
using Data.Types;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.ImportHelper;
using Logic.Messages.UtilMessages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurschiessenMitgliederImportViewModel : ViewModelLoadListData<SchnurschiessenMitgliederImportModel>
    {
        SchnurschiessenImportHistoryModel data;
         public SchnurschiessenMitgliederImportViewModel()
        {
            Title = "Import Mitglieder";
            ImportCommand = new RelayCommand(() => ExecuteImportCommand());
            SaveCommand = new RelayCommand(() => ExecuteSaveCommand());
        }

        #region Bindings
        public ICommand ImportCommand { get; set; }
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
                    bool erfolgreich = false;
                    Messenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Mitglieder werden importiert" }, "SchnurschiessenMitgliederImport");
                    await new ImportHelper().PostSchnurschiessenFile(openFileDialog.FileName).ContinueWith(async task =>
                    {
                        if (task.Result.IsSuccessStatusCode)
                        {
                            data = await task.Result.Content.ReadAsAsync<SchnurschiessenImportHistoryModel>();
                            Response.Data = data.Importlist;
                            erfolgreich = true;
                            RaisePropertyChanged("ItemList");
                        }
                        else
                        {
                            
                            return;
                        }
                    });
                    Messenger.Default.Send(new CloseLoadingViewMessage(), "SchnurschiessenMitgliederImport");

                    if (!erfolgreich)
                    {   
                        SendExceptionMessage("Import-Datei konnte nicht eingelesen werden");
                    }
                }
            }
        }

        private async void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                Messenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Mitglieder werden gespeichert." }, "SchnurschiessenMitgliederImport");
                HttpResponseMessage resp = null;
                try
                {
                     resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Import/schnurschiessen/Save", data);

                }catch (Exception ex) { }
                Messenger.Default.Send(new CloseLoadingViewMessage(), "SchnurschiessenMitgliederImport");

                if (resp.IsSuccessStatusCode)
                {
                    Response.Data.Clear();
                    RaisePropertyChanged("ItemList");
                    Messenger.Default.Send(new AktualisiereViewMessage(), StammdatenTypes.schnurschiessen.ToString());
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
