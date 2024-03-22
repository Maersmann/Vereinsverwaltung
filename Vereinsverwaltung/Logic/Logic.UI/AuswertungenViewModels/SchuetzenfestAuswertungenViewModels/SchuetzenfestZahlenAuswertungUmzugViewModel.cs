using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.AuswertungModels.VereinsmeisterschaftAuswertungModels;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using Logic.Core.Validierungen.Base;
using Prism.Commands;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Data.Model.AuswertungModels.SchuetzenfestAuswertungModels;
using CommunityToolkit.Mvvm.Input;

namespace Logic.UI.AuswertungenViewModels.SchuetzenfestAuswertungenViewModels
{
    public class SchuetzenfestZahlenAuswertungUmzugViewModel : ViewModelAuswertung<SchuetzenfestZahlenAuswertungUmzugModel>
    {

        private  LineSeries<int> sonntagSeries;
        private  LineSeries<int> montagVormittagSeries;
        private  LineSeries<int> montagNachmittagSeries;

        public SchuetzenfestZahlenAuswertungUmzugViewModel()
        {
            Title = "Auswertung Schützenfest Umzug";
            LoadDataCommand = new RelayCommand(() => ExcecuteLoadDataCommand());
            sonntagSeries = new LineSeries<int>();
            montagVormittagSeries = new LineSeries<int>();
            montagNachmittagSeries = new LineSeries<int>();

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
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/schuetzenfestzahlen/umzug");
            if (resp.IsSuccessStatusCode)
            {
                ItemList = await resp.Content.ReadAsAsync<List<SchuetzenfestZahlenAuswertungUmzugModel>>();

                IList<int> valuesSonntag = [];
                IList<int> valuesMontagVormittag = [];
                IList<int> valuesMontagNachmittag = [];

                Labels = new string[ItemList.Count];
                int index = 0;

                ItemList.ToList().ForEach(a =>
                {
                    valuesSonntag.Add(a.AnzahlSonntag);
                    valuesMontagVormittag.Add(a.AnzahlMontagvormittag);
                    valuesMontagNachmittag.Add(a.AnzahlMontagnachmittag);
                    Labels[index] = a.Jahr.ToString();
                    index++;
                });

                sonntagSeries = new LineSeries<int>
                {
                    Values = valuesSonntag,
                    Name = "Sonntag",
                };
                montagVormittagSeries = new LineSeries<int>
                {
                    Values = valuesMontagVormittag,
                    Name = "Montag Vormittag",
                };
                montagNachmittagSeries = new LineSeries<int>
                {
                    Values = valuesMontagNachmittag,
                    Name = "Montag Nachmittag",
                };

                XAxes.First().Labels = Labels;
                XAxes.First().Name = "Jahr";
                YAxes.First().Name = "Anzahl";
                Series = new LineSeries<int>[3] { sonntagSeries, montagVormittagSeries, montagNachmittagSeries };

                OnPropertyChanged(nameof(Series));
                OnPropertyChanged(nameof(XAxes));
                OnPropertyChanged(nameof(YAxes));
            }
            RequestIsWorking = false;
        }


        #region Bindings
        public ICommand LoadDataCommand { get; set; }
       

        public bool MontagNachmittagSeriesVisibility
        {
            get { return montagNachmittagSeries.IsVisible; }
            set
            {
                montagNachmittagSeries.IsVisible = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        public bool MontagVormittagSeriesVisibility
        {
            get { return montagVormittagSeries.IsVisible; }
            set
            {
                montagVormittagSeries.IsVisible = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        public bool SonntagSeriesVisibility
        {
            get { return sonntagSeries.IsVisible; }
            set
            {
                sonntagSeries.IsVisible = value;
                OnPropertyChanged(nameof(Series));
            }
        }
        #endregion
    }
}
