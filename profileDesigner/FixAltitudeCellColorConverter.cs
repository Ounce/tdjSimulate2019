using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace profileDesigner
{
    class DesignProfileFixAltitudeCellColorConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            double g;
            g = System.Convert.ToDouble(value);
            if (g < -0.00001) return HorizontalAlignment.Left;
            else if (g > 0.00001) return HorizontalAlignment.Right;
            else return HorizontalAlignment.Center;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
