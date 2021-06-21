using Data.Model.SchnurrschiessenModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.UI.BaseViewModels;
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

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurauszeichnungStammdatenViewModel : ViewModelStammdaten<SchnurauszeichnungModel>, IViewModelStammdaten
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
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnur");
                if (resp.IsSuccessStatusCode)
                    alleSchnuere = await resp.Content.ReadAsAsync<ObservableCollection<SchnurModel>>();

                alleSichtbarenSchnuere = alleSchnuere.Where(s => s.Sichtbar).ToList();
            }
            this.RaisePropertyChanged("Schnuere");
            this.RaisePropertyChanged("SchnuereZusatz");
        }

        public async void ZeigeStammdatenAn(int id)
        {
            LoadData = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnurauszeichnung/{id}");
                if (resp.IsSuccessStatusCode)
                    data = await resp.Content.ReadAsAsync<SchnurauszeichnungModel>();
            }
            Bezeichnung = data.Bezeichnung;
            Rangfolge = data.Rangfolge;
            Hauptteil = alleSichtbarenSchnuere.First(s => s.ID == data.HauptteilID);
            Zusatz = alleSchnuere.FirstOrDefault(s => s.ID == data.ZusatzID);
            state = State.Bearbeiten;
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schnurauszeichnung;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/Schnurauszeichnung", data);
                data.HauptteilID = Hauptteil.ID;
                data.ZusatzID = Zusatz.ID;
                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), GetStammdatenTyp());
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage("Rangfolge ist schon vorhanden");
                    return;
                }
            }
        }
        #endregion

        #region Bindings

        public String Bezeichnung
        {
            get
            {
                return data.Bezeichnung;
            }
            set
            {
                if (LoadData || !string.Equals(data.Bezeichnung, value))
                {
                    ValidateBezeichnung(value);
                    data.Bezeichnung = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int? Rangfolge
        {
            get => data.Rangfolge;
            set
            {
                if (LoadData || !string.Equals(data.Rangfolge, value))
                {
                    ValidateZahl(value, "Rangfolge");
                    data.Rangfolge = value.GetValueOrDefault(0);
                    this.RaisePropertyChanged();
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
                var schnur = alleSichtbarenSchnuere.FirstOrDefault(s => s.ID == data.HauptteilID);
                if (alleSichtbarenSchnuere.Count == 0)
                    schnur = new SchnurModel();
                else if (schnur == null)
                    schnur = alleSichtbarenSchnuere.First();
                return schnur;
            }
            set
            {
                if (LoadData || (this.data.HauptteilID != value.ID))
                {
                    this.data.HauptteilID = value.ID;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public SchnurModel Zusatz
        {
            get { return alleSchnuere.FirstOrDefault(s => s.ID == data.ZusatzID); }
            set
            {
                if (LoadData || (this.data.ZusatzID != value.ID))
                {
                    this.data.ZusatzID = value.ID;
                    this.RaisePropertyChanged();
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
            data = new SchnurauszeichnungModel
            {
                ZusatzID = 1
            };
            Bezeichnung = "";
            Rangfolge = null;
            state = State.Neu;
        }
    }
}
