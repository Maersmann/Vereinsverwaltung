using GalaSoft.MvvmLight.Messaging;
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
            Messenger.Default.Register<OpenPinAusgabeAuswahlMessage>(this, "PinAusgabeAuswertungTagStunde", m => ReceiveOpenPinAusgabeAuswahlMessage(m));
        }

        private void ReceiveOpenPinAusgabeAuswahlMessage(OpenPinAusgabeAuswahlMessage m)
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
            Messenger.Default.Unregister<OpenPinAusgabeAuswahlMessage>(this);
        }
    }
}
