using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using Data.Types.AuswahlTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Logic.UI.AuswahlViewModels
{
    public class VereinsmeisterschaftAuswahlViewModel : ViewModelAuswahl<VereinsmeisterschaftModel, StammdatenTypes>
    {
        AuswahlVereinsmeisterschaftTypes auswahltyp;
        public VereinsmeisterschaftAuswahlViewModel()
        {
            auswahltyp = AuswahlVereinsmeisterschaftTypes.alle;
            Title = "Auswahl Vereinsmeisterschaften";
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.pinAusgabe; }
        protected override string GetREST_API()
        {
            return auswahltyp.Equals(AuswahlVereinsmeisterschaftTypes.alle)
                ? $"​/api/Vereinsmeisterschaften​"
                : $"/api/Vereinsmeisterschaften/abgeschlossene";
        }
        protected override bool WithPagination() { return true; }
        protected override bool LoadingOnCreate() => false;
        protected override void ExecuteCloseWindowCommand(Window window)
        {
            AuswahlGetaetigt = true;
            base.ExecuteCloseWindowCommand(window);
        }
        public AuswahlVereinsmeisterschaftTypes AuswahlTyp
        {
            get => auswahltyp;
            set
            {
                auswahltyp = value;
                _ = LoadData();
            }
        }

        #region Bindings
        public int? ID()
        {
            return SelectedItem == null ? null : (int?)SelectedItem.ID;
        }
        public int? Jahr()
        {
            return SelectedItem == null ? null : (int?)SelectedItem.Jahr;
        }
        #endregion
    }
}
