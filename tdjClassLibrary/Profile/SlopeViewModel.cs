using Accessibility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace tdjClassLibrary.Profile
{
    public class SlopeViewModel : NotifyPropertyChanged, ISlope, IScale
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
                    UpdateBeginPolylineY();
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
                    UpdateEndPolylineY();
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
                    UpdateBeginPolylineX();
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
                    UpdateEndPolylineX();
                    OnPropertyChanged("EndMileage");
                }
            }
        }
        private double _endMileage;

        public Point BeginPolylinePoint
        {
            get { return _beginPolylinePoint; }
            set
            {
                if (value != _beginPolylinePoint)
                {
                    _beginPolylinePoint = value;
                    OnPropertyChanged("BeginPolylinePoint");
                }
            }
        }
        private Point _beginPolylinePoint;

        public Point EndPolylinePoint
        {
            get { return _endPolylinePoint; }
            set
            {
                if (value != _endPolylinePoint)
                {
                    if (value != _endPolylinePoint)
                    {
                        _endPolylinePoint = value;
                        OnPropertyChanged("EndPolylinePoint");
                    }
                }
            }
        }
        private Point _endPolylinePoint;

        /// <summary>
        /// 水平比例。单位：图形单位/长度单位。
        /// </summary>
        public double HorizontalScale 
        {
            get { return _hScale; }
            set
            {
                if (value != _hScale)
                {
                    _hScale = value;
                    UpdateBeginPolylineX();
                    UpdateEndPolylineX();
                    OnPropertyChanged("HorizontalScale");
                }
            }
        }
        private double _hScale;

        /// <summary>
        /// 垂直比例，高程与图形单位之间的比例，单位：图形单位/高程单位。
        /// </summary>
        public double VerticalScale
        {
            get { return _vScale; }
            set
            {
                if (value != _vScale)
                {
                    _vScale = value;
                    UpdateBeginPolylineY();
                    UpdateEndPolylineY();
                    OnPropertyChanged("VerticalScale");
                }
            }
        }
        private double _vScale;

        public SlopeViewModel()
        {

        }

        private void UpdateBeginPolylineX()
        {
            BeginPolylinePoint = new Point(BeginMileage * HorizontalScale, BeginPolylinePoint.Y);
        }

        private void UpdateBeginPolylineY()
        {
            BeginPolylinePoint = new Point(BeginPolylinePoint.X, BeginAltitude * VerticalScale);
        }

        private void UpdateEndPolylineX()
        {
            EndPolylinePoint = new Point(EndMileage * HorizontalScale, EndPolylinePoint.Y);
        }

        private void UpdateEndPolylineY()
        {
            EndPolylinePoint = new Point(EndPolylinePoint.X, EndAltitude * VerticalScale);
        }

    }

    public class Slopes : ObservableCollection<SlopeViewModel>
    {

    }
}
