using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.UtilMessages;
using Logic.Messages.VereinsmeisterschaftMessages;
using Logic.UI.UtilsViewModels;
using Logic.UI.VereinsmeisterschaftViewModels;
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
using UI.Desktop.BaseViews;

namespace UI.Desktop.Vereinsmeisterschaft
{
    /// <summary>
    /// Interaktionslogik für VereinsmeisterschaftAktuelleUebersichtView.xaml
    /// </summary>
    public partial class VereinsmeisterschaftAktivView : BaseUsercontrol
    {
        public VereinsmeisterschaftAktivView()
        {
            RegisterMessages("VereinsmeisterschaftAktiveVereinsmeisterschaft");
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<NeueVereinsmeisterschaftErstellenMessage, string>(this, "VereinsmeisterschaftAktiveVereinsmeisterschaft", (r, m) => ReceiveNeueVereinsmeisterschaftErstellenMessage(m));
            WeakReferenceMessenger.Default.Register<NeuerSchuetzeErstellenMessage, string>(this, "VereinsmeisterschaftAktiveVereinsmeisterschaft", (r, m) => ReceiveNeuerSchuetzeErstellenMessage(m));
            WeakReferenceMessenger.Default.Register<OpenVereinsmeisterschaftGruppenMitSchuetzenMessage, string>(this, "VereinsmeisterschaftAktiveVereinsmeisterschaft", (r, m) => ReceiveOpenVereinsmeisterschaftGruppenMitSchuetzenMessage(m));
            WeakReferenceMessenger.Default.Register<VereinsmeisterschaftErgebnisEintragenMessage, string>(this, "VereinsmeisterschaftAktiveVereinsmeisterschaft", (r, m) => ReceiveVereinsmeisterschaftErgebnisEintragenMessage(m));
            
        }

        private static void ReceiveVereinsmeisterschaftErgebnisEintragenMessage(VereinsmeisterschaftErgebnisEintragenMessage m)
        {
            var view = new VereinsmeisterschaftErgebnisEintragenView()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is VereinsmeisterschaftErgebnisEintragenViewModel model)
            {
                model.ZeigeStammdatenAnAsync(m.SchuetzenErgebnisID);
                view.ShowDialog();
            }
        }

        private static void ReceiveOpenVereinsmeisterschaftGruppenMitSchuetzenMessage(OpenVereinsmeisterschaftGruppenMitSchuetzenMessage m)
        {
            var view = new VereinsmeisterschaftGruppenMitSchuetzenView()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is VereinsmeisterschaftGruppenMitSchuetzenViewModel model)
            {
                model.VereinsmeisterschaftID = m.VereinsmeisterschaftID;
                view.ShowDialog();
            }
        }

        private static void ReceiveNeuerSchuetzeErstellenMessage(NeuerSchuetzeErstellenMessage m)
        {
            var view = new VereinsmeisterschaftNeuerSchuetzeView()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is VereinsmeisterschaftNeuerSchuetzeViewModel model)
            {
                model.VereinsmeisterschaftID = m.VereinsmeisterschaftID;
                view.ShowDialog();
                m.Callback(true);
            }
        }

        private void ReceiveNeueVereinsmeisterschaftErstellenMessage(NeueVereinsmeisterschaftErstellenMessage m)
        {
            var view = new VereinsmeisterschaftNeueErstellenView()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is VereinsmeisterschaftNeueErstellenViewModel model)
            {
                view.ShowDialog();
                WeakReferenceMessenger.Default.Unregister<NeueVereinsmeisterschaftErstellenMessage, string>(this, "VereinsmeisterschaftAktiveVereinsmeisterschaft");
                m.Callback(model.NeueVereinsmeisterschaftErstellt);
            }        
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<NeuerSchuetzeErstellenMessage, string>(this, "VereinsmeisterschaftAktiveVereinsmeisterschaft");
            WeakReferenceMessenger.Default.Unregister<OpenVereinsmeisterschaftGruppenMitSchuetzenMessage, string>(this, "VereinsmeisterschaftAktiveVereinsmeisterschaft");
            WeakReferenceMessenger.Default.Unregister<VereinsmeisterschaftErgebnisEintragenMessage, string>(this, "VereinsmeisterschaftAktiveVereinsmeisterschaft"); 
            WeakReferenceMessenger.Default.Unregister<NeueVereinsmeisterschaftErstellenMessage, string>(this, "VereinsmeisterschaftAktiveVereinsmeisterschaft");

            base.Window_Unloaded(sender, e);
        }
    }
}