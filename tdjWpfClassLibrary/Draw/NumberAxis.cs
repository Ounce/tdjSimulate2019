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
            get => _x1;
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
            get => _x2;
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
            get => _y1;
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
            get => _y2;
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
    public class TickMarks : ObservableCollection<double>
    {
        /// <summary>
        /// 刻度线的相对于数轴单位的单位值。
        /// </summary>
        public double Unit;

        /// <summary>
        /// 构造函数需定义刻度线的单位。
        /// </summary>
        /// <param name="unit">该刻度的单位。</param>
        public TickMarks(double unit)
        {
            Unit = unit;
        }

        /// <summary>
        /// 按照 positionList 包含的坐标值添加刻度线。scale为比例，每个单位对应的像素数量。
        /// </summary>
        /// <param name="positionList">坐标值数组。</param>
        /// <param name="scale">比例，每个单位对应的像素数量。</param>
        public void Add(List<double> positionList, double scale)
        {
            foreach (double i in positionList)
            {
                Add(i * scale);
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

        public NumberAxis(string title)
        {
            Title = title;
            MultiTicks = new ObservableCollection<TickMarks>();
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

        public void SetValue(double startValue, double endValue, double scale)
        {
            StartValue = startValue;
            EndValue = endValue;
            foreach (var i in MultiTicks)
            {
                i.Add(GetTickValues(startValue, endValue, i.Unit), scale);
            }
        }

        public void AddTickMarks(double unit)
        {
            MultiTicks.Add(new TickMarks(unit));
        }

    }

}
