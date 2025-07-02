using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Data.Model.SchluesselverwaltungModels;
using Data.Model.SchluesselverwaltungModels.DTO;
using Data.Types;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.Messages.UtilMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.SchluesselverwaltungViewModels
{
    public class SchluesselverteilungKennungEintragenViewModel : ViewModelStammdaten<SchluesselKennungEintragenDTO, StammdatenTypes>, IViewModelStammdaten
    {
        public SchluesselverteilungKennungEintragenViewModel()
        {
            Title = "Schlüsselverteilung Dokumentation";
        }

        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.schluesselzuteilung;


        public int SchluesselbesitzerId
        {
            set
            {
                Data.SchlueselzuteilungID = value;
                
            }
        }

        public void ZeigeStammdatenAnAsync(int id)
        {
            throw new NotImplementedException();
        }


        #region Bindings
        public string Kennung
        {
            get => Data.Kennung;
            set
            {
                if (RequestIsWorking || !Equals(Data.Kennung, value))
                {
                    Validatekennung(value);
                    Data.Kennung = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Commands

        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/schluesselverwaltung/zuteilung/kennung", Data);

                if (resp.IsSuccessStatusCode)
                {
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp().ToString());
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else
                {
                    SendExceptionMessage("Kennung konnte nicht gespeichert werden.");
                }
                RequestIsWorking = false;

            }
        }
        #endregion

        private bool Validatekennung(string kennung)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateString(kennung, "", out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Kennung", validationErrors);
            return isValid;
        }

        protected override void OnActivated()
        {
            Data = new SchluesselKennungEintragenDTO();
            Kennung = "";
            state = State.Neu;
        }
    }
}
