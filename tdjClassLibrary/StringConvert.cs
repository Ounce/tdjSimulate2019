using System;
using System.Collections.Generic;
using System.Text;

namespace tdjClassLibrary
{
    public static class StringConvert
    {
        public static double ToDouble(string value)
        {
            if (String.IsNullOrEmpty(value))
                return 0;
            else
                return Convert.ToDouble(value);
        }

        public static double ToDouble(string value, double defult)
        {
            if (String.IsNullOrEmpty(value))
                return defult;
            else
                return Convert.ToDouble(value);
        }
    }

    public static class NullableDoubleConvert
    {
        public static string ToString(double? value, int deci)
        {
            string result;
            if (value == null)
                result = "";
            else
            {
                double d = System.Math.Pow(10, deci);
                if (value < 0)
                {
                    result = ((int)((double)value * d - 0.5) / d).ToString();
                }
                else
                {
                    result = ((int)((double)value * d + 0.5) / d).ToString();
                }
            }
            return result;
        }
    }
}

