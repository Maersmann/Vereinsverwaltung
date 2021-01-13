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
using Vereinsverwaltung.Logic.UI.MitgliederViewModels;

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
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public MitgliederStammdatenViewModel MitgliederStammdaten => ServiceLocator.Current.GetInstance<MitgliederStammdatenViewModel>();
        public MitgliederUebersichtViewModel MitgliederUebersichtView => ServiceLocator.Current.GetInstance<MitgliederUebersichtViewModel>();

        public static void Cleanup()
        {

        }
    }
}