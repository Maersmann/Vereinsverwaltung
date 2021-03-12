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
using System.Windows.Shapes;

namespace Vereinsverwaltung.UI.Desktop.Schluesselverwaltung.Pages
{
    /// <summary>
    /// Interaktionslogik für SchluesselverteilungBesitzerUebersichtPage.xaml
    /// </summary>
    public partial class SchluesselverteilungBesitzerUebersichtPage : Page
    {
        public SchluesselverteilungBesitzerUebersichtPage()
        {
            InitializeComponent();
            ContainerLeft.NavigationService.Navigate(new SchluesselverteilungBesitzerUebersichtView());
            ContainerRight.NavigationService.Navigate(new SchluesselverteilungBesitzerUebersichtDetailView());
        }
    }
}
