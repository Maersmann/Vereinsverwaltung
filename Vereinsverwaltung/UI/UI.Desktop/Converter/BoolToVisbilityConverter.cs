﻿using Logic.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace UI.Desktop.Converter
{
    [ValueConversion(typeof(bool), typeof(Visibility))]

    public class BoolToVisbilityConverter : IValueConverter
    {

        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }

        public BoolToVisbilityConverter()
        {
            // set defaults
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return null;
            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
                return true;
            if (Equals(value, FalseValue))
                return false;
            return null;
        }
    }
}
