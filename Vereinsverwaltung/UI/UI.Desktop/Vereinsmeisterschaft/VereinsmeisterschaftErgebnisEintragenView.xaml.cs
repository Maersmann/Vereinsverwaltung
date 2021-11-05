using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.BaseMessages;
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

namespace UI.Desktop.Vereinsmeisterschaft
{
    /// <summary>
    /// Interaktionslogik für VereinsmeisterschaftErgebnisEintragenView.xaml
    /// </summary>
    public partial class VereinsmeisterschaftErgebnisEintragenView : StammdatenView
    {
        public VereinsmeisterschaftErgebnisEintragenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.vereinsmeisterschaftSchuetzeErgebnis);
            Messenger.Default.Register<CloseViewMessage>(this, "VereinsmeisterschaftErgebnisEintragen", m => ReceivCloseViewMessage());
        }

        private void ReceivCloseViewMessage()
        {
            Window.GetWindow(this).Close();
        }

        private void StammdatenView_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<CloseViewMessage>(this);
        }
    }
}
