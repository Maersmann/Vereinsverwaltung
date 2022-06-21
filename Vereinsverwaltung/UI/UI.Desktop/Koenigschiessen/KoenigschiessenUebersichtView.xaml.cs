using GalaSoft.MvvmLight.Messaging;
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
    /// Interaktionslogik für KoenigsschiessenUebersichtView.xaml
    /// </summary>
    public partial class KoenigschiessenUebersichtView : UserControl
    {
        public KoenigschiessenUebersichtView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenKoenigschiessenAnmeldungViewMessage>(this, "KoenigschiessenUebersicht", m => ReceiveOpenKoenigschiessenViewMessage(m));
        }

        private void ReceiveOpenKoenigschiessenViewMessage(OpenKoenigschiessenAnmeldungViewMessage m)
        {
            KoenigschiessenAnmeldungUebersichtView view = new KoenigschiessenAnmeldungUebersichtView
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

        private void WindowUnloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<OpenKoenigschiessenAnmeldungViewMessage>(this);
        }
    }
}
