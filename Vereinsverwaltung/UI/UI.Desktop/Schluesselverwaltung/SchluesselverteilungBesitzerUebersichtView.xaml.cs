using GalaSoft.MvvmLight.Messaging;
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
using Vereinsverwaltung.Logic.Messages.SchluesselMessages;
using Vereinsverwaltung.Logic.UI.SchluesselverwaltungViewModels;

namespace Vereinsverwaltung.UI.Desktop.Schluesselverwaltung
{
    /// <summary>
    /// Interaktionslogik für SchluesselzuteilungBesitzerUebersichtView.xaml
    /// </summary>
    public partial class SchluesselverteilungBesitzerUebersichtView : UserControl
    {
        public SchluesselverteilungBesitzerUebersichtView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenSchluesselzuteilungMessage>(this, "SchluesselverteilungBesitzerUebersicht", m => ReceiveOpenSchluesselzuteilungMessage(m));
            Messenger.Default.Register<OpenSchluesselRueckgabeMessage>(this, "SchluesselverteilungBesitzerUebersicht", m => ReceiveOpenSchluesselRueckgabeMessage(m));
        }

        private void ReceiveOpenSchluesselRueckgabeMessage(OpenSchluesselRueckgabeMessage m)
        {
            var view = new SchluesselRueckgabeStammdatenView();

            if (view.DataContext is SchluesselRueckgabeStammdatenViewModel model)
            {
                model.SetInformation(m.ID, m.AuswahlTypes);
            }
            view.ShowDialog();
        }

        private void ReceiveOpenSchluesselzuteilungMessage(OpenSchluesselzuteilungMessage m)
        {
            var view = new SchluesselzuteilungStammdatenView();

            if (view.DataContext is SchluesselzuteilungStammdatenViewModel model)
            {
                model.BySchluesselbesitzerID(m.ID);
            }
            view.ShowDialog();
        }
    }
}
