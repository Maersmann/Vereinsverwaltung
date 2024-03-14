using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.AuswertungModels.MitgliederAuswertungModels;
using GalaSoft.MvvmLight.CommandWpf;
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
using System.Windows.Input;

namespace Logic.UI.AuswertungenViewModels.MitgliederAuswertungenViewModels
{
    public class MitgliederAuswertungJahreImVereinViewModel : ViewModelAuswertung<MitgliederAuswertungJahreImVereinModel>
    {
        private DateTime? stichtag;

        public MitgliederAuswertungJahreImVereinViewModel()
        {
            stichtag = DateTime.Now;
            Title = "Auswertung Mitglieder Jahre im Verein";
            LoadDataCommand = new DelegateCommand(ExcecuteLoadDataCommand, CanExcecuteLoadDataCommand);
            YAxes = new List<Axis>
                {
                    new Axis()
                    {
                        LabelsPaint = new SolidColorPaint{ Color = SKColors.CornflowerBlue },
                        Position = AxisPosition.Start,
                        Labeler = (value) =>  string.Format("{0}", value),
                    }
                };
        }
        private async void ExcecuteLoadDataCommand()
        {
            RequestIsWorking = true;
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/mitglieder/JahreImVerein?stichtag={stichtag.Value.ToString("MM/dd/yyyy")}");
            if (resp.IsSuccessStatusCode)
            {
                ItemList = await resp.Content.ReadAsAsync<List<MitgliederAuswertungJahreImVereinModel>>();
                SetDataIntoChart();
            }
            RequestIsWorking = false;
        }

        private bool CanExcecuteLoadDataCommand()
        {
            return ValidationErrors.Count == 0;
        }

        private void SetDataIntoChart()
        {
            Labels = new string[ItemList.Count];
            int index = 0;
            ItemList.ToList().ForEach(a =>
            {
                Labels[index] = a.Jahre.ToString();
                index++;
            });
            var auswertungSeries = new ColumnSeries<MitgliederAuswertungJahreImVereinModel>
            {
                Values = ItemList,
                Mapping = (model, index) => new LiveChartsCore.Kernel.Coordinate(index, (double)model.Anzahl),
                Name = "Anzahl",
                XToolTipLabelFormatter= (point) => point.Model.Jahre.ToString()+ " Jahre im Verein",
            };

            XAxes.First().Labels = Labels;
            XAxes.First().Name = "Jahre im Verein";
            YAxes.First().Name = "Anzahl";

            Series = new ColumnSeries<MitgliederAuswertungJahreImVereinModel>[1] { auswertungSeries };

            RaisePropertyChanged(nameof(Series));
            RaisePropertyChanged(nameof(XAxes));
            RaisePropertyChanged(nameof(YAxes));
        }

        #region Bindings
        public ICommand LoadDataCommand { get; set; }

        public DateTime? Stichtag
        {
            get { return stichtag; }
            set
            {
                if ( !Equals(stichtag, value))
                {
                    ValidateDatum(value);
                    stichtag = value;
                    RaisePropertyChanged();
                    ((DelegateCommand)LoadDataCommand).RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region
        private bool ValidateDatum(DateTime? datum)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateDatum(datum, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Stichtag", validationErrors);

            return isValid;
        }
        #endregion
    }
}
