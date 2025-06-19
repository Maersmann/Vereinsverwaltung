using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using Base.Logic.Wrapper;
using Data.Model.MitgliederModels;
using Data.Model.SchnurrschiessenModels;
using Data.Model.SchnurrschiessenModels.DTO;
using Data.Types;
using Data.Types.MitgliederTypes;
using Data.Types.SchnurschiessenTypes;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.BaseMessages;
using Logic.Messages.SchnurschiessenMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace Logic.UI.SchnurschiessenViewModels
{
    public class AktivesSchnurschiessenTeilnahmeEintragenViewModel : ViewModelStammdaten<AktivesSchnurschiessenTeilnahmeEintragenModel, StammdatenTypes>, IViewModelStammdaten
    {
        private AktivesSchnurschiessenTeilnahmeEintragenRueckgabeModel selectedItem;
        public AktivesSchnurschiessenTeilnahmeEintragenViewModel()
        {
            Title = "Teilnahme eintragen";
            ZurueckgegebenCommand = new RelayCommand(() => ExecuteZurueckgegebenCommand());
            BeschaedigtCommand = new RelayCommand(() => ExecuteBeschaedigtCommand());
            VerlorenCommand = new RelayCommand(() => ExecuteVerlorenCommand());
            RueckgaengigCommand = new RelayCommand(() => ExecuteRueckgaengigCommand());


        }

        public async void ZeigeStammdatenAnAsync(int id)
        {
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                    HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/SchnurschiessenMitglied/AktuellenStand?mitgliedID={id}");
                if (resp.IsSuccessStatusCode)
                    Response = await resp.Content.ReadAsAsync<Response<AktivesSchnurschiessenTeilnahmeEintragenModel>>();
            }
            OnPropertyChanged(nameof(AlterRang));
            OnPropertyChanged(nameof(NeuerRang));
            OnPropertyChanged(nameof(ZuerhalteneAuszeichnung));
            OnPropertyChanged(nameof(Rueckgabe));
            OnPropertyChanged(nameof(CanAusgeben));
            OnPropertyChanged(nameof(CanAusgebenVisbility));
            Title = "Teilname eintragen für" + Data.Name;
            RequestIsWorking = false;
        }


        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schnurschiessen;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;

                if (!Data.AuszeichnungNichtAusgegeben && !Data.AuszeichnungAusgegeben)
                {
                    SendInformationMessage("Information ob die Schnur ausgegegeben/nicht ausgegeben wurde fehlt!");
                    RequestIsWorking = false;
                    return;
                }
                
                var DTO = new SchnurschiessenErfolgreicheTeilnahmeDTO
                {
                    AuszeichnungAusgegeben = Data.AuszeichnungAusgegeben,
                    MitgliedID = Data.MitgliedID,
                    NeuerRangID = Data.NeuerRangID,

                };
                Data.Auszeichnungen.ToList().ForEach(auszeichnung =>
                {
                    DTO.SchnurauszeichnungRueckgaben.Add(new SchnurauszeichnungRueckgabeDTO
                    {
                        ID = auszeichnung.ID,
                        RueckgabeTyp = auszeichnung.RueckgabeTyp
                    });
                });

                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Schnurschiessen/ErfolgreicheTeilnahme", DTO);

                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Teilnahme eingetragen" }, GetStammdatenTyp().ToString());
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if (resp.StatusCode.Equals(HttpStatusCode.Conflict))
                {
                    SendExceptionMessage(await resp.Content.ReadAsStringAsync());
                }
                else if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Teilnahme konnte nicht eingetragen werden.");
                }
                RequestIsWorking = false;

            }
        }

        private void ExecuteZurueckgegebenCommand()
        {
            selectedItem.RueckgabeTyp = SchnurrauszeichnungRueckgabeTyp.RueckgabeErfolgt;
            OnPropertyChanged(nameof(Rueckgabe));
            OnPropertyChanged(nameof(CanAusgeben));
            OnPropertyChanged(nameof(CanAusgebenVisbility));
        }

        private void ExecuteBeschaedigtCommand()
        {
            selectedItem.RueckgabeTyp = SchnurrauszeichnungRueckgabeTyp.beschaedigt;
            OnPropertyChanged(nameof(Rueckgabe));
            OnPropertyChanged(nameof(CanAusgeben));
            OnPropertyChanged(nameof(CanAusgebenVisbility));
        }

        private void ExecuteVerlorenCommand()
        {
            selectedItem.RueckgabeTyp = SchnurrauszeichnungRueckgabeTyp.verloren;
            OnPropertyChanged(nameof(Rueckgabe));
            OnPropertyChanged(nameof(CanAusgeben));
            OnPropertyChanged(nameof(CanAusgebenVisbility));
        }

        private void ExecuteRueckgaengigCommand()
        {
            selectedItem.RueckgabeTyp = SchnurrauszeichnungRueckgabeTyp.RueckgabeOffen;
            Data.AuszeichnungAusgegeben = false;
            Data.AuszeichnungNichtAusgegeben = false;
            OnPropertyChanged(nameof(Rueckgabe));
            OnPropertyChanged(nameof(CanAusgeben));
            OnPropertyChanged(nameof(AuszeichnungAusgegeben));
            OnPropertyChanged(nameof(AuszeichnungNichtAusgegeben));
            OnPropertyChanged(nameof(CanAusgebenVisbility));
        }
        #endregion

        #region Bindings

        public ICommand ZurueckgegebenCommand { get; set; }
        public ICommand BeschaedigtCommand { get; set; }
        public ICommand VerlorenCommand { get; set; }
        public ICommand RueckgaengigCommand { get; set; }


        public string AlterRang => Data.AlterRang;
        public string NeuerRang => Data.NeuerRang;
        public string ZuerhalteneAuszeichnung => Data.ZuerhalteneAuszeichung;

        public ObservableCollection<AktivesSchnurschiessenTeilnahmeEintragenRueckgabeModel> Rueckgabe
        {
            get => Data.Auszeichnungen;
            set
            {
                if (RequestIsWorking)
                {
                    Data.Auszeichnungen = value;
                    base.OnPropertyChanged();
                }
            }
        }
        public bool AuszeichnungAusgegeben
        {
            get => Data.AuszeichnungAusgegeben;
            set
            {

                if (RequestIsWorking || !Equals(Data.AuszeichnungAusgegeben, value))
                {
                    Data.AuszeichnungAusgegeben = value;
                    Data.AuszeichnungNichtAusgegeben = !value;
                    base.OnPropertyChanged();
                    OnPropertyChanged(nameof(Rueckgabe));
                    OnPropertyChanged(nameof(AuszeichnungNichtAusgegeben));
                }
            }
        }

        public bool AuszeichnungNichtAusgegeben
        {
            get => Data.AuszeichnungNichtAusgegeben;
            set
            {

                if (RequestIsWorking || !Equals(Data.AuszeichnungNichtAusgegeben, value))
                {
                    Data.AuszeichnungNichtAusgegeben = value;
                    Data.AuszeichnungAusgegeben = !value;
                    base.OnPropertyChanged();
                    OnPropertyChanged(nameof(Rueckgabe));
                    OnPropertyChanged(nameof(AuszeichnungAusgegeben));

                }
            }
        }

        public bool CanAusgebenVisbility => Data.Auszeichnungen != null && Data.Auszeichnungen.FirstOrDefault(ausz => ausz.RueckgabeTyp.Equals(SchnurrauszeichnungRueckgabeTyp.RueckgabeOffen)) != null;

        public bool CanAusgeben => Data.DarfNaechstenAusgeben && Data.Auszeichnungen != null && Data.Auszeichnungen.FirstOrDefault(ausz => ausz.RueckgabeTyp.Equals(SchnurrauszeichnungRueckgabeTyp.RueckgabeOffen)) == null;
        public virtual AktivesSchnurschiessenTeilnahmeEintragenRueckgabeModel SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }
        #endregion


        protected override void OnActivated()
        {
            Data = new AktivesSchnurschiessenTeilnahmeEintragenModel();
            state = State.Neu;
        }
    }
}
