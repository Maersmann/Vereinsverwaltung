using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.AuswertungModels.KkSchiessenAuswertungModels;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Logic.Core.Validierungen.Base;
using Prism.Commands;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.AuswertungenViewModels
{
    public class KkSchiessenMonatAuswertungViewModel : ViewModelAuswertung<KkSchiessenMonatAuswertungModel>
    {
        private int jahrvon;
        private int jahrbis;
        private LineSeries<int> anzahlSeries;
        private LineSeries<int> getraenkeSeries;
        private LineSeries<int> munitionSeries;
        public KkSchiessenMonatAuswertungViewModel()
        {
            Title = "Auswertung KK-Schießen je Monat";
            jahrvon = DateTime.Now.Year;
            jahrbis = DateTime.Now.Year;
            LoadDataCommand = new DelegateCommand(ExcecuteLoadDataCommand, CanExcecuteLoadDataCommand);
            anzahlSeries = new LineSeries<int>();
            getraenkeSeries = new LineSeries<int>();
            munitionSeries = new LineSeries<int>();
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
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/kkschiessen/Monate?jahrVon={jahrvon}&jahrBis={jahrbis}");
            if (resp.IsSuccessStatusCode)
            {
                ItemList = await resp.Content.ReadAsAsync<List<KkSchiessenMonatAuswertungModel>>();

                IList<int> valuesAnzahl = new List<int>();
                IList<int> valuesGetraenke = new List<int>();
                IList<int> valuesMunition = new List<int>();
                Labels = new string[ItemList.Count];
                int index = 0;

                ItemList.ToList().ForEach(a =>
                {
                    valuesAnzahl.Add(a.Anzahl);
                    valuesMunition.Add(a.Munition);
                    valuesGetraenke.Add(a.Getraenke);
                    Labels[index] = a.Datum.ToString("MMMM yyyy", CultureInfo.CurrentCulture);
                    index++;
                });
                anzahlSeries = new LineSeries<int>
                {
                    Values = valuesAnzahl,
                    Name = "Veranstaltungen",
                };
                getraenkeSeries = new LineSeries<int>
                {
                    Values = valuesGetraenke,
                    Name = "Getränke",
                };
                munitionSeries = new LineSeries<int>
                {
                    Values = valuesMunition,
                    Name = "Munition",
                };

                XAxes.First().Labels = Labels;
                XAxes.First().Name = "Monat";
                YAxes.First().Name = "Anzahl";
                Series = new LineSeries<int>[3] { anzahlSeries, getraenkeSeries, munitionSeries };

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
