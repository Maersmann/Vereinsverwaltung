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

namespace UI.Desktop.KkSchiessen
{
    /// <summary>
    /// Interaktionslogik für KkSchiessgruppeStammdatenView.xaml
    /// </summary>
    public partial class KkSchiessgruppeStammdatenView : StammdatenView
    {
        public KkSchiessgruppeStammdatenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.kkSchiessgruppe);
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            base.Window_Unloaded(sender, e);
        }
    }
}
