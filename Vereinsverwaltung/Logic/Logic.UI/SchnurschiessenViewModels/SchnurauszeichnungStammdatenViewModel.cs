using Data.Model.SchnurrschiessenModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Base.Logic.ViewModels;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Base.Logic.Core;
using Base.Logic.Types;
using Base.Logic.Messages;
using Base.Logic.Wrapper;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurauszeichnungStammdatenViewModel : ViewModelStammdaten<SchnurauszeichnungModel, StammdatenTypes>, IViewModelStammdaten
    {
        private IList<SchnurModel> alleSichtbarenSchnuere;
        private IList<SchnurModel> alleSchnuere;
        public SchnurauszeichnungStammdatenViewModel()
        {
            alleSichtbarenSchnuere = new List<SchnurModel>();
            alleSchnuere = new List<SchnurModel>();
            Title = "Schnurauszeichung Stammdaten";
            LoadSchnuere();      
        }

        private async void LoadSchnuere()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnur");
                if (resp.IsSuccessStatusCode)
                { 
                    var ResponseSchnur = await resp.Content.ReadAsAsync<Response<ObservableCollection<SchnurModel>>>();
                    alleSchnuere = ResponseSchnur.Data;
                }

                alleSichtbarenSchnuere = alleSchnuere.Where(s => s.Sichtbar).ToList();
                RequestIsWorking = false;
            }
            RaisePropertyChanged("Schnuere");
            RaisePropertyChanged("SchnuereZusatz");
        }

        public async void ZeigeStammdatenAn(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnurauszeichnung/{id}");
                if (resp.IsSuccessStatusCode)
                    Response = await resp.Content.ReadAsAsync<Response<SchnurauszeichnungModel>>();
            }
            RequestIsWorking = true;
            Bezeichnung = Data.Bezeichnung;
            Rangfolge = Data.Rangfolge;
            Hauptteil = alleSichtbarenSchnuere.First(s => s.ID == Data.HauptteilID);
            Zusatz = alleSchnuere.FirstOrDefault(s => s.ID == Data.ZusatzID);
            state = State.Bearbeiten;
            RequestIsWorking = false;
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schnurauszeichnung;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                Data.HauptteilID = Hauptteil.ID;
                Data.ZusatzID = Zusatz.ID;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnurauszeichnung", Data);
                RequestIsWorking = false;
                
                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if ((int)resp.StatusCode == 909)
                {
                    SendExceptionMessage("Rangfolge ist schon vorhanden");
                }

            }
        }
        #endregion

        #region Bindings

        public string Bezeichnung
        {
            get => Data.Bezeichnung;
            set
            {
                if (RequestIsWorking || !Equals(Data.Bezeichnung, value))
                {
                    ValidateBezeichnung(value);
                    Data.Bezeichnung = value;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int? Rangfolge
        {
            get => Data.Rangfolge;
            set
            {
                if (RequestIsWorking || !Equals(Data.Rangfolge, value))
                {
                    ValidateZahl(value, "Rangfolge");
                    Data.Rangfolge = value.GetValueOrDefault(0);
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public IEnumerable<SchnurModel> Schnuere => alleSichtbarenSchnuere;
        public IEnumerable<SchnurModel> SchnuereZusatz => alleSchnuere;


        public SchnurModel Hauptteil
        {
            get
            {
                SchnurModel schnur = alleSichtbarenSchnuere.FirstOrDefault(s => s.ID == Data.HauptteilID);
                if (alleSichtbarenSchnuere.Count == 0)
                {
                    schnur = new SchnurModel();
                }
                else if (schnur == null)
                { 
                    schnur = alleSichtbarenSchnuere.First();
                }
                return schnur;
            }
            set
            {
                if (RequestIsWorking || (Data.HauptteilID != value.ID))
                {
                    Data.HauptteilID = value.ID;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public SchnurModel Zusatz
        {
            get { return alleSchnuere.FirstOrDefault(s => s.ID == Data.ZusatzID); }
            set
            {
                if (RequestIsWorking || (Data.ZusatzID != value.ID))
                {
                    Data.ZusatzID = value.ID;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Validierung

        private bool ValidateBezeichnung(string bezeichnung)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(bezeichnung, "", out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Bezeichnung", validationErrors);
            return isValid;
        }

        private bool ValidateZahl(int? anzahl, string fieldname)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateInteger(anzahl, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, fieldname, validationErrors);
            return isValid;
        }
        #endregion

        public override void Cleanup()
        {
            Data = new SchnurauszeichnungModel
            {
                ZusatzID = 1
            };
            Bezeichnung = "";
            Rangfolge = null;
            state = State.Neu;
        }
    }
}
