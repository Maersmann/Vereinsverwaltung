using CommunityToolkit.Mvvm.Messaging;
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
            WeakReferenceMessenger.Default.Register<OpenVereinsmeisterschaftSchuetzenDerGruppeMessage, string>(this, "SchiessgruppenUebersicht", (r, m) => ReceiveOpenVereinsmeisterschaftSchuetzenDerGruppeMessage(m));
            WeakReferenceMessenger.Default.Register<OpenVereinsmeisterschaftGruppeErgebnisseMessage, string>(this, "SchiessgruppenUebersicht", (r, m) => ReceiveOpenVereinsmeisterschaftGruppeErgebnisseMessage(m));
        }

        private static void ReceiveOpenVereinsmeisterschaftGruppeErgebnisseMessage(OpenVereinsmeisterschaftGruppeErgebnisseMessage m)
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

        private static void ReceiveOpenVereinsmeisterschaftSchuetzenDerGruppeMessage(OpenVereinsmeisterschaftSchuetzenDerGruppeMessage m)
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
            WeakReferenceMessenger.Default.Unregister<OpenVereinsmeisterschaftSchuetzenDerGruppeMessage, string>(this, "SchiessgruppenUebersicht");
            WeakReferenceMessenger.Default.Unregister<OpenVereinsmeisterschaftGruppeErgebnisseMessage, string>(this, "SchiessgruppenUebersicht");
        }
    }
}
