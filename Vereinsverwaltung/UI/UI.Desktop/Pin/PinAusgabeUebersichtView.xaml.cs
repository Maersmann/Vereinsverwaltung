using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.PinMessages;
using Logic.UI.PinViewModels;
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

namespace UI.Desktop.Pin
{
    /// <summary>
    /// Interaktionslogik für PinAusgabeUebersichtView.xaml
    /// </summary>
    public partial class PinAusgabeUebersichtView : UserControl
    {
        public PinAusgabeUebersichtView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenPinAusgabeMitgliederViewMessage>(this, "PinAusgabeUebersicht", m => ReceiveOpenPinAusgabeMitgliederViewMessage(m));
        }

        private void ReceiveOpenPinAusgabeMitgliederViewMessage(OpenPinAusgabeMitgliederViewMessage m)
        {
            var view = new PinAusgabeMitgliedUebersichtView();
            if (view.DataContext is PinAusgabeMitgliedUebersichtViewModel model)
            {
                model.LoadData(m.ID);
                view.ShowDialog();
            }
        }
    }
}
