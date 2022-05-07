using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.AuswertungModels.VereinsmeisterschaftAuswertungModels;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Logic.Core.Validierungen.Base;
using Prism.Commands;
using SkiaSharp;
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
        private LineSeries<int> maenner16_30Series;
        private LineSeries<int> maenner31_50Series;
        private LineSeries<int> maenner51Series;
        private LineSeries<int> frauenSeries;
        private LineSeries<int> sportschuetzenSeries;
        public AuswertungVereinsmeisterschaftEntwicklungSchuetzenViewModel()
        {
            Title = "Auswertung Entwicklung Schützen";
            jahrvon = DateTime.Now.Year;
            jahrbis = DateTime.Now.Year;
            LoadDataCommand = new DelegateCommand(ExcecuteLoadDataCommand, CanExcecuteLoadDataCommand);
            maenner16_30Series = new LineSeries<int>();
            maenner31_50Series = new LineSeries<int>();
            frauenSeries = new LineSeries<int>();
            maenner51Series = new LineSeries<int>();
            sportschuetzenSeries = new LineSeries<int>();
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

                IList<int> valuesFrauen = new List<int>();
                IList<int> valuesMaenner16_30 = new List<int>();
                IList<int> valuesMaenner31_50 = new List<int>();
                IList<int> valuesMaenner51 = new List<int>();
                IList<int> valuesSportschuetzen = new List<int>();
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

                frauenSeries = new LineSeries<int>
                {
                    Values = valuesFrauen,
                    Name = "Anzahl Frauen",
                    TooltipLabelFormatter = (point) => "Anzahl Frauen " + point.PrimaryValue.ToString()
                };
                maenner16_30Series = new LineSeries<int>
                {
                    Values = valuesMaenner16_30,
                    Name = "Anzahl Männer 16-30",
                    TooltipLabelFormatter = (point) => "Anzahl Männer 16-30" + point.PrimaryValue.ToString()
                };
                maenner31_50Series = new LineSeries<int>
                {
                    Values = valuesMaenner31_50,
                    Name = "Anzahl Männer 31-50",
                    TooltipLabelFormatter = (point) => "Anzahl Männer 31-50 " + point.PrimaryValue.ToString()
                };
                maenner51Series = new LineSeries<int>
                {
                    Values = valuesMaenner51,
                    Name = "Anzahl Männer ab 50",
                    TooltipLabelFormatter = (point) => "Anzahl Männer ab 50 " + point.PrimaryValue.ToString()
                };
                sportschuetzenSeries = new LineSeries<int>
                {
                    Values = valuesSportschuetzen,
                    Name = "Anzahl Sportschützen",
                    TooltipLabelFormatter = (point) => "Anzahl Männer " + point.PrimaryValue.ToString()
                };

                XAxes.First().Labels = Labels;
                XAxes.First().Name = "Jahr";
                YAxes.First().Name = "Anzahl";
                Series = new LineSeries<int>[5] { maenner16_30Series, maenner31_50Series, maenner51Series, frauenSeries, sportschuetzenSeries };

                RaisePropertyChanged(nameof(Series));
                RaisePropertyChanged(nameof(XAxes));
                RaisePropertyChanged(nameof(YAxes));

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
            get { return maenner16_30Series.IsVisible; }
            set
            {
                maenner16_30Series.IsVisible = value;
                RaisePropertyChanged(nameof(Series));
            }
        }

        public bool Maenner31_50SeriesVisibility
        {
            get { return maenner31_50Series.IsVisible; }
            set
            {
                maenner31_50Series.IsVisible = value;
                RaisePropertyChanged(nameof(Series));
            }
        }

        public bool Maenner51SeriesVisibility
        {
            get { return maenner51Series.IsVisible; }
            set
            {
                maenner51Series.IsVisible = value;
                RaisePropertyChanged(nameof(Series));
            }
        }

        public bool FrauenSeriesVisibility
        {
            get { return frauenSeries.IsVisible; }
            set
            {
                frauenSeries.IsVisible = value;
                RaisePropertyChanged(nameof(Series));
            }
        }

        public bool SportschuetzenSeriesVisibility
        {
            get { return sportschuetzenSeries.IsVisible; }
            set
            {
                sportschuetzenSeries.IsVisible = value;
                RaisePropertyChanged(nameof(Series));
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
