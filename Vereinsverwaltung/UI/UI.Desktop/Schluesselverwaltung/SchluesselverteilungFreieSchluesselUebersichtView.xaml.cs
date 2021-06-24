using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.SchluesselMessages;
using Logic.UI.SchluesselverwaltungViewModels;
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
using UI.Desktop.Schluesselverwaltung;

namespace Vereinsverwaltung.UI.Desktop.Schluesselverwaltung
{
    /// <summary>
    /// Interaktionslogik für SchluesselverteilungFreieSchluesselUebersichtView.xaml
    /// </summary>
    public partial class SchluesselverteilungFreieSchluesselUebersichtView : UserControl
    {
        public SchluesselverteilungFreieSchluesselUebersichtView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenSchluesselzuteilungMessage>(this, "SchluesselverteilungFreieSchluesselUebersicht", m => ReceiveOpenSchluesselzuteilungMessage(m));
            Messenger.Default.Register<OpenSchluesselRueckgabeMessage>(this, "SchluesselverteilungFreieSchluesselUebersicht", m => ReceiveOpenSchluesselRueckgabeMessage(m));
        }

        private void ReceiveOpenSchluesselRueckgabeMessage(OpenSchluesselRueckgabeMessage m)
        {
            var view = new SchluesselRueckgabeStammdatenView()
            {
                Owner = Application.Current.MainWindow
            };

            if (view.DataContext is SchluesselRueckgabeStammdatenViewModel model)
            {
                model.SetInformation(m.ID, m.AuswahlTypes);
            }
            view.ShowDialog();
        }

        private void ReceiveOpenSchluesselzuteilungMessage(OpenSchluesselzuteilungMessage m)
        {
            var view = new SchluesselzuteilungStammdatenView()
            {
                Owner = Application.Current.MainWindow
            };

            if (view.DataContext is SchluesselzuteilungStammdatenViewModel model)
            {
                model.BySchluesselID(m.ID);
            }
            view.ShowDialog();
        }
    }
}
