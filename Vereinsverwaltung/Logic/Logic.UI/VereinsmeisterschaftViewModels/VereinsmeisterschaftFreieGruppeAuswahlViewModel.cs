using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using Data.Types.MitgliederTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftFreieGruppeAuswahlViewModel : ViewModelAuswahl<VereinsmeisterschaftGruppeVerfügbarkeitModel, StammdatenTypes>
    {
        private int vereinsmeisterschaftID;
        private Geschlecht? geschlecht;
        public VereinsmeisterschaftFreieGruppeAuswahlViewModel()
        {
            vereinsmeisterschaftID = 0;
            Title = "Auswahl Freie Gruppen";
            RegisterAktualisereViewMessage(StammdatenTypes.schiessgruppe.ToString());
        }
        protected override bool LoadingOnCreate() => false;
        public int? ID()
        {
            return SelectedItem == null ? null : (int?)SelectedItem.ID;
        }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schiessgruppe; }
        protected override string GetREST_API() 
        {
            return geschlecht.HasValue
                ? $"/api/Schiessgruppen/Vereinsmeisterschaft/Frei?vereinsmeisterschaftId={vereinsmeisterschaftID}&geschlecht={(int) geschlecht}"
                : $"/api/Schiessgruppen/Vereinsmeisterschaft/Frei?vereinsmeisterschaftId={vereinsmeisterschaftID}";
        }
        protected override bool WithPagination() { return true; }

        public async void SetInfoForLoading(Geschlecht? geschlecht, int vereinsmeisterschaftID)
        {
            this.vereinsmeisterschaftID = vereinsmeisterschaftID;
            this.geschlecht = geschlecht;
            await LoadData();
        }

        #region Commands

        protected override void ExecuteCloseWindowCommand(Window window)
        {
            AuswahlGetaetigt = true;
            base.ExecuteCloseWindowCommand(window);
        }

        #endregion
    }
}
