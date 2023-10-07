using GalaSoft.MvvmLight.Messaging;
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
            Messenger.Default.Register<NeueVereinsmeisterschaftErstellenMessage>(this, "VereinsmeisterschaftAktiveVereinsmeisterschaft", m => ReceiveNeueVereinsmeisterschaftErstellenMessage(m));
            Messenger.Default.Register<NeuerSchuetzeErstellenMessage>(this, "VereinsmeisterschaftAktiveVereinsmeisterschaft", m => ReceiveNeuerSchuetzeErstellenMessage(m));
            Messenger.Default.Register<OpenVereinsmeisterschaftGruppenMitSchuetzenMessage>(this, "VereinsmeisterschaftAktiveVereinsmeisterschaft", m => ReceiveOpenVereinsmeisterschaftGruppenMitSchuetzenMessage(m));
            Messenger.Default.Register<VereinsmeisterschaftErgebnisEintragenMessage>(this, "VereinsmeisterschaftAktiveVereinsmeisterschaft", m => ReceiveVereinsmeisterschaftErgebnisEintragenMessage(m));
            
        }

        private void ReceiveVereinsmeisterschaftErgebnisEintragenMessage(VereinsmeisterschaftErgebnisEintragenMessage m)
        {
            var view = new VereinsmeisterschaftErgebnisEintragenView()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is VereinsmeisterschaftErgebnisEintragenViewModel model)
            {
                model.ZeigeStammdatenAn(m.SchuetzenErgebnisID);
                view.ShowDialog();
            }
        }

        private void ReceiveOpenVereinsmeisterschaftGruppenMitSchuetzenMessage(OpenVereinsmeisterschaftGruppenMitSchuetzenMessage m)
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

        private void ReceiveNeuerSchuetzeErstellenMessage(NeuerSchuetzeErstellenMessage m)
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
                Messenger.Default.Unregister<NeueVereinsmeisterschaftErstellenMessage>(this);
                m.Callback(model.NeueVereinsmeisterschaftErstellt);
            }        
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<NeuerSchuetzeErstellenMessage>(this);
            Messenger.Default.Unregister<OpenVereinsmeisterschaftGruppenMitSchuetzenMessage>(this);
            Messenger.Default.Unregister<VereinsmeisterschaftErgebnisEintragenMessage>(this); 
            Messenger.Default.Unregister<NeueVereinsmeisterschaftErstellenMessage>(this);

            base.Window_Unloaded(sender, e);
        }
    }
}