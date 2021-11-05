using Data.Model.AuswahlModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using Logic.Core;
using Base.Logic.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Logic.UI.AuswahlViewModels
{
    public class SchluesselzuteilungAuswahlViewModel : ViewModelAuswahl<SchluesselzuteilungAuswahlModel, StammdatenTypes>
    {
        private SchluesselzuteilungTypes auswahlTypes;
        private int id;

        public SchluesselzuteilungAuswahlViewModel()
        {
            Title = "Auswahl Besitzer";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung.ToString());
        }

        public async void SetAuswahlState(int id, SchluesselzuteilungTypes auswahlTypes)
        {
            this.auswahlTypes = auswahlTypes;
            this.id = id;
            await LoadData();
        }

        public int? ID()
        {
            return SelectedItem == null ? null : (int?)SelectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluesselzuteilung; }
        protected override bool WithPagination() { return true; }
        protected override string GetREST_API() 
        {
            return auswahlTypes.Equals(SchluesselzuteilungTypes.Besitzer)
                ? $"/api/schluesselverwaltung/zuteilung/besitzer/{id}/schluessel"
                : $"/api/schluesselverwaltung/zuteilung/schluessel/{id}/besitzer";
        }
        protected override void ExecuteCloseWindowCommand(Window window)
        {
            AuswahlGetaetigt = true;
            base.ExecuteCloseWindowCommand(window);
        }
    }
}
