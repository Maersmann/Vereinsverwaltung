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
using Vereinsverwaltung.UI.Desktop.Schluesselverwaltung.Core;

namespace Vereinsverwaltung.UI.Desktop.Schluesselverwaltung
{
    /// <summary>
    /// Interaktionslogik für SchluesselUebersichtView.xaml
    /// </summary>
    public partial class SchluesselUebersichtView : UserControl
    {
        private SchluesselzuteilungHistoryUebersichtHelper helper;
        public SchluesselUebersichtView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenSchluesselzuteilungMessage>(this, "SchluesselUebersicht", m => ReceiveOpenSchluesselzuteilungMessage(m));
            Messenger.Default.Register<OpenSchluesselRueckgabeMessage>(this, "SchluesselUebersicht", m => ReceiveOpenSchluesselRueckgabeMessage(m));
            helper = new SchluesselzuteilungHistoryUebersichtHelper();
            helper.RegisterMessage("SchluesselUebersicht");
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
                model.BySchluesselID(m.ID);
            }
            view.ShowDialog();
        }
    }
}
