using CommunityToolkit.Mvvm.Messaging;
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
            WeakReferenceMessenger.Default.Register<OpenPinAusgabeMitgliederViewMessage, string>(this, "PinAusgabeUebersicht", (r, m) => ReceiveOpenPinAusgabeMitgliederViewMessage(m));
        }

        private static void ReceiveOpenPinAusgabeMitgliederViewMessage(OpenPinAusgabeMitgliederViewMessage m)
        {
            PinAusgabeMitgliedUebersichtView view = new()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is PinAusgabeMitgliedUebersichtViewModel model)
            {
                model.SetFilterData(m.FilterText, m.ZeigeNurOffene);
                model.LoadData(m.ID);
                view.ShowDialog();
            }
        }
    }
}
