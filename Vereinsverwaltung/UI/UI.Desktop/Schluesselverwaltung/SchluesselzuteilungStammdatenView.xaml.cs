using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.AuswahlMessages;
using Logic.UI.AuswahlViewModels;
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
using System.Windows.Shapes;
using Vereinsverwaltung.UI.Desktop.Auswahl;
using UI.Desktop.BaseViews;

namespace UI.Desktop.Schluesselverwaltung
{
    /// <summary>
    /// Interaktionslogik für SchluesselzuteilungView.xaml
    /// </summary>
    public partial class SchluesselzuteilungStammdatenView : StammdatenView
    {
        public SchluesselzuteilungStammdatenView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<OpenSchluesselbesitzerAuswahlMessage, string>(this, "SchluesselzuteilungStammdaten", (r, m) => ReceiveOpenSchluesselbesitzerAuswahlMessage(m));
            WeakReferenceMessenger.Default.Register<OpenSchluesselAuswahlMessage, string>(this, "SchluesselzuteilungStammdaten", (r, m) => ReceiveOpenSchluesselAuswahlMessage(m));
           
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.schluesselzuteilung);
        }

        private static void ReceiveOpenSchluesselAuswahlMessage(OpenSchluesselAuswahlMessage m)
        {
            var view = new SchluesselAuswahlView()
            {
                Owner = Application.Current.MainWindow
            };
            view.ShowDialog();
            if (view.DataContext is SchluesselAuswahlViewModel model)
            {
                if (model.AuswahlGetaetigt && model.ID().HasValue)
                    m.Callback(true, model.ID().Value);
                else
                    m.Callback(false, 0);
            }
        }

        private static void ReceiveOpenSchluesselbesitzerAuswahlMessage(OpenSchluesselbesitzerAuswahlMessage m)
        {
            var view = new SchluesselbesitzerAuswahlView()
            {
                Owner = Application.Current.MainWindow
            };
            view.ShowDialog();
            if (view.DataContext is SchluesselbesitzerAuswahlViewModel model)
            {
                if (model.AuswahlGetaetigt && model.ID().HasValue)
                    m.Callback(true, model.ID().Value);
                else
                    m.Callback(false, 0);
            }
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<OpenSchluesselbesitzerAuswahlMessage, string>(this, "SchluesselzuteilungStammdaten");
            WeakReferenceMessenger.Default.Unregister<OpenSchluesselAuswahlMessage, string>(this, "SchluesselzuteilungStammdaten");
            base.Window_Unloaded(sender, e);
        }
    }
}
