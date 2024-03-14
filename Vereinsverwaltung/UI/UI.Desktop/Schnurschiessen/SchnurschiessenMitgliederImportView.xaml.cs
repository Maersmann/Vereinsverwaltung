﻿using System;
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
using UI.Desktop.BaseViews;

namespace UI.Desktop.Schnurschiessen
{
    /// <summary>
    /// Interaktionslogik für SchnurschiessenMitgliederImportView.xaml
    /// </summary>
    public partial class SchnurschiessenMitgliederImportView :  BaseUsercontrol

    {
        public SchnurschiessenMitgliederImportView()
        {
            InitializeComponent();
            RegisterMessages("SchnurschiessenMitgliederImport");
        }
    }
}
