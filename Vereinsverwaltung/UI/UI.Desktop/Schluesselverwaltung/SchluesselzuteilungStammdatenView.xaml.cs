using GalaSoft.MvvmLight.Messaging;
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
            Messenger.Default.Register<OpenSchluesselbesitzerAuswahlMessage>(this, "SchluesselzuteilungStammdaten", m => ReceiveOpenSchluesselbesitzerAuswahlMessage(m));
            Messenger.Default.Register<OpenSchluesselAuswahlMessage>(this, "SchluesselzuteilungStammdaten", m => ReceiveOpenSchluesselAuswahlMessage(m));
           
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.schluesselzuteilung);
        }

        private void ReceiveOpenSchluesselAuswahlMessage(OpenSchluesselAuswahlMessage m)
        {
            var view = new SchluesselAuswahlView();
            view.ShowDialog();
            if (view.DataContext is SchluesselAuswahlViewModel model)
            {
                if (model.SchluesselID().HasValue)
                    m.Callback(true, model.SchluesselID().Value);
                else
                    m.Callback(false, 0);
            }
        }

        private void ReceiveOpenSchluesselbesitzerAuswahlMessage(OpenSchluesselbesitzerAuswahlMessage m)
        {
            var view = new SchluesselbesitzerAuswahlView();
            view.ShowDialog();
            if (view.DataContext is SchluesselbesitzerAuswahlViewModel model)
            {
                if (model.SchluesselbestizerID().HasValue)
                    m.Callback(true, model.SchluesselbestizerID().Value);
                else
                    m.Callback(false, 0);
            }
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<OpenSchluesselbesitzerAuswahlMessage>(this);
            Messenger.Default.Unregister<OpenSchluesselAuswahlMessage>(this);
            base.Window_Unloaded(sender, e);
        }
    }
}
