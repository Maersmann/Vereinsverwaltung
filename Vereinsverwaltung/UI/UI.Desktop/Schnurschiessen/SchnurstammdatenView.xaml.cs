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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vereinsverwaltung.UI.Desktop.BaseViews;

namespace Vereinsverwaltung.UI.Desktop.Schnurschiessen
{
    /// <summary>
    /// Interaktionslogik für SchnurrstammdatenView.xaml
    /// </summary>
    public partial class SchnurstammdatenView : StammdatenView
    {
        public SchnurstammdatenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.schnur);
        }
    }
}
