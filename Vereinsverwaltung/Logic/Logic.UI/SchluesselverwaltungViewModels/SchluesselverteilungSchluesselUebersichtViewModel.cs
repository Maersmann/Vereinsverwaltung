﻿using Data.Model.SchluesselverwaltungModels;
using Data.Types;
using Data.Types.SchluesselverwaltungTypes;

using CommunityToolkit.Mvvm.Messaging;
using Logic.Core;
using Logic.Messages.SchluesselMessages;
using Logic.UI.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungSchluesselUebersichtViewModel : ViewModelSchluesselverwaltungUebersicht<SchluesselverteilungSchluesselUebersichtModel, StammdatenTypes>
    {
        public SchluesselverteilungSchluesselUebersichtViewModel()
        {
            MessageToken = "SchluesselverteilungSchluesselUebersicht";
            Title = "Übersicht Verteilung Schlüssel";
            RegisterAktualisereViewMessage(StammdatenTypes.schluesselzuteilung.ToString());
            RegisterAktualisereViewMessage(StammdatenTypes.schluessel.ToString());
        }

        protected override int GetID() { return SelectedItem.SchluesselID; }
        protected override SchluesselzuteilungTypes GetSchluesselzuteilungAuswahlTyp() { return SchluesselzuteilungTypes.Schluessel; }
        protected override string GetREST_API() { return $"/api/schluesselverwaltung/zuteilung/schluessel"; }
        protected override bool WithPagination() { return true; }

        #region Bindings

        public override SchluesselverteilungSchluesselUebersichtModel SelectedItem
        {
            get => base.SelectedItem;
            set
            {
                base.SelectedItem = value;
                if (SelectedItem != null)
                {
                    WeakReferenceMessenger.Default.Send(new LoadSchluesselverteilungSchluesselDetailMessage { ID = SelectedItem.SchluesselID }, messageToken);
                }
            }
        }
        #endregion
    }
}
