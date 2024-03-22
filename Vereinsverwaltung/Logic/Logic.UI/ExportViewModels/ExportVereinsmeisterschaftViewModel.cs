using Base.Logic.Core;
using Base.Logic.ViewModels;
using Data.Types.AuswahlTypes;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.AuswahlMessages;
using Logic.Messages.UtilMessages;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Logic.UI.ExportViewModels
{
    public class ExportVereinsmeisterschaftViewModel : ViewModelBasis
    {
        public int? vereinsmeisterschaftID;
        public int jahr;
        public ExportVereinsmeisterschaftViewModel()
        {
            vereinsmeisterschaftID = null;
            jahr = 0;
            MessageToken = "ExportVereinsmeisterschaft";
            Title = "Export Vereinsmeisterschaft";

            ExportMedaillenCommand = new DelegateCommand(ExcecuteExportMedaillenCommand, CanExecuteCommand);
            ExportErgebnisseCommand = new DelegateCommand(ExcecuteExportErgebnisseCommand, CanExecuteCommand);
            AuswahlVereinsmeisterschaftCommand = new RelayCommand(() => ExcecuteAuswahlVereinsmeisterschaftCommand());
        }


        #region Bindings
        public ICommand ExportMedaillenCommand { get; set; }
        public ICommand ExportErgebnisseCommand { get; set; }
        public ICommand AuswahlVereinsmeisterschaftCommand { get; set; }
        public string Vereinsmeisterschaft => vereinsmeisterschaftID.HasValue ? $"Vereinsmeisterschaft " + jahr : "";
        #endregion

        #region Commands
        private async void ExcecuteExportErgebnisseCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Zuteilung wird heruntergeladen" }, messageToken);
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/export/vereinsverwaltung/Ergebnisse?vereinsmeisterschaftID={vereinsmeisterschaftID.Value}");
            if (resp.IsSuccessStatusCode)
            {
                string pfad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Vereinsverwaltung\\Export\\Vereinsmeisterschaften";
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
            WeakReferenceMessenger.Default.Send(new CloseLoadingViewMessage(), messageToken);
        }
        private async void ExcecuteExportMedaillenCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "Schlüsselliste wird heruntergeladen" }, messageToken);
            HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/export/vereinsverwaltung/Medaillen?vereinsmeisterschaftID={vereinsmeisterschaftID.Value}");
            if (resp.IsSuccessStatusCode)
            {
                string pfad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Vereinsverwaltung\\Export\\Vereinsmeisterschaften";
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
            WeakReferenceMessenger.Default.Send(new CloseLoadingViewMessage(), messageToken);
        }
        private void ExcecuteAuswahlVereinsmeisterschaftCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenVereinsmeisterschaftAuswahlMessage(OpenVereinsmeisterschatAuswahlCallback){ AuswahlTyp = AuswahlVereinsmeisterschaftTypes.nurAbgeschlossene }, messageToken);
        }


        private bool CanExecuteCommand() => vereinsmeisterschaftID.HasValue;

        #endregion

        #region Callback
        private void OpenVereinsmeisterschatAuswahlCallback(bool confirmed, int id, int jahr)
        {
            if (confirmed)
            {
                vereinsmeisterschaftID = id;
                this.jahr = jahr;
                OnPropertyChanged(nameof(Vereinsmeisterschaft));
            }
            ((DelegateCommand)ExportMedaillenCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)ExportErgebnisseCommand).RaiseCanExecuteChanged();
        }
        #endregion
    }
}