using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace tdjWpfClassLibrary.Draw
{
    /// <summary>
    /// 加法转换器，可多个数值相加，如果参数不能转换成double则会报错。用于XAML文件。
    /// </summary>
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

    /// <summary>
    /// 反值转换器，如果参数不能转换成double则会报错。用于XAML文件。
    /// </summary>
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

    public static class ValueConverter
    {
        /// <summary>
        /// 转换至屏幕坐标。这个函数保持唯一，避免多种方法同时使用。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double VerticalValue(double value)
        {
            return - value;
        }

    }
}
