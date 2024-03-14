using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.AuswertungModels.MitgliederAuswertungModels;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using LiveChartsCore.SkiaSharpView;
using System.Linq;
using System.Globalization;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Measure;
using LiveChartsCore;

namespace Logic.UI.AuswertungenViewModels.MitgliederAuswertungenViewModels
{
    public class MitgliederAuswertungEintrittViewModel : ViewModelAuswertung<MitgliederAuswertungEintrittModel>
    {

        public MitgliederAuswertungEintrittViewModel()
        {
            Title = "Auswertung Eintritte pro Jahr";
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
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/mitglieder/Eintritt");
            if (resp.IsSuccessStatusCode)
            {
                ItemList = await resp.Content.ReadAsAsync<List<MitgliederAuswertungEintrittModel>>();
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
                Labels[index] = a.Jahr.ToString();
                index++;
            });
            var auswertungSeries = new ColumnSeries<MitgliederAuswertungEintrittModel>
            {
                Values = ItemList,
                DataLabelsFormatter = (point) => point.TertiaryValue.ToString(),
                Mapping = (model, index) => new LiveChartsCore.Kernel.Coordinate(index, (double)model.Anzahl),
                Name = "Anzahl",
                TooltipLabelFormatter = (point) => "Eintritte im Jahr: " + point.Model.Jahr.ToString() + ": "   + point.Model.Anzahl.ToString(),
            };

            XAxes.First().Labels = Labels;
            XAxes.First().Name = "Jahr";
            YAxes.First().Name = "Anzahl";

            Series = new ColumnSeries<MitgliederAuswertungEintrittModel>[1] { auswertungSeries };

            RaisePropertyChanged(nameof(Series));
            RaisePropertyChanged(nameof(XAxes));
            RaisePropertyChanged(nameof(YAxes));
        }

        #region Bindings
        public ICommand LoadDataCommand { get; set; }
        #endregion
    }
}
