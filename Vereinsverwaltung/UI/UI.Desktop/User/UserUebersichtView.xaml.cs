using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.UserMessages;
using Logic.UI.UserViewModels;
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

namespace UI.Desktop.User
{
    /// <summary>
    /// Interaktionslogik für UserUebersichtView.xaml
    /// </summary>
    public partial class UserUebersichtView : UserControl
    {
        public UserUebersichtView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<OpenUserBerechtigungenMessage, string>(this, "UserUebersicht", (r, m) => ReceiveOpenUserBerechtigungenMessage(m));
        }

        private static void ReceiveOpenUserBerechtigungenMessage(OpenUserBerechtigungenMessage m)
        {
            UserBerechtigungenUebersichtView view = new()
            {
                Owner = Application.Current.MainWindow
            };

            if (view.DataContext is UserBerechtigungenUebersichtViewModel model)
            {
                model.UserID = m.UserID;
            }

            _ = view.ShowDialog();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool visible)
            {
                if (visible)
                {
                    if (DataContext is UserUebersichtViewModel modelUebersicht)
                    {
                        modelUebersicht.SetConnection();
                        _ = modelUebersicht.LoadData();
                    }
                       
                }
            }

        }
    }
}
