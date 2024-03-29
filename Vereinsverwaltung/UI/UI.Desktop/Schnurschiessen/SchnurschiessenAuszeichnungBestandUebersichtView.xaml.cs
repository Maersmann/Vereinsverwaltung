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
    /// Interaktionslogik für SchnuereBestandUebersichtView.xaml
    /// </summary>
    public partial class SchnurschiessenAuszeichnungBestandUebersichtView : UserControl
    {
        public SchnurschiessenAuszeichnungBestandUebersichtView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<OpenAuszeichnungGekauftEintragenMessage, string>(this, "SchnurschiessenAuszeichnungBestandUebersicht", (r, m) => ReceiveOpenSchnurGekauftEintragenMessage(m));
        }

        private static void ReceiveOpenSchnurGekauftEintragenMessage(OpenAuszeichnungGekauftEintragenMessage m)
        {
            var view = new SchnurschiessenAuszeichnungGekauftEintragenView
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is SchnurschiessenAuszeichnungGekauftEintragenViewModel model)
            {
                model.SetzeInformationen(m.SchnurauszeichnungId, m.Bezeichnung);
            }
            view.ShowDialog();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<OpenAuszeichnungGekauftEintragenMessage, string>(this, "SchnurschiessenAuszeichnungBestandUebersicht");
        }
    }
}
