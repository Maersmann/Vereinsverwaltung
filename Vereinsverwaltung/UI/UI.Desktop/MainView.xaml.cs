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
using Vereinsverwaltung.Logic.Messages.BaseMessages;
using Vereinsverwaltung.UI.Desktop.Mitglieder;

namespace Vereinsverwaltung.UI.Desktop
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainView
    {

        private static MitgliederUebersichtView mitgliederUebersichtView;
        private static MitgliederImportView mitgliederImportView;

        public MainView()
        {
            InitializeComponent();

            Messenger.Default.Register<OpenViewMessage>(this, m => ReceiveOpenViewMessage(m));
            Messenger.Default.Register<ExceptionMessage>(this, m => ReceiveExceptionMessage(m));

            Naviagtion(ViewType.viewMitgliederUebersicht);
        }

        private void ReceiveExceptionMessage(ExceptionMessage m)
        {
            MessageBox.Show(m.Message);
        }

        private void ReceiveOpenViewMessage(OpenViewMessage m)
        {
            Naviagtion(m.ViewType);
        }

        public void Naviagtion(ViewType inType)
        {
            switch (inType)
            {
                case ViewType.viewMitlgiederStammdaten:
                    new MitgliedStammdatenView().ShowDialog();
                    break;
                case ViewType.viewMitgliederUebersicht:
                    mitgliederUebersichtView = mitgliederUebersichtView ?? new MitgliederUebersichtView();
                    Container.NavigationService.Navigate(mitgliederUebersichtView);
                    break;
                case ViewType.viewMitgliederImport:
                    mitgliederImportView = mitgliederImportView ?? new MitgliederImportView();
                    Container.NavigationService.Navigate(mitgliederImportView);
                    break;
                default:
                    break;
            }
        }

    }

}
