using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace tdjWpfClassLibrary.Profile
{
    class SlopeTableGradeStartYConverter : IMultiValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        /// <summary>
        /// 根据坡度(Values[0])值，小于0返回Values[1]; 等于0，返回 0.5 * Values[1]; 大于0，返回0.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentNullException("values can not be null");

            double g, h;
            g = System.Convert.ToDouble(values[0]);
            h = System.Convert.ToDouble(values[1]);

            if (g < -0.00001) return h;
            else if (g > 0.00001) return 0;
            else return 0.5 * h;
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    class SlopeTableGradeEndYConverter : IMultiValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        /// <summary>
        /// 根据坡度(Values[0])值，小于0返回Values[1]; 等于0，返回 0.5 * Values[1]; 大于0，返回0.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentNullException("values can not be null");

            double g, h;
            g = System.Convert.ToDouble(values[0]);
            h = System.Convert.ToDouble(values[1]);

            if (g < -0.00001) return 0;
            else if (g > 0.00001) return h;
            else return 0.5 * h;
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    class SlopeTableGradeStartPointConverter : IMultiValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        /// <summary>
        /// 根据坡度(Values[0])值，小于0返回Point(values[1], Values[2]); 等于0，返回Point(values[1], 0.5 * values[2]; 大于0，返回Point(values[1]. 0)。
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentNullException("values can not be null");

            double g, x, h;
            g = System.Convert.ToDouble(values[0]);
            x = System.Convert.ToDouble(values[1]);
            h = System.Convert.ToDouble(values[2]);

            if (g < -0.00001) return new Point(x, h);
            else if (g > 0.00001) return new Point(x, 0);
            else return new Point(x, 0.5 * h);
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    class SlopeTableGradeEndPointConverter : IMultiValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        /// <summary>
        /// 根据坡度(Values[0])值，小于0返回Point(values[1], Values[2]); 等于0，返回Point(values[1], 0.5 * values[2]; 大于0，返回Point(values[1]. 0)。
        /// </summary>
        /// <param name="values">[0],Grade; [1],X; [2], Y(Height)</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentNullException("values can not be null");

            double g, x, h;
            g = System.Convert.ToDouble(values[0]);
            x = System.Convert.ToDouble(values[1]);
            h = System.Convert.ToDouble(values[2]);

            if (g < -0.00001) return new Point(x, 0);
            else if (g > 0.00001) return new Point(x, h);
            else return new Point(x, 0.5 * h);
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    class SlopeTableGradeLabelAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            double g;
            g = System.Convert.ToDouble(value);
            if (g < -0.00001) return HorizontalAlignment.Left;
            else if (g > 0.00001) return HorizontalAlignment.Right;
            else return HorizontalAlignment.Center;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    class SlopeTableLengthLabelAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            double g;
            g = System.Convert.ToDouble(value);
            if (g < -0.00001) return HorizontalAlignment.Right;
            else if (g > 0.00001) return HorizontalAlignment.Left;
            else return HorizontalAlignment.Center;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
