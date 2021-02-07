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
    public class TickMark : NotifyPropertyChanged
    {
        private double _x1, _x2, _y1, _y2;
        public double X1
        {
            get => default;
            set
            {
                if (value != _x1)
                {
                    _x1 = value;
                    OnPropertyChanged("X1");
                }
            }
        }

        public double X2
        {
            get => default;
            set
            {
                if (value != _x2)
                {
                    _x2 = value;
                    OnPropertyChanged("X2");
                }
            }
        }

        public double Y1
        {
            get => default;
            set
            {
                if (value != _y1)
                {
                    _y1 = value;
                    OnPropertyChanged("Y1");
                }
            }
        }

        public double Y2
        {
            get => default;
            set
            {
                if (value != _y2)
                {
                    _y2 = value;
                    OnPropertyChanged("Y2");
                }
            }
        }
    }

    /// <summary>
    /// 刻度，同样长度、单位的一组刻度线。
    /// </summary>
    public class TickMarks : ObservableCollection<TickMark>
    {
        /// <summary>
        /// 刻度线方向，与数轴方向垂直。
        /// </summary>
        public AxisDirection Direction;

        /// <summary>
        /// 刻度线的相对于数轴单位的单位值。
        /// </summary>
        public double Unit;

        /// <summary>
        /// 刻度线的长度（像素）；
        /// </summary>
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
        /// 数轴原点在画布上X轴坐标。
        /// </summary>
        public double X
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// 数轴原点在画布上Y轴坐标。
        /// </summary>
        public double Y
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// 构造函数需定义刻度线的方向、长度，并提供数轴原点的坐标。
        /// </summary>
        /// <param name="direction">刻度线的方向。</param>
        /// <param name="length">刻度线的长度。</param>
        /// <param name="unit">该刻度的单位。</param>
        public TickMarks(AxisDirection direction, double length, double unit)
        {
            Length = length;
            Direction = direction;
            Unit = unit;
        }

        /// <summary>
        /// 添加刻度线，p为刻度线在坐标轴上的坐标值。length为刻度线的长度（像素）。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="length">刻度线的长度（像素）</param>
        public void Add(double p)
        {
            TickMark tick = new TickMark();
            switch (Direction)
            {
                case AxisDirection.Horizontal:
                    tick.X1 = X;
                    tick.X2 = X + Length;
                    tick.Y1 = tick.Y2 = p;
                    break;
                case AxisDirection.Vertical:
                    tick.X1 = tick.X2 = p;
                    tick.Y1 = Y;
                    tick.Y2 = Y - Length;
                    break;
            }
        }
    }

    public class NumberAxis : NotifyPropertyChanged
    {
        /// <summary>
        /// 数轴名称，例如：米，秒等。
        /// </summary>
        public string Title;   
        
        /// <summary>
        /// 数轴起点对应的数值。
        /// </summary>
        public double StartValue;

        /// <summary>
        /// 数轴终点对应的数值。
        /// </summary>
        public double EndValue;

        private AxisDirection Direction; //数轴方向，初始化时确定，其后不得更改。
        private AxisDirection TickDirection;

        /// <summary>
        /// 刻度线数组，内有多个刻度线。初始化时确定，其后不得修改。
        /// </summary>
        public ObservableCollection<TickMarks> MultiTicks;

        protected NumberAxis(AxisDirection direction, string title)
        {
            Title = title;
            Direction = direction;
            TickDirection = Converter.DirectionChange(direction);
        }

        /// <summary>
        /// 计算数轴上的刻度值。
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public List<double> GetTickValues(double startValue, double endValue, double unit)
        {
            List<double> values = new List<double>();
            double start = startValue % unit + startValue;
            string a = start.ToString();
            int l = a.Length - a.IndexOf(".") - 1;
            for (double i = start; i < endValue; i += unit)
            {
                values.Add(Math.Round(i, l, MidpointRounding.AwayFromZero));
            }
            return values;
        }

        public void SetValue(double startValue, double endValue)
        {
            StartValue = startValue;
            EndValue = endValue;


        }

        public void AddTickMarks(TickMarks tickMarks)
        {
            tickMarks.Direction = TickDirection;
            MultiTicks.Add(tickMarks);
        }

    }

    public class HorizontalAxis : NumberAxis
    {
        public HorizontalAxis(string title):base(AxisDirection.Horizontal, title){ }

        public void AddTicks(string title, double unit, double length)
        {
            TickMarks tickMarks = new TickMarks(AxisDirection.Horizontal, length, unit);
        }
    }

    public class VerticalAxis : NumberAxis
    {
        public VerticalAxis(string title) : base(AxisDirection.Vertical, title) { }
    }
}
