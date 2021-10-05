using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.UtilMessages;
using Logic.UI.UtilsViewModels;
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
using UI.Desktop.BaseViews;

namespace UI.Desktop.Export
{
    /// <summary>
    /// Interaktionslogik für ExportSchluessel.xaml
    /// </summary>
    public partial class ExportSchluesselView : Window
    {
        private LoadingView loadingView;
        public ExportSchluesselView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenLoadingViewMessage>(this, "ExportSchluessel", m => ReceiveOpenLoadingViewMessage(m));
            Messenger.Default.Register<CloseLoadingViewMessage>(this, "ExportSchluessel", m => CloseLoadingViewMessage());
        }

        private void CloseLoadingViewMessage()
        {
            if (loadingView.IsActive)
            {
                loadingView.Close();
            }
        }

        private void ReceiveOpenLoadingViewMessage(OpenLoadingViewMessage m)
        {
            loadingView = new LoadingView();

            if (loadingView.DataContext is LoadingViewModel model)
            {
                model.Beschreibung = m.Beschreibung;
            }
            loadingView.Show();
        }
    }
}
