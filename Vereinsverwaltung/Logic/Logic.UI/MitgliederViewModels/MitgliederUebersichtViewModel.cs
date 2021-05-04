using Data.Model.MitgliederModels;
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
using Logic.UI.BaseViewModels;
using Data.Types;
using Logic.Core;
using System.Net.Http;

namespace Logic.UI.MitgliederViewModels
{
    public class MitgliederUebersichtViewModel : ViewModelUebersicht<MitgliederUebersichtModel>
    {

        public MitgliederUebersichtViewModel()
        {
            Title = "Übersicht Mitglieder";
            RegisterAktualisereViewMessage(StammdatenTypes.mitglied);    
        }
        protected override int GetID() { return selectedItem.ID; }
        protected override StammdatenTypes GetStammdatenType() { return StammdatenTypes.mitglied; }

        public async override void LoadData()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                HttpResponseMessage resp2 = await Client.GetAsync("https://localhost:5001/api/Mitglieder");
                if (resp2.IsSuccessStatusCode)
                    itemList = await resp2.Content.ReadAsAsync<ObservableCollection<MitgliederUebersichtModel>>();
            }
            base.LoadData();
        }

        #region Commands

        protected override void ExecuteEntfernenCommand()
        {
            // Todo: Request
            /*
            try
            {
                new MitgliedAPI().Entfernen(selectedItem.ID);
            }
            catch (SchluesselbesitzerSindSchluesselZugeteiltException)
            {
                SendExceptionMessage("Mitglied kann nicht gelöscht werden" + Environment.NewLine + Environment.NewLine + "Zugeteilter Schlüssel vorhanden");
                return;
            }
            SendInformationMessage("Mitglied gelöscht");
            base.ExecuteEntfernenCommand();
            */
        }
        #endregion
    }
}
