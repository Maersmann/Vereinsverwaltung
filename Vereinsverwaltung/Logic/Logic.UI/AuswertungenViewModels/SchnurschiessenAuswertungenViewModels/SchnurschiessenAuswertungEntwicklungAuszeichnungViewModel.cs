using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.AuswertungModels.SchnurschiessenAuswertungModels;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.AuswahlMessages;

namespace Logic.UI.AuswertungenViewModels.SchnurschiessenAuswertungenViewModels
{
    public class SchnurschiessenAuswertungEntwicklungAuszeichnungViewModel : ViewModelAuswertung<SchnurschiessenAuswertungEntwicklungAuszeichnungModel>
    {

        public SchnurschiessenAuswertungEntwicklungAuszeichnungViewModel()
        {
            Title = "Entwicklung - Auszeichnung";
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
            Messenger.Default.Send(new OpenSchnurschiessenAuszeichungMessage(ExcecuteLoadDataCallback), "SchnurschiessenAuswertungEntwicklung");
        }

        private async void ExcecuteLoadDataCallback(bool confirmed, int auszeichungID)
        {
            if (confirmed)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/schnurschiessen/Entwicklung/Auszeichnung?schnurschiessenAuszeichungID={auszeichungID}");
                if (resp.IsSuccessStatusCode)
                {
                    ItemList = await resp.Content.ReadAsAsync<List<SchnurschiessenAuswertungEntwicklungAuszeichnungModel>>();
                    SetDataIntoChart();
                }
                RequestIsWorking = false;
            }
        }

        private void SetDataIntoChart()
        {
            Labels = new string[ItemList.Count];
            int index = 0;
            ItemList.ToList().ForEach(a =>
            {
                Labels[index] = a.Jahr;
                index++;
            });
            var auswertungSeries = new LineSeries<SchnurschiessenAuswertungEntwicklungAuszeichnungModel>
            {
                Values = ItemList,
                Mapping = (model, index) => new LiveChartsCore.Kernel.Coordinate(index, (double)model.Anzahl),
                Name = "Anzahl",
            };

            XAxes.First().Labels = Labels;
            XAxes.First().Name = "Jahr";
            YAxes.First().Name = "Anzahl";

            Series = new LineSeries<SchnurschiessenAuswertungEntwicklungAuszeichnungModel>[1] { auswertungSeries };

            RaisePropertyChanged(nameof(Series));
            RaisePropertyChanged(nameof(XAxes));
            RaisePropertyChanged(nameof(YAxes));
        }

        #region Bindings
        public ICommand AuswahlCommand { get; set; }
        #endregion
    }
}
