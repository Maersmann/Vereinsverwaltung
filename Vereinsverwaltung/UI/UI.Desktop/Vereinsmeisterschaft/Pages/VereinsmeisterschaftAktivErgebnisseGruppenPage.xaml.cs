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

namespace UI.Desktop.Vereinsmeisterschaft.Pages
{
    /// <summary>
    /// Interaktionslogik für VereinsmeisterschaftAktivErgebnisseGruppenPage.xaml
    /// </summary>
    public partial class VereinsmeisterschaftAktivErgebnisseGruppenPage : Page
    {
        public VereinsmeisterschaftAktivErgebnisseGruppenPage()
        {
            InitializeComponent();
            ContainerLeft.NavigationService.Navigate(new VereinsmeisterschaftAktivErgebnisseGruppentypenView());
            ContainerRight.NavigationService.Navigate(new VereinsmeisterschaftAktivErgebnisseVonGruppentypView());
        }
    }
}
