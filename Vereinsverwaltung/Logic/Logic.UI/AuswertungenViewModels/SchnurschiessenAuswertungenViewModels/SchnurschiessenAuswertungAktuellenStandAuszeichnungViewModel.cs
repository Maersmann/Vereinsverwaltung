using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.AuswertungModels.MitgliederAuswertungModels;
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
using Data.Model.AuswertungModels.SchnurschiessenAuswertungModels;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using LiveChartsCore.Defaults;
using System.Collections.ObjectModel;


namespace Logic.UI.AuswertungenViewModels.SchnurschiessenAuswertungenViewModels
{
    public class SchnurschiessenAuswertungAktuellenStandAuszeichnungViewModel : ViewModelAuswertung<SchnurschiessenAuswertungAktuellenStandAuszeichnungModel>
    {

        public SchnurschiessenAuswertungAktuellenStandAuszeichnungViewModel()
        {
            Title = "Aktuellen Stand - Auszeichungen";
            LoadDataCommand = new RelayCommand(() => ExcecuteLoadDataCommand());
            YAxes =
                [
                    new Axis()
                    {
                        LabelsPaint = new SolidColorPaint{ Color = SKColors.CornflowerBlue },
                        Position = AxisPosition.Start,
                        Labeler = (value) =>  string.Format("{0}", value)
                    }
                ];
        }
        private async void ExcecuteLoadDataCommand()
        {
            RequestIsWorking = true;
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/schnurschiessen/AktuellenStand/Auszeichnung");
            if (resp.IsSuccessStatusCode)
            {
                ItemList = await resp.Content.ReadAsAsync<List<SchnurschiessenAuswertungAktuellenStandAuszeichnungModel>>();
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
                Labels[index] = a.Auszeichnung;
                index++;
            });
            var auswertungSeries = new ColumnSeries<SchnurschiessenAuswertungAktuellenStandAuszeichnungModel>
            {
                Values = ItemList,
                Mapping = (model, index) => new LiveChartsCore.Kernel.Coordinate(index, (double)model.Anzahl),
                Name = "Anzahl",
            };

            XAxes.First().Labels = Labels;
            XAxes.First().Name = "Auszeichnung";
            YAxes.First().Name = "Anzahl";

            Series = new ColumnSeries<SchnurschiessenAuswertungAktuellenStandAuszeichnungModel>[1] { auswertungSeries };

            OnPropertyChanged(nameof(Series));
            OnPropertyChanged(nameof(XAxes));
            OnPropertyChanged(nameof(YAxes));
        }

        #region Bindings
        public ICommand LoadDataCommand { get; set; }
        #endregion
    }
}
