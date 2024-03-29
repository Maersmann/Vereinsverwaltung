﻿using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.Types;
using Base.Logic.ViewModels;
using Data.Model.VereinsmeisterschaftModels;
using Data.Types;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Core.Validierungen.Base;
using Logic.Messages.BaseMessages;
using Logic.UI.InterfaceViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Logic.UI.VereinsmeisterschaftViewModels
{
    public class VereinsmeisterschaftNeueErstellenViewModel : ViewModelStammdaten<VereinsmeisterschaftModel, StammdatenTypes>, IViewModelStammdaten
    {
        private bool neueVereinsmeisterschaftErstellt;
        public VereinsmeisterschaftNeueErstellenViewModel()
        {
            Title = "Neue Vereinsmeisterschaft";
            neueVereinsmeisterschaftErstellt = false;
        }

        public void ZeigeStammdatenAnAsync(int id)
        {
        }
        public bool NeueVereinsmeisterschaftErstellt => neueVereinsmeisterschaftErstellt;
        protected override StammdatenTypes GetStammdatenTyp() => StammdatenTypes.vereinsmeisterschaft;

        #region Commands
        protected async override void ExecuteSaveCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsJsonAsync(GlobalVariables.BackendServer_URL + $"/api/vereinsmeisterschaften", Data);

                if (resp.IsSuccessStatusCode)
                {
                    neueVereinsmeisterschaftErstellt = true;
                    WeakReferenceMessenger.Default.Send(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Neue Vereinsmeisterschaft erstellt" }, GetStammdatenTyp().ToString());
                    WeakReferenceMessenger.Default.Send(new AktualisiereViewMessage(), GetStammdatenTyp().ToString());
                }
                else if (!resp.IsSuccessStatusCode)
                {
                    SendExceptionMessage("Neue Vereinsmeisterschaft konnte nicht erstellt werden.");
                }
                RequestIsWorking = false;

            }
        }
        #endregion

        #region Bindings

        public DateTime? Stichttag
        {
            get => Data.Stichttag;
            set
            {

                if (RequestIsWorking || !Equals(Data.Stichttag, value))
                {
                    ValidateDatum(value);
                    Data.Stichttag = value.GetValueOrDefault(DateTime.Now);
                    base.OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Validierung

        private bool ValidateDatum(DateTime? eintrittsdatum)
        {
            var Validierung = new BaseValidierung();

            bool isValid = Validierung.ValidateDatum(eintrittsdatum, out ICollection<string> validationErrors);

            AddValidateInfo(isValid, "Stichttag", validationErrors);

            return isValid;
        }
        #endregion

        protected override void OnActivated()
        {
            Data = new VereinsmeisterschaftModel();
            Stichttag = DateTime.Now;
            state = State.Neu;   
        }
    }
}
