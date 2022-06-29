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
    /// Interaktionslogik für KoenigsschiessenErstellenView.xaml
    /// </summary>
    public partial class KoenigschiessenErstellenView : StammdatenView
    {
        public KoenigschiessenErstellenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.koenigschiessen);
            RegisterMessages("KoenigschiessenErstellen");
        }
    }
}
