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
    /// Interaktionslogik für VereinsmeisterschaftPlatzierungenSchuetzenPage.xaml
    /// </summary>
    public partial class VereinsmeisterschaftPlatzierungenSchuetzenPage : UserControl
    {
        public VereinsmeisterschaftPlatzierungenSchuetzenPage()
        {
            InitializeComponent();
        }

        public void Initialize(int vereinsmeisterschaftID)
        {
            VereinsmeisterschaftPlatzierungenSchuetzentypenView containerLeft = new VereinsmeisterschaftPlatzierungenSchuetzentypenView();
            if (containerLeft.DataContext is VereinsmeisterschaftPlatzierungenSchuetzentypenViewModel model)
            {
                model.VereinsmeisterschaftID = vereinsmeisterschaftID;
            }
            ContainerLeft.NavigationService.Navigate(containerLeft);
            ContainerRight.NavigationService.Navigate(new VereinsmeisterschaftPlatzierungenVonSchuetzentypView());
        }
    }
}
