﻿using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.AuswahlMessages;
using Logic.UI.AuswahlViewModels;
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
using UI.Desktop.Auswahl;

namespace UI.Desktop.Auswertungen
{
    /// <summary>
    /// Interaktionslogik für PinAusgabeAuswertungTagStundeView.xaml
    /// </summary>
    public partial class PinAusgabeAuswertungTagStundeView : UserControl
    {
        public PinAusgabeAuswertungTagStundeView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<OpenPinAusgabeAuswahlMessage, string>(this, "PinAusgabeAuswertungTagStunde", (r, m) => ReceiveOpenPinAusgabeAuswahlMessage(m));
        }

        private static void ReceiveOpenPinAusgabeAuswahlMessage(OpenPinAusgabeAuswahlMessage m)
        {
            var view = new PinAusgabeAuswahlView
            {
                Owner = Application.Current.MainWindow
            };
            view.ShowDialog();
            if (view.DataContext is PinAusgabeAuswahlViewModel model)
            {
                if (model.AuswahlGetaetigt && model.ID().HasValue)
                    m.Callback(true, model.ID().Value);
                else
                    m.Callback(false, 0);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<OpenPinAusgabeAuswahlMessage, string>(this, "PinAusgabeAuswertungTagStunde");
        }
    }
}
