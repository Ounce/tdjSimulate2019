using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary.Profile
{
    public static class Scale 
    {
        /// <summary>
        /// 水平比例。
        /// </summary>
        public static double Horizontal;

        /// <summary>
        /// 垂直比例，应根据具体应用自行定义。
        /// </summary>
        public static double Vertical;

        public static double VerticalHorizontalScale;

        static Scale()
        {
            Horizontal = 50;
            Vertical = 1;
            VerticalHorizontalScale = Horizontal * Vertical;
        }

        /// <summary>
        /// 计算比例。
        /// </summary>
        /// <param name="height">画布的高</param>
        /// <param name="width">画布的宽</param>
        /// <param name="maxAltitude">最大高程</param>
        /// <param name="minAltitude">最大高程</param>
        /// <param name="length">纵断面的长度</param>
        public static void SetScale(double height, double width, double maxAltitude, double minAltitude, double length)
        {
            double v = height / (maxAltitude - minAltitude);
            double h = width / length;
            if (h * VerticalHorizontalScale < v)
            {
                Horizontal = h;
                Vertical = h * VerticalHorizontalScale;
            }
            else
            {
                Vertical = v;
                Horizontal = v / VerticalHorizontalScale;
            }
        }
    }
}
