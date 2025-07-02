using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.AuswahlMessages;
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
    /// Interaktionslogik für SchluesselverteilungSchluesselUebersichtDetailView.xaml
    /// </summary>
    public partial class SchluesselverteilungSchluesselUebersichtDetailView : UserControl
    {
        public SchluesselverteilungSchluesselUebersichtDetailView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<OpenSchluesselKennungEintragenMessage, string>(this, "SchluesselverteilungSchluesselUebersichtDetail", (r, m) => ReceiveOpenSchluesselKennungEintragenMessage(m));
        }


        private static void ReceiveOpenSchluesselKennungEintragenMessage(OpenSchluesselKennungEintragenMessage m)
        {
            var view = new SchluesselverteilungKennungEintragenView
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is SchluesselverteilungKennungEintragenViewModel model)
            {
                model.SchluesselbesitzerId = m.ID;
            }
            view.ShowDialog();
            m.Command();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<OpenSchluesselKennungEintragenMessage, string>(this, "SchluesselverteilungSchluesselUebersichtDetail");
        }
    }
}
