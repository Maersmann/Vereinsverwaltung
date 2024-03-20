using Data.Types.KoenigschiessenTypes;
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
using System.Windows.Shapes;
using UI.Desktop.BaseViews;

namespace UI.Desktop.Koenigschiessen
{
    /// <summary>
    /// Interaktionslogik für KoenigschiessenAnmeldungUebersichtView.xaml
    /// </summary>
    public partial class KoenigschiessenAnmeldungUebersichtView : BaseView
    {
        public KoenigschiessenAnmeldungUebersichtView()
        {
            InitializeComponent();
            RegisterMessages("KoenigschiessenAnmeldungUebersicht");
            WeakReferenceMessenger.Default.Register<OpenKoenigschiessenBestaetigungMessage, string>(this, "KoenigschiessenAnmeldungUebersicht", (r, m) => ReceivOpenKoenigschiessenBestaetigungMessage( m));

        }

        public void SetzeWerte(KoenigschiessenVarianten variante)
        {
            if (variante.Equals(KoenigschiessenVarianten.koenigschiessen))
            {
                KoenigschiessenAnmeldungWerteKoenigView WerteKoenigschiessen = new();
                Werte.NavigationService.Navigate(WerteKoenigschiessen);
                if (DataContext is KoenigschiessenAnmeldungUebersichtViewModel model)
                {
                    if (WerteKoenigschiessen.DataContext is KoenigschiessenAnmeldungWerteKoenigViewModel modelWerte)
                        model.SetzeWerteInterface(modelWerte);
                }
            }
            else
            {
                KoenigschiessenAnmeldungWerteJugendkoenigView WerteKoenigschiessen = new();
                Werte.NavigationService.Navigate(WerteKoenigschiessen);
                if (DataContext is KoenigschiessenAnmeldungUebersichtViewModel model)
                {
                    if (WerteKoenigschiessen.DataContext is KoenigschiessenAnmeldungWerteJugendkoenigViewModel modelWerte)
                        model.SetzeWerteInterface(modelWerte);
                }
            }
        }

        private static void ReceivOpenKoenigschiessenBestaetigungMessage(OpenKoenigschiessenBestaetigungMessage m)
        {
            KoenigschiessenAnmeldungBestaetigungView view = new()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is KoenigschiessenAnmeldungBestaetigungViewModel model)
            {
                model.ZeigeDatenAn(m.KoenigschiessenAnmeldung);
                view.ShowDialog();
                if (model.Bestaetigt)
                {
                    m.Command();
                }
            }
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<OpenKoenigschiessenBestaetigungMessage, string>(this, "KoenigschiessenAnmeldungUebersicht");
            base.Window_Unloaded(sender, e);
        }
    }
}
