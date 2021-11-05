using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.VereinsmeisterschaftMessages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.Desktop.Vereinsmeisterschaft.Pages;

namespace UI.Desktop.Vereinsmeisterschaft
{
    /// <summary>
    /// Interaktionslogik für VereinsmeisterschaftenUebersichtView.xaml
    /// </summary>
    public partial class VereinsmeisterschaftenUebersichtView : UserControl
    {
        public VereinsmeisterschaftenUebersichtView()
        {
            InitializeComponent();
            Unloaded += UserControl_Unloaded;
            Messenger.Default.Register<OpenVereinsmeisterschaftPlatzierungenSchuetzentypenMessage>(this, "VereinsmeisterschaftenUebersicht", m => ReceiveLoadVereinsmeisterschaftPlatzierungenSchuetzentypenMessage(m));
            Messenger.Default.Register<OpenVereinsmeistschaftPlatzierungenGruppentypenMessage>(this, "VereinsmeisterschaftenUebersicht", m => ReceiveLoadVereinsmeistschaftPlatzierungenGruppentypenMessage(m));
        }

        private void ReceiveLoadVereinsmeisterschaftPlatzierungenSchuetzentypenMessage(OpenVereinsmeisterschaftPlatzierungenSchuetzentypenMessage m)
        {
            VereinsmeisterschaftPlatzierungenSchuetzenPage page = new VereinsmeisterschaftPlatzierungenSchuetzenPage();
            page.Initialize(m.VereinsmeisterschaftID);

            Window window = new Window
            {
                Content = page,
                MinHeight = 300,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Owner = Application.Current.MainWindow,
                ShowInTaskbar = false
            };
            _ = window.ShowDialog();
        }

        private void ReceiveLoadVereinsmeistschaftPlatzierungenGruppentypenMessage(OpenVereinsmeistschaftPlatzierungenGruppentypenMessage m)
        {
            var page = new VereinsmeisterschaftPlatzierungenGruppenPage();
            page.Initialize(m.VereinsmeisterschaftID);

            Window window = new Window
            {
                Content = page,
                MinHeight = 300,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Owner = Application.Current.MainWindow,
                ShowInTaskbar = false
            };
            _ = window.ShowDialog();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<OpenVereinsmeisterschaftPlatzierungenSchuetzentypenMessage>(this);
            Messenger.Default.Unregister<OpenVereinsmeistschaftPlatzierungenGruppentypenMessage>(this);
        }
    }
}
