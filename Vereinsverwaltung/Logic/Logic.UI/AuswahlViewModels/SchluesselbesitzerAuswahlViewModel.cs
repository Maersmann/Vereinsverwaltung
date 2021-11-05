using Data.Model.AuswahlModels;
using Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Logic.ViewModels;
using System.Net.Http;
using Logic.Core;
using System.Collections.ObjectModel;
using System.Windows;

namespace Logic.UI.AuswahlViewModels
{
    public class SchluesselbesitzerAuswahlViewModel : ViewModelAuswahl<SchluesselbesitzerAuswahlModel, StammdatenTypes>
    {
        public SchluesselbesitzerAuswahlViewModel()
        {
            Title = "Auswahl Besitzer";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselbesitzer.ToString());
        }

        public int? ID()
        {
            return SelectedItem == null ? null : (int?)SelectedItem.ID;
        }

        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schluesselbesitzer; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/besitzer"; }
        protected override bool WithPagination() { return true; }
        protected override void ExecuteCloseWindowCommand(Window window)
        {
            AuswahlGetaetigt = true;
            base.ExecuteCloseWindowCommand(window);
        }

    }
}
