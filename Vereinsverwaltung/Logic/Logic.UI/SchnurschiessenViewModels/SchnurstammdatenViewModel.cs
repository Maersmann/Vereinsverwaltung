using Data.Model.SchnurrschiessenModels;
using Data.Types;
using Data.Types.SchnurschiessenTypes;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.UI.BaseViewModels;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class SchnurStammdatenViewModel : ViewModelStammdaten<SchnurModel>, IViewModelStammdaten
    {
        public SchnurStammdatenViewModel()
        {
            Title = "Schnur Stammdaten";
        }

        public async void ZeigeStammdatenAn(int id)
        {
            LoadData = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp2 = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/schnur/{id}");
                if (resp2.IsSuccessStatusCode)
                    data = await resp2.Content.ReadAsAsync<SchnurModel>();
            }
            Bezeichnung = data.Bezeichnung;
            Schnurtyp = data.Schnurtyp;
            state = State.Bearbeiten;
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schnur;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/schnurschiessen/schnur", data);

                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), GetStammdatenTyp());
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage("Nummer ist schon vorhanden");
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
                if ( LoadData || !string.Equals(data.Bezeichnung, value))
                {
                    ValidateBezeichnung(value);
                    data.Bezeichnung = value;
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public IEnumerable<Schnurtypes> Schnurtypes
        {
            get
            {
                return Enum.GetValues(typeof(Schnurtypes)).Cast<Schnurtypes>();
            }
        }
        public Schnurtypes Schnurtyp
        {
            get { return data.Schnurtyp; }
            set
            {
                if (LoadData || (this.data.Schnurtyp != value))
                {
                    this.data.Schnurtyp = value;
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
        #endregion

        public override void Cleanup()
        {
            data = new SchnurModel();
            Bezeichnung = "";
            Schnurtyp = Data.Types.SchnurschiessenTypes.Schnurtypes.schnur;
            state = State.Neu;
        }
    }
}
