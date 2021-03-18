using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Model.SchnurEntitys;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Core.SchnurschiessenCore;
using Vereinsverwaltung.Logic.Messages.BaseMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.SchnurschiessenViewModels
{
    public class SchnurauszeichnungUebersichtViewModel : ViewModelUebersicht<Schnurauszeichnung>
    {
        public SchnurauszeichnungUebersichtViewModel()
        {
            MessageToken = "SchnurauszeichnungUebersicht";
            Title = "Übersicht Schnurauszeichnungen";
            NeuCommand = new DelegateCommand(this.ExecuteNeuCommand, this.CanExecuteNeuCommand);
            RegisterAktualisereViewMessage(StammdatenTypes.schnurauszeichnung);
            RegisterAktualisereViewMessage(StammdatenTypes.schnur);
        }

        protected override void ReceiveAktualisiereViewMessage(AktualisiereViewMessage m)
        {
            ((DelegateCommand)NeuCommand).RaiseCanExecuteChanged();
            base.ReceiveAktualisiereViewMessage(m);
        }
        protected override int GetID() { return selectedItem.ID; }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.schnurauszeichnung; }

        public override void LoadData()
        {
            itemList = new SchnurauszeichnungAPI().LadeAlle();
            this.RaisePropertyChanged("ItemList");
        }

        #region Commands
        protected override void ExecuteEntfernenCommand()
        {
            try
            {
                new SchnurauszeichnungAPI().Entfernen(selectedItem.ID);
            }
            catch (Exception)
            {
                SendExceptionMessage("Schlüssel kann nicht gelöscht werden" + Environment.NewLine + Environment.NewLine + "Schlüssel ist Besitzer zugeordnet");
                return;
            }

            SendInformationMessage("Auszeichnung gelöscht");
            base.ExecuteEntfernenCommand();
        }

        private bool CanExecuteNeuCommand()
        {
            return new SchnurAPI().LadeAlleSichtbaren().Count > 0;
        }

        #endregion
    }
}
