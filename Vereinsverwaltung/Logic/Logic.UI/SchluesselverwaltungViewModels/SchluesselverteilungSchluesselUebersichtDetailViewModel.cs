using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Core.SchluesselCore;
using Vereinsverwaltung.Logic.Messages.BaseMessages;
using Vereinsverwaltung.Logic.Messages.SchluesselMessages;
using Vereinsverwaltung.Logic.UI.BaseViewModels;

namespace Vereinsverwaltung.Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungSchluesselUebersichtDetailViewModel : ViewModelUebersicht<Schluesselzuteilung>
    {
        private int schluesselid;
        public SchluesselverteilungSchluesselUebersichtDetailViewModel()
        {
            MessageToken = "SchluesselverteilungSchluesselUebersichtDetail";
            Title = "Übersicht der Besitzer";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung);
            Messenger.Default.Register<LoadSchluesselverteilungSchluesselDetailMessage>(this, "SchluesselverteilungSchluesselUebersicht", m => ReceiveLoadSchluesselverteilungSchluesselDetailMessage(m));
        }

        private void ReceiveLoadSchluesselverteilungSchluesselDetailMessage(LoadSchluesselverteilungSchluesselDetailMessage m)
        {
            schluesselid = m.ID;
            LoadData(schluesselid);
        }

        protected override void ReceiveAktualisiereViewMessage(AktualisiereViewMessage m)
        {
            LoadData(schluesselid);
        }

        public override void LoadData(int id)
        {
            itemList = new SchluesselzuteilungAPI().LadeAlleFuerSchluessel(id);
            this.RaisePropertyChanged("ItemList");
        }

        #region Commands
        protected override void ExecuteEntfernenCommand()
        {
            new SchluesselverteilungAPI().EntferneZuteilung(selectedItem.ID);
            SendInformationMessage("Eintrag gelöscht");
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), StammdatenTypes.schluesselzuteilung);
            base.ExecuteEntfernenCommand();
        }
        #endregion

    }
}
