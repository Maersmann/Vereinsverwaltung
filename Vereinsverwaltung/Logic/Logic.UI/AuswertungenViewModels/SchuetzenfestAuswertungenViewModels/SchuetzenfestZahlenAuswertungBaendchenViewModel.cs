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
    public class SchuetzenfestZahlenAuswertungBaendchenViewModel : ViewModelAuswertung<SchuetzenfestzahlenAuswertungBaendchenModel>
    {
        private LineSeries<int> samstagMitgliederSeries;
        private LineSeries<int> samstagGaesteSeries;
        private LineSeries<int> montagMitgliederSeries;
        private LineSeries<int> montagGaesteSeries;

        public SchuetzenfestZahlenAuswertungBaendchenViewModel()
        {
            Title = "Auswertung Schützenfest Bändchen";
            LoadDataCommand = new RelayCommand(() => ExcecuteLoadDataCommand());
            samstagMitgliederSeries = new LineSeries<int>();
            samstagGaesteSeries = new LineSeries<int>();
            montagMitgliederSeries = new LineSeries<int>();
            montagGaesteSeries = new LineSeries<int>();

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
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/schuetzenfestzahlen/baendchen");
            if (resp.IsSuccessStatusCode)
            {
                ItemList = await resp.Content.ReadAsAsync<List<SchuetzenfestzahlenAuswertungBaendchenModel>>();

                IList<int> valuesSamstagMitglieder = [];
                IList<int> valuesSamstagGaeste = [];
                IList<int> valuesMontagMitglieder = [];
                IList<int> valuesMontagGaeste = [];

                Labels = new string[ItemList.Count];
                int index = 0;

                ItemList.ToList().ForEach(a =>
                {
                    valuesSamstagMitglieder.Add(a.BaendchenSamstagMitglieder);
                    valuesSamstagGaeste.Add(a.BaendchenSamstagGaeste);
                    valuesMontagMitglieder.Add(a.BaendchenMontagMitglieder);
                    valuesMontagGaeste.Add(a.BaendchenMontagGaeste);
                    Labels[index] = a.Jahr.ToString();
                    index++;
                });

                samstagMitgliederSeries = new LineSeries<int>
                {
                    Values = valuesSamstagMitglieder,
                    Name = "Samstag - Mitglieder",
                };
                samstagGaesteSeries = new LineSeries<int>
                {
                    Values = valuesSamstagGaeste,
                    Name = "Samstag - Gäste",
                };
                montagMitgliederSeries = new LineSeries<int>
                {
                    Values = valuesMontagMitglieder,
                    Name = "Montag - Mitglieder",
                };
                montagGaesteSeries = new LineSeries<int>
                {
                    Values = valuesMontagGaeste,
                    Name = "Montag - Gäste",
                };

                XAxes.First().Labels = Labels;
                XAxes.First().Name = "Jahr";
                YAxes.First().Name = "Anzahl";
                Series = new LineSeries<int>[4] { montagGaesteSeries, montagMitgliederSeries, samstagGaesteSeries, samstagMitgliederSeries };

                OnPropertyChanged(nameof(Series));
                OnPropertyChanged(nameof(XAxes));
                OnPropertyChanged(nameof(YAxes));
            }
            RequestIsWorking = false;
        }


        #region Bindings
        public ICommand LoadDataCommand { get; set; }
       

        public bool MontagGaesteSeriesVisibility
        {
            get { return montagGaesteSeries.IsVisible; }
            set
            {
                montagGaesteSeries.IsVisible = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        public bool MontagMitgliederSeriesVisibility
        {
            get { return montagMitgliederSeries.IsVisible; }
            set
            {
                montagMitgliederSeries.IsVisible = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        public bool SamstagGaesteSeriesVisibility
        {
            get { return samstagGaesteSeries.IsVisible; }
            set
            {
                samstagGaesteSeries.IsVisible = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        public bool SamstagMitgliederSeriesVisibility
        {
            get { return samstagMitgliederSeries.IsVisible; }
            set
            {
                samstagMitgliederSeries.IsVisible = value;
                OnPropertyChanged(nameof(Series));
            }
        }
        #endregion
    }
}
