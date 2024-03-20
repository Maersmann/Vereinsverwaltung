using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;

using CommunityToolkit.Mvvm.Messaging;
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
using Base.Logic.Wrapper;
using CommunityToolkit.Mvvm.Input;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselRueckgabeStammdatenViewModel : ViewModelStammdaten<SchluesselRueckgabeStammdatenModel, StammdatenTypes>
    {
        private SchluesselzuteilungTypes typ;
        private int id;
        private bool schluesselAusgewaehlt;

        public SchluesselRueckgabeStammdatenViewModel() : base()
        {
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
        public string SchluesselBez => Data.Schluesselbezeichnung;

        public string Schluesselbesitzer => Data.SchluesselbesitzerName;

        public int? Anzahl
        {
            get => Data.Anzahl;
            set
            {
                if (!string.Equals(Data.Anzahl, value))
                {
                    ValidateAnzahl(value, "Anzahl");
                    Data.Anzahl = value.GetValueOrDefault(0);
                    this.OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }   
        }
        public DateTime? RueckgabeAm
        {
            get => Data.RueckgabeAm;
            set
            {

                if (!Equals(Data.RueckgabeAm, value))
                { 
                    Data.RueckgabeAm = value.GetValueOrDefault(DateTime.Now);
                    ValidateDatum(Data.RueckgabeAm);
                    OnPropertyChanged();
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
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL+ $"/api/schluesselverwaltung/rueckgabe", Data);

                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Rückgabe gespeichert" }, GetStammdatenTyp().ToString());
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), StammdatenTypes.schluesselzuteilung.ToString());
                }
                else if ((int)resp.StatusCode == 907)
                {
                    SendExceptionMessage("Es werden zu viele Schlüssel zurückgegeben");
                }
                else
                {
                    SendExceptionMessage("Schlüsselrückgabe konnte nicht gespeichert werden.");
                }
                RequestIsWorking = false;

            }
        }


        private void ExecuteOpenAuswahlSchluesselzuteilungCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenSchluesselzuteilungAuswahlMessage(OpenSchluesselzuteilungAuswahlCallback, id, typ), "SchluesselRueckgabeStammdaten");
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
                        Response<SchluesselzuteilungModel> resData = await resp.Content.ReadAsAsync<Response<SchluesselzuteilungModel>>();
                        Data.SchluesselzuteilungID = id;
                        Data.Schluesselbezeichnung = resData.Data.SchluesselBezeichnung;
                        Data.SchluesselbesitzerName = resData.Data.SchluesselbesitzerName;
                        Anzahl = resData.Data.Anzahl;
                        OnPropertyChanged(nameof(SchluesselBez));
                        OnPropertyChanged(nameof(Schluesselbesitzer));
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

        protected override void OnActivated()
        {
            Data = new SchluesselRueckgabeStammdatenModel { };
            RueckgabeAm = DateTime.Now;
            Anzahl = null;
            state = State.Neu;
            schluesselAusgewaehlt = false;
        }
    }
}
