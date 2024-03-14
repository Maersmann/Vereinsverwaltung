using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.AuswertungModels.SchnurschiessenAuswertungModels;
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
using GalaSoft.MvvmLight.CommandWpf;
using System.Linq;
using Prism.Commands;
using Logic.Core.Validierungen.Base;

namespace Logic.UI.AuswertungenViewModels.SchnurschiessenAuswertungenViewModels
{
    public class SchnurschiessenAuswertungErhalteneAuszeichnungViewModel : ViewModelAuswertung<SchnurschiessenAuswertungErhalteneAuszeichnungModel>
    {
        private int jahr;
        public SchnurschiessenAuswertungErhalteneAuszeichnungViewModel()
        {
            Title = "Ausgegebene Auszeichnungen";
            jahr = DateTime.Now.Year;
            LoadDataCommand = new DelegateCommand(ExcecuteLoadDataCommand, CanExcecuteLoadDataCommand);
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
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/schnurschiessen/Ausgegeben?jahr={jahr}");
            if (resp.IsSuccessStatusCode)
            {
                ItemList = await resp.Content.ReadAsAsync<List<SchnurschiessenAuswertungErhalteneAuszeichnungModel>>();
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
            var auswertungSeries = new ColumnSeries<SchnurschiessenAuswertungErhalteneAuszeichnungModel>
            {
                Values = ItemList,
                Mapping = (model, index) => new LiveChartsCore.Kernel.Coordinate(index, (double)model.Anzahl),
                Name = "Anzahl",
            };

            XAxes.First().Labels = Labels;
            XAxes.First().Name = "Auszeichnung";
            YAxes.First().Name = "Anzahl";

            Series = new ColumnSeries<SchnurschiessenAuswertungErhalteneAuszeichnungModel>[1] { auswertungSeries };

            RaisePropertyChanged(nameof(Series));
            RaisePropertyChanged(nameof(XAxes));
            RaisePropertyChanged(nameof(YAxes));
        }

        #region Bindings
        public ICommand LoadDataCommand { get; set; }

        public int? Jahr
        {
            get => jahr;
            set
            {
                ValidatZahl(value, nameof(Jahr));
                RaisePropertyChanged();
                ((DelegateCommand)LoadDataCommand).RaiseCanExecuteChanged();
                jahr = value.GetValueOrDefault(0);
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
