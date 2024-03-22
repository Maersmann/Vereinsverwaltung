using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.AuswahlMessages;
using Logic.UI.AuswahlViewModels;
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
using System.Windows.Shapes;
using UI.Desktop.Auswahl;
using UI.Desktop.BaseViews;

namespace UI.Desktop.Export
{
    /// <summary>
    /// Interaktionslogik für ExportVereinsmeisterschaftView.xaml
    /// </summary>
    public partial class ExportVereinsmeisterschaftView : BaseView
    {
        public ExportVereinsmeisterschaftView()
        {
            InitializeComponent();
            RegisterMessages("ExportVereinsmeisterschaft");
            WeakReferenceMessenger.Default.Register<OpenVereinsmeisterschaftAuswahlMessage, string>(this, "ExportVereinsmeisterschaft", (r, m) => ReceiveOpenVereinsmeisterschaftAuswahlMessage(m));
        }

        private static void ReceiveOpenVereinsmeisterschaftAuswahlMessage(OpenVereinsmeisterschaftAuswahlMessage m)
        {
            VereinsmeisterschaftAuswahlView view = new()
            {
                Owner = Application.Current.MainWindow
            };
            
            if (view.DataContext is VereinsmeisterschaftAuswahlViewModel model)
            {
                model.AuswahlTyp = m.AuswahlTyp;
                _ = view.ShowDialog();
                if (model.AuswahlGetaetigt && model.ID().HasValue)
                {
                    m.Callback(true, model.ID().Value, model.Jahr().Value);
                }
                else
                {
                    m.Callback(false, 0, 0);
                }
            }
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            base.Window_Unloaded(sender, e);
            WeakReferenceMessenger.Default.Unregister<OpenVereinsmeisterschaftAuswahlMessage, string>(this, "ExportVereinsmeisterschaft");
        }
    }
}
