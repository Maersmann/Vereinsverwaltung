using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.BaseMessages;
using Logic.Messages.SchluesselMessages;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungBesitzerUebersichtDetailViewModel : ViewModelUebersicht<SchluesselverteilungBesitzerUebersichtDetailModel>
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
            // Todo: Request
            /*
            itemList = new SchluesselzuteilungAPI().LadeAlleFuerBesitzer(id);
            this.RaisePropertyChanged("ItemList");
            */
        }

        #region Commands
        protected override void ExecuteEntfernenCommand()
        {
            // Todo: Request
            /*
            new SchluesselverteilungAPI().EntferneZuteilung(selectedItem.ID);
            SendInformationMessage("Eintrag gelöscht");
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), StammdatenTypes.schluesselzuteilung);
            base.ExecuteEntfernenCommand();
            */
        }
        #endregion
    }
}
