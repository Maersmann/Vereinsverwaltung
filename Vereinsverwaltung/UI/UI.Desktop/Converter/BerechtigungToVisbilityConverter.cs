using Logic.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace UI.Desktop.Converter
{
    public class BerechtigungToVisbilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool Return = (bool)value;
            if (parameter != null)
            {
                Data.Types.BerechtigungTypes Berechtigung = BerechtigungenService.StringToTyp(parameter.ToString());
                Return = BerechtigungenService.HatBerechtigung(Berechtigung);
            }
            
            return Return ? Visibility.Visible : Visibility.Collapsed;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible;
        }
    }
}
