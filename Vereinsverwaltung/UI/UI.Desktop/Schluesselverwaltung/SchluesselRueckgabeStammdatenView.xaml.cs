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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vereinsverwaltung.UI.Desktop.Auswahl;
using Vereinsverwaltung.UI.Desktop.BaseViews;

namespace Vereinsverwaltung.UI.Desktop.Schluesselverwaltung
{
    /// <summary>
    /// Interaktionslogik für SchluesselRueckgabeStammdatenView.xaml
    /// </summary>
    public partial class SchluesselRueckgabeStammdatenView : StammdatenView
    {
        public SchluesselRueckgabeStammdatenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.schluesselrueckgabe);
            Messenger.Default.Register<OpenSchluesselzuteilungAuswahlMessage>(this, "SchluesselRueckgabeStammdaten", m => ReceiveOpenSchluesselzuteilungAuswahlMessage(m));
        }

        private void ReceiveOpenSchluesselzuteilungAuswahlMessage(OpenSchluesselzuteilungAuswahlMessage m)
        {
            var view = new SchluesselzuteilungAuswahlView();
            if (view.DataContext is SchluesselzuteilungAuswahlViewModel model)
            {
                model.SetAuswahlState(m.ID, m.AuswahlTypes);
                view.ShowDialog();
                if (model.SchluesselzuteilungID().HasValue)
                    m.Callback(true, model.SchluesselzuteilungID().Value);
                else
                    m.Callback(false, 0);
            }
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<OpenSchluesselzuteilungAuswahlMessage>(this);
            base.Window_Unloaded(sender, e);
        }
    }
}
