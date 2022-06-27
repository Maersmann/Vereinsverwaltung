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

namespace UI.Desktop.Koenigschiessen
{
    /// <summary>
    /// Interaktionslogik für KoenigschiessenErgebnisEintragenView.xaml
    /// </summary>
    public partial class KoenigschiessenErgebnisEintragenView : Window
    {
        public KoenigschiessenErgebnisEintragenView()
        {
            InitializeComponent();
            Messenger.Default.Register<CloseViewMessage>(this, "KoenigschiessenErgebnisEintragen", m => ReceivCloseViewMessage());
        }

        private void ReceivCloseViewMessage()
        {
            GetWindow(this).Close();
        }
    }
}
