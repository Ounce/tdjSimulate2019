using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace tdjClassLibrary.Profile
{
    /// <summary>
    /// 图的基类，封装了一些基本参数。
    /// </summary>
    public class BaseDrawing : NotifyPropertyChanged
    {
        /// <summary>
        /// 用于绘图的Canvas的引用，使用绘图函数前应先赋值，Canvas应在项目的Windows中定义。
        /// </summary>
        public Canvas Canvas { get; set; }

        /// <summary>
        /// 水平比例。
        /// </summary>
        public double HorizontalScale
        {
            get { return _hScale; }
            set
            {
                if (value != _hScale)
                {
                    _hScale = value;
                    OnPropertyChanged("HorizontalScale");
                }
            }
        }
        private double _hScale;

        /// <summary>
        /// 垂直比例
        /// </summary>
        public double VerticalScale
        {
            get { return _vScale; }
            set
            {
                if (value != _vScale)
                {
                    _vScale = value;
                    OnPropertyChanged("VerticalScale");
                }
            }
        }
        private double _vScale;

        /// <summary>
        /// 图的顶端在Canvas中的垂直方向的位置。
        /// </summary>
        public double TopPosition { get; set; }

        /// <summary>
        /// 图的底在Canvas中的垂直方向的位置。
        /// </summary>
        public double BottomPosition { get; set; }
    }
}
