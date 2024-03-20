using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels;
using Data.Model.KoenigschiessenModels.DTOs;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.BaseMessages;
using Logic.Messages.KoenigschiessenMessages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class KoenigschiessenAnmeldungBestaetigungViewModel : ViewModelBasis
    {
        private KoenigschiessenAnmeldungUebersichtModel koenigschiessenAnmeldung;
        public KoenigschiessenAnmeldungBestaetigungViewModel()
        {
            koenigschiessenAnmeldung = new KoenigschiessenAnmeldungUebersichtModel();
            Title = "Bestästigung Anmeldung";
            BestaetigungCommand = new RelayCommand(() => ExecuteBestaetigungCommand());
            AbbrechenCommand = new RelayCommand(() => ExecuteAbbrechenCommand());
            Bestaetigt = false;
        }

        public void ZeigeDatenAn(KoenigschiessenAnmeldungUebersichtModel koenigschiessenAnmeldung)
        {
            this.koenigschiessenAnmeldung = koenigschiessenAnmeldung;
            OnPropertyChanged(nameof(KoenigschiessenAnmeldung));
        }

        public bool Bestaetigt { get; private set; }


        #region Bindings
        public ICommand BestaetigungCommand { get; set; }
        public ICommand AbbrechenCommand { get; set; }

        public KoenigschiessenAnmeldungUebersichtModel KoenigschiessenAnmeldung => koenigschiessenAnmeldung;
        #endregion


        #region Commands

        private async void ExecuteBestaetigungCommand()
        {
            try
            {
                if (GlobalVariables.ServerIsOnline)
                {
                    HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/KoenigschiessenAnmeldung", new KoenigschiessenAnmeldungDTO { SchuetzeID = koenigschiessenAnmeldung.ID });
                    if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                    {
                        SendExceptionMessage($"{koenigschiessenAnmeldung.Nachname} ist schon angemeldet");
                    }
                    else if (!resp.IsSuccessStatusCode)
                    {
                        SendExceptionMessage("Fehler: Anmeldung bei: " + koenigschiessenAnmeldung.Nachname + Environment.NewLine + await resp.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        Bestaetigt = true;
                    }                   
                    WeakReferenceMessenger.Default.Send(new CloseViewMessage(), "KoenigschiessenAnmeldungBestaetigung");
                }
            }
            catch (Exception e)
            {
                SendExceptionMessage(e.Message); ;
            }

        }

        private void ExecuteAbbrechenCommand()
        {
            WeakReferenceMessenger.Default.Send(new CloseViewMessage(), "KoenigschiessenAnmeldungBestaetigung");
        }
        #endregion

    }
}
