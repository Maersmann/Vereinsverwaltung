using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vereinsverwaltung.Data.Entitys.MitgliederEntitys;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Core.MitgliederCore;
using Vereinsverwaltung.Logic.Messages.BaseMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.MitgliederViewModels
{
    public class MitgliederUebersichtViewModel : ViewModelUebersicht<Mitglied>
    {

        public MitgliederUebersichtViewModel()
        {
            Title = "Übersicht Mitglieder";
            RegisterAktualisereViewMessage(StammdatenTypes.mitglied);    
        }
        protected override int GetID() { return selectedItem.ID; }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.mitglied; }

        public override void LoadData()
        {
            itemList = new MitgliedAPI().LadeAlle();
            base.LoadData();
        }

        #region Commands

        protected override void ExecuteEntfernenCommand()
        {
            new MitgliedAPI().Entfernen(selectedItem.ID);
            SendInformationMessage("Mitglied gelöscht");
            base.ExecuteEntfernenCommand();
        }
        #endregion
    }
}
