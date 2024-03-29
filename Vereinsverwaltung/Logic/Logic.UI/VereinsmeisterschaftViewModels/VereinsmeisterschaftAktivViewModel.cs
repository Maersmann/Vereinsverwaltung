﻿using Base.Logic.Core;
using Base.Logic.Filter;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.UtilMessages;
using Logic.Messages.VereinsmeisterschaftMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftAktivViewModel : ViewModelUebersicht<VereinsmeisterschaftschuetzeErgebnisModel, StammdatenTypes>
    {
        private bool vereinsmeisterschaftAbgeschlossen = false;
        private bool zeigeNurOffene;
        private VereinsmeisterschaftMitInfoModel vereinsmeisterschaft;
        public VereinsmeisterschaftAktivViewModel()
        {
            MessageToken = "VereinsmeisterschaftAktiveVereinsmeisterschaft";
            Title = "Aktive Vereinsmeisterschaft";
            zeigeNurOffene = true;
            _ = LoadVereinsmeisterschaft();
            SucheCommand = new DelegateCommand(ExecuteSucheCommand, CanExecuteCommand);
            NeuerSchuetzeCommand = new DelegateCommand(ExecuteNeuerSchuetzeCommand, CanExecuteCommand);
            OpenGruppenViewCommand = new DelegateCommand(ExecuteOpenGruppenViewCommand, CanExecuteCommand);
            ErgebnisEintragenViewCommand = new RelayCommand(() => ExecuteErgebnisEintragenViewCommand());
            VereinsmeisterschaftAbschliessenCommand = new DelegateCommand(ExecuteVereinsmeisterschaftAbschliessenCommand, CanExecuteCommand);
            RegisterAktualisereViewMessage(StammdatenTypes.vereinsmeisterschaftSchuetzeErgebnis.ToString());
        }

        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.vereinsmeisterschaftSchuetzeErgebnis; }
        protected override bool WithPagination() { return true; }
        protected override string GetREST_API() { return $"/api/VereinsmeisterschaftschuetzeErgebnisse/Vereinsmeisterschaft?vereinsmeisterschaftId={(vereinsmeisterschaft != null ? vereinsmeisterschaft.ID : 0)}&nurOffene={ZeigeNurOffene}"; }

        private async Task LoadVereinsmeisterschaft()
        {
            vereinsmeisterschaft = new VereinsmeisterschaftMitInfoModel();
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                string URL = GlobalVariables.BackendServer_URL + "/api/vereinsmeisterschaften/aktiv";
                HttpResponseMessage resp = await Client.GetAsync(URL);
                if (resp.IsSuccessStatusCode)
                {
                    Response<VereinsmeisterschaftMitInfoModel> VereinsmeisterschaftResponse = await resp.Content.ReadAsAsync<Response<VereinsmeisterschaftMitInfoModel>>();
                    vereinsmeisterschaft = VereinsmeisterschaftResponse.Data;
                    await LoadData();
                    RequestIsWorking = false;
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.NotFound) && !vereinsmeisterschaftAbgeschlossen)
                {
                    RequestIsWorking = false;
                    WeakReferenceMessenger.Default.Send(new NeueVereinsmeisterschaftErstellenMessage(NeueVereinsmeisterschaftErstellenCallback), messageToken);
                }                             
            }
            ((DelegateCommand)NeuerSchuetzeCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)OpenGruppenViewCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)VereinsmeisterschaftAbschliessenCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)SucheCommand).RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(AnzahlFrauen));
            OnPropertyChanged(nameof(AnzahlHerren16_30));
            OnPropertyChanged(nameof(AnzahlHerren31_50));
            OnPropertyChanged(nameof(AnzahlHerren51));
            OnPropertyChanged(nameof(AnzahlSportschuetzen));
            OnPropertyChanged(nameof(AnzahlGruppen));
            OnPropertyChanged(nameof(AnzahlGruppenFrauen));
            OnPropertyChanged(nameof(Jahr));
            OnPropertyChanged(nameof(AnzahlGruppenMaenner));
            OnPropertyChanged(nameof(IsEnabled));
        }

        private async void SchliesseVereinsmeisterschaftAb()
        {
            WeakReferenceMessenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Wird Abgeschlossen" }, messageToken);
            string URL = GlobalVariables.BackendServer_URL + $"/api/Vereinsmeisterschaften/Abschliessen?vereinsmeisterschaftID={vereinsmeisterschaft.ID}";
            HttpResponseMessage resp = await Client.GetAsync(URL);
            if (!resp.IsSuccessStatusCode)
            {
                SendExceptionMessage("Vereinsmeisterschaft konnte nicht abgeschlossen werden.");
            }
            else
            {
                vereinsmeisterschaftAbgeschlossen = true;
                await LoadVereinsmeisterschaft();
                await LoadData();
            }
            WeakReferenceMessenger.Default.Send(new CloseLoadingViewMessage(), messageToken);
        }

        #region Bindings
        public string Jahr => "Vereinsmeistschaft im Jahr " +vereinsmeisterschaft.Jahr;
        public int AnzahlSchuetzen => vereinsmeisterschaft.AnzahlSchuetzen;
        public int AnzahlFrauen => vereinsmeisterschaft.AnzahlFrauen;
        public int AnzahlHerren16_30 => vereinsmeisterschaft.AnzahlMaenner16_30;
        public int AnzahlHerren31_50 => vereinsmeisterschaft.AnzahlMaenner31_50;
        public int AnzahlHerren51 => vereinsmeisterschaft.AnzahlMaenner51;
        public int AnzahlSportschuetzen => vereinsmeisterschaft.AnzahlSportschuetzen;
        public int AnzahlGruppen => vereinsmeisterschaft.AnzahlGruppen;
        public int AnzahlGruppenFrauen => vereinsmeisterschaft.AnzahlGruppenFrauen;
        public int AnzahlGruppenMaenner => vereinsmeisterschaft.AnzahlGruppenMaenner;
        public ICommand NeuerSchuetzeCommand { get;set;}
        public ICommand OpenGruppenViewCommand { get; set; }
        public ICommand ErgebnisEintragenViewCommand { get; set; }
        public ICommand VereinsmeisterschaftAbschliessenCommand { get; set; }
        public bool ZeigeNurOffene
        {
            get => zeigeNurOffene;
            set
            {
                zeigeNurOffene = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled => vereinsmeisterschaft.ID > 0;
        #endregion

        #region Commannds
        private void ExecuteNeuerSchuetzeCommand()
        {
            WeakReferenceMessenger.Default.Send(new NeuerSchuetzeErstellenMessage(NeuerSchuetzeErstellenCallback, vereinsmeisterschaft.ID), messageToken);
        }
        private void ExecuteOpenGruppenViewCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenVereinsmeisterschaftGruppenMitSchuetzenMessage { VereinsmeisterschaftID = vereinsmeisterschaft.ID }, messageToken);
        }
        protected override bool CanExecuteCommand() => vereinsmeisterschaft.ID > 0;
        protected async override void ExecuteEntfernenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL + $"/api/VereinsmeisterschaftschuetzeErgebnisse/{SelectedItem.ID}");
                RequestIsWorking = false;
                if ((int)resp.StatusCode == 915)
                {
                    SendExceptionMessage("Der Schütze kann nicht gelöscht werden." + Environment.NewLine + "Es wurde schon ein Ergebnis eingetragen.");
                    return;
                }
                SendInformationMessage("Schütze gelöscht");
                base.ExecuteEntfernenCommand();
            }
        }
        private void ExecuteErgebnisEintragenViewCommand()
        {
            WeakReferenceMessenger.Default.Send(new VereinsmeisterschaftErgebnisEintragenMessage { SchuetzenErgebnisID = SelectedItem.ID}, messageToken);
        }

        private void ExecuteVereinsmeisterschaftAbschliessenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenBestaetigungViewMessage { Beschreibung = "Soll die Vereinsmeisterschaft abgeschlossen werden?", Command = SchliesseVereinsmeisterschaftAb }, messageToken);
        }

        #endregion


        #region Callback
        private async void NeueVereinsmeisterschaftErstellenCallback(bool confirmed)
        {
            if (confirmed)
            {
               await LoadVereinsmeisterschaft();
            }
        }

        private async void NeuerSchuetzeErstellenCallback(bool confirmed)
        {
            if (confirmed)
            {
                await LoadData();
                await LoadVereinsmeisterschaft();
            }
        }
        #endregion
    }
}
