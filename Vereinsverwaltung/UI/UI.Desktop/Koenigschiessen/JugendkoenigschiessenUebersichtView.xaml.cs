using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.KoenigschiessenMessages;
using Logic.UI.KoenigschiessenViewModels;
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

namespace UI.Desktop.Koenigschiessen
{
    /// <summary>
    /// Interaktionslogik für JugendkoenigschiessenUebersichtView.xaml
    /// </summary>
    public partial class JugendkoenigschiessenUebersichtView : UserControl
    {
        public JugendkoenigschiessenUebersichtView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<OpenKoenigschiessenAnmeldungViewMessage, string>(this, "JugendkoenigUebersicht", (r, m) => ReceiveOpenKoenigschiessenViewMessage(m));
            WeakReferenceMessenger.Default.Register<OpenKoenigschiessenRundeTeilnehmerUebersichtViewMessage, string>(this, "JugendkoenigUebersicht", (r, m) => ReceiveOpenKoenigschiessenErgebnisViewMessage(m));
            WeakReferenceMessenger.Default.Register<OpenKoenigschiessenZahlenMessage, string>(this, "JugendkoenigUebersicht", (r, m) => ReceiveOpenKoenigschiessenZahlenMessage(m));
        }

        private async void ReceiveOpenKoenigschiessenErgebnisViewMessage(OpenKoenigschiessenRundeTeilnehmerUebersichtViewMessage m)
        {
            KoenigschiessenRundeTeilnehmerUebersichtView view = new()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is KoenigschiessenRundeTeilnehmerUebersichtViewModel model)
            {
                model.LadeUebersicht(m.Jahr, m.Variante, m.Runde, m.Art);
                view.ShowDialog();
                if (model.RundeBeendet)
                {
                    if (DataContext is JugendkoenigUebersichtViewModel modelUebersicht)
                    {
                        await modelUebersicht.LoadData();
                    }
                }
            }
        }

        private static void ReceiveOpenKoenigschiessenViewMessage(OpenKoenigschiessenAnmeldungViewMessage m)
        {
            KoenigschiessenAnmeldungUebersichtView view = new()
            {
                Owner = Application.Current.MainWindow
            };
            view.SetzeWerte(m.Variante);
            if (view.DataContext is KoenigschiessenAnmeldungUebersichtViewModel model)
            {
                model.LadeUebersicht(m.Jahr, m.Variante);
                view.ShowDialog();
            }
        }

        private static void ReceiveOpenKoenigschiessenZahlenMessage(OpenKoenigschiessenZahlenMessage m)
        {
            KoenigschiessenRundenZahlenView view = new()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is KoenigschiessenRundenZahlenViewModel model)
            {
                model.ZeigeDatenAn(m.Jahr, m.Variante);
                view.ShowDialog();
            }
        }

        private void WindowUnloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<OpenKoenigschiessenAnmeldungViewMessage, string>(this, "JugendkoenigUebersicht");
            WeakReferenceMessenger.Default.Unregister<OpenKoenigschiessenRundeTeilnehmerUebersichtViewMessage, string>(this, "JugendkoenigUebersicht");
            WeakReferenceMessenger.Default.Unregister<OpenKoenigschiessenZahlenMessage, string>(this, "JugendkoenigUebersicht");
        }
    }
}
