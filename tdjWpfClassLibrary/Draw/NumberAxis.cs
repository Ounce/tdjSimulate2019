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
        public double X1, X2, Y1, Y2;
    }

    public class TickMarks : ObservableCollection<TickMark>
    {
        /// <summary>
        /// 刻度线方向，与数轴方向垂直。
        /// </summary>
        public AxisDirection Direction;
        public double Length
        {
            get { return _length; }
            set
            {
                switch (Direction)
                {
                    case AxisDirection.Horizontal:
                        foreach (var i in this)
                        {
                            i.X2 = i.X1 + value;
                        }
                        break;
                    case AxisDirection.Vertical:
                        foreach (var i in this)
                        {
                            i.Y2 = i.Y1 - value;
                        }
                        break;
                }
            }
        }
        private double _length;

        /// <summary>
        /// 添加刻度线，x，y为刻度线在坐标轴上的位置。length为刻度线的长度（像素）。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="length">刻度线的长度（像素）</param>
        public void Add(double x, double y, double length)
        {
            TickMark tick = new TickMark();
            switch (Direction)
            {
                case AxisDirection.Horizontal:
                    tick.X1 = x;
                    tick.X2 = x + length;
                    tick.Y1 = tick.Y2 = y;
                    break;
                case AxisDirection.Vertical:
                    tick.X1 = tick.X2 = x;
                    tick.Y1 = y;
                    tick.Y2 = y - length;
                    break;
            }
        }
    }

    class NumberAxis : NotifyPropertyChanged
    {
        /// <summary>
        /// 数轴名称。
        /// </summary>
        public string Title;
        public AxisDirection Direction
        {
            get { return _direction; }
            set
            {
                if (value != _direction)
                {
                    _direction = value;
                    OnPropertyChanged("Direction");
                    switch (_direction)
                    {
                        case AxisDirection.Horizontal:
                            BigTicks.Direction = SmallTicks.Direction = AxisDirection.Vertical;
                            break;
                        case AxisDirection.Vertical:
                            BigTicks.Direction = SmallTicks.Direction = AxisDirection.Horizontal;
                            break;
                    }
                }
            }
        }
        private AxisDirection _direction;
        public TickMarks BigTicks;
        public TickMarks SmallTicks;

        /// <summary>
        /// 数轴原点的位置。
        /// </summary>
        public double X, Y;

        public double BigDivide
        {
            get { return _bigDivide; }
            set
            {
                if (value != _bigDivide)
                {
                    value = _bigDivide;
                    OnPropertyChanged("BigDivide");
                    foreach(var i in BigTicks)
                    {
                        switch (Direction)
                        {
                            case AxisDirection.Horizontal:
                                i.Y2 = i.Y1 + value;
                                break;
                            case AxisDirection.Vertical:
                                i.X2 = i.X1 + value;
                                break;
                        }
                    }
                }
            }
        }
        public double SmallDivide;
        private double _smallDivide, _bigDivide;
        public TickMark LongMark;
        public TickMark ShortMark;


    }
}
