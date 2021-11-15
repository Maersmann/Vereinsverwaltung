using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.AuswertungModels.VereinsmeisterschaftAuswertungModels;
using LiveCharts;
using LiveCharts.Wpf;
using Logic.Core.Validierungen.Base;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Logic.UI.AuswertungenViewModels
{
    public class AuswertungVereinsmeisterschaftEntwicklungSchuetzenViewModel : ViewModelAuswertung<AuswertungVereinsmeisterschaftEntwicklungSchuetzenModel>
    {
        private int jahrvon;
        private int jahrbis;
        private bool maenner16_30SeriesVisibility;
        private bool maenner31_50SeriesVisibility;
        private bool maenner51SeriesVisibility;
        private bool frauenSeriesVisibility;
        private bool sportschuetzenSeriesVisibility;
        public AuswertungVereinsmeisterschaftEntwicklungSchuetzenViewModel()
        {
            maenner16_30SeriesVisibility = true;
            maenner31_50SeriesVisibility = true;
            maenner51SeriesVisibility = true;
            frauenSeriesVisibility = true;
            sportschuetzenSeriesVisibility = true;
            Title = "Auswertung Entwicklung Schützen";
            jahrvon = DateTime.Now.Year;
            jahrbis = DateTime.Now.Year;
            LoadDataCommand = new DelegateCommand(this.ExcecuteLoadDataCommand, this.CanExcecuteLoadDataCommand);
            Formatter = value => string.Format("{0:N0}", value);
        }

        private bool CanExcecuteLoadDataCommand()
        {
            return ValidationErrors.Count == 0;
        }

        private async void ExcecuteLoadDataCommand()
        {
            RequestIsWorking = true;
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/vereinsmeisterschaften/Entwicklung/Schuetzen?jahrVon={jahrvon}&jahrBis={jahrbis}");
            if (resp.IsSuccessStatusCode)
            {
                ItemList = await resp.Content.ReadAsAsync<List<AuswertungVereinsmeisterschaftEntwicklungSchuetzenModel>>();

                ChartValues<int> valuesFrauen = new ChartValues<int>();
                ChartValues<int> valuesMaenner16_30 = new ChartValues<int>();
                ChartValues<int> valuesMaenner31_50 = new ChartValues<int>();
                ChartValues<int> valuesMaenner51 = new ChartValues<int>();
                ChartValues<int> valuesSportschuetzen = new ChartValues<int>();
                Labels = new string[ItemList.Count];
                int index = 0;

                ItemList.ToList().ForEach(a =>
                {
                    valuesFrauen.Add(a.AnzahlFrauen);
                    valuesMaenner16_30.Add(a.AnzahlMaenner16_30);
                    valuesMaenner31_50.Add(a.AnzahlMaenner31_50);
                    valuesMaenner51.Add(a.AnzahlMaenner51);
                    valuesSportschuetzen.Add(a.AnzahlSportschuetzen);
                    Labels[index] = a.Jahr.ToString();
                    index++;
                });


                Binding Maenner16_30SeriesVisbilityBinding = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(Maenner16_30SeriesVisibility)),
                    Converter = new BooleanToVisibilityConverter(),
                    Mode = BindingMode.OneWay,
                };
                Binding FrauenSeriesVisbilityBinding = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(FrauenSeriesVisibility)),
                    Converter = new BooleanToVisibilityConverter(),
                    Mode = BindingMode.OneWay,
                };
                Binding Maenner31_50SeriesVisbilityBinding = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(Maenner31_50SeriesVisibility)),
                    Converter = new BooleanToVisibilityConverter(),
                    Mode = BindingMode.OneWay,
                };
                Binding Maenner51SeriesVisbilityBinding = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(Maenner51SeriesVisibility)),
                    Converter = new BooleanToVisibilityConverter(),
                    Mode = BindingMode.OneWay,
                };
                Binding SportschuetzenSeriesVisbilityBinding = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(SportschuetzenSeriesVisibility)),
                    Converter = new BooleanToVisibilityConverter(),
                    Mode = BindingMode.OneWay,
                };

                LineSeries Maenner16_30Series = new LineSeries
                {
                    Values = valuesMaenner16_30,
                    Title = "Anzahl Männer 16-30"
                };
                LineSeries Maenner31_50Series = new LineSeries
                {
                    Values = valuesMaenner31_50,
                    Title = "Anzahl Männer 31-50"
                };
                LineSeries Maenner51Series = new LineSeries
                {
                    Values = valuesMaenner51,
                    Title = "Anzahl Männer ab 50"
                };
                LineSeries FrauenSeries = new LineSeries
                {
                    Values = valuesFrauen,
                    Title = "Anzahl Frauen",
                };
                LineSeries SportschuetzenSeries = new LineSeries
                {
                    Values = valuesSportschuetzen,
                    Title = "Anzahl Sportschützen"
                };

                Maenner16_30Series.SetBinding(UIElement.VisibilityProperty, Maenner16_30SeriesVisbilityBinding);
                Maenner31_50Series.SetBinding(UIElement.VisibilityProperty, Maenner31_50SeriesVisbilityBinding);
                Maenner51Series.SetBinding(UIElement.VisibilityProperty, Maenner51SeriesVisbilityBinding);
                SportschuetzenSeries.SetBinding(UIElement.VisibilityProperty, SportschuetzenSeriesVisbilityBinding);
                FrauenSeries.SetBinding(UIElement.VisibilityProperty, FrauenSeriesVisbilityBinding);

                SeriesCollection = new SeriesCollection
                {
                    Maenner16_30Series,
                    Maenner31_50Series,
                    Maenner51Series,
                    FrauenSeries,
                    SportschuetzenSeries
                };

                RaisePropertyChanged(nameof(SeriesCollection));
                RaisePropertyChanged(nameof(Labels));
                RaisePropertyChanged(nameof(Formatter));
            }
            RequestIsWorking = false;
        }


        #region Bindings
        public ICommand LoadDataCommand { get; set; }
        public int? JahrVon
        {
            get => jahrvon;
            set
            {
                ValidatZahl(value, nameof(JahrVon));
                RaisePropertyChanged();
                ((DelegateCommand)LoadDataCommand).RaiseCanExecuteChanged();
                jahrvon = value.GetValueOrDefault(0);
            }
        }
        public int? JahrBis
        {
            get => jahrbis;
            set
            {
                ValidatZahl(value, nameof(JahrBis));
                RaisePropertyChanged();
                ((DelegateCommand)LoadDataCommand).RaiseCanExecuteChanged();
                jahrbis = value.GetValueOrDefault(0);
            }
        }

        public bool Maenner16_30SeriesVisibility
        {
            get { return maenner16_30SeriesVisibility; }
            set
            {
                maenner16_30SeriesVisibility = value;
                RaisePropertyChanged();
            }
        }

        public bool Maenner31_50SeriesVisibility
        {
            get { return maenner31_50SeriesVisibility; }
            set
            {
                maenner31_50SeriesVisibility = value;
                RaisePropertyChanged();
            }
        }

        public bool Maenner51SeriesVisibility
        {
            get { return maenner51SeriesVisibility; }
            set
            {
                maenner51SeriesVisibility = value;
                RaisePropertyChanged();
            }
        }

        public bool FrauenSeriesVisibility
        {
            get { return frauenSeriesVisibility; }
            set
            {
                frauenSeriesVisibility = value;
                RaisePropertyChanged();
            }
        }

        public bool SportschuetzenSeriesVisibility
        {
            get { return sportschuetzenSeriesVisibility; }
            set
            {
                sportschuetzenSeriesVisibility = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Validate
        private bool ValidatZahl(int? zahl, string fieldname)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateAnzahl(zahl, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, fieldname, validationErrors);
            return isValid;
        }
        #endregion
    }
}
