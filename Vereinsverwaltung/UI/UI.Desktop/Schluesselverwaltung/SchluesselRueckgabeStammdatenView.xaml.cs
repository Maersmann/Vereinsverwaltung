﻿using CommunityToolkit.Mvvm.Messaging;
using Logic.Messages.AuswahlMessages;
using Logic.UI.AuswahlViewModels;
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
using Vereinsverwaltung.UI.Desktop.Auswahl;
using UI.Desktop.BaseViews;

namespace UI.Desktop.Schluesselverwaltung
{
    /// <summary>
    /// Interaktionslogik für SchluesselRueckgabeStammdatenView.xaml
    /// </summary>
    public partial class SchluesselRueckgabeStammdatenView : StammdatenView
    {
        public SchluesselRueckgabeStammdatenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.schluesselrueckgabe);
            WeakReferenceMessenger.Default.Register<OpenSchluesselzuteilungAuswahlMessage, string>(this, "SchluesselRueckgabeStammdaten", (r, m) => ReceiveOpenSchluesselzuteilungAuswahlMessage(m));
        }

        private static void ReceiveOpenSchluesselzuteilungAuswahlMessage(OpenSchluesselzuteilungAuswahlMessage m)
        {
            var view = new SchluesselzuteilungAuswahlView()
            {
                Owner = Application.Current.MainWindow
            };
            if (view.DataContext is SchluesselzuteilungAuswahlViewModel model)
            {
                model.SetAuswahlState(m.ID, m.AuswahlTypes);
                view.ShowDialog();
                if (model.AuswahlGetaetigt && model.ID().HasValue)
                {
                    m.Callback(true, model.ID().Value);
                }
                else
                {
                    m.Callback(false, 0);
                }
            }
        }

        protected override void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<OpenSchluesselzuteilungAuswahlMessage, string>(this, "SchluesselRueckgabeStammdaten");
            base.Window_Unloaded(sender, e);
        }
    }
}
