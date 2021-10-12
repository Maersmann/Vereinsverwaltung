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
    public partial class ExportSchluesselView : BaseView
    {
        
        public ExportSchluesselView()
        {
            InitializeComponent();
            RegisterMessages("ExportSchluessel");
        }
    }
}
