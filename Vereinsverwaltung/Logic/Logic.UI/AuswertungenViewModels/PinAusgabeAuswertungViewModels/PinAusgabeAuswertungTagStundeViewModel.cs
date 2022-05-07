using Data.Model.AuswertungModels.PinAusgabeAuswertungModels;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Messages.AuswahlMessages;
using Base.Logic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Base.Logic.Core;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Measure;

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
            YAxes = new List<Axis>
                {
                    new Axis()
                    {
                        LabelsPaint = new SolidColorPaint{ Color = SKColors.CornflowerBlue },
                        Position = AxisPosition.Start,
                        Labeler = (value) =>  string.Format("{0}", value)
                    }
                };
        }

        private void ExcecuteAuswahlCommand()
        {
            Messenger.Default.Send(new OpenPinAusgabeAuswahlMessage(LoadAuswertungCallback), "PinAusgabeAuswertungTagStunde");
        }

        public async void LoadAuswertungCallback(bool confirmed, int id)
        {
            if (confirmed && GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/auswertungen/pinausgabe/TagStunde/{id}");
                if (resp.IsSuccessStatusCode)
                {
                    Data = await resp.Content.ReadAsAsync<PinAusgabeAuswertungTagModel>();
                    SecondTitle = "Auswertung von: " + Data.Bezeichnung;
                    RaisePropertyChanged(nameof(SecondTitle));
                    RaisePropertyChanged(nameof(Data));
                    RaisePropertyChanged(nameof(Pin));
                    RaisePropertyChanged(nameof(Abgeschlossen));
                    RaisePropertyChanged(nameof(Verteilt));
                    RaisePropertyChanged(nameof(Offen));

                    IList<int> values = new List<int>();
                    Labels = new string[Data.Auswertung.Count];
                    int index = 0;
                    Data.Auswertung.OrderBy(s => s.Tag).ToList().ForEach(a =>
                    {
                        values.Add(a.Anzahl);
                        Labels[index] = a.Tag.ToString("dd.MM HH:mm");
                        index++;
                    });

                    var auswertungSeries = new ColumnSeries<int>
                    {
                        Values = values,
                        Name = "Anzahl",
                        TooltipLabelFormatter = (point) => "Anzahl " + point.PrimaryValue.ToString()
                    };

                    XAxes.First().Labels = Labels;
                    XAxes.First().Name = "Tag";
                    YAxes.First().Name = "Anzahl";

                    Series = new ColumnSeries<int>[1] { auswertungSeries };

                    RaisePropertyChanged(nameof(Series));
                    RaisePropertyChanged(nameof(XAxes));
                    RaisePropertyChanged(nameof(YAxes));             
                }
                RequestIsWorking = false;
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
