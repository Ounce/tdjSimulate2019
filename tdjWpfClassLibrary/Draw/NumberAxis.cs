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
    public class Tick : NotifyPropertyChanged
    {
        public double Position { get; set; }
        public string Text { get; set; }
    }

    /// <summary>
    /// 刻度，同样长度、单位的一组刻度线。
    /// </summary>
    public class TickMarks : ObservableCollection<Tick>
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
            Clear();
            foreach (double i in positionList)
            {
                Add(i * scale, i.ToString());
            }
        }

        private void Add(double position, string text)
        {
            Add(new Tick() { Position = position, Text = text });
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

        /// <summary>
        /// 像素数与数轴值比例。单位：像素/数值。
        /// </summary>
        public double Scale;

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
            Scale = scale;
            foreach (var i in MultiTicks)
            {
                i.Add(GetTickValues(startValue, endValue, i.Unit), scale);
            }
        }

        /// <summary>
        /// 增加一个刻度。此函数需在SetValue前使用，没有计算刻度线位置。刻度线位置计算在SetValue中进行。
        /// </summary>
        /// <param name="unit"></param>
        public void AddTickMarks(double unit)
        {
            MultiTicks.Add(new TickMarks(unit));
        }

    }

}
