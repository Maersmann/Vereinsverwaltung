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

namespace UI.Desktop.Auswertungen.Schnurschiessen
{
    /// <summary>
    /// Interaktionslogik für SchnurschiessenAuswertungEntwicklungAuszeichnungView.xaml
    /// </summary>
    public partial class SchnurschiessenAuswertungEntwicklungAuszeichnungView : UserControl
    {
        public SchnurschiessenAuswertungEntwicklungAuszeichnungView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenSchnurschiessenAuszeichungMessage>(this, "SchnurschiessenAuswertungEntwicklung", m => ReceiveOpenSchnurschiessenAuszeichungMessage(m));
        }

        private void ReceiveOpenSchnurschiessenAuszeichungMessage(OpenSchnurschiessenAuszeichungMessage m)
        {
            var view = new SchnurschiessenAuszeichnungAuswahlView
            {
                Owner = Application.Current.MainWindow
            };
            view.ShowDialog();
            if (view.DataContext is SchnurschiessenAuszeichnungAuswahlViewModel model)
            {
                if (model.AuswahlGetaetigt && model.ID().HasValue)
                    m.Callback(true, model.ID().Value);
                else
                    m.Callback(false, 0);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<OpenSchnurschiessenAuszeichungMessage>(this);
        }
    }
}
