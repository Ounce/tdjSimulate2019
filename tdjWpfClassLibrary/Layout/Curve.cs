using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace tdjWpfClassLibrary.Layout
{
    public class Curve : NotifyPropertyChanged
    {
        /// <summary>
        /// 曲线编号或名称。
        /// </summary>
        public string Name 
        { 
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _name;

        public double Begin 
        { 
            get => _begin;
            set
            {
                if (value != _begin)
                {
                    _begin = value;
                    OnPropertyChanged("Begin");
                }
            }
        }
        private double _begin;

        public double End 
        { 
            get => _end;
            set
            {
                if (value != _end)
                {
                    _end = value;
                    OnPropertyChanged("End");
                }
            }
        }
        private double _end;

        /// <summary>
        /// 曲线角度，正为凸，负为凹。
        /// </summary>
        public double Angle 
        { 
            get => _angle;
            set
            {
                if (value != _angle)
                {
                    _angle = value;
                    OnPropertyChanged("Angle");
                }
            }
        }
        private double _angle;

        /// <summary>
        /// 曲线方向，凸凹。
        /// </summary>
        public bool Direction { get; set; }
    }

    public class CurveList : ObservableCollection<Curve>
    {
        /// <summary>
        /// 转角之和。考虑转角有 +- ，此值为绝对值之和。
        /// </summary>
        public double AngleSum
        {
            get
            {
                double a = 0;
                foreach (Curve c in this)
                {
                    a += System.Math.Abs(c.Angle);
                }
                return a;
            }
        }
    }
}
