﻿using Data.Model.AuswertungModels.PinAusgabeAuswertungModels;

using CommunityToolkit.Mvvm.Messaging;
using Logic.Core;
using Logic.Messages.AuswahlMessages;
using Base.Logic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Base.Logic.Core;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Measure;
using SkiaSharp;
using CommunityToolkit.Mvvm.Input;

namespace Logic.UI.AuswertungenViewModels
{
    public class PinAusgabeAuswertungTagViewModel : ViewModelAuswertung<PinAusgabeAuswertungTagModel>
    {

        public PinAusgabeAuswertungTagViewModel()
        {
            Data = new PinAusgabeAuswertungTagModel();
            SecondTitle = "";
            Title = "Auswertung Tag";
            AuswahlCommand = new RelayCommand(() => ExcecuteAuswahlCommand());
            YAxes =
                [
                    new Axis()
                    {
                        LabelsPaint = new SolidColorPaint{ Color = SKColors.CornflowerBlue },
                        Position = AxisPosition.Start,
                        Labeler = (value) =>  string.Format("{0}", value)
                    }
                ];
        }

        private void ExcecuteAuswahlCommand()
        {
            WeakReferenceMessenger.Default.Send(new OpenPinAusgabeAuswahlMessage(LoadAuswertungCallback), "PinAusgabeAuswertungTag");
        }

        public async void LoadAuswertungCallback(bool confirmed, int id)
        {
            if (confirmed && GlobalVariables.ServerIsOnline)
            {
                RequestIsWorking = true;
                HttpResponseMessage resp = await Client.GetAsync(GlobalVariables.BackendServer_URL+ $"/api/auswertungen/pinausgabe/Tag/{id}");
                if (resp.IsSuccessStatusCode)
                {
                    Data = await resp.Content.ReadAsAsync<PinAusgabeAuswertungTagModel>();
                    SecondTitle = "Auswertung von: " + Data.Bezeichnung;
                    OnPropertyChanged(nameof(SecondTitle));
                    OnPropertyChanged(nameof(Data));
                    OnPropertyChanged(nameof(SecondTitle));
                    OnPropertyChanged(nameof(Data));
                    OnPropertyChanged(nameof(Pin));
                    OnPropertyChanged(nameof(Abgeschlossen));
                    OnPropertyChanged(nameof(Verteilt));
                    OnPropertyChanged(nameof(Offen));

                    IList<int> values = [];
                    Labels = new string[Data.Auswertung.Count];
                    int index = 0;
                    Data.Auswertung.OrderBy(s => s.Tag).ToList().ForEach(a =>
                    {
                        values.Add(a.Anzahl);
                        Labels[index] = a.Tag.ToString("dd.MM");
                        index++;
                    });

                    var auswertungSeries = new ColumnSeries<int>
                    {
                        Values = values,
                        Name = "Anzahl",
                    };

                    XAxes.First().Labels = Labels;
                    XAxes.First().Name = "Tag";
                    YAxes.First().Name = "Anzahl";

                    Series = new ColumnSeries<int>[1] { auswertungSeries };

                    OnPropertyChanged(nameof(Series));
                    OnPropertyChanged(nameof(XAxes));
                    OnPropertyChanged(nameof(YAxes));                 
                }
                RequestIsWorking = false;
            }
        }
        

        public string SecondTitle { get; set; }
        public string Pin => Data.PinBezeichnung;
        public string Abgeschlossen
        {
            get
            {
                if (Data.Bezeichnung.Equals("")) return "";
                else if (Data.Abgeschlossen) return "Ja";
                else return "Nein";
            }
        }
        public int Verteilt => Data.Verteilt;
        public int Offen => Data.Offen;
        public ICommand AuswahlCommand { get; set; }
    }
}
