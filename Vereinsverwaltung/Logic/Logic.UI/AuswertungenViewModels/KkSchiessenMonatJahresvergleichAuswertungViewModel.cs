using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Model.AuswertungModels.KkSchiessenAuswertungModels;
using Data.Types.AuswertungTypes;
using LiveCharts;
using LiveCharts.Wpf;
using Logic.Core.Validierungen.Base;
using Prism.Commands;
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
            Formatter = value => string.Format("{0:N0}", value);
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
                SeriesCollection = new SeriesCollection();
                ItemList.ToList().ForEach(item =>
                {
                    ColumnSeries coloumn = new ColumnSeries
                    {
                        Title = item.Jahr.ToString(),
                        Values = new ChartValues<int>()
                    };
                    item.Monatswerte.ToList().ForEach(mw =>
                    {
                        coloumn.Values.Add(mw.Anzahl);
                    });
                    SeriesCollection.Add(coloumn);

                });

                for (int monat = 1; monat <= 12; monat++)
                {
                    Labels[monat - 1] = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monat);
                }

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
