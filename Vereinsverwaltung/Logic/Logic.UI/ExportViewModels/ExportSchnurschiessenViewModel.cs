using Base.Logic.Core;
using Base.Logic.ViewModels;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.UtilMessages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.ExportViewModels
{
    public class ExportSchnurschiessenViewModel : ViewModelBasis
    {
        public ExportSchnurschiessenViewModel()
        {
            Title = "Export vom Schnurschiessen";
            ExportAktuellenStandCommand = new RelayCommand(() => ExcecuteExportAktuellenStandCommand());
            ExportAlleMitStandCommand = new RelayCommand(() => ExcecuteExportAlleMitStandCommand());

        }


        #region Bindings
        public ICommand ExportAktuellenStandCommand { get; set; }
        public ICommand ExportAlleMitStandCommand { get; set; }
        #endregion

        #region Commands
        private async void ExcecuteExportAktuellenStandCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Akutellen Stand wird heruntergeladen" }, "ExportSchnurschiessen");
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/export/schnurschiessen/AktuellenStand");
            if (resp.IsSuccessStatusCode)
            {
                string pfad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Vereinsverwaltung\\Export\\Schnurschiessen";
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
            WeakReferenceMessenger.Default.Send(new CloseLoadingViewMessage(), "ExportSchnurschiessen");
        }

        private async void ExcecuteExportAlleMitStandCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Akutellen Stand wird heruntergeladen" }, "ExportSchnurschiessen");
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/export/schnurschiessen/AlleMitStand");
            if (resp.IsSuccessStatusCode)
            {
                string pfad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Vereinsverwaltung\\Export\\Schnurschiessen";
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
            WeakReferenceMessenger.Default.Send(new CloseLoadingViewMessage(), "ExportSchnurschiessen");
        }
        #endregion

    }
}