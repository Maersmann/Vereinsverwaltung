using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.SchluesselMessages;
using Logic.UI.SchluesselverwaltungViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Vereinsverwaltung.UI.Desktop.Schluesselverwaltung.Core
{
    public class SchluesselzuteilungHistoryUebersichtHelper
    {

        public void RegisterMessage(string messagetoken)
        {
            WeakReferenceMessenger.Default.Register<OpenSchluesselzuteilungHistoryUebersichtMessage, string>(this, messagetoken, (r, m) => ReceiveOpenSchluesselzuteilungHistoryUebersichtMessage(m));
        }

        private static void ReceiveOpenSchluesselzuteilungHistoryUebersichtMessage(OpenSchluesselzuteilungHistoryUebersichtMessage m)
        {
            var view = new SchluesselzuteilungHistoryUebersichtView();
            if (view.DataContext is SchluesselzuteilungHistoryUebersichtViewModel model)
            {
                model.SetAuswahlState(m.ID, m.AuswahlTypes);
            }

            Window window = new ()
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
