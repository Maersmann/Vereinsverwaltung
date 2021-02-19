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
using Vereinsverwaltung.Data.Types;
using Vereinsverwaltung.Logic.Messages.MitgliederMessages;
using Vereinsverwaltung.Logic.UI.MitgliederViewModels;

namespace Vereinsverwaltung.UI.Desktop.Mitglieder
{
    /// <summary>
    /// Interaktionslogik für MitgliederUebersichtView.xaml
    /// </summary>
    public partial class MitgliederUebersichtView : UserControl
    {
        public MitgliederUebersichtView()
        {
            InitializeComponent();

            Messenger.Default.Register<OpenMitgliederStammdatenMessage>(this, m => ReceiveOpenMitgliederStammdatenMessage(m));
            Messenger.Default.Register<MitgliedEntferntMessage>(this, m => ReceiveMitgliedEntferntMessage());
        }

        private void ReceiveOpenMitgliederStammdatenMessage(OpenMitgliederStammdatenMessage m)
        {
            var view = new MitgliedStammdatenView();
            if (view.DataContext is MitgliederStammdatenViewModel model)
            {
                if (m.State == State.Bearbeiten)
                {
                    model.ZeigeMitglied(m.MitgliedID.Value);
                }
                
            }
            view.ShowDialog();
        }

        private void ReceiveMitgliedEntferntMessage()
        {
            MessageBox.Show("Mitglied entfernt.");
        }
    }
}
