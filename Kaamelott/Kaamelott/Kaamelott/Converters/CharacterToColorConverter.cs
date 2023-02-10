using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Kaamelott.Converters
{
    public class CharacterToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is string stringvalue)
            {
                if (stringvalue == "Arthur")
                    return Color.RoyalBlue;
                else if (stringvalue == "Bohort")
                    return Color.DarkGreen;
            }
            return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
