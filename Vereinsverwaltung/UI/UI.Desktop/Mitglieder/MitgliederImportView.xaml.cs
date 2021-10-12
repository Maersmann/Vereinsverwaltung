using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.UtilMessages;
using Logic.UI.UtilsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Vereinsverwaltung.UI.Desktop.Mitglieder
{
    /// <summary>
    /// Interaktionslogik für MitgliederImportView.xaml
    /// </summary>
    public partial class MitgliederImportView : UserControl
    {
        private LoadingView loadingView;
        public MitgliederImportView()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenLoadingViewMessage>(this, "MitgliederImport", m => ReceiveOpenLoadingViewMessage(m));
            Messenger.Default.Register<CloseLoadingViewMessage>(this, "MitgliederImport", m => CloseLoadingViewMessage());
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
