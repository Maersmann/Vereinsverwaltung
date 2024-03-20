using CommunityToolkit.Mvvm.Messaging;
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
    /// Interaktionslogik für SchluesselverteilungSchluesselUebersichtView.xaml
    /// </summary>
    public partial class SchluesselverteilungSchluesselUebersichtView : UserControl
    {
        public SchluesselverteilungSchluesselUebersichtView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<OpenSchluesselzuteilungMessage, string>(this, "SchluesselverteilungSchluesselUebersicht", (r, m) => ReceiveOpenSchluesselzuteilungMessage(m));
            WeakReferenceMessenger.Default.Register<OpenSchluesselRueckgabeMessage, string>(this, "SchluesselverteilungSchluesselUebersicht", (r, m) => ReceiveOpenSchluesselRueckgabeMessage(m));
        }

        private static void ReceiveOpenSchluesselRueckgabeMessage(OpenSchluesselRueckgabeMessage m)
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

        private static void ReceiveOpenSchluesselzuteilungMessage(OpenSchluesselzuteilungMessage m)
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
