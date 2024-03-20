using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using Data.Types.MitgliederTypes;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class SchiessgruppeStammdatenViewModel : ViewModelStammdaten<SchiessgruppeModel, StammdatenTypes>, IViewModelStammdaten
    {
        public SchiessgruppeStammdatenViewModel()
        {
            Title = "Schiessgruppen Stammdaten";
        }

        public async void ZeigeStammdatenAnAsync(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/Schiessgruppen/{id}");
                if (resp.IsSuccessStatusCode)
                {
                    Response = await resp.Content.ReadAsAsync<Response<SchiessgruppeModel>>();
                }
            }

            Name = Data.Name;
            Geschlecht = Data.Geschlecht;
            state = State.Bearbeiten;
            RequestIsWorking = false;

        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schiessgruppe;

        #region Bindings
        public string Name
        {
            get => Data.Name;
            set
            {
                if (RequestIsWorking || !Equals(Data.Name, value))
                {
                    ValidateName(value);
                    Data.Name = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public static IEnumerable<Geschlecht> Geschlechter => Enum.GetValues(typeof(Geschlecht)).Cast<Geschlecht>();
        public Geschlecht Geschlecht
        {
            get => Data.Geschlecht;
            set
            {
                if (RequestIsWorking || (Data.Geschlecht != value))
                {
                    Data.Geschlecht = value;
                    OnPropertyChanged();
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
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Schiessgruppen", Data);

                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp().ToString());
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if ((int)resp.StatusCode == 911)
                {
                    SendExceptionMessage("Der Name ist schon vergeben");
                }
                else
                {
                    SendExceptionMessage("Schiessgruppe konnte nicht gespeichert werden.");
                }
                RequestIsWorking = false;

            }
        }

        #endregion

        #region Validierung
        private bool ValidateName(string name)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(name, "", out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Name", validationErrors);
            return isValid;
        }
        #endregion


        protected override void OnActivated()
        {
            Data = new SchiessgruppeModel();
            Name = "";
            state = State.Neu;
        }
    }
}
