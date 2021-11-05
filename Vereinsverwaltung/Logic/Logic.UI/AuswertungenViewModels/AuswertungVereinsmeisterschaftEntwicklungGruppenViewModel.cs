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
using System.Windows.Input;

namespace Logic.UI.AuswertungenViewModels
{
    public class AuswertungVereinsmeisterschaftEntwicklungGruppenViewModel : ViewModelAuswertung<AuswertungVereinsmeisterschaftEntwicklungGruppenModel>
    {
        private int jahrvon;
        private int jahrbis;
        public AuswertungVereinsmeisterschaftEntwicklungGruppenViewModel()
        {
            Title = "Auswertung Entwicklung Gruppen";
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
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/auswertungen/vereinsmeisterschaften/Entwicklung/Gruppen?jahrVon={jahrvon}&jahrBis={jahrbis}");
            if (resp.IsSuccessStatusCode)
            {
                ItemList = await resp.Content.ReadAsAsync<List<AuswertungVereinsmeisterschaftEntwicklungGruppenModel>>();

                ChartValues<int> valuesFrauen = new ChartValues<int>();
                ChartValues<int> valuesMaenner = new ChartValues<int>();
                Labels = new string[ItemList.Count];
                int index = 0;

                ItemList.ToList().ForEach(a =>
                {
                    valuesFrauen.Add(a.AnzahlFrauen);
                    valuesMaenner.Add(a.AnzahlMaenner);
                    Labels[index] = a.Jahr.ToString();
                    index++;
                });
                SeriesCollection = new SeriesCollection
                {
                    new LineSeries{ Values = valuesFrauen, Title="Anzahl Frauen" },
                    new LineSeries{ Values = valuesMaenner, Title="Anzahl Männer" },
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
