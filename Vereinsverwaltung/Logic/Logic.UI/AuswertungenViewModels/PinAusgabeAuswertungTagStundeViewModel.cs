using Data.Model.AuswertungModels.PinAusgabeAuswertungModels;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Messages.AuswahlMessages;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.AuswertungenViewModels
{
    public class PinAusgabeAuswertungTagStundeViewModel : ViewModelAuswertung<PinAusgabeAuswertungTagModel>
    {

        public PinAusgabeAuswertungTagStundeViewModel()
        {
            Data = new PinAusgabeAuswertungTagModel();
            SecondTitle = "";
            Title = "Auswertung Tag/Stunde";
            AuswahlCommand = new RelayCommand(() => ExcecuteAuswahlCommand());
        }

        private void ExcecuteAuswahlCommand()
        {
            Messenger.Default.Send<OpenPinAusgabeAuswahlMessage>(new OpenPinAusgabeAuswahlMessage(LoadAuswertungCallback), "PinAusgabeAuswertungTagStunde");
        }

        public async void LoadAuswertungCallback(bool confirmed, int id)
        {
            if (confirmed && GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/auswertungen/pinausgabe/TagStunde/{id}");
                if (resp.IsSuccessStatusCode)
                {
                    Data = await resp.Content.ReadAsAsync<PinAusgabeAuswertungTagModel>();
                    SecondTitle = "Auswertung von: " + Data.Bezeichnung;
                    this.RaisePropertyChanged(nameof(SecondTitle));
                    this.RaisePropertyChanged(nameof(Data));
                    this.RaisePropertyChanged(nameof(Pin));
                    this.RaisePropertyChanged(nameof(Abgeschlossen));
                    this.RaisePropertyChanged(nameof(Verteilt));
                    this.RaisePropertyChanged(nameof(Offen));
                }
                    
            }
        }
        #region Bindings
        public string SecondTitle { get; set; }
        public string Pin => Data.PinBezeichnung;
        public string Abgeschlossen
        {
            get
            {
                if (Data.Bezeichnung.Equals("")) return "";
                else if (Data.Abgeschlossen) return "Ja";
                else return  "Nein";
            }
        }
        public int Verteilt => Data.Verteilt;
        public int Offen => Data.Offen;
        public ICommand AuswahlCommand { get; set; }

        #endregion
    }
}
