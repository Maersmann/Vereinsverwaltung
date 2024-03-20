using CommunityToolkit.Mvvm.Messaging;
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
    /// Interaktionslogik für KoenigschiessenAnmeldungBestaetigungView.xaml
    /// </summary>
    public partial class KoenigschiessenAnmeldungBestaetigungView : Window
    {
        public KoenigschiessenAnmeldungBestaetigungView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<CloseViewMessage, string>(this, "KoenigschiessenAnmeldungBestaetigung", (r, m) => ReceivCloseViewMessage());
        }

        private void ReceivCloseViewMessage()
        {
            GetWindow(this).Close();
        }
    }
}
