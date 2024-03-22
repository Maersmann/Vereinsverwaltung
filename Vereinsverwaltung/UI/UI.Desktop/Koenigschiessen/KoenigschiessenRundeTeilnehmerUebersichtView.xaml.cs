using Data.Types.KoenigschiessenTypes;
using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.BaseMessages;
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
    /// Interaktionslogik für KoenigschiessenErgebnisseUebersichtView.xaml
    /// </summary>
    public partial class KoenigschiessenRundeTeilnehmerUebersichtView : BaseView
    {
        public KoenigschiessenRundeTeilnehmerUebersichtView()
        {
            InitializeComponent();
            RegisterMessages("KoenigschiessenRundeTeilnehmerUebersicht");
            WeakReferenceMessenger.Default.Register<OpenKoenigschiessenErgebnisEintragenMessage, string>(this, "KoenigschiessenRundeTeilnehmerUebersicht", (r, m) => ReceiveOpenKoenigschiessenBestaetigungMessage(m));
            WeakReferenceMessenger.Default.Register<OpenKoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewMessage, string>(this, "KoenigschiessenRundeTeilnehmerUebersicht", (r, m) => ReceiveOpenKoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewMessage(m));
            WeakReferenceMessenger.Default.Register<KoenigschiessenRundeBeendetMessage, string>(this, "KoenigschiessenRundeTeilnehmerUebersicht", (r, m) => ReceiveKoenigschiessenRundeBeendetMessage(m));
            WeakReferenceMessenger.Default.Register<CloseViewMessage, string>(this, "KoenigschiessenRundeTeilnehmerUebersicht", (r, m) => ReceivCloseViewMessage());

            KoenigschiessenRundeTeilnehmerWerteView WerteKoenigschiessen = new();
            Werte.NavigationService.Navigate(WerteKoenigschiessen);
            if (DataContext is KoenigschiessenRundeTeilnehmerUebersichtViewModel model)
            {
                if (WerteKoenigschiessen.DataContext is KoenigschiessenRundeTeilnehmerWerteViewModel modelWerte)
                    model.SetzeWerte(modelWerte);
            }
           
        }

        private static void ReceiveKoenigschiessenRundeBeendetMessage(KoenigschiessenRundeBeendetMessage m)
        {
            if (m.KoenigschiessenAbschluss.KoenigschiessenBeendet)
            {
                KoenigschiessenRundeAbschlussBeendetView view = new()
                {
                    Owner = Application.Current.MainWindow
                };
                if (view.DataContext is KoenigschiessenRundeAbschlussViewModel model)
                {
                    model.ZeigeDatenAn(m.KoenigschiessenAbschluss);
                    view.ShowDialog();
                }
            }
            else
            {
                KoenigschiessenRundeAbschlussNaechsteRundeView NaechsteRundeview = new()
                {
                    Owner = Application.Current.MainWindow
                };
                if (NaechsteRundeview.DataContext is KoenigschiessenRundeAbschlussViewModel model)
                {
                    model.ZeigeDatenAn(m.KoenigschiessenAbschluss);
                    NaechsteRundeview.ShowDialog();
                }
            }
        }

        private void ReceivCloseViewMessage()
        {
            GetWindow(this).Close();
        }

        private static void ReceiveOpenKoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewMessage(OpenKoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewMessage m)
        {
            KoenigschiessenHoechsteErgebnisSchuetzenUebersichtView view = new()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is KoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewModel model)
            {
                model.ZeigeDatenAn(m.Jahr, m.Runde, m.Art);
                view.ShowDialog();
            }
        }

        private static void ReceiveOpenKoenigschiessenBestaetigungMessage(OpenKoenigschiessenErgebnisEintragenMessage m)
        {
            KoenigschiessenErgebnisEintragenView view = new()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is KoenigschiessenErgebnisEintragenViewModel model)
            {
                model.ZeigeDatenAn(m.KoenigschiessenRundeTeilnehmer, m.variante);
                view.ShowDialog();
                if (model.Bestaetigt)
                {
                    m.Command();
                }
            }
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<OpenKoenigschiessenErgebnisEintragenMessage, string>(this, "KoenigschiessenRundeTeilnehmerUebersicht");
            WeakReferenceMessenger.Default.Unregister<OpenKoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewMessage, string>(this, "KoenigschiessenRundeTeilnehmerUebersicht");
            WeakReferenceMessenger.Default.Unregister<CloseViewMessage, string>(this, "KoenigschiessenRundeTeilnehmerUebersicht");
            WeakReferenceMessenger.Default.Unregister<KoenigschiessenRundeBeendetMessage, string>(this, "KoenigschiessenRundeTeilnehmerUebersicht");

            base.Window_Unloaded(sender, e);
        }
    }
}
