using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.AuswahlMessages;
using Logic.Messages.VereinsmeisterschaftMessages;
using Logic.UI.AuswahlViewModels;
using Logic.UI.VereinsmeisterschaftViewModels;
using System;
using System.Windows;
using UI.Desktop.Auswahl;
using UI.Desktop.BaseViews;

namespace UI.Desktop.Vereinsmeisterschaft
{
    /// <summary>
    /// Interaktionslogik für VereinsmeisterschaftNeuerSchuetzeView.xaml
    /// </summary>
    public partial class VereinsmeisterschaftNeuerSchuetzeView : StammdatenView
    {
        public VereinsmeisterschaftNeuerSchuetzeView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.vereinsmeisterschaftSchuetzeErgebnis);
            WeakReferenceMessenger.Default.Register<OpenSchuetzeAuswahlMessage, string>(this, "VereinsmeisterschaftNeuerSchuetze", (r,m) => ReceiveOpenVereinsmeisterschaftSchuetzeAuswahlMessage(m));
            WeakReferenceMessenger.Default.Register<OpenVereinsmeisterschaftFreieGruppeAuswahlMessage, string>(this, "VereinsmeisterschaftNeuerSchuetze", (r, m) => ReceiveOpenVereinsmeisterschaftFreieGruppeAuswahlMessage(m));
        }

        private static void ReceiveOpenVereinsmeisterschaftFreieGruppeAuswahlMessage(OpenVereinsmeisterschaftFreieGruppeAuswahlMessage m)
        {
            var view = new VereinsmeisterschaftFreieGruppeAuswahlView
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is VereinsmeisterschaftFreieGruppeAuswahlViewModel model)
            {
                model.SetInfoForLoading(m.Geschlecht, m.VereinsmeisterschaftID);
            }
            view.ShowDialog();
            if (view.DataContext is VereinsmeisterschaftFreieGruppeAuswahlViewModel mod)
            {
                if (mod.ID().HasValue && mod.AuswahlGetaetigt)
                {
                    m.Callback(true, mod.ID().Value);
                }
                else
                {
                    m.Callback(false, 0);
                }
            }
        }

        private static void ReceiveOpenVereinsmeisterschaftSchuetzeAuswahlMessage(OpenSchuetzeAuswahlMessage m)
        {
            var view = new VereinsmeisterschaftSchuetzeAuswahlView
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is VereinsmeisterschaftSchuetzeAuswahlViewModel modl)
            {
                modl.SetInfoForLoading(m.Geschlecht, m.VereinsmeisterschaftID.Value, m.AuswahlTyp);           
            }
            view.ShowDialog();
            if (view.DataContext is VereinsmeisterschaftSchuetzeAuswahlViewModel model)
            {
                if (model.ID().HasValue && model.AuswahlGetaetigt)
                {
                    m.Callback(true, model.ID().Value);
                }
                else
                {
                    m.Callback(false, 0);
                }
            }
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            base.Window_Unloaded(sender, e);
            WeakReferenceMessenger.Default.Unregister<OpenSchuetzeAuswahlMessage, string>(this, "VereinsmeisterschaftNeuerSchuetze");
            WeakReferenceMessenger.Default.Unregister<OpenVereinsmeisterschaftFreieGruppeAuswahlMessage, string>(this, "VereinsmeisterschaftNeuerSchuetze");
        }
    }
}
