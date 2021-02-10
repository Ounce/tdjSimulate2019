﻿using System;
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
            if (values == null || values.Length < 2)
                return DependencyProperty.UnsetValue;
            double verValue = (double)values[0];
            double horValue = (double)values[1];
            return verValue + horValue;
        }
        //反向修改
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            //返回空，标记不可双向转换
            return null;
        }
    }
}
