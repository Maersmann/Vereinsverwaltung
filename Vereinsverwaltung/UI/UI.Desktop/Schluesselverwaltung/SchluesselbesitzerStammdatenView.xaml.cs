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
    /// Interaktionslogik für SchluesselbesitzerStammdatenView.xaml
    /// </summary>
    public partial class SchluesselbesitzerStammdatenView : StammdatenView
    {
        public SchluesselbesitzerStammdatenView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenMitgliedAuswahlMessage>(this, "SchluesselbesitzerStammdaten", m => ReceiveOpenMitgliedAuswahlMessage(m));
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.schluesselbesitzer);
        }
        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            base.Window_Unloaded(sender, e);
            Messenger.Default.Unregister<OpenMitgliedAuswahlMessage>(this);
        }

        private void ReceiveOpenMitgliedAuswahlMessage(OpenMitgliedAuswahlMessage m)
        {
            var view = new MitgliedAuswahlView
            {
                Owner = Application.Current.MainWindow
            };
            view.ShowDialog();
            if (view.DataContext is MitgliedAuswahlViewModel model)
            {
                if (model.AuswahlGetaetigt && model.ID().HasValue)
                    m.Callback(true, model.ID().Value);
                else
                    m.Callback(false,0);
            }     
        }
    }
}
