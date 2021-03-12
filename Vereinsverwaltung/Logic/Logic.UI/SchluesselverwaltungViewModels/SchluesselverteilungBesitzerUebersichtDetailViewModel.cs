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
    public class SchluesselverteilungBesitzerUebersichtDetailViewModel : ViewModelUebersicht<Schluesselzuteilung>
    {
        private int besitzerid;
        public SchluesselverteilungBesitzerUebersichtDetailViewModel()
        {
            MessageToken = "SchluesselzuteilungBesitzerSchluesselUebersicht";
            Title = "Vorhandene Schlüssel des Besitzer";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung);
            Messenger.Default.Register<LoadSchluesselverteilungBesitzerDetailMessage>(this, "SchluesselverteilungBesitzerUebersicht", m => ReceiveLoadSchluesselverteilungBesitzerDetailMessage(m));
        }

        private void ReceiveLoadSchluesselverteilungBesitzerDetailMessage(LoadSchluesselverteilungBesitzerDetailMessage m)
        {
            besitzerid = m.ID;
            LoadData(besitzerid);
        }

        protected override void ReceiveAktualisiereViewMessage(AktualisiereViewMessage m)
        {
                LoadData(besitzerid);
        }

        public override void LoadData(int id)
        {
            itemList = new SchluesselzuteilungAPI().LadeAlleFuerBesitzer(id);
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
