using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using CommunityToolkit.Mvvm.Messaging;
using Data.Model.SchuetzenfestModels;
using Data.Model.SchuetzenfestModels.DTO;
using Data.Types;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.SchuetzenfestViewModels
{
    public class SchuetzenfestZahlenStammdatenViewModel : ViewModelStammdaten<SchuetzenfestZahlenModel, StammdatenTypes>, IViewModelStammdaten
    {
        public SchuetzenfestZahlenStammdatenViewModel()
        {
            Title = "Stammdaten Zalen Schützenfest";
        }

        public async void ZeigeStammdatenAnAsync(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/SchuetzenfestZahlen/{id}");
                if (resp.IsSuccessStatusCode)
                    Response = await resp.Content.ReadAsAsync<Response<SchuetzenfestZahlenModel>>();
            }
            BaendchenSamstagGaeste = Data.BaendchenSamstagGaeste;
            BaendchenSamstagMitglieder = Data.BaendchenSamstagMitglieder;
            BaendchenMontagGaeste = Data.BaendchenMontagGaeste;
            BaendchenMontagMitglieder = Data.BaendchenMontagMitglieder;

            AnzahlUmzugMontagVormittag = Data.AnzahlUmzugMontagVormittag;
            AnzahlUmzugMontagNachmittag = Data.AnzahlUmzugMontagNachmittag;
            AnzahlUmzugSonntag = Data.AnzahlUmzugSonntag;

            Jahr = Data.Jahr;

            state = State.Bearbeiten;
            RequestIsWorking = false;
        }
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schuetzenfestZahlen;
        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/SchuetzenfestZahlen", new SchuetzenfestZahlenDTO
                {
                    AnzahlUmzugMontagNachmittag= Data.AnzahlUmzugMontagNachmittag,
                    AnzahlUmzugMontagVormittag = Data.AnzahlUmzugMontagVormittag,
                    AnzahlUmzugSonntag = Data.AnzahlUmzugSonntag,
                    BaendchenMontagGaeste = Data.BaendchenMontagGaeste,
                    BaendchenMontagMitglieder = Data.BaendchenMontagMitglieder,
                    BaendchenSamstagGaeste = Data.BaendchenSamstagGaeste,
                    BaendchenSamstagMitglieder = Data.BaendchenSamstagMitglieder,
                    ID = Data.ID,
                    Jahr = Data.Jahr
                });

                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp().ToString());
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else
                {
                    SendExceptionMessage("Zahlen konnte nicht gespeichert werden.");
                }
                RequestIsWorking = false;
            }
        }
        #endregion

        #region Bindings
        public int? AnzahlUmzugSonntag
        {
            get => Data.AnzahlUmzugSonntag;
            set
            {
                if (RequestIsWorking || !Equals(Data.AnzahlUmzugSonntag, value))
                {
                    Data.AnzahlUmzugSonntag = value;
                    base.OnPropertyChanged();
                }
            }
        }

        public int? AnzahlUmzugMontagNachmittag
        {
            get => Data.AnzahlUmzugMontagNachmittag;
            set
            {
                if (RequestIsWorking || !Equals(Data.AnzahlUmzugMontagNachmittag, value))
                {
                    Data.AnzahlUmzugMontagNachmittag = value;
                    base.OnPropertyChanged();
                }
            }
        }

        public int? AnzahlUmzugMontagVormittag
        {
            get => Data.AnzahlUmzugMontagVormittag;
            set
            {
                if (RequestIsWorking || !Equals(Data.AnzahlUmzugMontagVormittag, value))
                {
                    Data.AnzahlUmzugMontagVormittag = value;
                    base.OnPropertyChanged();
                }
            }
        }

        public int? BaendchenMontagMitglieder
        {
            get => Data.BaendchenMontagMitglieder;
            set
            {
                if (RequestIsWorking || !Equals(Data.BaendchenMontagMitglieder, value))
                {
                    Data.BaendchenMontagMitglieder = value;
                    base.OnPropertyChanged();
                }
            }
        }

        public int? BaendchenMontagGaeste
        {
            get => Data.BaendchenMontagGaeste;
            set
            {
                if (RequestIsWorking || !Equals(Data.BaendchenMontagGaeste, value))
                {
                    Data.BaendchenMontagGaeste = value;
                    base.OnPropertyChanged();
                }
            }
        }

        public int? BaendchenSamstagGaeste
        {
            get => Data.BaendchenSamstagGaeste;
            set
            {
                if (RequestIsWorking || !Equals(Data.BaendchenSamstagGaeste, value))
                {
                    Data.BaendchenSamstagGaeste = value;
                    base.OnPropertyChanged();
                }
            }
        }

        public int? BaendchenSamstagMitglieder
        {
            get => Data.BaendchenSamstagMitglieder;
            set
            {
                if (RequestIsWorking || !Equals(Data.BaendchenSamstagMitglieder, value))
                {
                    Data.BaendchenSamstagMitglieder = value;
                    base.OnPropertyChanged();
                }
            }
        }

        public int? Jahr
        {
            get => Data.Jahr;
            set
            {
                if (RequestIsWorking || !string.Equals(Data.Jahr, value))
                {
                    ValidateAnzahl(value, "Jahr");
                    Data.Jahr = value.GetValueOrDefault(0);
                    this.OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
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
        #endregion

        protected override void OnActivated()
        {
            Data = new SchuetzenfestZahlenModel();
            OnPropertyChanged();
            ValidateAnzahl(null, "Jahr");
            state = State.Neu;
            base.OnActivated();
        }
    }
}
