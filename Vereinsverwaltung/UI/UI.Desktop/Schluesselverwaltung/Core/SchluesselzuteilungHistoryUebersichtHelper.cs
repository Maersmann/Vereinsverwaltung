using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vereinsverwaltung.Logic.Messages.SchluesselMessages;
using Vereinsverwaltung.Logic.UI.SchluesselverwaltungViewModels;

namespace Vereinsverwaltung.UI.Desktop.Schluesselverwaltung.Core
{
    public class SchluesselzuteilungHistoryUebersichtHelper
    {

        public void RegisterMessage(string messagetoken)
        {
            Messenger.Default.Register<OpenSchluesselzuteilungHistoryUebersichtMessage>(this, messagetoken, m => ReceiveOpenSchluesselzuteilungHistoryUebersichtMessage(m));
        }

        private void ReceiveOpenSchluesselzuteilungHistoryUebersichtMessage(OpenSchluesselzuteilungHistoryUebersichtMessage m)
        {
            var view = new SchluesselzuteilungHistoryUebersichtView();
            if (view.DataContext is SchluesselzuteilungHistoryUebersichtViewModel model)
            {
                model.SetAuswahlState(m.ID, m.AuswahlTypes);
            }

            Window window = new Window
            {
                Content = view,
                MinHeight = 300,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            window.ShowDialog();
        }
    }
}
