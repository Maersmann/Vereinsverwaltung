using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.UtilMessages;
using Logic.UI.UtilsViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using UI.Desktop.UtilsViews;

namespace UI.Desktop.BaseViews
{
    public class BaseUsercontrol : UserControl
    {
        private LoadingView loadingView;

        public BaseUsercontrol()
        {
            Unloaded += Window_Unloaded;
        }

        public void RegisterMessages(string token)
        {
            Messenger.Default.Register<OpenLoadingViewMessage>(this, token, m => ReceiveOpenLoadingViewMessage(m));
            Messenger.Default.Register<CloseLoadingViewMessage>(this, token, m => CloseLoadingViewMessage());
            Messenger.Default.Register<OpenBestaetigungViewMessage>(this, token, m => ReceiveOpenBestaetigungViewMessage(m));
        }

        private void ReceiveOpenBestaetigungViewMessage(OpenBestaetigungViewMessage m)
        {
            var Bestaetigung = new BestaetigungView
            {
                Owner = Application.Current.MainWindow
            };
            if (Bestaetigung.DataContext is BestaetigungViewModel model)
            {
                model.Beschreibung = m.Beschreibung;
                Bestaetigung.ShowDialog();
                if (model.Bestaetigt)
                {
                    m.Command();
                }
            }
           
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
            loadingView = new LoadingView 
            {
                Owner = Application.Current.MainWindow
            };

            if (loadingView.DataContext is LoadingViewModel model)
            {
                model.Beschreibung = m.Beschreibung;
            }
            loadingView.Show();
        }

        protected virtual void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<CloseLoadingViewMessage>(this);
            Messenger.Default.Unregister<OpenLoadingViewMessage>(this);
            Messenger.Default.Unregister<OpenBestaetigungViewMessage>(this);
        }
    }
}
