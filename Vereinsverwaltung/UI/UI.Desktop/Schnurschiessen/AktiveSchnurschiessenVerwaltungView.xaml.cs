using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.SchnurschiessenMessages;
using Logic.Messages.VereinsmeisterschaftMessages;
using Logic.UI.SchnurschiessenViewModels;
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
using UI.Desktop.Vereinsmeisterschaft;

namespace UI.Desktop.Schnurschiessen
{
    /// <summary>
    /// Interaktionslogik für AktiveSchnurschiessenVerwaltungView.xaml
    /// </summary>
    public partial class AktiveSchnurschiessenVerwaltungView : BaseUsercontrol
    {
        public AktiveSchnurschiessenVerwaltungView()
        {
            InitializeComponent();
            RegisterMessages("AktiveSchnurschiessenVerwaltung");
            Messenger.Default.Register<NeuesSchnurschiessenErstellenMessage>(this, "AktiveSchnurschiessenVerwaltung", m => ReceiveNeuesSchnurschiessenErstellenMessage(m));
            Messenger.Default.Register<OpenAktivesSchnurschiessenVerwaltungAusgabeSchnurMessage>(this, "AktiveSchnurschiessenVerwaltung", m => ReceiveOpenAktivesSchnurschiessenVerwaltungAusgabeSchnurMessage(m));


        }

        private void ReceiveOpenAktivesSchnurschiessenVerwaltungAusgabeSchnurMessage(OpenAktivesSchnurschiessenVerwaltungAusgabeSchnurMessage m)
        {
            var view = new AktivesSchnurschiessenVerwaltungAusgabeSchnurView
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is AktivesSchnurschiessenVerwaltungAusgabeSchnurViewModel model)
            {
                model.SetzeInformationen(m.SchnurschiessenBestandID, m.Bezeichnung);
            }
            view.ShowDialog();
        }

        private void ReceiveNeuesSchnurschiessenErstellenMessage(NeuesSchnurschiessenErstellenMessage m)
        {
            var view = new SchnurschiessenNeuesErstellenView()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is SchnurschiessenNeuesErstellenViewModel model)
            {
                view.ShowDialog();
                Messenger.Default.Unregister<NeueVereinsmeisterschaftErstellenMessage>(this);
                m.Callback(model.NeuesSchnurschiessenErstellt);
            }
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<NeuesSchnurschiessenErstellenMessage>(this);
            Messenger.Default.Unregister<OpenAktivesSchnurschiessenVerwaltungAusgabeSchnurMessage>(this);
            base.Window_Unloaded(sender, e);
        }
    }
}
