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
using System.Windows.Shapes;
using UI.Desktop.BaseViews;

namespace UI.Desktop.Schnurschiessen
{
    /// <summary>
    /// Interaktionslogik für SchnurauszeichnungStammdatenView.xaml
    /// </summary>
    public partial class SchnurauszeichnungStammdatenView : StammdatenView
    {
        public SchnurauszeichnungStammdatenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.schnurauszeichnung);
        }
    }
}
