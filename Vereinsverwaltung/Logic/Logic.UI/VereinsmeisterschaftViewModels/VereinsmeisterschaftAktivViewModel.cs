using Base.Logic.Core;
using Base.Logic.Filter;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.UtilMessages;
using Logic.Messages.VereinsmeisterschaftMessages;
using Microsoft.AspNetCore.Http;
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
        private bool zeigeNurOffene;
        private VereinsmeisterschaftMitInfoModel vereinsmeisterschaft;
        public VereinsmeisterschaftAktivViewModel()
        {
            MessageToken = "VereinsmeisterschaftAktiveVereinsmeisterschaft";
            Title = "Aktive Vereinsmeisterscahft";
            zeigeNurOffene = true;
            _ = LoadVereinsmeisterschaft();
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
                else if (resp.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    RequestIsWorking = false;
                    Messenger.Default.Send(new NeueVereinsmeisterschaftErstellenMessage(NeueVereinsmeisterschaftErstellenCallback), messageToken);
                }                             
            }
            ((DelegateCommand)NeuerSchuetzeCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)OpenGruppenViewCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)VereinsmeisterschaftAbschliessenCommand).RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(AnzahlFrauen));
            RaisePropertyChanged(nameof(AnzahlHerren16_30));
            RaisePropertyChanged(nameof(AnzahlHerren31_50));
            RaisePropertyChanged(nameof(AnzahlHerren51));
            RaisePropertyChanged(nameof(AnzahlSportschuetzen));
            RaisePropertyChanged(nameof(AnzahlGruppen));
            RaisePropertyChanged(nameof(AnzahlGruppenFrauen));
            RaisePropertyChanged(nameof(Jahr));
            RaisePropertyChanged(nameof(AnzahlGruppenMaenner));
        }

        private async void SchliesseVereinsmeisterschaftAb()
        {
            Messenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Wird Abgeschlossen" }, messageToken);
            string URL = GlobalVariables.BackendServer_URL + $"/api/Vereinsmeisterschaften/Abschliessen?vereinsmeisterschaftID={vereinsmeisterschaft.ID}";
            HttpResponseMessage resp = await Client.GetAsync(URL);
            if (!resp.IsSuccessStatusCode)
            {
                SendExceptionMessage("Vereinsmeisterschaft konnte nicht abgeschlossen werden.");
            }
            else
            {
                await LoadVereinsmeisterschaft();
                await LoadData();
            }
            Messenger.Default.Send(new CloseLoadingViewMessage(), messageToken);
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
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Commannds
        private void ExecuteNeuerSchuetzeCommand()
        {
            Messenger.Default.Send(new NeuerSchuetzeErstellenMessage(NeuerSchuetzeErstellenCallback, vereinsmeisterschaft.ID), messageToken);
        }
        private void ExecuteOpenGruppenViewCommand()
        {
            Messenger.Default.Send(new OpenVereinsmeisterschaftGruppenMitSchuetzenMessage { VereinsmeisterschaftID = vereinsmeisterschaft.ID }, messageToken);
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
            Messenger.Default.Send(new VereinsmeisterschaftErgebnisEintragenMessage { SchuetzenErgebnisID = SelectedItem.ID}, messageToken);
        }

        private void ExecuteVereinsmeisterschaftAbschliessenCommand()
        {
            Messenger.Default.Send(new OpenBestaetigungViewMessage { Beschreibung = "Soll die Vereinsmeisterschaft abgeschlossen werden?", Command = SchliesseVereinsmeisterschaftAb }, messageToken);
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
