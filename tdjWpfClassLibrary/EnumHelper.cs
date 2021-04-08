using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace tdjWpfClassLibrary
{
    /// <summary>
    /// 得到枚举的DescriptionAttribute值。此类未测试。
    /// </summary>
    public static class EnumHelper
    {
        static public string GetDescription(Enum en)
        {
            Type enumType = en.GetType();
            var name = Enum.GetName(enumType, en);
            if (name == null)
                return string.Empty;
            object[] objs = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
                return string.Empty;
            DescriptionAttribute attr = objs[0] as DescriptionAttribute;
            return attr.Description;
        }
    }

    /// <summary>
    /// 反值转换器，如果参数不能转换成double则会报错。用于XAML文件。
    /// </summary>
    public class ConverterEnumDescription : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            return EnumHelper.GetDescription((Enum)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            foreach (Enum week in Enum.GetValues(targetType))
            {
                if (value.Equals(EnumHelper.GetDescription(week)) == true)
                    return week;
            }
            return null;
        }
    }

    public class EnumDictionary : Dictionary<Enum, string>
    {
        public EnumDictionary()
        {
            foreach (Enum item in Enum.GetValues(typeof(Enum)))
                Add(item, EnumHelper.GetDescription(item));
        }
    }
}
