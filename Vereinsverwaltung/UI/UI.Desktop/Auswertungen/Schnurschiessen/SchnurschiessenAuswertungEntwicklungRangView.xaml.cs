using CommunityToolkit.Mvvm.Messaging;
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
    /// Interaktionslogik für SchnurschiessenAuswertungEntwicklungRangView.xaml
    /// </summary>
    public partial class SchnurschiessenAuswertungEntwicklungRangView : UserControl
    {
        public SchnurschiessenAuswertungEntwicklungRangView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<OpenSchnurschiessenRangMessage, string>(this, "SchnurschiessenAuswertungEntwicklungRang", (r, m) => ReceiveOpenSchnurschiessenRangMessage(m));
        }

        private static void ReceiveOpenSchnurschiessenRangMessage(OpenSchnurschiessenRangMessage m)
        {
            var view = new SchnurschiessenRangAuswahlView
            {
                Owner = Application.Current.MainWindow
            };
            view.ShowDialog();
            if (view.DataContext is SchnurschiessenRangAuswahlViewModel model)
            {
                if (model.AuswahlGetaetigt && model.ID().HasValue)
                    m.Callback(true, model.ID().Value);
                else
                    m.Callback(false, 0);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<OpenSchnurschiessenRangMessage, string>(this, "SchnurschiessenAuswertungEntwicklungRang");
        }
    }
}
