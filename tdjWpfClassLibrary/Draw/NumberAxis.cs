using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace tdjWpfClassLibrary.Draw
{
    /// <summary>
    /// 标尺的方向。刻度线的方向与此垂直。
    /// </summary>
    public enum AxisDirection { Horizontal, Vertical }

    /// <summary>
    /// 刻度线类。
    /// </summary>
    public class TickMark
    {
        public string Number;
        public double Length;
        double X1, X2, Y1, Y2;
    }

    class NumberAxis : NotifyPropertyChanged
    {
        public string Title;
        public AxisDirection Direction;
        public ObservableCollection<TickMark> Ticks;
        /// <summary>
        /// 数轴原点的位置。
        /// </summary>
        public double X, Y;

        /// <summary>
        /// 数轴的显示部分的最大和最小值。
        /// </summary>
        public double MinValue, MaxValue;


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
