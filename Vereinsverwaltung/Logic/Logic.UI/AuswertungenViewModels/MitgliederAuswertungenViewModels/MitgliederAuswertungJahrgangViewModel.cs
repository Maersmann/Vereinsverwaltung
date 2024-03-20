using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.AuswertungModels.MitgliederAuswertungModels;
using CommunityToolkit.Mvvm.Input;
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
                Mapping = (model, index) => new LiveChartsCore.Kernel.Coordinate(index, (double)model.Anzahl),
                Name = "Anzahl",
            };

            XAxes.First().Labels = Labels;
            XAxes.First().Name = "Jahrgang";
            YAxes.First().Name = "Anzahl";

            Series = new ColumnSeries<MitgliederAuswertungJahrgangModel>[1] { auswertungSeries };

            OnPropertyChanged(nameof(Series));
            OnPropertyChanged(nameof(XAxes));
            OnPropertyChanged(nameof(YAxes));
        }

        #region Bindings
        public ICommand LoadDataCommand { get; set; }
        #endregion
    }
}
