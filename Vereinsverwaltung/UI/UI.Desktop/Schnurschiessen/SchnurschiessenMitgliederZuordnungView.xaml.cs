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
using Vereinsverwaltung.UI.Desktop.Auswahl;

namespace UI.Desktop.Schnurschiessen
{
    /// <summary>
    /// Interaktionslogik für SchnurschiessenMitgliederZuordnungView.xaml
    /// </summary>
    public partial class SchnurschiessenMitgliederZuordnungView : UserControl
    {
        public SchnurschiessenMitgliederZuordnungView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<OpenMitgliedAuswahlMessage, string>(this, "SchnurschiessenMitgliederZuordnung", (r, m) => ReceiveOpenMitgliedAuswahlMessage(m));
        }
        protected void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<OpenMitgliedAuswahlMessage, string>(this, "SchnurschiessenMitgliederZuordnung");
        }

        private static void ReceiveOpenMitgliedAuswahlMessage(OpenMitgliedAuswahlMessage m)
        {
            var view = new MitgliedAuswahlView
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is MitgliedAuswahlViewModel model)
            {
                model.NurOhneSchnurschiessenRang();
                view.ShowDialog();
                if (model.AuswahlGetaetigt && model.ID().HasValue)
                {
                    m.Callback(true, model.ID().Value);
                }
                else
                {
                    m.Callback(false, 0);
                }
            }
        }
    }
}
