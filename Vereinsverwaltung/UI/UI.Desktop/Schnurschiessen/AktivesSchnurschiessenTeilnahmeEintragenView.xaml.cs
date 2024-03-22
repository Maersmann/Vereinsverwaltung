using Logic.UI.SchnurschiessenViewModels;
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
using System.Windows.Shapes;
using UI.Desktop.BaseViews;

namespace UI.Desktop.Schnurschiessen
{
    /// <summary>
    /// Interaktionslogik für AktivesSchnurschiessenTeilnahmeEintragenView.xaml
    /// </summary>
    public partial class AktivesSchnurschiessenTeilnahmeEintragenView : StammdatenView
    {
        public AktivesSchnurschiessenTeilnahmeEintragenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.schnurschiessen);
            ((AktivesSchnurschiessenTeilnahmeEintragenViewModel)DataContext).PropertyChanged += ViewModel_PropertyChanged;
        }

        void ViewModel_PropertyChanged(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Rueckgabe")
            {
                Grid.Items.Refresh();
            }
        }
    }
}
