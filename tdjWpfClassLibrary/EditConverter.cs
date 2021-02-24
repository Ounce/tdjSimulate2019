using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace tdjWpfClassLibrary
{
    public class ColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string r = "";
            if (values == null || values.Length < 2 || values[0] == null || values[1] == null)
                return DependencyProperty.UnsetValue;
        /*    if ((int)values[0] == (int)values[1])
                return "Red";*/
            return "Red";
        }
        //反向修改
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            //返回空，标记不可双向转换
            return null;
        }
    }
}
