using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vereinsverwaltung.UI.Desktop.Schnurschiessen;

namespace UI.Desktop.Schnurschiessen.Pages
{
    /// <summary>
    /// Interaktionslogik für SchnurschiessenAuszeichnungBestandHistoriePage.xaml
    /// </summary>
    public partial class SchnurschiessenAuszeichnungBestandHistoriePage : Page
    {
        public SchnurschiessenAuszeichnungBestandHistoriePage()
        {
            InitializeComponent();
            ContainerLeft.NavigationService.Navigate(new SchnurschiessenAuszeichnungBestandUebersichtView());
            ContainerRight.NavigationService.Navigate(new SchnurschiessenAuszeichnungBestandHistorieView());
        }
    }
}
