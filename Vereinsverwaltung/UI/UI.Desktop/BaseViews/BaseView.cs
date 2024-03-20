using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.UtilMessages;
using Logic.UI.UtilsViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using UI.Desktop.UtilsViews;

namespace UI.Desktop.BaseViews
{
    public class BaseView : Window
    {
        private LoadingView loadingView;
        private string token;
        public BaseView()
        {
            Unloaded += Window_Unloaded;
        }

        public void RegisterMessages(string token)
        {
            WeakReferenceMessenger.Default.Register<OpenLoadingViewMessage, string>(this, token, (r, m) => ReceiveOpenLoadingViewMessage(m));
            WeakReferenceMessenger.Default.Register<CloseLoadingViewMessage, string>(this, token, (r, m) => CloseLoadingViewMessage());
            WeakReferenceMessenger.Default.Register<OpenBestaetigungViewMessage, string>(this, token, (r, m) => ReceiveOpenBestaetigungViewMessage(m));
            this.token = token;
        }

        private static void ReceiveOpenBestaetigungViewMessage(OpenBestaetigungViewMessage m)
        {
            var Bestaetigung = new BestaetigungView();
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
            if (token != null)
            {
                WeakReferenceMessenger.Default.Unregister<CloseLoadingViewMessage, string>(this, token);
                WeakReferenceMessenger.Default.Unregister<OpenLoadingViewMessage, string>(this, token);
                WeakReferenceMessenger.Default.Unregister<OpenBestaetigungViewMessage, string>(this, token);
            }
        }
    }
}
