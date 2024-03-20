using Data.Model.PinModels;
using Data.Types;

using CommunityToolkit.Mvvm.Messaging;
using Logic.Core;
using Logic.Messages.PinMessages;
using Base.Logic.ViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Base.Logic.Core;
using CommunityToolkit.Mvvm.Input;

namespace Logic.UI.PinViewModels
{
    public class PinAusgabeUebersichtViewModel : ViewModelUebersicht<PinAusgabenUebersichtModel, StammdatenTypes>
    {
        private bool zeigeNurOffene;
        public PinAusgabeUebersichtViewModel()
        {
            Title = "Übersicht Pin Ausgaben";
            RegisterAktualisereViewMessage(StammdatenTypes.pinAusgabe.ToString());
            OeffneAusgabeCommand = new RelayCommand(() => ExecuteOeffneAusgabeCommand());
            ErledigeAusgabeCommand = new RelayCommand(() => ExecuteErledigeAusgabeCommand());
            zeigeNurOffene = true;
            _ = LoadData();
        }
        protected override int GetID() { return SelectedItem.ID; }
        protected override StammdatenTypes GetStammdatenTyp() { return StammdatenTypes.pinAusgabe; }
        protected override string GetREST_API() { return $"/api/Pins/Ausgabe/Uebersicht?nurOffene={zeigeNurOffene}"; }
        protected override bool WithPagination() { return true; }
        protected override bool LoadingOnCreate() => false;

        #region Bindings
        public bool ZeigeNurOffene
        {
            get => zeigeNurOffene;
            set
            {
                zeigeNurOffene = value;
                OnPropertyChanged();

            }
        }
        public ICommand OeffneAusgabeCommand { get; private set; }
        public ICommand ErledigeAusgabeCommand { get; private set; }
        #endregion

        #region Commands


        private void ExecuteOeffneAusgabeCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenPinAusgabeMitgliederViewMessage { ID = SelectedItem.ID, FilterText = FilterText, ZeigeNurOffene = zeigeNurOffene }, "PinAusgabeUebersicht");
        }

        private async void ExecuteErledigeAusgabeCommand()
        {
            SelectedItem.Abgeschlossen = true;
            RequestIsWorking = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Pins/Ausgabe/erledigen", SelectedItem.ID);
                if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Ausgabe konnte nicht abgeschlossen werden.");
                }
            }
            RequestIsWorking = false;

            await LoadData();
        }
        #endregion
    }
}
