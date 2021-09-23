using Data.Model.SchluesselverwaltungModels;
using Data.Types.SchluesselverwaltungTypes;
using Logic.Core;
using Base.Logic.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Data.Types;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselzuteilungHistoryUebersichtViewModel : ViewModelLoadData<SchluesselzuteilungHistoryModel>
    {
        private SchluesselzuteilungTypes auswahlTypes;
        private int id;
        public SchluesselzuteilungHistoryUebersichtViewModel()
        {
            MessageToken = "SchluesselzuteilungHistoryUebersicht";
            Title = "History Verteilung";
        }

        protected override string GetREST_API() { return $"/api/schluesselverwaltung/history?id={id}&type={auswahlTypes}"; }

        public void SetAuswahlState(int id, SchluesselzuteilungTypes auswahlTypes)
        {
            this.auswahlTypes = auswahlTypes;
            this.id = id;
            LoadData();
        }
    }
}
