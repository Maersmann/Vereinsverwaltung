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
    public class SchluesselverteilungSchluesselUebersichtDetailViewModel : ViewModelUebersicht<SchluesselverteilungSchluesselUebersichtDetailModel>
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
            // Todo: Request
            /*
            itemList = new SchluesselzuteilungAPI().LadeAlleFuerSchluessel(id);
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
