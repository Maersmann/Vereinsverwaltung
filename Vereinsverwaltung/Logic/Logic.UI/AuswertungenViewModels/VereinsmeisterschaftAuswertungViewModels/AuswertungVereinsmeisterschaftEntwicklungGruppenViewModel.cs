﻿using Base.Logic.Core;
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
    public class AuswertungVereinsmeisterschaftEntwicklungGruppenViewModel : ViewModelAuswertung<AuswertungVereinsmeisterschaftEntwicklungGruppenModel>
    {
        private int jahrvon;
        private int jahrbis;
        private LineSeries<int> frauenSeries;
        private LineSeries<int> maennerSeries;
        public AuswertungVereinsmeisterschaftEntwicklungGruppenViewModel()
        {
            Title = "Auswertung Entwicklung Gruppen";
            jahrvon = DateTime.Now.Year;
            jahrbis = DateTime.Now.Year;
            LoadDataCommand = new DelegateCommand(ExcecuteLoadDataCommand, CanExcecuteLoadDataCommand);
            frauenSeries = new LineSeries<int>();
            maennerSeries = new LineSeries<int>();
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
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/vereinsmeisterschaften/Entwicklung/Gruppen?jahrVon={jahrvon}&jahrBis={jahrbis}");
            if (resp.IsSuccessStatusCode)
            {
                ItemList = await resp.Content.ReadAsAsync<List<AuswertungVereinsmeisterschaftEntwicklungGruppenModel>>();

                IList<int> valuesFrauen = new List<int>();
                IList<int> valuesMaenner = new List<int>();
                Labels = new string[ItemList.Count];
                int index = 0;

                ItemList.ToList().ForEach(a =>
                {
                    valuesFrauen.Add(a.AnzahlFrauen);
                    valuesMaenner.Add(a.AnzahlMaenner);
                    Labels[index] = a.Jahr.ToString();
                    index++;
                });

                frauenSeries = new LineSeries<int>
                {
                    Values = valuesFrauen,
                    Name = "Anzahl Frauen",
                    TooltipLabelFormatter = (point) => "Anzahl Frauen " + point.PrimaryValue.ToString()
                };
                maennerSeries = new LineSeries<int>
                {
                    Values = valuesMaenner,
                    Name = "Anzahl Männer",
                    TooltipLabelFormatter = (point) => "Anzahl Männer " + point.PrimaryValue.ToString()
                };

                XAxes.First().Labels = Labels;
                XAxes.First().Name = "Jahr";
                YAxes.First().Name = "Anzahl";
                Series = new LineSeries<int>[2] { frauenSeries, maennerSeries };

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

        public bool FrauenSeriesVisibility
        {
            get { return frauenSeries.IsVisible; }
            set
            {
                frauenSeries.IsVisible = value;
                RaisePropertyChanged(nameof(Series));
            }
        }

        public bool MaennerSeriesVisibility
        {
            get { return maennerSeries.IsVisible; }
            set
            {
                maennerSeries.IsVisible = value;
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