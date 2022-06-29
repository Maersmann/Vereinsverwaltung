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

namespace UI.Desktop.Koenigschiessen
{
    /// <summary>
    /// Interaktionslogik für JugendkoenigschiessenErstellenView.xaml
    /// </summary>
    public partial class JugendkoenigschiessenErstellenView : StammdatenView
    {
        public JugendkoenigschiessenErstellenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.jugendkoenigschiessen);
            RegisterMessages("JugendkoenigschiessenErstellen");
        }
    }
}
