using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace tdjWpfClassLibrary.Draw
{
    public class ConverterPlus : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double r = 0;
            if (values == null || values.Length < 2)
                return DependencyProperty.UnsetValue;
            foreach (var i in values)
            {
                r += (double)i;
            }
            return r;
        }
        //反向修改
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            //返回空，标记不可双向转换
            return null;
        }
    }

    public class ConverterNegate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            double douValue = (double)value;
            return -douValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
