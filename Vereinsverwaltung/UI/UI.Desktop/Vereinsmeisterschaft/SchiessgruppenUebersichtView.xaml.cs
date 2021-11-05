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
    /// Interaktionslogik für VereinsmeisterschaftGruppenUebersichtView.xaml
    /// </summary>
    public partial class SchiessgruppenUebersichtView : UserControl
    {
        public SchiessgruppenUebersichtView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenVereinsmeisterschaftSchuetzenDerGruppeMessage>(this, "SchiessgruppenUebersicht", m => ReceiveOpenVereinsmeisterschaftSchuetzenDerGruppeMessage(m));
            Messenger.Default.Register<OpenVereinsmeisterschaftGruppeErgebnisseMessage>(this, "SchiessgruppenUebersicht", m => ReceiveOpenVereinsmeisterschaftGruppeErgebnisseMessage(m));
        }

        private void ReceiveOpenVereinsmeisterschaftGruppeErgebnisseMessage(OpenVereinsmeisterschaftGruppeErgebnisseMessage m)
        {
            var view = new VereinsmeisterschaftGruppeErgebnisseView()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is VereinsmeisterschaftGruppeErgebnisseViewModel model)
            {
                model.SchiessgruppeID = m.SchiessgruppeID;
                view.ShowDialog();
            }
        }

        private void ReceiveOpenVereinsmeisterschaftSchuetzenDerGruppeMessage(OpenVereinsmeisterschaftSchuetzenDerGruppeMessage m)
        {
            var view = new VereinsmeisterschaftSchuetzenDerGruppeView()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is VereinsmeisterschaftSchuetzenDerGruppeViewModel model)
            {
                model.SchiessgruppeID = m.SchiessgruppeID;
                view.ShowDialog();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<OpenVereinsmeisterschaftSchuetzenDerGruppeMessage>(this);
            Messenger.Default.Unregister<OpenVereinsmeisterschaftGruppeErgebnisseMessage>(this);
        }
    }
}
