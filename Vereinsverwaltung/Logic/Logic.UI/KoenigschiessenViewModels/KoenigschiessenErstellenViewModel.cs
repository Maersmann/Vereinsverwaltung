using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.ViewModels;
using Data.Model.KoenigschiessenModels;
using Data.Types;
using Data.Types.KoenigschiessenTypes;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.Messages.UtilMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Logic.UI.KoenigschiessenViewModels
{
    public class KoenigschiessenErstellenViewModel : ViewModelStammdaten<KoenigschiessenErstellenModel, StammdatenTypes>
    {
        public KoenigschiessenErstellenViewModel()
        {
            Title = "Neues Königschiessen";
            Data = new KoenigschiessenErstellenModel { Intervall = KoenigschiessenIntervall.alt, Stichtag = DateTime.Now};
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.koenigschiessen;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                WeakReferenceMessenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Königschiessen wird erstellt." }, "KoenigschiessenErstellen");
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Koenigschiessen", Data);
                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp().ToString());
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                {
                    SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                }
                else
                {
                    SendExceptionMessage("Königschiessen konnte nicht erstellt werden.");
                }
                WeakReferenceMessenger.Default.Send(new CloseLoadingViewMessage(), "KoenigschiessenErstellen"); 
                RequestIsWorking = false;
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
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public IEnumerable<KoenigschiessenIntervall> KoenigschiessenIntervalle => Enum.GetValues(typeof(KoenigschiessenIntervall)).Cast<KoenigschiessenIntervall>();
        public KoenigschiessenIntervall KoenigschiessenIntervall
        {
            get { return Data.Intervall; }
            set
            {
                if (RequestIsWorking || (Data.Intervall != value))
                {
                    Data.Intervall = value;
                    OnPropertyChanged();
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
