using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.Messages.UtilMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class JugendkoenigschiessenErstellenViewModel : ViewModelStammdaten<JugendkoenigschiessenErstellenModel, StammdatenTypes>
    {
        public JugendkoenigschiessenErstellenViewModel()
        {
            Title = "Neues Jugendkönigschiessen";
            Data = new JugendkoenigschiessenErstellenModel { Stichtag = DateTime.Now };
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.jugendkoenigschiessen;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                Messenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Jugendkönigschiessen wird erstellt." }, "JugendkoenigschiessenErstellen");
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Jugendkoenigschiessen", Data);
                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Erstellt" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                {
                    SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                }
                else
                {
                    SendExceptionMessage("Jugendkönigschiessen konnte nicht erstellt werden.");
                }
                Messenger.Default.Send(new CloseLoadingViewMessage(), "JugendkoenigschiessenErstellen");
            }
        }
        #endregion

        #region Bindings

        public DateTime? Stichtag
        {
            get { return Data.Stichtag; }
            set
            {

                if (RequestIsWorking || !Equals(Data.Stichtag, value))
                {
                    ValidateStichtag(value);
                    Data.Stichtag = value;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Validierung
        private bool ValidateStichtag(DateTime? stichtag)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateDatum(stichtag, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Stichtag", validationErrors);

            return isValid;
        }
        #endregion
    }
}
