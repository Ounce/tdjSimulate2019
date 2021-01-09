using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary.Profile.StaticClass
{
    public enum Measure { Metric, British }

    /// <summary>
    /// 标尺的方向。刻度线的方向与此垂直。
    /// </summary>
    public enum SliceDirection { Horizontal, Vertical }

    public class TickMark
    {
        public double Length;
        double X1, X2, Y1, Y2;
    }

    public class SliceUnit : NotifyPropertyChanged
    {
        public SliceDirection Direction;
        public Measure Measure;
        public TickMark BigTickMark;
        public TickMark SmallTickMark;

        protected double[] units;

        public double LongUnit;
        public double ShortUnit;
        public TickMark LongMark;
        public TickMark ShortMark;

        public void SetUnit(double maxLength)
        {
            ShortUnit = LongUnit = 0;
            foreach (double longUnit in units)
            {
                if (longUnit < maxLength)
                {
                    ShortUnit = LongUnit;
                    LongUnit = longUnit;
                }
            }
        }
    }

    public class LengthMeasure : SliceUnit
    {
        new public Measure Measure 
        {
            get { return base.Measure; }
            set 
            { 
                switch (value)
                {
                    case Measure.British:
                        units = british;
                        break;
                    case Measure.Metric:
                    default:
                        units = metric;
                        break;
                }
            } 
        }

        private double[] metric = { 0.001, 0.01, 0.1, 1, 10, 100, 1000 };
        private double[] british = { };



    }
}
