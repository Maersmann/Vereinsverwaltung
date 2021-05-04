using GalaSoft.MvvmLight.Messaging;
using Logic.Messages.BaseMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aktien.UI.Desktop.Base
{
    public class StammdatenView : Window
    {
        public StammdatenView()
        {
            Messenger.Default.Register<StammdatenGespeichertMessage>(this, m => ReceiveNeueDividendeGespeichertMessage(m));
        }

        private void ReceiveNeueDividendeGespeichertMessage(StammdatenGespeichertMessage m)
        {
            if (m.Erfolgreich)
            {
                MessageBox.Show(m.Message);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show(m.Message);
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<StammdatenGespeichertMessage>(this);
        }
    }
}
