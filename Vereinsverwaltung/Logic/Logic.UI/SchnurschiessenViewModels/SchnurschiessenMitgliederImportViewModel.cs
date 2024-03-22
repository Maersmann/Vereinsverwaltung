using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.ViewModels;
using Data.Model.ImportModel;
using Data.Model.MitgliederModels;
using Data.Model.SchnurrschiessenModels;
using Data.Types;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Core.ImportHelper;
using Logic.Messages.UtilMessages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Prism.Commands;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurschiessenMitgliederImportViewModel : ViewModelLoadListData<SchnurschiessenMitgliederImportModel>
    {
        SchnurschiessenImportHistoryModel data;
         public SchnurschiessenMitgliederImportViewModel()
        {
            Title = "Import Mitglieder";
            ImportCommand = new DelegateCommand(ExecuteImportCommand, CanExecuteCommand);
            SaveCommand = new DelegateCommand(ExecuteSaveCommand, CanExecuteCommand);
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
                OpenFileDialog openFileDialog = new();
                if (openFileDialog.ShowDialog() == true)
                {
                    RequestIsWorking = true;
                    bool erfolgreich = false;
                    WeakReferenceMessenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Mitglieder werden importiert" }, "SchnurschiessenMitgliederImport");
                    await new ImportHelper().PostSchnurschiessenFile(openFileDialog.FileName).ContinueWith(async task =>
                    {
                        if (task.Result.IsSuccessStatusCode)
                        {
                            data = await task.Result.Content.ReadAsAsync<SchnurschiessenImportHistoryModel>();
                            Response.Data = data.Importlist;
                            erfolgreich = true;
                            OnPropertyChanged("ItemList");
                        }
                        else
                        {
                            
                            return;
                        }
                    });
                    WeakReferenceMessenger.Default.Send(new CloseLoadingViewMessage(), "SchnurschiessenMitgliederImport");
                    RequestIsWorking = false;

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
                WeakReferenceMessenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Mitglieder werden gespeichert." }, "SchnurschiessenMitgliederImport");
                HttpResponseMessage resp = null;
                try
                {
                     resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Import/schnurschiessen/Save", data);

                }catch (Exception) { }
                WeakReferenceMessenger.Default.Send(new CloseLoadingViewMessage(), "SchnurschiessenMitgliederImport");

                if (resp.IsSuccessStatusCode)
                {
                    Response.Data.Clear();
                    OnPropertyChanged("ItemList");
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), StammdatenTypes.schnurschiessen.ToString());
                }
                else
                {
                    SendExceptionMessage("Mitglieder konnten nicht gespeichert werden.");
                    return;
                }
            }
        }
        public bool CanExecuteCommand() => !RequestIsWorking;
        #endregion

        public override bool RequestIsWorking
        {
            get => base.RequestIsWorking;
            set
            {
                base.RequestIsWorking = value;
                if (ImportCommand != null)
                {
                    ((DelegateCommand)ImportCommand).RaiseCanExecuteChanged();
                }
                if (SaveCommand != null)
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
    }
}
