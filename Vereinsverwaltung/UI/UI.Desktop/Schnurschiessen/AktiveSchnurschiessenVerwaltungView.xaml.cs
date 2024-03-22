using CommunityToolkit.Mvvm.Messaging;
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
            WeakReferenceMessenger.Default.Register<NeuesSchnurschiessenErstellenMessage, string>(this, "AktiveSchnurschiessenVerwaltung", (r, m) => ReceiveNeuesSchnurschiessenErstellenMessage(m));
            WeakReferenceMessenger.Default.Register<OpenAktivesSchnurschiessenVerwaltungAusgabeSchnurMessage, string>(this, "AktiveSchnurschiessenVerwaltung", (r, m) => ReceiveOpenAktivesSchnurschiessenVerwaltungAusgabeSchnurMessage(m));


        }

        private static void ReceiveOpenAktivesSchnurschiessenVerwaltungAusgabeSchnurMessage(OpenAktivesSchnurschiessenVerwaltungAusgabeSchnurMessage m)
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
                WeakReferenceMessenger.Default.Unregister<NeueVereinsmeisterschaftErstellenMessage, string>(this, "AktiveSchnurschiessenVerwaltung");
                m.Callback(model.NeuesSchnurschiessenErstellt);
            }
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<NeuesSchnurschiessenErstellenMessage, string>(this, "AktiveSchnurschiessenVerwaltung");
            WeakReferenceMessenger.Default.Unregister<OpenAktivesSchnurschiessenVerwaltungAusgabeSchnurMessage, string>(this, "AktiveSchnurschiessenVerwaltung");
            base.Window_Unloaded(sender, e);
        }
    }
}
