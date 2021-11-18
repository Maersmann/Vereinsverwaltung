using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.ExportModels;
using Data.Types;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.UtilMessages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows.Input;

namespace Logic.UI.ExportViewModels
{
    public class ExportMitgliederAenderungenViewModel : ViewModelUebersicht<ExportMitgliederAenderungenModel, StammdatenTypes>
    {
        private bool canErledigen;
        private ObservableCollection<MitgliedAenderungDatenModel> detailList;
        public ExportMitgliederAenderungenViewModel()
        {
            detailList = new ObservableCollection<MitgliedAenderungDatenModel>();
            Title = "Export Mitgliederänderungen";
            canErledigen = false;
            SpeichernCommand = new RelayCommand(() => ExecuteSpeichernCommand());
            ErledigenCommand = new RelayCommand(() => ExecuteErledigenCommandAsync());
        }

        protected override int GetID() { return SelectedItem.ID; }
        protected override bool WithPagination() { return true; }
        protected override string GetREST_API() { return $"/api/export/mitgliedAenderungen/Uebersicht"; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.mitglied; }

        public override ExportMitgliederAenderungenModel SelectedItem 
        { 
            get => base.SelectedItem;
            set 
            {
                base.SelectedItem = value;
                if(SelectedItem != null)
                {
                    detailList = SelectedItem.Daten;
                }
                else
                {
                    detailList = new ObservableCollection<MitgliedAenderungDatenModel>();
                }
                
                RaisePropertyChanged(nameof(DetailList));
            } 
        }

        #region Binding
        public ICommand SpeichernCommand { get; private set; }
        public ICommand ErledigenCommand { get; private set; }
        public bool CanErledigen => canErledigen;
        public IEnumerable<MitgliedAenderungDatenModel> DetailList
        {
            get
            {
                return detailList;
            }
        }
        #endregion

        #region Commands
        private async void ExecuteErledigenCommandAsync()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL + $"/api/export/mitgliedAenderungen/Entfernen");
                if (resp.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage("Änderungen können nicht gelöscht werden") ;
                    return;
                }

                SendInformationMessage("Änderungen erledigt");
                await LoadData();
                RequestIsWorking = false;
            }
        }

        private async void ExecuteSpeichernCommand()
        {
            Messenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Liste wird heruntergeladen" }, "ExportMitgliederAenderungen");
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/export/mitgliedAenderungen/Export");
            if (resp.IsSuccessStatusCode)
            {
                string pfad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Vereinsverwaltung\\Export\\Mitgliederänderungen";
                if (!Directory.Exists(pfad))
                {
                    Directory.CreateDirectory(pfad);
                }

                await File.WriteAllBytesAsync(Path.Combine(pfad, resp.Content.Headers.ContentDisposition.FileNameStar), resp.Content.ReadAsByteArrayAsync().Result);
                Process.Start("explorer.exe", $@"{pfad}");
                Messenger.Default.Send(new CloseLoadingViewMessage(), "ExportMitgliederAenderungen");
                canErledigen = true;
                RaisePropertyChanged(nameof(CanErledigen));
            }
            else
            {
                SendExceptionMessage("Datei konnte nicht heruntergeladen werden.");
            }                     
        }
        #endregion
    }
}
