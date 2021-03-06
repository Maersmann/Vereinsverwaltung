﻿using System;
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

namespace Vereinsverwaltung.UI.Desktop.Schluesselverwaltung.Pages
{
    /// <summary>
    /// Interaktionslogik für SchluesselverteilungSchluesselBesitzerPage.xaml
    /// </summary>
    public partial class SchluesselverteilungSchluesselUebersichtPage : Page
    {
        public SchluesselverteilungSchluesselUebersichtPage()
        {
            InitializeComponent();
            ContainerLeft.NavigationService.Navigate(new SchluesselverteilungSchluesselUebersichtView());
            ContainerRight.NavigationService.Navigate(new SchluesselverteilungSchluesselUebersichtDetailView());
        }
    }
}
