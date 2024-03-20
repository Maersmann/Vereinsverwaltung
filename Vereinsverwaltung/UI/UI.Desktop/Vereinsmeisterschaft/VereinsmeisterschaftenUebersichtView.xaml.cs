using CommunityToolkit.Mvvm.Messaging;
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
            WeakReferenceMessenger.Default.Register<OpenVereinsmeisterschaftPlatzierungenSchuetzentypenMessage, string>(this, "VereinsmeisterschaftenUebersicht", (r,m) => ReceiveLoadVereinsmeisterschaftPlatzierungenSchuetzentypenMessage(m));
            WeakReferenceMessenger.Default.Register<OpenVereinsmeistschaftPlatzierungenGruppentypenMessage, string>(this, "VereinsmeisterschaftenUebersicht", (r,m) => ReceiveLoadVereinsmeistschaftPlatzierungenGruppentypenMessage(m));
        }

        private static void ReceiveLoadVereinsmeisterschaftPlatzierungenSchuetzentypenMessage(OpenVereinsmeisterschaftPlatzierungenSchuetzentypenMessage m)
        {
            VereinsmeisterschaftPlatzierungenSchuetzenPage page = new();
            page.Initialize(m.VereinsmeisterschaftID);

            Window window = new()
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

        private static void ReceiveLoadVereinsmeistschaftPlatzierungenGruppentypenMessage(OpenVereinsmeistschaftPlatzierungenGruppentypenMessage m)
        {
            var page = new VereinsmeisterschaftPlatzierungenGruppenPage();
            page.Initialize(m.VereinsmeisterschaftID);

            Window window = new()
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
            WeakReferenceMessenger.Default.Unregister<OpenVereinsmeisterschaftPlatzierungenSchuetzentypenMessage, string>(this, "VereinsmeisterschaftenUebersicht");
            WeakReferenceMessenger.Default.Unregister<OpenVereinsmeistschaftPlatzierungenGruppentypenMessage, string>(this, "VereinsmeisterschaftenUebersicht");
        }
    }
}
