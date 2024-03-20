using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.MitgliederModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using Data.Types.MitgliederTypes;

using CommunityToolkit.Mvvm.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.AuswahlMessages;
using Logic.Messages.BaseMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class SchuetzeStammdatenViewModel : ViewModelStammdaten<SchuetzeModel, StammdatenTypes>, IViewModelStammdaten
    {
        public SchuetzeStammdatenViewModel()
        {
            messageToken = "SchuetzeStammdaten";
            Title = "Schützen Stammdaten";
            MitgliedHinterlegenCommand = new RelayCommand(() => ExcecuteMitgliedHinterlegenCommand());
            DeleteMitgliedDataCommand = new DelegateCommand(ExecuteDeleteMitgliedDataCommand, CanExecuteDeleteMitgliedDataCommand);
        }

        public async void ZeigeStammdatenAnAsync(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/Schuetzen/{id}");
                if (resp.IsSuccessStatusCode)
                {
                    Response = await resp.Content.ReadAsAsync<Response<SchuetzeModel>>();
                }
            }

            Name = Data.Name;
            Sportschuetze = Data.Sportschuetze;
            Geburtstag = Data.Geburtstag;
            Geschlecht = Data.Geschlecht;
            ((DelegateCommand)DeleteMitgliedDataCommand).RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(KeinMitgliedHinterlegt));
            OnPropertyChanged(nameof(Mitgliedsnr));
            state = State.Bearbeiten;
            RequestIsWorking = false;

        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schuetze;

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
                    Geburtstag = Data.Geburtstag;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? Geburtstag
        {
            get => Data.Geburtstag;
            set
            {
                if (Data.Geschlecht.Equals(Geschlecht.maennlich))
                {
                    ValidateGeburtstag(value);
                }
                else
                {
                    DeleteValidateInfo("Geburtstag");
                }
                if (RequestIsWorking || !Equals(Data.Geburtstag, value))
                {
                    Data.Geburtstag = value;
                    OnPropertyChanged();
                }
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }
        public bool Sportschuetze
        {
            get => Data.Sportschuetze;
            set
            {
                if (RequestIsWorking || !Equals(Data.Name, value))
                {
                    Data.Sportschuetze = value;
                    OnPropertyChanged();
                }
            }
        }
        public int? Mitgliedsnr => Data.MitgliedsNr;
        public bool KeinMitgliedHinterlegt { get { return !Data.MitgliedID.HasValue; } }
        public ICommand MitgliedHinterlegenCommand { get; set; }
        public ICommand DeleteMitgliedDataCommand { get; set; }
        #endregion

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Schuetzen", Data);
                RequestIsWorking = false;

                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp().ToString());
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if ((int)resp.StatusCode == 912)
                {
                    SendExceptionMessage("Mitglied ist schon vergeben");
                }
                else
                {
                    SendExceptionMessage("Schütze konnte nicht gespeichert werden.");
                }
            }
        }

        private void ExcecuteMitgliedHinterlegenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenMitgliedAuswahlMessage(OpenMitgliedAuswahlCallback), messageToken);
        }

        private async void OpenMitgliedAuswahlCallback(bool confirmed, int id)
        {
            if (confirmed)
            {
                if (GlobalVariables.ServerIsOnline)
                {
                    RequestIsWorking = true;
                    HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/Schuetzen/MitgliedVergeben?id={id}");
                    if (resp.IsSuccessStatusCode)
                    {
                        if (await resp.Content.ReadAsAsync<bool>())
                        {
                            SendInformationMessage("Mitglied hat schon ein Datensatz");
                            RequestIsWorking = false;
                            return;
                        }
                    }

                    resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/Mitglieder/{id}");

                    if (resp.IsSuccessStatusCode)
                    {
                        Response<MitgliederModel> Mitglied = await resp.Content.ReadAsAsync<Response<MitgliederModel>>();
                        Data.MitgliedID = id;
                        Name = Mitglied.Data.Vorname + " " + Mitglied.Data.Name;
                        Geburtstag = Mitglied.Data.Geburtstag;
                        Geschlecht = Mitglied.Data.Geschlecht;
                        Data.MitgliedsNr = Mitglied.Data.Mitgliedsnr;
                        ((DelegateCommand)DeleteMitgliedDataCommand).RaiseCanExecuteChanged();
                        OnPropertyChanged(nameof(KeinMitgliedHinterlegt));
                        OnPropertyChanged(nameof(Mitgliedsnr));
                    }
                    RequestIsWorking = false;
                }
            }
        }

        private bool CanExecuteDeleteMitgliedDataCommand()
        {
            return !KeinMitgliedHinterlegt;
        }

        private void ExecuteDeleteMitgliedDataCommand()
        {
            Data.MitgliedID = null;
            Data.MitgliedsNr = null;
            Geburtstag = null;
            Geschlecht = Geschlecht.maennlich;
            ((DelegateCommand)DeleteMitgliedDataCommand).RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(Mitgliedsnr));
            Name = "";
            OnPropertyChanged(nameof( KeinMitgliedHinterlegt));
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

        private bool ValidateGeburtstag(DateTime? geburtstag)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateDatum(geburtstag, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Geburtstag", validationErrors);

            return isValid;
        }
        #endregion


        protected override void OnActivated()
        {
            Data = new SchuetzeModel { };
            Name = "";
            Geburtstag = null;
            Sportschuetze = false;
            state = State.Neu;
        }
    }
}