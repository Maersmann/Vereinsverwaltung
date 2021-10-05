using Base.Logic.Core;
using Base.Logic.ViewModels;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.UtilMessages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.ExportViewModels
{
    public class ExportSchluesselViewModel : ViewModelBasis
    {
        public ExportSchluesselViewModel()
        {
            Title = "Export von Schlüssel";
            ExportSchluesselCommand = new RelayCommand(() => ExcecuteExportSchluesselCommand());
            ExportZuteilungCommand = new RelayCommand(() => ExcecuteExportZuteilungCommand());
        }


        #region Bindings
        public ICommand ExportSchluesselCommand { get; set; }
        public ICommand ExportZuteilungCommand { get; set; }
        #endregion

        #region Commands
        private async void ExcecuteExportZuteilungCommand()
        {
            Messenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Zuteilung wird heruntergeladen"}, "ExportSchluessel");
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/schluesselverwaltung/zuteilung/Export/Zuteilung/");
            if (resp.IsSuccessStatusCode)
            {
                string pfad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Vereinsverwaltung\\Export\\Schluesselzuteilung";
                if (!Directory.Exists(pfad))
                {
                    Directory.CreateDirectory(pfad);
                }

                await File.WriteAllBytesAsync(Path.Combine(pfad, resp.Content.Headers.ContentDisposition.FileNameStar), resp.Content.ReadAsByteArrayAsync().Result);
                Process.Start("explorer.exe" , $@"{pfad}");
            }
            else
            {
                SendExceptionMessage("Datei konnte nicht heruntergeladen werden.");
            }
            Messenger.Default.Send(new CloseLoadingViewMessage(), "ExportSchluessel");
        }

        private async void ExcecuteExportSchluesselCommand()
        {
            Messenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Schlüsselliste wird heruntergeladen"}, "ExportSchluessel");
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/schluesselverwaltung/zuteilung/Export/Schluessel/");
            if (resp.IsSuccessStatusCode)
            {
                string pfad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Vereinsverwaltung\\Export\\Schluesselliste";
                if (!Directory.Exists(pfad))
                {
                    Directory.CreateDirectory(pfad);
                }
                await File.WriteAllBytesAsync(Path.Combine(pfad, resp.Content.Headers.ContentDisposition.FileNameStar), resp.Content.ReadAsByteArrayAsync().Result);
                Process.Start("explorer.exe", $@"{pfad}");
            }
            else
            {
                SendExceptionMessage("Datei konnte nicht heruntergeladen werden.");
            }
            Messenger.Default.Send(new CloseLoadingViewMessage(), "ExportSchluessel");
        }
        #endregion
    }
}
