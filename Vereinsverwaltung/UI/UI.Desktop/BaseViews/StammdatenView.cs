using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Data.Types;
using Logic.Messages.BaseMessages;

namespace UI.Desktop.BaseViews
{
    public class StammdatenView : BaseView
    {

        StammdatenTypes types;
        public StammdatenView() : base()
        {
    
        }
        
        public void RegisterStammdatenGespeichertMessage(StammdatenTypes types)
        {
            WeakReferenceMessenger.Default.Register<StammdatenGespeichertMessage, string>(this, types.ToString(), (r,m) => ReceiveStmmdatenGespeichertMessage(m));
            this.types = types;
        }

        private void ReceiveStmmdatenGespeichertMessage(StammdatenGespeichertMessage m)
        {
            if (!m.Message.Trim().Equals(""))
                MessageBox.Show(m.Message);

            if (m.Erfolgreich)
                DialogResult = true;
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<StammdatenGespeichertMessage, string>(this, types.ToString());
            base.Window_Unloaded(sender, e);
        }
    }
}
