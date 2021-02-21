﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace tdjWpfClassLibrary.Draw
{
    /// <summary>
    /// 刻度线类。
    /// </summary>
    public class Tick : NotifyPropertyChanged
    {
        public double Position { get; set; }

        public double VerticalPosition { get; set; }
        public string Text { get; set; }
    }

    /// <summary>
    /// 刻度，同样长度、单位的一组刻度线。
    /// </summary>
    public class Graduation : ObservableCollection<Tick>
    {
        /// <summary>
        /// 刻度线的相对于数轴单位的单位值。
        /// </summary>
        public double Unit;

        /// <summary>
        /// 构造函数需定义刻度线的单位。
        /// </summary>
        /// <param name="unit">该刻度的单位。</param>
        public Graduation(double unit)
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
            Add(new Tick() { Position = position, VerticalPosition= ValueConverter.VerticalValue(position), Text = text });
        }
    }

    public class NumberAxis : NotifyPropertyChanged
    {
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
        public ObservableCollection<Graduation> Graduations;

        public NumberAxis()
        {
            Graduations = new ObservableCollection<Graduation>();
        }

        /// <summary>
        /// 计算数轴上的刻度值。
        /// </summary>
        /// <param name="startValue">开始值</param>
        /// <param name="endValue">结束值</param>
        /// <param name="unit">单位</param>
        /// <returns></returns>
        private List<double> GetTickValues(double unit)
        {
            List<double> values = new List<double>();
            string a = unit.ToString();
            int l = a.Length - a.IndexOf(".") - 1;
            for (double i = StartValue; i <= EndValue; i += unit)
            {
                values.Add(Math.Round(i, l, MidpointRounding.AwayFromZero));
            }
            return values;
        }

        /// <summary>
        /// 设置数轴开始值和结束值，比例。
        /// </summary>
        /// <param name="startValue">开始值</param>
        /// <param name="endValue">结束值</param>
        /// <param name="scale">比例</param>
        public void SetValue(double startValue, double endValue, double scale)
        {
            double s, e;
            StartValue = double.MaxValue;
            EndValue = double.MinValue;
            foreach (var g in Graduations)
            {
                s = Math.Floor(startValue / g.Unit) * g.Unit;
                if (s < StartValue) StartValue = s;
                e = Math.Ceiling(endValue / g.Unit) * g.Unit;
                if (e > EndValue) EndValue = e;
            }
            Scale = scale;
            foreach (var i in Graduations)
            {
                i.Add(GetTickValues(i.Unit), scale);
            }
        }

        /// <summary>
        /// 增加一个刻度。此函数需在SetValue前使用，没有计算刻度线位置。刻度线位置计算在SetValue中进行。
        /// </summary>
        /// <param name="unit"></param>
        public void AddGraduation(double unit)
        {
            Graduations.Add(new Graduation(unit));
        }

    }

}
