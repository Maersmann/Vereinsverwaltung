using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels;
using Data.Model.KoenigschiessenModels.DTO;
using Data.Types.KoenigschiessenTypes;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class KoenigschiessenErgebnisEintragenViewModel : ViewModelValidate
    {
        private KoenigschiessenRundeTeilnehmerUebersichtModel koenigschiessenRundeTeilnehmerUebersicht;
        private int ergebnis;
        private KoenigschiessenVarianten variante;
        public KoenigschiessenErgebnisEintragenViewModel()
        {
            koenigschiessenRundeTeilnehmerUebersicht = new KoenigschiessenRundeTeilnehmerUebersichtModel();
            Title = "Ergebnis eintragen";
            BestaetigungCommand = new DelegateCommand(ExecuteBestaetigungCommand, CanExecuteBestaetigungCommand);
            AbbrechenCommand = new RelayCommand(() => ExecuteAbbrechenCommand());
            Bestaetigt = false;
            Ergebnis = "";
            variante = KoenigschiessenVarianten.koenigschiessen;
        }

        public void ZeigeDatenAn(KoenigschiessenRundeTeilnehmerUebersichtModel koenigschiessenRundeTeilnehmerUebersicht, KoenigschiessenVarianten variante)
        {
            this.koenigschiessenRundeTeilnehmerUebersicht = koenigschiessenRundeTeilnehmerUebersicht;
            this.variante = variante;
            RaisePropertyChanged(nameof(KoenigschiessenRundeTeilnehmer));
        }

        public bool Bestaetigt { get; private set; }


        #region Bindings
        public ICommand BestaetigungCommand { get; set; }
        public ICommand AbbrechenCommand { get; set; }

        public KoenigschiessenRundeTeilnehmerUebersichtModel KoenigschiessenRundeTeilnehmer => koenigschiessenRundeTeilnehmerUebersicht;

        public string Ergebnis
        {
            get
            {
                return ergebnis.Equals(-1) ? "" : ergebnis.ToString();
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    value = "-1";
                }

                if (!int.TryParse(value, out int Ergebnis)) return;

                if (RequestIsWorking || !Equals(ergebnis, Ergebnis))
                {
                    ValidateErgebnis(Ergebnis);
                    ergebnis = Ergebnis;
                    RaisePropertyChanged();
                    ((DelegateCommand)BestaetigungCommand).RaiseCanExecuteChanged();
                }
            }
        }
        #endregion


        #region Commands

        private async void ExecuteBestaetigungCommand()
        {
            try
            {
                if((ergebnis > 20 && variante.Equals(KoenigschiessenVarianten.koenigschiessen)) || (ergebnis > 30 && variante.Equals(KoenigschiessenVarianten.jugendkoenigschiessen)))
                {
                    SendExceptionMessage("Das Ergebnis ist zu hoch.");
                    return;
                }

                if (GlobalVariables.ServerIsOnline)
                {
                    HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/KoenigschiessenErgebnis",
                        new KoenigschiessenErgebnisEintragenDTO
                        {
                            KoenigschiessenRundeSchuetzeID = koenigschiessenRundeTeilnehmerUebersicht.KoenigschiessenRundeSchuetzeID,
                            Ergebnis = ergebnis
                        });
                    if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                    {
                        SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                    }
                    else if (!resp.IsSuccessStatusCode)
                    {
                        SendExceptionMessage("Fehler: Anmeldung bei: " + koenigschiessenRundeTeilnehmerUebersicht.Nachname + Environment.NewLine + await resp.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        Bestaetigt = true;
                    }
                    Messenger.Default.Send(new CloseViewMessage(), "KoenigschiessenErgebnisEintragen");
                }
            }
            catch (Exception e)
            {
                SendExceptionMessage(e.Message); ;
            }

        }

        private void ExecuteAbbrechenCommand()
        {
            Messenger.Default.Send(new CloseViewMessage(), "KoenigschiessenErgebnisEintragen");
        }

        private bool CanExecuteBestaetigungCommand()
        {
            return ValidationErrors.Count == 0;
        }
        #endregion

        #region Validierung
        private bool ValidateErgebnis(int? ergebnis)
        {
            var Validierung = new BaseValidierung();

            if(ergebnis.Equals(-1))
            {
                ergebnis = null;
            }

            bool isValid = Validierung.ValidateIntegerAllow0(ergebnis, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Ergebnis", validationErrors);
            return isValid;
        }
        #endregion
    }
}
