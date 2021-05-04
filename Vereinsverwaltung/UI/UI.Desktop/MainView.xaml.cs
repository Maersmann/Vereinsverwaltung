using Data.Types;
using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.BaseMessages;
using Logic.UI.InterfaceViewModels;
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
using UI.Desktop;
using Vereinsverwaltung.UI.Desktop.BaseViews;
using Vereinsverwaltung.UI.Desktop.Mitglieder;
using Vereinsverwaltung.UI.Desktop.Schluesselverwaltung;
using Vereinsverwaltung.UI.Desktop.Schluesselverwaltung.Pages;
using Vereinsverwaltung.UI.Desktop.Schnurschiessen;
using Vereinsverwaltung.UI.Desktop.Schnurschiessen.Pages;

namespace Vereinsverwaltung.UI.Desktop
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainView
    {

        private static MitgliederUebersichtView mitgliederUebersichtView;
        private static MitgliederImportView mitgliederImportView;
        private static SchluesselUebersichtView schluesselUebersicht;
        private static SchluesselbesitzerUebersichtView schluesselbesitzerUebersicht;
        private static SchluesselverteilungBesitzerUebersichtPage schluesselzuteilungBesitzerUebersicht;
        private static SchluesselverteilungSchluesselUebersichtPage schluesselverteilungSchluesselUebersicht;
        private static SchluesselverteilungFreieSchluesselUebersichtView schluesselverteilungFreieSchluesselUebersicht;

        public MainView()
        {
            InitializeComponent();

            Messenger.Default.Register<OpenViewMessage>(this, m => ReceiveOpenViewMessage(m));
            Messenger.Default.Register<ExceptionMessage>(this, m => ReceiveExceptionMessage(m));
            Messenger.Default.Register<InformationMessage>(this, m => ReceiveInformationMessage(m));
            Messenger.Default.Register<BaseStammdatenMessage>(this, m => ReceiceOpenStammdatenMessage(m));
            Messenger.Default.Register<OpenStartingViewMessage>(this, m => ReceiceOpenStartingViewMessage(m));

            //Naviagtion(ViewType.viewMitgliederUebersicht);

            SchnurschiessenOption.NavigationService.Navigate(new SchnuroptionPage());
        }

        private void ReceiveInformationMessage(InformationMessage m)
        {
            MessageBox.Show(m.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ReceiveExceptionMessage(ExceptionMessage m)
        {
            MessageBox.Show(m.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
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
                case ViewType.viewSchluesselUebersicht:
                    schluesselUebersicht = schluesselUebersicht ?? new SchluesselUebersichtView();
                    Container.NavigationService.Navigate(schluesselUebersicht);
                    break;
                case ViewType.viewSchluesselbesitzerUebersicht:
                    schluesselbesitzerUebersicht = schluesselbesitzerUebersicht ?? new SchluesselbesitzerUebersichtView();
                    Container.NavigationService.Navigate(schluesselbesitzerUebersicht);
                    break;
                case ViewType.viewZuteilungSchluesselbesitzerUebersicht:
                    schluesselzuteilungBesitzerUebersicht = schluesselzuteilungBesitzerUebersicht ?? new SchluesselverteilungBesitzerUebersichtPage();
                    Container.NavigationService.Navigate(schluesselzuteilungBesitzerUebersicht);
                    break;
                case ViewType.viewZuteilungSchluesselUebersicht:
                    schluesselverteilungSchluesselUebersicht = schluesselverteilungSchluesselUebersicht ?? new SchluesselverteilungSchluesselUebersichtPage();
                    Container.NavigationService.Navigate(schluesselverteilungSchluesselUebersicht);
                    break;
                case ViewType.viewZuteilungFreieAnzahlUebersicht:
                    schluesselverteilungFreieSchluesselUebersicht = schluesselverteilungFreieSchluesselUebersicht ?? new SchluesselverteilungFreieSchluesselUebersichtView();
                    Container.NavigationService.Navigate(schluesselverteilungFreieSchluesselUebersicht);
                    break;
                    
                default:
                    break;
            }
        }


        private void ReceiceOpenStammdatenMessage(BaseStammdatenMessage m)
        {
            StammdatenView view = null;
            switch ( m.Stammdaten)
            {
                case StammdatenTypes.mitglied:
                    view = new MitgliedStammdatenView();
                    break;
                case StammdatenTypes.schluessel:
                    view = new SchluesselStammdatenView();
                    break;
                case StammdatenTypes.schluesselbesitzer:
                    view = new SchluesselbesitzerStammdatenView();
                    break;
                case StammdatenTypes.schnur:
                    view = new SchnurstammdatenView();
                    break;
                case StammdatenTypes.schnurauszeichnung:
                    view = new SchnurauszeichnungStammdatenView();
                    break;
                default:
                    break;
            }

            if (view.DataContext is IViewModelStammdaten model)
            {
                if (m.State == State.Bearbeiten)
                {
                    model.ZeigeStammdatenAn(m.ID.Value);
                }

            }
            view.ShowDialog();
        }

        private void ReceiceOpenStartingViewMessage(OpenStartingViewMessage m)
        {
            var view = new StartingProgrammView();
            view.ShowDialog();

        }
    }

}
