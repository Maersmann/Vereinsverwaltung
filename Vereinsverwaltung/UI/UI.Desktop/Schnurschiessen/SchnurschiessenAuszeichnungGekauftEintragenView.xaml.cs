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
using System.Windows.Shapes;
using UI.Desktop.BaseViews;

namespace UI.Desktop.Schnurschiessen
{
    /// <summary>
    /// Interaktionslogik für SchnurschiessenAuszeichnungGekauftEintragenView.xaml
    /// </summary>
    public partial class SchnurschiessenAuszeichnungGekauftEintragenView : StammdatenView
    {
        public SchnurschiessenAuszeichnungGekauftEintragenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.schnurschiessenAuszeichnungBestand);
        }
    }
}
