using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftErgebnisEintragenViewModel : ViewModelStammdaten<VereinsmeisterschaftErgebnisEintragenModel, StammdatenTypes>, IViewModelStammdaten
    {
        private string ergebnis;
        public VereinsmeisterschaftErgebnisEintragenViewModel()
        {
            Title = "Ergebnis eintragen";
        }

        public async void ZeigeStammdatenAnAsync(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/VereinsmeisterschaftschuetzeErgebnisse/{id}");
                if (resp.IsSuccessStatusCode)
                {
                    var ErgebnisResponse = await resp.Content.ReadAsAsync<Response<VereinsmeisterschaftschuetzeErgebnisModel>>();
                    Ergebnis = ErgebnisResponse.Data.Ergebnis.ToString();
                    Data.SchuetzenergebnisID = id;
                    await LadeSchuetzenname(ErgebnisResponse.Data.SchuetzeID);
                }
                else
                {
                    SendExceptionMessage("Schütze konnte nicht gefunden werden.");
                    Messenger.Default.Send(new CloseViewMessage(), "VereinsmeisterschaftErgebnisEintragen");
                }
            }
            
            state = State.Bearbeiten;
            RequestIsWorking = false;

        }

        private async Task LadeSchuetzenname(int schuetzenid)
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/Schuetzen/{schuetzenid}");
                if (resp.IsSuccessStatusCode)
                {
                    var SchuetzenResp = await resp.Content.ReadAsAsync<Response<SchuetzeModel>>();
                    Name = SchuetzenResp.Data.Name;
                }
            }
        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.vereinsmeisterschaftSchuetzeErgebnis;

        #region Bindings
        public string Name
        {
            get => "Ergebnis von: " + Data.Name;
            set
            {
                if (RequestIsWorking || !Equals(Data.Name, value))
                {
                    Data.Name = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string Ergebnis
        {
            get
            {
                return ergebnis.Equals("0") ? "" : ergebnis;
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    value = "0";
                }

                if (!double.TryParse(value, out double Ergebnis)) return;
                ergebnis = value;

                if (RequestIsWorking || !Equals(Data.Ergebnis, Ergebnis))
                {
                    ValidateErgebnis(Ergebnis);
                    Data.Ergebnis = Ergebnis;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/VereinsmeisterschaftschuetzeErgebnisse/Ergebnis", Data);
                RequestIsWorking = false;

                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else
                {
                    SendExceptionMessage("Ergebnis konnte nicht gespeichert werden.");
                }
            }
        }

        #endregion

        #region Validierung
        private bool ValidateErgebnis(double? ergebnis)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateDouble(ergebnis, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Ergebnis", validationErrors);
            return isValid;
        }
        #endregion


        public override void Cleanup()
        {
            Data = new VereinsmeisterschaftErgebnisEintragenModel();
            Name = "";
            ergebnis = "";
            state = State.Neu;
        }
    }
}
