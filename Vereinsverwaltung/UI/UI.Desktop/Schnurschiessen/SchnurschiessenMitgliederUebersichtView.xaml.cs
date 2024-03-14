using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.AuswahlMessages;
using Logic.Messages.SchluesselMessages;
using Logic.Messages.SchnurschiessenMessages;
using Logic.UI.SchluesselverwaltungViewModels;
using Logic.UI.SchnurschiessenViewModels;
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
using UI.Desktop.Schluesselverwaltung;

namespace UI.Desktop.Schnurschiessen
{
    /// <summary>
    /// Interaktionslogik für SchnurschiessenMitgliederUebersichtView.xaml
    /// </summary>
    public partial class SchnurschiessenMitgliederUebersichtView : UserControl
    {
        public SchnurschiessenMitgliederUebersichtView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenSchnurschiessenMitgliedHistorieUebersicht>(this, "SchnurschiessenMitgliederUebersicht", m => ReceiveOpenSchnurschiessenMitgliedHistorieUebersicht(m));
        }

        private void ReceiveOpenSchnurschiessenMitgliedHistorieUebersicht(OpenSchnurschiessenMitgliedHistorieUebersicht m)
        {
            var view = new SchnurschiessenMitgliedHistorieUebersichtView
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is SchnurschiessenMitgliedHistorieUebersichtViewModel model)
            {
                model.Lade(m.Id);
            }
            view.ShowDialog();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<OpenSchnurschiessenMitgliedHistorieUebersicht>(this);
        }
    }
}
