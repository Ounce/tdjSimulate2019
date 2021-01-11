using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using System.Text;
using System.Windows;

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
        public ObservableCollection<CanvasLabel> SliceValues { get; set; }

        public ObservableCollection<Slice> Slices { get; set; }
    }

    public class Slice : ObservableCollection<SliceLine>
    {
        /// <summary>
        /// 刻度线高度。
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// 刻度单位（间隔）。根据显示比例计算。
        /// </summary>
        public double Unit { get; set; }
    }

    /// <summary>
    /// 刻度线
    /// </summary>
    public class SliceLine : NotifyPropertyChanged
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

        /// <summary>
        /// 起点，只读，可通过X1,Y1设置。
        /// </summary>
        public Point StartPoint
        {
            get
            {
                return new Point(_x1, _y1);
            }
        }

        public Point EndPoint
        {
            get
            {
                return new Point(_x2, _y2);
            }
        }
    }
}
