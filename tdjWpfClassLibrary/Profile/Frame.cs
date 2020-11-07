using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using System.Text;

namespace tdjWpfClassLibrary.Profile
{
    public class Frame : NotifyPropertyChanged
    {
        public Axis XAxis;
        public Axis YAxis;
    }

    public class Axis : NotifyPropertyChanged
    {
        /// <summary>
        /// 轴名称。
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 大刻度线的高度。
        /// </summary>
        public double Slice1Height { get; set; }

        /// <summary>
        /// 小刻度线高度
        /// </summary>
        public double Slice2Height { get; set; }

        /// <summary>
        /// 刻度起点。
        /// </summary>
        public double BeginValue { get; set; }

        /// <summary>
        /// 刻度终点。
        /// </summary>
        public double EndValue { get; set; }

        /// <summary>
        /// 大刻度的数值
        /// </summary>
        public ObservableCollection<Label> SliceValues { get; set; }

        public ObservableCollection<Slice> Slices { get; set; }
    }

    public class Slice : NotifyPropertyChanged
    {
        private double _x1, _x2, _y1, _y2;
        public double X1
        {
            get { return _x1; }
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
            get { return _x2; }
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
            get { return _y1; }
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
            get { return _y2; }
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
}
