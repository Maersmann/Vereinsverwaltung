using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.KoenigschiessenModels;
using Data.Model.KoenigschiessenModels.DTOs;
using Data.Types;
using Data.Types.KoenigschiessenTypes;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.BaseMessages;
using Logic.Messages.KoenigschiessenMessages;
using Logic.Messages.UtilMessages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class KoenigschiessenAnmeldungUebersichtViewModel : ViewModelUebersicht<KoenigschiessenAnmeldungUebersichtModel, StammdatenTypes>
    {
        private int jahr;
        private bool zeigeNurNichtAngemeldete; 
        private KoenigschiessenVarianten variante;
        private IKoenigschiessenAnmeldungWerteViewModel koenigschiessenAnmeldungWerteViewModel;
        public KoenigschiessenAnmeldungUebersichtViewModel()
        {
            variante = KoenigschiessenVarianten.koenigschiessen;
            zeigeNurNichtAngemeldete = true;
            jahr = 2022;
            Title = "Übersicht Anmeldung Königsschiessen";
            AnmeldeCommand = new RelayCommand(() => ExecuteAnmeldeCommand());
            RueckgaengigCommand = new RelayCommand(() => ExcecuteRueckgaengigCommand());
        }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.mitglied; }
        protected override int GetID() { return SelectedItem.MitgliedID; }
        protected override string GetREST_API() { return $"/api/KoenigschiessenAnmeldung?jahr={jahr}&nurNichtAngemeldete={zeigeNurNichtAngemeldete}&variante={variante}"; }
        protected override bool WithPagination() { return true; }
        protected override bool LoadingOnCreate() => false;

        public async void LadeUebersicht(int jahr, KoenigschiessenVarianten variante)
        {
            this.variante = variante;
            this.jahr = jahr;
            await LoadData();
            koenigschiessenAnmeldungWerteViewModel.LadeWerte(jahr);
        }

        public void SetzeWerteInterface(IKoenigschiessenAnmeldungWerteViewModel koenigschiessenAnmeldungWerteViewModel)
        {
            this.koenigschiessenAnmeldungWerteViewModel = koenigschiessenAnmeldungWerteViewModel;
        }

        #region bindings
        public ICommand AnmeldeCommand { get; private set; }
        public ICommand RueckgaengigCommand { get; private set; }
        public bool ZeigeNurNichtAngemeldete
        {
            get => zeigeNurNichtAngemeldete;
            set
            {
                zeigeNurNichtAngemeldete = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Commands

        private void ExecuteAnmeldeCommand()
        {
            Messenger.Default.Send(new OpenKoenigschiessenBestaetigungMessage { KoenigschiessenAnmeldung = SelectedItem, Command = KoenigschiessenAnmeldungBestaetigt }, "KoenigschiessenAnmeldungUebersicht");
        }


        private void KoenigschiessenAnmeldungBestaetigt()
        {
            SelectedItem.Angemeldet = true;
            SelectedItem.AngemeldetAm = DateTime.Now;
            FilterText = "";
            RaisePropertyChanged(nameof(FilterText));
            LadeUebersicht(jahr, variante);
        }
        private void ExcecuteRueckgaengigCommand()
        {
            Messenger.Default.Send(new OpenBestaetigungViewMessage { Beschreibung = "Soll die Anmeldung Rückgängig gemacht werden?", Command = KoenigschiessenAnmeldungRueckgaengig }, "KoenigschiessenAnmeldungUebersicht");
        }

        private async void KoenigschiessenAnmeldungRueckgaengig()
        {
            try
            {
                if (GlobalVariables.ServerIsOnline)
                {
                    HttpResponseMessage resp = await Client.DeleteAsync(GlobalVariables.BackendServer_URL + $"/api/KoenigschiessenAnmeldung/{SelectedItem.ID}");

                    if (!resp.IsSuccessStatusCode)
                    {
                        if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                        {
                            SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                        }
                        else
                        {
                            SendExceptionMessage("Fehler:Rücknahme bei: " + SelectedItem.Nachname + Environment.NewLine + await resp.Content.ReadAsStringAsync());
                            return;
                        }
                    }
                    else
                    {
                        SelectedItem.Angemeldet = false;
                        SelectedItem.AngemeldetAm = null;
                        LadeUebersicht(jahr, variante);
                    }
                }
            }
            catch (Exception e)
            {
                SendExceptionMessage(e.Message);
            }
        }

        protected override void ExecuteBearbeitenCommand()
        {
            base.ExecuteBearbeitenCommand();
            FilterText = SelectedItem.Fullname;
            LadeUebersicht(jahr, variante);
        }

        #endregion

    }
}