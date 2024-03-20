﻿using Data.Model.ImportModel;
using Data.Model.MitgliederModels;
using Data.Types;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Core;
using Logic.Core.ImportHelper;
using Logic.Messages.BaseMessages;
using Logic.Messages.UtilMessages;
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
using Base.Logic.ViewModels;
using Base.Logic.Core;
using Base.Logic.Messages;

namespace Logic.UI.MitgliederViewModels
{
    public class MitgliederImportViewModel : ViewModelLoadListData<MitgliederImportModel>
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
                    WeakReferenceMessenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Mitglieder werden importiert" }, "MitgliederImport");
                    await new ImportHelper().PostFile( openFileDialog.FileName ).ContinueWith(async task =>
                    {                      
                        if (task.Result.IsSuccessStatusCode)
                        {
                            data = await task.Result.Content.ReadAsAsync<MitgliedImportHistoryModel>();
                            Response.Data = data.Importlist;
                            OnPropertyChanged("ItemList");
                        }
                        else
                        {
                            SendExceptionMessage("Import-Datei konnte nicht eingelesen werden");
                            return;
                        }
                    });
                    WeakReferenceMessenger.Default.Send(new CloseLoadingViewMessage(), "MitgliederImport");
                }
            }         
        }

        private async void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                WeakReferenceMessenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Mitglieder werden gespeichert" }, "MitgliederImport");
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/Import/Mitglieder/Save", data);
                WeakReferenceMessenger.Default.Send(new CloseLoadingViewMessage(), "MitgliederImport");

                if (resp.IsSuccessStatusCode)
                {
                    Response.Data.Clear();
                    OnPropertyChanged("ItemList");
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), StammdatenTypes.mitglied.ToString());
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
