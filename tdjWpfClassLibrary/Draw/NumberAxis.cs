using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary.Draw
{
    /// <summary>
    /// 单位制。
    /// </summary>
    public enum Measure { Metric, British }

    /// <summary>
    /// 标尺的方向。刻度线的方向与此垂直。
    /// </summary>
    public enum AxisDirection { Horizontal, Vertical }

    /// <summary>
    /// 刻度线类。
    /// </summary>
    public class TickMark
    {
        public double Length;
        double X1, X2, Y1, Y2;
    }

    class NumberAxis : NotifyPropertyChanged
    {
        public double BigDivide;
        public double SmallDivide;
        public TickMark LongMark;
        public TickMark ShortMark;

        protected double[] units;

        public void SetUnit(double maxLength)
        {
            SmallDivide = BigDivide = 0;
            foreach (double longUnit in units)
            {
                if (longUnit < maxLength)
                {
                    SmallDivide = BigDivide;
                    BigDivide = longUnit;
                }
            }
        }
    }
}
