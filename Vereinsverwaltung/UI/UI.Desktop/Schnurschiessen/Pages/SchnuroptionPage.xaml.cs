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
    /// Interaktionslogik für SchnuroptionPage.xaml
    /// </summary>
    public partial class SchnuroptionPage : Page
    {
        public SchnuroptionPage()
        {
            InitializeComponent();
            ContainerUp.NavigationService.Navigate(new SchnurUebersichtView());
            ContainerDown.NavigationService.Navigate(new SchnurauszeichnungUebersichtView());
        }
    }
}
