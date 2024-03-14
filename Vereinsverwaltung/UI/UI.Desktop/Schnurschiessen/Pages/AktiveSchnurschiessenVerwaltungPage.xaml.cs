using System;
using System.Collections.Generic;
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

namespace UI.Desktop.Schnurschiessen.Pages
{
    /// <summary>
    /// Interaktionslogik für AktiveSchnurschiessenVerwaltungPage.xaml
    /// </summary>
    public partial class AktiveSchnurschiessenVerwaltungPage : Page
    {
        public AktiveSchnurschiessenVerwaltungPage()
        {
            InitializeComponent();
            ContainerLeft.NavigationService.Navigate(new AktiveSchnurschiessenVerwaltungView());
            ContainerRight.NavigationService.Navigate(new AktiveSchnurschiessenBestandHistorieView());
        }
    }
}
