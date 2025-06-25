using Base.Logic.Core;
using Base.Logic.Messages;
using Base.Logic.ViewModels;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.BaseMessages;
using Logic.Messages.UtilMessages;
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
    public class SchluesselverteilungDokumentationViewModel : ViewModelBasis
    {
        private int schluesselbesitzerId;
        private bool dokumentationRueckgabeErstellt;
        private bool dokumentationRueckgabeAbgeschlossen;
        private bool dokumentationZuteilungErstellt;
        private bool dokumentationZuteilungAbgeschlossen;
        public SchluesselverteilungDokumentationViewModel()
        {
            Title = "Schlüsselverteilung Dokumentation";
            CreateZuteilungPDFCommand = new RelayCommand(() => ExecuteCreateZuteilungPDFCommand());
            CreateRueckgabePDFCommand = new RelayCommand(() => ExecuteCreateRueckgabePDFCommand());
            OffeneZuteilungErledigenCommand = new RelayCommand(() => ExecuteOffeneZuteilungErledigenCommand());
            OffenRueckgabenErledigenCommand = new RelayCommand(() => ExecuteOffeneRueckgabenErledigenCommand());
        }

        public void SetSchluesselbesitzerId(int schluesselbesitzerId, bool dokumentationRueckgabeErstellt ,bool dokumentationRueckgabeAbgeschlossen ,bool dokumentationZuteilungErstellt , bool dokumentationZuteilungAbgeschlossen)
        {
            this.schluesselbesitzerId = schluesselbesitzerId;
            this.dokumentationRueckgabeErstellt = dokumentationRueckgabeErstellt;
            this.dokumentationRueckgabeAbgeschlossen = dokumentationRueckgabeAbgeschlossen;
            this.dokumentationZuteilungErstellt = dokumentationZuteilungErstellt;
            this.dokumentationZuteilungAbgeschlossen = dokumentationZuteilungAbgeschlossen;
            OnPropertyChanged(nameof(DokumentationZuteilungErstellt));
            OnPropertyChanged(nameof(DokumentationZuteilungAbgeschlossen));
            OnPropertyChanged(nameof(DokumentationRueckgabeAbgeschlossen));
            OnPropertyChanged(nameof(DokumentationRueckgabeErstellt));

        }


        #region Bindings
        public ICommand CreateZuteilungPDFCommand { get; set; }
        public ICommand CreateRueckgabePDFCommand { get; set; }
        public ICommand OffeneZuteilungErledigenCommand { get; set; }
        public ICommand OffenRueckgabenErledigenCommand { get; set; }

        public bool DokumentationRueckgabeErstellt => !dokumentationRueckgabeErstellt;
        public bool DokumentationRueckgabeAbgeschlossen => !dokumentationRueckgabeAbgeschlossen;
        public bool DokumentationZuteilungErstellt => !dokumentationZuteilungErstellt;
        public bool DokumentationZuteilungAbgeschlossen => !dokumentationZuteilungAbgeschlossen;

        #endregion

        #region Commands
        private async void ExecuteCreateZuteilungPDFCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                WeakReferenceMessenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "PDF wird erstellt" }, "SchluesselverteilungDokumentation");
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/schluesselverwaltung/dokumentation/zuteilung/pdf?schluesselbesitzerId={schluesselbesitzerId}");
                if (resp.IsSuccessStatusCode)
                {
                    string pfad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Vereinsverwaltung\\Dokumentation";
                    if (!Directory.Exists(pfad))
                    {
                        Directory.CreateDirectory(pfad);
                    }
                    dokumentationZuteilungErstellt = true;
                    OnPropertyChanged(nameof(DokumentationZuteilungErstellt));
                    await File.WriteAllBytesAsync(Path.Combine(pfad, resp.Content.Headers.ContentDisposition.FileNameStar), resp.Content.ReadAsByteArrayAsync().Result);
                    Process.Start("explorer.exe", $@"{pfad}");
                }
                else
                {
                    SendExceptionMessage("Datei konnte nicht heruntergeladen werden.");
                }
                WeakReferenceMessenger.Default.Send(new CloseLoadingViewMessage(), "SchluesselverteilungDokumentation");
            }
        }

        private async void ExecuteCreateRueckgabePDFCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                WeakReferenceMessenger.Default.Send(new OpenLoadingViewMessage { Beschreibung = "PDF wird erstellt" }, "SchluesselverteilungDokumentation");
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL + $"/api/schluesselverwaltung/dokumentation/rueckgabe/pdf?schluesselbesitzerId={schluesselbesitzerId}");
                if (resp.IsSuccessStatusCode)
                {
                    string pfad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Vereinsverwaltung\\Dokumentation";
                    if (!Directory.Exists(pfad))
                    {
                        Directory.CreateDirectory(pfad);
                    }
                    dokumentationRueckgabeErstellt = true;
                    OnPropertyChanged(nameof(DokumentationRueckgabeErstellt));
                    await File.WriteAllBytesAsync(Path.Combine(pfad, resp.Content.Headers.ContentDisposition.FileNameStar), resp.Content.ReadAsByteArrayAsync().Result);
                    Process.Start("explorer.exe", $@"{pfad}");
                }
                else
                {
                    SendExceptionMessage("Datei konnte nicht heruntergeladen werden.");
                }
                WeakReferenceMessenger.Default.Send(new CloseLoadingViewMessage(), "SchluesselverteilungDokumentation");
            }
        }

        public async void ExecuteOffeneZuteilungErledigenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsync(GlobalVariables.BackendServer_URL + $"/api/schluesselverwaltung/dokumentation/zuteilung/erledigen?schluesselbesitzerId={schluesselbesitzerId}", null);
                if (resp.IsSuccessStatusCode)
                {
                    dokumentationZuteilungAbgeschlossen = true;
                    OnPropertyChanged(nameof(DokumentationZuteilungAbgeschlossen));
                    SendInformationMessage("Zuteilung wurde erledigt.");
                }
                else
                {
                    SendExceptionMessage("Zuteilung konnte nicht erledigt werden.");
                }
                RequestIsWorking = false;
            }
        }

        public async void ExecuteOffeneRueckgabenErledigenCommand()
        {
            if (GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.PostAsync(GlobalVariables.BackendServer_URL + $"/api/schluesselverwaltung/dokumentation/rueckgabe/erledigen?schluesselbesitzerId={schluesselbesitzerId}", null);
                if (resp.IsSuccessStatusCode)
                {
                    dokumentationRueckgabeAbgeschlossen = true;
                    OnPropertyChanged(nameof(DokumentationRueckgabeAbgeschlossen));
                    SendInformationMessage("Rückgabe wurde erledigt.");
                }
                else
                {
                    SendExceptionMessage("Rückgabe konnte nicht erledigt werden.");
                }
                RequestIsWorking = false;
            }
        }
        #endregion
    }
}
