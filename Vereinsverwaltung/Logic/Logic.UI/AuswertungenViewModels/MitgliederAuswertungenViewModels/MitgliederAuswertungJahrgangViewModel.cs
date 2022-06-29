using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.AuswertungModels.MitgliederAuswertungModels;
using GalaSoft.MvvmLight.CommandWpf;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.AuswertungenViewModels.MitgliederAuswertungenViewModels
{
    public class MitgliederAuswertungJahrgangViewModel : ViewModelAuswertung<MitgliederAuswertungJahrgangModel>
    {

        public MitgliederAuswertungJahrgangViewModel()
        {
            Title = "Auswertung Mitglieder im Jahrgang";
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
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/mitglieder/Jahrgaenge");
            if (resp.IsSuccessStatusCode)
            {
                ItemList = await resp.Content.ReadAsAsync<List<MitgliederAuswertungJahrgangModel>>();
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

            var auswertungSeries = new ColumnSeries<MitgliederAuswertungJahrgangModel>
            {
                Values = ItemList,
                DataLabelsFormatter = (point) => point.TertiaryValue.ToString(),
                Mapping = (model, point) => {
                    point.PrimaryValue = model.Anzahl;
                    point.SecondaryValue = point.Context.Index;
                },
                Name = "Anzahl",
                TooltipLabelFormatter = (point) => "Jahrgang: " + point.Model.Jahr.ToString() + ": " + point.Model.Anzahl.ToString(),
            };

            XAxes.First().Labels = Labels;
            XAxes.First().Name = "Jahrgang";
            YAxes.First().Name = "Anzahl";

            Series = new ColumnSeries<MitgliederAuswertungJahrgangModel>[1] { auswertungSeries };

            RaisePropertyChanged(nameof(Series));
            RaisePropertyChanged(nameof(XAxes));
            RaisePropertyChanged(nameof(YAxes));
        }

        #region Bindings
        public ICommand LoadDataCommand { get; set; }
        #endregion
    }
}
