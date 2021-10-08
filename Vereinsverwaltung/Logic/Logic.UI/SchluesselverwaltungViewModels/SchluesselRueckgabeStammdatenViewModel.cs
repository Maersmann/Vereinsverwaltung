using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Core.Validierungen.Base;
using Logic.Messages.AuswahlMessages;
using Logic.Messages.BaseMessages;
using Base.Logic.ViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselRueckgabeStammdatenViewModel : ViewModelStammdaten<SchluesselRueckgabeStammdatenModel, StammdatenTypes>
    {
        private SchluesselzuteilungTypes typ;
        private int id;
        private bool schluesselAusgewaehlt;

        public SchluesselRueckgabeStammdatenViewModel() : base()
        {
            Cleanup();
            Title = "Schlüsselrückgabe";
            OpenAuswahlSchluesselzuteilungCommand = new RelayCommand(() => ExecuteOpenAuswahlSchluesselzuteilungCommand());
        }

        public void SetInformation(int id, SchluesselzuteilungTypes typ)
        {
            RueckgabeAm = DateTime.Now;
            this.typ = typ;
            this.id = id;
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schluesselrueckgabe;

        #region Bindings
        public String SchluesselBez => data.Schluesselbezeichnung;

        public String Schluesselbesitzer => data.SchluesselbesitzerName;

        public int? Anzahl
        {
            get => data.Anzahl;
            set
            {
                if (!string.Equals(data.Anzahl, value))
                {
                    ValidateAnzahl(value, "Anzahl");
                    data.Anzahl = value.GetValueOrDefault(0);
                    this.RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }   
        }
        public DateTime? RueckgabeAm
        {
            get => data.RueckgabeAm;
            set
            {

                if (!Equals(data.RueckgabeAm, value))
                { 
                    data.RueckgabeAm = value.GetValueOrDefault(DateTime.Now);
                    ValidateDatum(data.RueckgabeAm);
                    RaisePropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand OpenAuswahlSchluesselzuteilungCommand { get; set; }
        #endregion

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/rueckgabe", data);
                RequestIsWorking = false;

                if (resp.IsSuccessStatusCode)
                {
                    Messenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Rückgabe gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                    Messenger.Default.Send(new AktualisiereViewMessage(), StammdatenTypes.schluesselzuteilung.ToString());
                }
                else if ((int)resp.StatusCode == 907)
                {
                    SendExceptionMessage("Es werden zu viele Schlüssel zurückgegeben");
                }
                else
                {
                    SendExceptionMessage("Schlüsselrückgabe konnte nicht gespeichert werden.");
                }
            }
        }


        private void ExecuteOpenAuswahlSchluesselzuteilungCommand()
        {
            Messenger.Default.Send<OpenSchluesselzuteilungAuswahlMessage>(new OpenSchluesselzuteilungAuswahlMessage(OpenSchluesselzuteilungAuswahlCallback, id, typ), "SchluesselRueckgabeStammdaten");
        }

        protected override bool CanExecuteSaveCommand()
        {
            return base.CanExecuteSaveCommand() && schluesselAusgewaehlt;
        }
        #endregion

        #region Callback
        private async void OpenSchluesselzuteilungAuswahlCallback(bool confirmed, int id)
        {
            if (confirmed)
            {
                RequestIsWorking = true;
                if (GlobalVariables.ServerIsOnline)
                {
                    HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/zuteilung/{id}");
                    if (resp.IsSuccessStatusCode)
                    {
                        SchluesselzuteilungModel resData = await resp.Content.ReadAsAsync<SchluesselzuteilungModel>();
                        data.SchluesselzuteilungID = id;
                        data.Schluesselbezeichnung = resData.SchluesselBezeichnung;
                        data.SchluesselbesitzerName = resData.SchluesselbesitzerName;
                        Anzahl = resData.Anzahl;
                        RaisePropertyChanged("SchluesselBez");
                        RaisePropertyChanged("Schluesselbesitzer");
                        schluesselAusgewaehlt = true;
                        ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    }
                }
                RequestIsWorking = false;
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
        private bool ValidateDatum(DateTime? eintrittsdatum)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateDatum(eintrittsdatum, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "RueckgabeAm", validationErrors);

            return isValid;
        }
        #endregion

        public override void Cleanup()
        {
            data = new SchluesselRueckgabeStammdatenModel { };
            RueckgabeAm = DateTime.Now;
            Anzahl = null;
            state = State.Neu;
            schluesselAusgewaehlt = false;
        }
    }
}
