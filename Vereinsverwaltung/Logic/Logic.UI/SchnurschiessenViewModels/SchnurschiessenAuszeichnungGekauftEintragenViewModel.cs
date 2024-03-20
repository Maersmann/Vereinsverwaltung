using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using Data.Model.SchnurrschiessenModels;
using Data.Model.SchnurrschiessenModels.DTO;
using Data.Types;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurschiessenAuszeichnungGekauftEintragenViewModel : ViewModelStammdaten<SchnurschiessenAuszeichnungGekauftEintragenModel, StammdatenTypes>
    {  
        public SchnurschiessenAuszeichnungGekauftEintragenViewModel()
        {
            Title = "xxx gekauft";
        }

        public void SetzeInformationen( int schnurauszeichnungID, string bez )
        { 
            Data.SchnurauszeichnungID = schnurauszeichnungID;
            Title = bez + " gekauft";
            OnPropertyChanged(nameof(Title));
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schnurschiessenAuszeichnungBestand;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Schnurschiessenauszeichnung/Gekauft", new SchnurschiessenAuszeichnungGekauftEintragenDTO
                {
                    Anzahl = Data.Anzahl,
                    SchnurauszeichnungID =Data.SchnurauszeichnungID
                });
                RequestIsWorking = false;

                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp().ToString());
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else
                {
                    SendExceptionMessage("Daten konnten nicht gespeichert werden.");
                }
            }
        }
        #endregion

        #region Bindings
        public int? Anzahl
        {
            get => Data.Anzahl;
            set
            {
                if (RequestIsWorking || !Equals(Data.Anzahl, value))
                {
                    ValidateAnzahl(value, "Anzahl");
                    Data.Anzahl = value.GetValueOrDefault();
                    base.OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
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

        protected override void OnActivated()
        {
            Data = new SchnurschiessenAuszeichnungGekauftEintragenModel();
            Anzahl = null;
            state = State.Neu;
        }

    }
}
