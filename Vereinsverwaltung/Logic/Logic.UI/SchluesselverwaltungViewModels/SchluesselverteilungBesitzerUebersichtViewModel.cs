using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.SchluesselMessages;
using Logic.UI.BaseViewModels;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungBesitzerUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<SchluesselverteilungBesitzerUebersichtModel, StammdatenTypes>
    {
        public SchluesselverteilungBesitzerUebersichtViewModel()
        {
            MessageToken = "SchluesselverteilungBesitzerUebersicht";
            Title = "Übersicht Verteilung Schlüsselbesitzer";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung.ToString());
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselbesitzer.ToString());
        }

        protected override int GetID() { return SelectedItem.SchluesselbesitzerID; }

        protected override SchluesselzuteilungTypes GetSchluesselzuteilungAuswahlTyp() { return SchluesselzuteilungTypes.Besitzer; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/zuteilung/besitzer"; }
        protected override bool WithPagination() { return true; }

        #region Bindings

        public override SchluesselverteilungBesitzerUebersichtModel SelectedItem 
        { 
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
                if (SelectedItem != null)
                {
                    WeakReferenceMessenger.Default.Send(new LoadSchluesselverteilungBesitzerDetailMessage { ID = SelectedItem.SchluesselbesitzerID }, messageToken);
                }
            } 
        }

        protected override void ExecuteOpenDokumentationCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenSchluesselzuteilungDokumentationMessage
            {
                ID = GetID(),
                DokumentationRueckgabeAbgeschlossen = SelectedItem.DokumentationRueckgabeAbgeschlossen,
                DokumentationRueckgabeErstellt = SelectedItem.DokumentationRueckgabeErstellt,
                DokumentationZuteilungAbgeschlossen = SelectedItem.DokumentationZuteilungAbgeschlossen,
                DokumentationZuteilungErstellt = SelectedItem.DokumentationZuteilungErstellt,
                Command = async () => await LoadData()
            }, messageToken);
        }
        #endregion
    }
}
