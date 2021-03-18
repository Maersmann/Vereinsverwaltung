/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:UI.Desktop"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using Vereinsverwaltung.Logic.UI.AuswahlViewModels;
using Vereinsverwaltung.Logic.UI.MitgliederViewModels;
using Vereinsverwaltung.Logic.UI.SchluesselverwaltungViewModels;
using Vereinsverwaltung.Logic.UI.SchnurschiessenViewModels;

namespace Vereinsverwaltung.Logic.UI
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models                
            }
            else
            {
                // Create run time view services and models                
            }
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MitgliederStammdatenViewModel>();
            SimpleIoc.Default.Register<MitgliederUebersichtViewModel>();
            SimpleIoc.Default.Register<MitgliederImportViewModel>();
            SimpleIoc.Default.Register<SchluesselStammdatenViewModel>();
            SimpleIoc.Default.Register<SchluesselUebersichtViewModel>();
            SimpleIoc.Default.Register<SchluesselzuteilungStammdatenViewModel>();
            SimpleIoc.Default.Register<SchluesselbesitzerAuswahlViewModel>();
            SimpleIoc.Default.Register<SchluesselbesitzerStammdatenViewModel>();
            SimpleIoc.Default.Register<MitgliedAuswahlViewModel>();
            SimpleIoc.Default.Register<SchluesselbesitzerUebersichtViewModel>();
            SimpleIoc.Default.Register<SchluesselAuswahlViewModel>();
            SimpleIoc.Default.Register<SchluesselverteilungBesitzerUebersichtViewModel>();
            SimpleIoc.Default.Register<SchluesselverteilungBesitzerUebersichtDetailViewModel>();
            SimpleIoc.Default.Register<SchluesselverteilungSchluesselUebersichtViewModel>();
            SimpleIoc.Default.Register<SchluesselverteilungSchluesselUebersichtDetailViewModel>();
            SimpleIoc.Default.Register<SchluesselverteilungFreieSchluesselUebersichtViewModel>();
            SimpleIoc.Default.Register<SchluesselRueckgabeStammdatenViewModel>();
            SimpleIoc.Default.Register<SchluesselzuteilungAuswahlViewModel>();
            SimpleIoc.Default.Register<SchluesselzuteilungHistoryUebersichtViewModel>();
            SimpleIoc.Default.Register<SchnurstammdatenViewModel>();
            SimpleIoc.Default.Register<SchnurUebersichtViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public MitgliederStammdatenViewModel MitgliederStammdaten => ServiceLocator.Current.GetInstance<MitgliederStammdatenViewModel>();
        public MitgliederUebersichtViewModel MitgliederUebersichtView => ServiceLocator.Current.GetInstance<MitgliederUebersichtViewModel>();
        public MitgliederImportViewModel MitgliederImport => ServiceLocator.Current.GetInstance<MitgliederImportViewModel>();
        public SchluesselStammdatenViewModel SchluesselStammdaten => ServiceLocator.Current.GetInstance<SchluesselStammdatenViewModel>();
        public SchluesselUebersichtViewModel SchluesselUebersicht => ServiceLocator.Current.GetInstance<SchluesselUebersichtViewModel>();
        public SchluesselzuteilungStammdatenViewModel SchluesselzuteilungStammdaten => new SchluesselzuteilungStammdatenViewModel();
        public SchluesselbesitzerAuswahlViewModel SchluesselbesitzerAuswahl => new SchluesselbesitzerAuswahlViewModel();
        public SchluesselbesitzerStammdatenViewModel SchluesselbesitzerStammdaten => ServiceLocator.Current.GetInstance<SchluesselbesitzerStammdatenViewModel>();
        public MitgliedAuswahlViewModel MitgliedAuswahl => new MitgliedAuswahlViewModel();
        public SchluesselbesitzerUebersichtViewModel SchluesselbesitzerUebersicht => ServiceLocator.Current.GetInstance<SchluesselbesitzerUebersichtViewModel>();
        public SchluesselAuswahlViewModel SchluesselAuswahl => ServiceLocator.Current.GetInstance<SchluesselAuswahlViewModel>();
        public SchluesselverteilungBesitzerUebersichtViewModel SchluesselverteilungBesitzerUebersicht => ServiceLocator.Current.GetInstance<SchluesselverteilungBesitzerUebersichtViewModel>();
        public SchluesselverteilungBesitzerUebersichtDetailViewModel SchluesselverteilungBesitzerUebersichtDetail => ServiceLocator.Current.GetInstance<SchluesselverteilungBesitzerUebersichtDetailViewModel>();
        public SchluesselverteilungSchluesselUebersichtViewModel SchluesselverteilungSchluesselUebersicht => ServiceLocator.Current.GetInstance<SchluesselverteilungSchluesselUebersichtViewModel>();
        public SchluesselverteilungSchluesselUebersichtDetailViewModel SchluesselverteilungSchluesselUebersichtDetail => ServiceLocator.Current.GetInstance<SchluesselverteilungSchluesselUebersichtDetailViewModel>();
        public SchluesselverteilungFreieSchluesselUebersichtViewModel SchluesselverteilungFreieSchluesselUebersicht => ServiceLocator.Current.GetInstance<SchluesselverteilungFreieSchluesselUebersichtViewModel>();
        public SchluesselRueckgabeStammdatenViewModel SchluesselRueckgabeStammdaten => ServiceLocator.Current.GetInstance<SchluesselRueckgabeStammdatenViewModel>();
        public SchluesselzuteilungAuswahlViewModel SchluesselzuteilungAuswahl => ServiceLocator.Current.GetInstance<SchluesselzuteilungAuswahlViewModel>();
        public SchluesselzuteilungHistoryUebersichtViewModel SchluesselzuteilungHistoryUebersicht => ServiceLocator.Current.GetInstance<SchluesselzuteilungHistoryUebersichtViewModel>();
        public SchnurstammdatenViewModel Schnurstammdaten => ServiceLocator.Current.GetInstance<SchnurstammdatenViewModel>();
        public SchnurUebersichtViewModel SchnurUebersicht => ServiceLocator.Current.GetInstance<SchnurUebersichtViewModel>();
        public static void Cleanup()
        {

        }
    }
}