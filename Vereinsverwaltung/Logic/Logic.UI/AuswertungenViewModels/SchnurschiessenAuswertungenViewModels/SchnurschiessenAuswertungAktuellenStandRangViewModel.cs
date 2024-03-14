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
using System.Linq;
using GalaSoft.MvvmLight.CommandWpf;

namespace Logic.UI.AuswertungenViewModels.SchnurschiessenAuswertungenViewModels
{
    public class SchnurschiessenAuswertungAktuellenStandRangViewModel : ViewModelAuswertung<SchnurschiessenAuswertungAktuellenStandRangModel>
    {

        public SchnurschiessenAuswertungAktuellenStandRangViewModel()
        {
            Title = "Aktuellen Stand - Rang";
            LoadDataCommand = new RelayCommand(() => ExcecuteLoadDataCommand());
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
        private async void ExcecuteLoadDataCommand()
        {
            RequestIsWorking = true;
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/schnurschiessen/AktuellenStand/Rang");
            if (resp.IsSuccessStatusCode)
            {
                ItemList = await resp.Content.ReadAsAsync<List<SchnurschiessenAuswertungAktuellenStandRangModel>>();
                SetDataIntoChart();
            }
            RequestIsWorking = false;
        }

        private void SetDataIntoChart()
        {
            Labels = new string[ItemList.Count];
            int index = 0;
            ItemList.ToList().ForEach(a =>
            {
                Labels[index] = a.RangNr.ToString();
                index++;
            });
            var auswertungSeries = new ColumnSeries<SchnurschiessenAuswertungAktuellenStandRangModel>
            {
                Values = ItemList,
                Mapping = (model, index) => new LiveChartsCore.Kernel.Coordinate(index, (double)model.Anzahl),
                Name = "Anzahl",
                XToolTipLabelFormatter = (point) => point.Model.Rang,
            };

            XAxes.First().Labels = Labels;
            XAxes.First().Name = "Rang";
            YAxes.First().Name = "Anzahl";

            Series = new ColumnSeries<SchnurschiessenAuswertungAktuellenStandRangModel>[1] { auswertungSeries };

            RaisePropertyChanged(nameof(Series));
            RaisePropertyChanged(nameof(XAxes));
            RaisePropertyChanged(nameof(YAxes));
        }

        #region Bindings
        public ICommand LoadDataCommand { get; set; }
        #endregion
    }
}
