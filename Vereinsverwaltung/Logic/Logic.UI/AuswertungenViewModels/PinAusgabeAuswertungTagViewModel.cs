using Data.Model.AuswertungModels.PinAusgabeAuswertungModels;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using Logic.Core;
using Logic.Messages.AuswahlMessages;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.AuswertungenViewModels
{
    public class PinAusgabeAuswertungTagViewModel : ViewModelAuswertung<PinAusgabeAuswertungTagModel>
    {

        public PinAusgabeAuswertungTagViewModel()
        {
            Data = new PinAusgabeAuswertungTagModel();
            SecondTitle = "";
            Title = "Auswertung Tag";
            AuswahlCommand = new RelayCommand(() => ExcecuteAuswahlCommand());
        }

        private void ExcecuteAuswahlCommand()
        {
            Messenger.Default.Send<OpenPinAusgabeAuswahlMessage>(new OpenPinAusgabeAuswahlMessage(LoadAuswertungCallback), "PinAusgabeAuswertungTag");
        }

        public async void LoadAuswertungCallback(bool confirmed, int id)
        {
            if (confirmed && GlobalVariables.ServerIsOnline)
            {
                DataIsLoading = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/auswertungen/pinausgabe/Tag/{id}");
                if (resp.IsSuccessStatusCode)
                {
                    Data = await resp.Content.ReadAsAsync<PinAusgabeAuswertungTagModel>();
                    SecondTitle = "Auswertung von: " + Data.Bezeichnung;
                    RaisePropertyChanged(nameof(SecondTitle));
                    RaisePropertyChanged(nameof(Data));
                    RaisePropertyChanged(nameof(SecondTitle));
                    RaisePropertyChanged(nameof(Data));
                    RaisePropertyChanged(nameof(Pin));
                    RaisePropertyChanged(nameof(Abgeschlossen));
                    RaisePropertyChanged(nameof(Verteilt));
                    RaisePropertyChanged(nameof(Offen));

                    ChartValues<int> values = new ChartValues<int>();
                    Labels = new string[Data.Auswertung.Count];
                    int index = 0;
                    Data.Auswertung.OrderBy(s => s.Tag).ToList().ForEach(a => 
                    { 
                        values.Add(a.Anzahl);
                        Labels[index] = a.Tag.ToString("dd.MM");
                        index++;
                    });
                    SeriesCollection = new SeriesCollection
                    {

                        new ColumnSeries{ Values = values, Title="Anzahl" }
                    };
                    
                    RaisePropertyChanged(nameof(SeriesCollection));
                    RaisePropertyChanged(nameof(Labels));
                    RaisePropertyChanged(nameof(Formatter));
                    DataIsLoading = false;
                }
            }
        }
        

        public string SecondTitle { get; set; }
        public string Pin => Data.PinBezeichnung;
        public string Abgeschlossen
        {
            get
            {
                if (Data.Bezeichnung.Equals("")) return "";
                else if (Data.Abgeschlossen) return "Ja";
                else return "Nein";
            }
        }
        public int Verteilt => Data.Verteilt;
        public int Offen => Data.Offen;
        public ICommand AuswahlCommand { get; set; }
    }
}
