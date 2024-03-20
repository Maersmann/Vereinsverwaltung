using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels;
using Data.Model.KoenigschiessenModels.DTOs;
using Data.Types;
using Data.Types.KoenigschiessenTypes;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.BaseMessages;
using Logic.Messages.KoenigschiessenMessages;
using Logic.Messages.UtilMessages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Prism.Commands;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class KoenigschiessenRundeTeilnehmerUebersichtViewModel : ViewModelUebersicht<KoenigschiessenRundeTeilnehmerUebersichtModel, StammdatenTypes>
    {
        private int jahr;
        private int runde;
        private bool zeigeNurOhneErgebnis;
        private KoenigschiessenVarianten variante;
        private KoenigschiessenArt art;
        private KoenigschiessenRundeTeilnehmerWerteViewModel koenigschiessenRundeTeilnehmerWerteViewModel;
        public KoenigschiessenRundeTeilnehmerUebersichtViewModel()
        {
            variante = KoenigschiessenVarianten.koenigschiessen;
            zeigeNurOhneErgebnis = true;
            jahr = 2022;
            runde = 1;
            Title = "Teilnehmer Aktuelle Runde";
            RundeBeendet = false;
            ErgebnisEintragenCommand = new RelayCommand(() => ExecuteErgebnisEintragenCommand());
            ErgebnisEntfernenCommand = new RelayCommand(() => ExcecuteErgebnisEntfernenCommand());
            BesteErgebnisseCommand = new RelayCommand(() => ExcecuteBesteErgebnisseCommand());
            RundeBeendenCommand = new DelegateCommand(ExcecuteRundeBeendenCommand, CanPost);
        }

        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.mitglied; }
        protected override int GetID() { return SelectedItem.MitgliedID; }
        protected override string GetREST_API() { return $"/api/KoenigschiessenRunde?jahr={jahr}&nurOhneErgebnis={zeigeNurOhneErgebnis}&runde={runde}&art={art}"; }
        protected override bool WithPagination() { return true; }
        protected override bool LoadingOnCreate() => false;

        public async void LadeUebersicht(int jahr, KoenigschiessenVarianten variante, int runde, KoenigschiessenArt art)
        {
            this.art = art;
            this.variante = variante;
            this.jahr = jahr;
            this.runde = runde;
            await LoadData();
            koenigschiessenRundeTeilnehmerWerteViewModel.LadeWerte(jahr, art, runde);
        }

        public void SetzeWerte(KoenigschiessenRundeTeilnehmerWerteViewModel koenigschiessenRundeTeilnehmerWerteViewModel)
        {
            this.koenigschiessenRundeTeilnehmerWerteViewModel = koenigschiessenRundeTeilnehmerWerteViewModel;
        }

        public bool RundeBeendet { get; private set; }

        #region bindings
        public ICommand ErgebnisEintragenCommand { get; private set; }
        public ICommand ErgebnisEntfernenCommand { get; private set; }
        public ICommand BesteErgebnisseCommand { get; private set; }
        public ICommand RundeBeendenCommand { get; private set; }
        public bool ZeigeNurOhneErgebnis
        {
            get => zeigeNurOhneErgebnis;
            set
            {
                zeigeNurOhneErgebnis = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands

        private void ExecuteErgebnisEintragenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenKoenigschiessenErgebnisEintragenMessage { KoenigschiessenRundeTeilnehmer = SelectedItem, Command = KoenigschiessenErgebnisEingetragen, variante = variante}, "KoenigschiessenRundeTeilnehmerUebersicht");
        }


        private void KoenigschiessenErgebnisEingetragen()
        {
            SelectedItem.ErgebnisAbgegeben = true;
            SelectedItem.ErgebnissVom = DateTime.Now;
            FilterText = "";
            OnPropertyChanged(nameof(FilterText));
            LadeUebersicht(jahr, variante, runde, art);
        }
        private void ExcecuteErgebnisEntfernenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenBestaetigungViewMessage { Beschreibung = "Soll das Ergebnis entfernt werden?", Command = ErgebnisEntfernen }, "KoenigschiessenRundeTeilnehmerUebersicht");
        }

        private async void ErgebnisEntfernen()
        {
            try
            {
                if (GlobalVariables.ServerIsOnline)
                {
                    HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL + $"/api/KoenigschiessenErgebnis/{SelectedItem.KoenigschiessenRundeSchuetzeID}");

                    if (!resp.IsSuccessStatusCode)
                    {
                        if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                        {
                            SendExceptionMessage($"Ergebnis wurde schon entfernt");
                        }
                        else
                        {
                            SendExceptionMessage("Fehler:Ergebnis entfernen bei: " + SelectedItem.Nachname + Environment.NewLine + await resp.Content.ReadAsStringAsync());
                            return;
                        }
                    }
                    SelectedItem.ErgebnisAbgegeben = false;
                    SelectedItem.ErgebnissVom = null;
                    LadeUebersicht(jahr, variante, runde,art);
                }
            }
            catch (Exception e)
            {
                SendExceptionMessage(e.Message);
            }
        }

        private void ExcecuteBesteErgebnisseCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenKoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewMessage { Art = art, Jahr = jahr, Runde = runde }, "KoenigschiessenRundeTeilnehmerUebersicht");
        }

        private void ExcecuteRundeBeendenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenBestaetigungViewMessage { Beschreibung = "Soll die Runde beendet werden?", Command = RundeBeenden }, "KoenigschiessenRundeTeilnehmerUebersicht");
        }

        private async void RundeBeenden()
        {
            try
            {
                if (GlobalVariables.ServerIsOnline)
                {
                    RequestIsWorking = true;
                    HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/KoenigschiessenRunde/Abschliessen",
                        new KoenigschiessenRundeAbschliessenDTO
                        {
                            Art = art,
                            Jahr = jahr,
                            Runde = runde
                        });
                    if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                    {
                        SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                    }
                    else if (!resp.IsSuccessStatusCode)
                    {
                        SendExceptionMessage("Fehler: Runde konnte nicht beendet werden");
                    }
                    else
                    {
                        KoenigschiessenAbschlussDTO koenigschiessenAbschluss = await resp.Content.ReadAsAsync<KoenigschiessenAbschlussDTO>();
                        WeakReferenceMessenger.Default.Send(new KoenigschiessenRundeBeendetMessage { KoenigschiessenAbschluss = koenigschiessenAbschluss }, "KoenigschiessenRundeTeilnehmerUebersicht");
                        RundeBeendet = true;

                        if (koenigschiessenAbschluss.KoenigschiessenBeendet)
                        {
                            WeakReferenceMessenger.Default.Send(new CloseViewMessage(), "KoenigschiessenRundeTeilnehmerUebersicht");
                        }
                        else
                        {
                            LadeUebersicht(jahr, variante, runde + 1, art);
                        }
                    }
                    RequestIsWorking = false;
                }
            }
            catch (Exception e)
            {
                SendExceptionMessage(e.Message);
                RequestIsWorking = false;
            }
        }

        public bool CanPost() => !RequestIsWorking;
        #endregion

        public override bool RequestIsWorking
        {
            get => base.RequestIsWorking;
            set
            {
                base.RequestIsWorking = value;
                if (RundeBeendenCommand != null)
                {
                    ((DelegateCommand)RundeBeendenCommand).RaiseCanExecuteChanged();
                }
            }
        }
    }
}
