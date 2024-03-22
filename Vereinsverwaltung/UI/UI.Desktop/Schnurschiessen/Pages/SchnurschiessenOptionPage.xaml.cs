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
using Vereinsverwaltung.UI.Desktop.Schluesselverwaltung;

namespace Vereinsverwaltung.UI.Desktop.Schnurschiessen.Pages
{
    /// <summary>
    /// Interaktionslogik für SchnurschiessenOptionPage.xaml
    /// </summary>
    public partial class SchnurschiessenOptionPage : Page
    {
        public SchnurschiessenOptionPage()
        {
            InitializeComponent();
            ContainerUp.NavigationService.Navigate(new SchnurschiessenAuszeichnungUebersichtView());
            ContainerDown.NavigationService.Navigate(new SchnurschiessenrangUebersichtView());
        }
    }
}
