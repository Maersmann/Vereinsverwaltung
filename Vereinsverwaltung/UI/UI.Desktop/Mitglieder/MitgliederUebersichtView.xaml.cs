using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.KoenigschiessenMessages;
using Logic.Messages.PinMessages;
using Logic.UI.KoenigschiessenViewModels;
using Logic.UI.PinViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.Desktop.Koenigschiessen;
using UI.Desktop.Pin;

namespace Vereinsverwaltung.UI.Desktop.Mitglieder
{
    /// <summary>
    /// Interaktionslogik für MitgliederUebersichtView.xaml
    /// </summary>
    public partial class MitgliederUebersichtView : UserControl
    {
        public MitgliederUebersichtView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<OpenPinsVomMitgliedUebersichtMessage, string>(this, "MitgliederUebersicht", (r, m) => ReceiveOpenPinsVomMitgliedUebersichtMessage(m));
            WeakReferenceMessenger.Default.Register<OpenKoenigschiessenErgebnisseVomMitgliedMessage, string>(this, "MitgliederUebersicht", (r, m) => ReceiveOpenKoenigschiessenErgebnisseVomMitgliedMessage(m));
        }

        private static void ReceiveOpenKoenigschiessenErgebnisseVomMitgliedMessage(OpenKoenigschiessenErgebnisseVomMitgliedMessage m)
        {
            var view = new KoenigschiessenErgebnisseVonMitgliedView
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is KoenigschiessenErgebnisseVonMitgliedViewModel model)
            {
                model.ZeigeDatenAn(m.MitgliedID);
            }
            view.ShowDialog();
        }

        private static void ReceiveOpenPinsVomMitgliedUebersichtMessage(OpenPinsVomMitgliedUebersichtMessage m)
        {
            var view = new PinsVomMitgliedUebersichtView
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is PinsVomMitgliedUebersichtViewModel model)
            {
                model.LoadData(m.ID);
            }
            view.ShowDialog();
        }
    }
}
