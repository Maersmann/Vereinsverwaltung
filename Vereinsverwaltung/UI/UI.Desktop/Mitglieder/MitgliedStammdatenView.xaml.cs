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
using Vereinsverwaltung.UI.Desktop.BaseViews;

namespace Vereinsverwaltung.UI.Desktop.Mitglieder
{
    /// <summary>
    /// Interaktionslogik für MitgliedStammdatenView.xaml
    /// </summary>
    public partial class MitgliedStammdatenView : StammdatenView
    {
        public MitgliedStammdatenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.mitglied);
        }
    }
}
