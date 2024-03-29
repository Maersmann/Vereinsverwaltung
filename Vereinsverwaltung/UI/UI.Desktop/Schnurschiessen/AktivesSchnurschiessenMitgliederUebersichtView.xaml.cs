﻿using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.SchnurschiessenMessages;
using Logic.UI.SchnurschiessenViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Desktop.Schnurschiessen
{
    /// <summary>
    /// Interaktionslogik für AktivesSchnurschiessenMitgliederUebersichtView.xaml
    /// </summary>
    public partial class AktivesSchnurschiessenMitgliederUebersichtView : UserControl
    {
        public AktivesSchnurschiessenMitgliederUebersichtView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<OpenAktivesSchnurschiessenTeilnahmeEintragenMessage, string>(this, "AktivesSchnurschiessenMitgliederUebersicht", (r, m) => ReceiveOpenAktivesSchnurschiessenTeilnahmeEintragenMessage(m));
        }

        private static void ReceiveOpenAktivesSchnurschiessenTeilnahmeEintragenMessage(OpenAktivesSchnurschiessenTeilnahmeEintragenMessage m)
        {
            var view = new AktivesSchnurschiessenTeilnahmeEintragenView
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is AktivesSchnurschiessenTeilnahmeEintragenViewModel model)
            {
                model.ZeigeStammdatenAnAsync(m.MitgliedId);
            }
            view.ShowDialog();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<OpenAktivesSchnurschiessenTeilnahmeEintragenMessage, string>(this, "AktivesSchnurschiessenMitgliederUebersicht");
        }
    }
}
