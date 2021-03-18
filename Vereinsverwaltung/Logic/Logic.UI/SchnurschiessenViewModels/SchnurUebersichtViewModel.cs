using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Model.SchnurEntitys;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Core.SchnurschiessenCore;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.SchnurschiessenViewModels
{
    public class SchnurUebersichtViewModel : ViewModelUebersicht<Schnur>
    {
        public SchnurUebersichtViewModel()
        {
            MessageToken = "SchnurUebersicht";
            Title = "Übersicht Schnüre";
            RegisterAktualisereViewMessage(StammdatenTypes.schnur);
        }

        protected override int GetID() { return selectedItem.ID; }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schnur; }

        public override void LoadData()
        {
            itemList = new SchnurAPI().LadeAlleSichtbaren();
            this.RaisePropertyChanged("ItemList");
        }

        #region Commands
        protected override void ExecuteEntfernenCommand()
        {
            try
            {
                new SchnurAPI().Entfernen(selectedItem.ID);
            }
            catch (Exception)
            {
                SendExceptionMessage("Schlüssel kann nicht gelöscht werden" + Environment.NewLine + Environment.NewLine + "Schlüssel ist Besitzer zugeordnet");
                return;
            }

            SendInformationMessage("Schnur gelöscht");
            base.ExecuteEntfernenCommand();
        }

        #endregion
    }
}
