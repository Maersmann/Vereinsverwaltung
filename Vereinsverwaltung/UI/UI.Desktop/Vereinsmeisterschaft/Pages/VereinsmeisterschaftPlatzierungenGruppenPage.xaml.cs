using Logic.UI.VereinsmeisterschaftViewModels;
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
    /// Interaktionslogik für VereinsmeisterschaftPlatzierungenGruppenPage.xaml
    /// </summary>
    public partial class VereinsmeisterschaftPlatzierungenGruppenPage : Page
    {
        public VereinsmeisterschaftPlatzierungenGruppenPage()
        {
            InitializeComponent();

        }

        public void Initialize(int vereinsmeisterschaftID)
        {
            VereinsmeisterschaftPlatzierungenGruppentypenView containerLeft = new VereinsmeisterschaftPlatzierungenGruppentypenView();
            if (containerLeft.DataContext is VereinsmeisterschaftPlatzierungenGruppentypenViewModel model)
            {
                model.VereinsmeisterschaftID = vereinsmeisterschaftID;
            }
            ContainerLeft.NavigationService.Navigate(containerLeft);
            ContainerRight.NavigationService.Navigate(new VereinsmeisterschaftPlatzierungenVonGruppentypView());
        }
    }
}
