using Data.Model.PinModels;
using Data.Types;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Messages.PinMessages;
using Logic.UI.BaseViewModels;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;

namespace Logic.UI.PinViewModels
{
    public class PinAusgabeUebersichtViewModel : ViewModelUebersicht<PinAusgabenUebersichtModel>
    {

        public PinAusgabeUebersichtViewModel()
        {
            Title = "Übersicht Pin Ausgaben";
            RegisterAktualisereViewMessage(StammdatenTypes.pinAusgabe);
            OeffneAusgabeCommand = new RelayCommand(() => ExecuteOeffneAusgabeCommand());
            ErledigeAusgabeCommand = new RelayCommand(() => ExecuteErledigeAusgabeCommand());
        }
        protected override int GetID() { return selectedItem.ID; }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.pinAusgabe; }
        protected override string GetREST_API() { return $"/api/Pins/Ausgabe/Uebersicht"; }

        #region Bindings
        public ICommand OeffneAusgabeCommand { get; private set; }
        public ICommand ErledigeAusgabeCommand { get; private set; }
        #endregion

        #region Commands


        private void ExecuteOeffneAusgabeCommand()
        {
            Messenger.Default.Send(new OpenPinAusgabeMitgliederViewMessage { ID = selectedItem.ID }, "PinAusgabeUebersicht");
        }

        private async void ExecuteErledigeAusgabeCommand()
        {
            selectedItem.Abgeschlossen = true;
            DataIsLoading = true;
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/Pins/Ausgabe/erledigen", selectedItem.ID);
                if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Ausgabe konnte nicht abgeschlossen werden.");
                }
            }
            DataIsLoading = false;

            LoadData();
        }
        #endregion
    }
}
