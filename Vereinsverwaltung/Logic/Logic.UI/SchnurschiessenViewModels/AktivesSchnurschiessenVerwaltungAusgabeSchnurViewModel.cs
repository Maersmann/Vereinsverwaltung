using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.SchnurrschiessenModels;
using Data.Model.SchnurrschiessenModels.DTO;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.Messages.SchnurschiessenMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class AktivesSchnurschiessenVerwaltungAusgabeSchnurViewModel : ViewModelStammdaten<AktivesSchnurschiessenVerwaltungAusgabeSchnurModel, StammdatenTypes>
    {
        public AktivesSchnurschiessenVerwaltungAusgabeSchnurViewModel()
        {
            Title = "xxx Ausgabe";
        }

        public void SetzeInformationen(int schnurschiessenBestandID, string bez)
        {
            Data.SchnurschiessenBestandID = schnurschiessenBestandID;
            Title = bez + " Ausgabe";
            RaisePropertyChanged(nameof(Title));
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schnurschiessenAuszeichnungBestand;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Schnurschiessen/AuszeichnungBestand/Ausgabe", new AktivesSchnurschiessenAuszeichnungAusgabeDTO { 
                    Anzahl = Data.Bestand, 
                    SchnurschiessenBestandID = Data.SchnurschiessenBestandID
                });
                RequestIsWorking = false;

                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if ((int)resp.StatusCode == 409)
                {
                    SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                }
                else
                {
                    SendExceptionMessage("Ausgabe konnte nicht gespeichert werden.");
                }
            }
        }
        #endregion

        #region Bindings
        public int? Anzahl
        {
            get => Data.Bestand;
            set
            {
                if (RequestIsWorking || !Equals(Data.Bestand, value))
                {
                    ValidateAnzahl(value, "Bestand");
                    Data.Bestand = value.GetValueOrDefault();
                    base.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int? Bestand
        {
            get => Data.Bestand;
        }


        #endregion

        #region Validierung
        private bool ValidateAnzahl(int? anzahl, string fieldname)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateInteger(anzahl, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, fieldname, validationErrors);
            return isValid;
        }

        #endregion

      

        public override void Cleanup()
        {
            Data = new AktivesSchnurschiessenVerwaltungAusgabeSchnurModel();
            Anzahl = 1;
            state = State.Neu;
        }

    }
}
