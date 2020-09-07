using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary.Profile
{
    public class Scale : NotifyPropertyChanged
    {
        /// <summary>
        /// 水平比例。
        /// </summary>
        public double Horizontal
        {
            get { return _hScale; }
            set
            {
                if (value != _hScale)
                {
                    _hScale = value;
                    OnPropertyChanged("Horizontal");
                }
            }
        }
        private double _hScale;

        /// <summary>
        /// 垂直比例，应根据具体应用自行定义。
        /// </summary>
        public double Vertical
        {
            get { return _vScale; }
            set
            {
                if (value != _vScale)
                {
                    _vScale = value;
                    OnPropertyChanged("Vertical");
                }
            }
        }
        private double _vScale;

        public double VerticalHorizontalScale;

        public Scale()
        {
            _hScale = 1;
            _vScale = 1;
            VerticalHorizontalScale = 50;
        }

        /// <summary>
        /// 计算比例。
        /// </summary>
        /// <param name="height">画布的高</param>
        /// <param name="width">画布的宽</param>
        /// <param name="maxAltitude">最大高程</param>
        /// <param name="minAltitude">最大高程</param>
        /// <param name="length">纵断面的长度</param>
        public void SetScale(double height, double width, double maxAltitude, double minAltitude, double length)
        {
            double v = height / (maxAltitude - minAltitude);
            double h = width / length;
            if (h * VerticalHorizontalScale < v)
            {
                Horizontal = h;
                Vertical = h * VerticalHorizontalScale;
            }
            else
            {
                Vertical = v;
                Horizontal = v / VerticalHorizontalScale;
            }
        }
    }
}
