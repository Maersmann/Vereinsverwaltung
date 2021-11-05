using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.VereinsmeisterschaftMessages;
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

namespace UI.Desktop.Vereinsmeisterschaft
{
    /// <summary>
    /// Interaktionslogik für VereinsmeisterschaftSchuetzenUebersichtView.xaml
    /// </summary>
    public partial class SchuetzenUebersichtView : UserControl
    {
        public SchuetzenUebersichtView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenVereinsmeisterschaftGruppenVonSchuetzeMessage>(this, "SchuetzenUebersicht", m => ReceiveOpenVereinsmeisterschaftGruppenVonSchuetzeMessage(m));
            Messenger.Default.Register<OpenVereinsmeisterschaftSchuetzeErgebnisseMessage>(this, "SchuetzenUebersicht", m => ReceiveOpenVereinsmeisterschaftSchuetzeErgebnisseMessage(m));
        }

        private void ReceiveOpenVereinsmeisterschaftSchuetzeErgebnisseMessage(OpenVereinsmeisterschaftSchuetzeErgebnisseMessage m)
        {
            var view = new VereinsmeisterschaftSchuetzeErgebnisseView()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is VereinsmeisterschaftSchuetzeErgebnisseViewModel model)
            {
                model.SchuetzeID = m.SchuetzeID;
                _ = view.ShowDialog();
            }
        }

        private void ReceiveOpenVereinsmeisterschaftGruppenVonSchuetzeMessage(OpenVereinsmeisterschaftGruppenVonSchuetzeMessage m)
        {
            var view = new VereinsmeisterschaftGruppenVonSchuetzeView()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is VereinsmeisterschaftGruppenVonSchuetzeViewModel model)
            {
                model.SchuetzeID = m.SchuetzeID;
                view.ShowDialog();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<OpenVereinsmeisterschaftGruppenVonSchuetzeMessage>(this);
            Messenger.Default.Unregister<OpenVereinsmeisterschaftSchuetzeErgebnisseMessage>(this);
        }
    }
}
