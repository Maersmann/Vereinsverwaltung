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
using System.Windows.Shapes;
using UI.Desktop.Auswahl;
using UI.Desktop.BaseViews;

namespace UI.Desktop.KkSchiessen
{
    /// <summary>
    /// Interaktionslogik für KKSchiessenStammdatenView.xaml
    /// </summary>
    public partial class KKSchiessenStammdatenView : StammdatenView
    {
        public KKSchiessenStammdatenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.kkSchiessen);
            WeakReferenceMessenger.Default.Register<OpenKkSchiessgruppeAuswahlMessage, string>(this, "KkSchiessenStammdaten", (r, m) => ReceiveOpenKkSchiessgruppeAuswahlMessage(m));
        }

        private static void ReceiveOpenKkSchiessgruppeAuswahlMessage(OpenKkSchiessgruppeAuswahlMessage m)
        {
            var view = new KkSchiessgruppeAuswahlView()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is KkSchiessgruppeAuswahlViewModel model)
            {
                view.ShowDialog();
                if (model.AuswahlGetaetigt && model.ID().HasValue)
                {
                    m.Callback(true, model.ID().Value);
                }
                else
                {
                    m.Callback(false, 0);
                }
            }
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<OpenKkSchiessgruppeAuswahlMessage, string>(this, "KkSchiessenStammdaten");
            base.Window_Unloaded(sender, e);
        }
    }
}
