using Data.Model.MitgliederModels;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Core.ImportHelper;
using Logic.UI.BaseViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.MitgliederViewModels
{
    public class MitgliederImportViewModel : ViewModelUebersicht<MitgliederImportModel>
    {
        public MitgliederImportViewModel()
        {
            Title = "Import Mitglieder";
            ImportCommand = new RelayCommand(() => ExecuteImportCommand());
            SaveCommand = new RelayCommand(() => ExecuteSaveCommand());
        }

        #region Bindings
        public  ICommand ImportCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        #endregion

        #region Commands
        private async void ExecuteImportCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    await new ImportHelper().PostFile( openFileDialog.FileName ).ContinueWith(async task =>
                    { 
                        if (task.Result.IsSuccessStatusCode)
                        {
                            itemList = await task.Result.Content.ReadAsAsync<ObservableCollection<MitgliederImportModel>>();
                            this.RaisePropertyChanged("ItemList");
                        }
                           
                    });
               
                   // itemList = new ClassMitgliederImport(history).StartImport(openFileDialog.FileName);
                    //
                }
              


                /*
                if (resp2.IsSuccessStatusCode)
                {
                    Messenger.Default.Send<StammdatenGespeichertMessage>(new StammdatenGespeichertMessage { Erfolgreich = true, Message = "Gespeichert" }, GetStammdatenTyp());
                    Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), GetStammdatenTyp());
                }
                else if (resp2.StatusCode.Equals(HttpStatusCode.InternalServerError))
                {
                    SendExceptionMessage("Mitgliedsnr ist schon vorhanden");
                    return;
                }
                */
            }

            
        }

        private void ExecuteSaveCommand()
        {
            //Todo: Request
            /*
            new ClassMitgliederImport(history).Uebernahme(itemList);   
            itemList.Clear();
            this.RaisePropertyChanged("ItemList");
            Messenger.Default.Send<AktualisiereViewMessage>(new AktualisiereViewMessage(), StammdatenTypes.mitglied);
            */
        }
        #endregion

    }
}
