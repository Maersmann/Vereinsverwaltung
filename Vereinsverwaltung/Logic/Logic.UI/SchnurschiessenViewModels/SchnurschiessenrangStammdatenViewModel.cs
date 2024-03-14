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
using Data.Types.SchnurschiessenTypes;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurschiessenrangStammdatenViewModel : ViewModelStammdaten<SchnurschiessenrangModel, StammdatenTypes>, IViewModelStammdaten
    {
        private IList<SchnurschiessenAuszeichnungModel> alleAuszeichnungen;
        public SchnurschiessenrangStammdatenViewModel()
        {
            alleAuszeichnungen = new List<SchnurschiessenAuszeichnungModel>();
            Title = "Schnurschiessen Rang Stammdaten";
            LoadAuszeichnungen();      
        }

        private async void LoadAuszeichnungen()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Schnurschiessenauszeichnung");
                if (resp.IsSuccessStatusCode)
                { 
                    var ResponseSchnur = await resp.Content.ReadAsAsync<Response<ObservableCollection<SchnurschiessenAuszeichnungModel>>>();
                    alleAuszeichnungen = ResponseSchnur.Data;
                }
                RequestIsWorking = false;
            }
            RaisePropertyChanged("Auszeichnungen");
        }

        public async void ZeigeStammdatenAnAsync(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/Schnurschiessenrang/{id}");
                if (resp.IsSuccessStatusCode)
                    Response = await resp.Content.ReadAsAsync<Response<SchnurschiessenrangModel>>();
            }
            RequestIsWorking = true;
            Bezeichnung = Data.Bezeichnung;
            NeueStufe = Data.NeueStufe;
            Rang = Data.Rang;
            DarfAuszeichnungBehalten = Data.DarfAuszeichnungBehalten;
            state = State.Bearbeiten;
            RequestIsWorking = false;
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schnurschiessenRang;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/Schnurschiessenrang", Data);
                RequestIsWorking = false;
                
                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if ((int)resp.StatusCode == 909)
                {
                    SendExceptionMessage("Rang ist schon vorhanden");
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

        public int? Rang
        {
            get => Data.Rang;
            set
            {
                if (RequestIsWorking || !Equals(Data.Rang, value))
                {
                    ValidateZahl(value, "Rang");
                    Data.Rang = value.GetValueOrDefault(0);
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public bool NeueStufe
        {
            get => Data.NeueStufe;
            set
            {
                if (RequestIsWorking || !Equals(Data.NeueStufe, value))
                {
                    Data.NeueStufe = value;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public bool DarfAuszeichnungBehalten
        {
            get => Data.DarfAuszeichnungBehalten;
            set
            {
                if (RequestIsWorking || !Equals(Data.DarfAuszeichnungBehalten, value))
                {
                    Data.DarfAuszeichnungBehalten = value;
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public IEnumerable<SchnurschiessenAuszeichnungModel> Auszeichnungen => alleAuszeichnungen;


        public SchnurschiessenAuszeichnungModel Auszeichnung
        {
            get
            {
                SchnurschiessenAuszeichnungModel auszeichnung = alleAuszeichnungen.FirstOrDefault(s => s.ID == Data.AuszeichnungID);
                if (alleAuszeichnungen.Count == 0)
                {
                    auszeichnung = new SchnurschiessenAuszeichnungModel();
                }
                else auszeichnung ??= alleAuszeichnungen.First();
                return auszeichnung;
            }
            set
            {
                if (RequestIsWorking || (Data.AuszeichnungID != value.ID))
                {
                    Data.AuszeichnungID = value.ID;
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
            Data = new SchnurschiessenrangModel { };
            Bezeichnung = "";
            Rang = null;
            state = State.Neu;
        }
    }
}
