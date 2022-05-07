using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.AuswertungModels.KkSchiessenAuswertungModels;
using Data.Types.AuswertungTypes;
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
    public class KkSchiessenMonatJahresvergleichAuswertungViewModel : ViewModelAuswertung<KkSchiessenMonatJahresvergleichAuswertungModel>
    {
        private int jahrvon;
        private int jahrbis;
        private KKSchiessenAnzahlTyp typ;

        public KkSchiessenMonatJahresvergleichAuswertungViewModel()
        {
            Title = "Auswertung KK-Schießen Jahresvergleich";
            jahrvon = DateTime.Now.Year;
            jahrbis = DateTime.Now.Year;
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
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/kkschiessen/Monate/Jahresvergleich?jahrVon={jahrvon}&jahrBis={jahrbis}&typ={typ}");
            if (resp.IsSuccessStatusCode)
            {
                ItemList = await resp.Content.ReadAsAsync<List<KkSchiessenMonatJahresvergleichAuswertungModel>>();

                Labels = new string[12];
                ColumnSeries<int>[] series = new ColumnSeries<int>[ItemList.Count];
                int index = 0;



                ItemList.ToList().ForEach(item =>
                {

                    ColumnSeries<int> coloumn = new ColumnSeries<int>
                    {
                        Name = item.Jahr.ToString(),
                        Values = new List<int>(),
                        TooltipLabelFormatter = (point) => item.Jahr + " " + point.PrimaryValue.ToString()
                    };

                    var anzahl = new List<int>();
                    item.Monatswerte.ToList().ForEach(mw =>
                    {
                        anzahl.Add(mw.Anzahl);
                    });
                    coloumn.Values = anzahl;
                    series.SetValue(coloumn, index);
                    index++;

                });

                for (int monat = 1; monat <= 12; monat++)
                {
                    Labels[monat - 1] = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monat);
                }

                XAxes.First().Labels = Labels;
                XAxes.First().Name = "Monat";
                YAxes.First().Name = "Anzahl";

                Series = series;

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

        public KKSchiessenAnzahlTyp Typ
        {
            get => typ;
            set
            {
                RaisePropertyChanged();
                typ = value;
            }
        }

        public static IEnumerable<KKSchiessenAnzahlTyp> Types => Enum.GetValues(typeof(KKSchiessenAnzahlTyp)).Cast<KKSchiessenAnzahlTyp>();
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
