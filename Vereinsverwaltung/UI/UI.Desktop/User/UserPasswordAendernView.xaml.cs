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
using System.Windows.Shapes;
using UI.Desktop.BaseViews;

namespace UI.Desktop.User
{
    /// <summary>
    /// Interaktionslogik für UserPasswordAendernView.xaml
    /// </summary>
    public partial class UserPasswordAendernView : StammdatenView
    {
        public UserPasswordAendernView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.user);
        }
    }
}
