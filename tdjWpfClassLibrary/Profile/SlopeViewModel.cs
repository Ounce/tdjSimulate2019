using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace tdjWpfClassLibrary.Profile
{
    public class SlopeViewModel : NotifyPropertyChanged, ISlope
    {
        private double _length;
        public double Length
        {
            get { return _length; }
            set
            {
                if (value != _length)
                {
                    _length = value;
                    OnPropertyChanged("Length");
                }
            }
        }

        public double Grade
        {
            get { return _grade; }
            set
            {
                if (value != _grade)
                {
                    _grade = value;
                    OnPropertyChanged("Grade");
                }
            }
        }
        private double _grade;

        public double BeginAltitude
        {
            get { return _beginAltitude; }
            set
            {
                if (value != _beginAltitude)
                {
                    _beginAltitude = value;
                    OnPropertyChanged("BeginAltitude");
                }
            }
        }
        private double _beginAltitude;

        public double EndAltitude
        {
            get { return _endAltitude; }
            set
            {
                if (value != _endAltitude)
                {
                    _endAltitude = value;
                    OnPropertyChanged("EndAltitude");
                }
            }
        }
        private double _endAltitude;

        public double BeginMileage
        {
            get { return _beginMileage; }
            set
            {
                if (value != _beginMileage)
                {
                    _beginMileage = value;
                    OnPropertyChanged("BeginMileage");
                }
            }
        }
        private double _beginMileage;

        public double EndMileage
        {
            get { return _endMileage; }
            set
            {
                if (value != _endMileage)
                {
                    _endMileage = value;
                    OnPropertyChanged("EndMileage");
                }
            }
        }
        private double _endMileage;

        public Point BeginPoint
        {
            get { return _beginPoint; }
            set
            {
                if (value != _beginPoint)
                {
                    _beginPoint = value;
                    OnPropertyChanged("BeginPoint");
                }
            }
        }
        private Point _beginPoint;

        public Point EndPoint
        {
            get { return _endPoint; }
            set
            {
                if (value != _endPoint)
                {
                    _endPoint = value;
                    OnPropertyChanged("EndPoint");
                }
            }
        }
        private Point _endPoint;

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
        /// 垂直比例，应根据具体应用自行定义。
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


        public SlopeViewModel()
        {
            BeginPoint = new Point(0, 0);
            EndPoint = new Point(0, 0);

        }

        private void UpdateBeginPoint()
        {
            BeginPoint = new Point(BeginMileage * HorizontalScale, BeginAltitude * VerticalScale);
        }

        private void UpdateBeginPointX()
        {
            BeginPoint = new Point(BeginMileage * HorizontalScale, BeginPoint.Y);
        }

        private void UpdateBeginPointY()
        {
            BeginPoint = new Point(BeginPoint.X, BeginAltitude * VerticalScale);
        }

        private void UpdateEndPoint()
        {
            EndPoint = new Point(EndMileage * HorizontalScale, EndAltitude * VerticalScale);
        }

        private void UpdateEndPointX()
        {
            EndPoint = new Point(EndMileage * HorizontalScale, EndPoint.Y);
        }

        private void UpdateEndPointY()
        {
            EndPoint = new Point(EndPoint.X, EndAltitude * VerticalScale);
        }

    }
}
