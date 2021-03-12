using GalaSoft.MvvmLight.Messaging;
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
using Vereinsverwaltung.Logic.Messages.AuswahlMessages;
using Vereinsverwaltung.UI.Desktop.Auswahl;
using Vereinsverwaltung.UI.Desktop.BaseViews;

namespace Vereinsverwaltung.UI.Desktop.Schluesselverwaltung
{
    /// <summary>
    /// Interaktionslogik für SchluesselStammdaten.xaml
    /// </summary>
    public partial class SchluesselStammdatenView : StammdatenView
    {
        public SchluesselStammdatenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.schluessel);
        }

    }
}
