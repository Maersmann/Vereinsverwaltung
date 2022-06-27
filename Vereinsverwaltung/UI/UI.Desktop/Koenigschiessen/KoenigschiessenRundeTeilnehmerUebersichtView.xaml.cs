using Data.Types.KoenigschiessenTypes;
using GalaSoft.MvvmLight.Messaging;
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
            Messenger.Default.Register<OpenKoenigschiessenErgebnisEintragenMessage>(this, "KoenigschiessenRundeTeilnehmerUebersicht", m => ReceiveOpenKoenigschiessenBestaetigungMessage(m));
            Messenger.Default.Register<OpenKoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewMessage>(this, "KoenigschiessenRundeTeilnehmerUebersicht", m => ReceiveOpenKoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewMessage(m));
            Messenger.Default.Register<KoenigschiessenRundeBeendetMessage>(this, "KoenigschiessenRundeTeilnehmerUebersicht", m => ReceiveKoenigschiessenRundeBeendetMessage(m));
            Messenger.Default.Register<CloseViewMessage>(this, "KoenigschiessenRundeTeilnehmerUebersicht", m => ReceivCloseViewMessage());

            KoenigschiessenRundeTeilnehmerWerteView WerteKoenigschiessen = new KoenigschiessenRundeTeilnehmerWerteView();
            Werte.NavigationService.Navigate(WerteKoenigschiessen);
            if (DataContext is KoenigschiessenRundeTeilnehmerUebersichtViewModel model)
            {
                if (WerteKoenigschiessen.DataContext is KoenigschiessenRundeTeilnehmerWerteViewModel modelWerte)
                    model.SetzeWerte(modelWerte);
            }
           
        }

        private void ReceiveKoenigschiessenRundeBeendetMessage(KoenigschiessenRundeBeendetMessage m)
        {
            if (m.KoenigschiessenAbschluss.KoenigschiessenBeendet)
            {
                KoenigschiessenRundeAbschlussBeendetView view = new KoenigschiessenRundeAbschlussBeendetView
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
                KoenigschiessenRundeAbschlussNaechsteRundeView NaechsteRundeview = new KoenigschiessenRundeAbschlussNaechsteRundeView
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

        private void ReceiveOpenKoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewMessage(OpenKoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewMessage m)
        {
            KoenigschiessenHoechsteErgebnisSchuetzenUebersichtView view = new KoenigschiessenHoechsteErgebnisSchuetzenUebersichtView
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is KoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewModel model)
            {
                model.ZeigeDatenAn(m.Jahr, m.Runde, m.Art);
                view.ShowDialog();
            }
        }

        private void ReceiveOpenKoenigschiessenBestaetigungMessage(OpenKoenigschiessenErgebnisEintragenMessage m)
        {
            KoenigschiessenErgebnisEintragenView view = new KoenigschiessenErgebnisEintragenView
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
            Messenger.Default.Unregister<OpenKoenigschiessenErgebnisEintragenMessage>(this);
            Messenger.Default.Unregister<OpenKoenigschiessenHoechsteErgebnisSchuetzenUebersichtViewMessage>(this);
            Messenger.Default.Unregister<CloseViewMessage>(this);
            base.Window_Unloaded(sender, e);
        }
    }
}
