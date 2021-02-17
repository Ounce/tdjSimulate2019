using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary
{
    public enum MeasureType { Metric, British }
    public class Unit
    {
        public string Name;
        public List<double> Units;
        public List<string> UnitNames;

        /// <summary>
        /// 根据unit获取一个合适的刻度单位。这个方法不理想，不建议使用。单位的选择应该由像素数量和比例决定。
        /// </summary>
        /// <param name="unit">根据这个数据获取一个合适的刻度单位</param>
        /// <returns></returns>
        public double getUnit(double unit) 
        {
            double s, l;
            s = l = 0;
            foreach (double u in Units)
            {
                s = l;
                l = u;
                if (unit < u)
                {
                    return s;
                }
            }
            return s;
        }
    }

    public class Measure
    {
        public Unit Length;
        public Unit Speed;
    }

    public static class Measures
    {
        public static Measure Metric;
        static Measures()
        {
            Metric.Length.Units = new List<double> { 0.001, 0.01, 0.1, 1, 10, 100, 1000 };
        }
    }

}
