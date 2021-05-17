using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Draw;

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
                    EndMileage = _beginMileage + _length;
                    // 移到 ProfileViewModel中计算。
                    //EndAltitude = Math.Round(_beginAltitude - _grade * _length, MidpointRounding.AwayFromZero);
                    OnPropertyChanged("Length");
                    OnPropertyChanged("Width");
                }
            }
        }

        public double Grade
        {
            get { return _grade * Option.GradeUnit; }
            set
            {
                double v = value / Option.GradeUnit;
                if (v != _grade)
                {
                    _grade = v;
                    // 移到 ProfileViewModel中计算。
                    //EndAltitude = Math.Round(_beginAltitude - _grade * _length, 3, MidpointRounding.AwayFromZero);
                    OnPropertyChanged("Grade");
                    OnPropertyChanged("GradeShowValue");
                    OnPropertyChanged("SlopeTableGradeLineStartPoint");
                    OnPropertyChanged("SlopeTableGradeLineEndPoint");
                    OnPropertyChanged("SlopeTableLengthLabelHorizontalAlignment");
                    OnPropertyChanged("SlopeTableGradeLabelHorizontalAlignment");
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
                    EndAltitude = _beginAltitude - _grade * _length;
                    OnPropertyChanged("BeginAltitude");
                    OnPropertyChanged("Y1");
                }
            }
        }
        private double _beginAltitude;

        public void SetBeginAltitudeByEndAltitude()
        {
            BeginAltitude = _endAltitude + _grade * _length;
        }

        public void SetEndAltitudeByBeginAltitude()
        {
            EndAltitude = _beginAltitude - _grade * _length;
        }

        public double EndAltitude
        {
            get { return _endAltitude; }
            set
            {
                if (value != _endAltitude)
                {
                    _endAltitude = value;
                    BeginAltitude = _endAltitude + _grade * _length;
                    OnPropertyChanged("EndAltitude");
                    OnPropertyChanged("Y2");
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
                    EndMileage = _beginMileage + _length;
                    X1 = _beginMileage * Scale.Horizontal;
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
                    X2 = _endMileage * Scale.Horizontal; 
                    OnPropertyChanged("EndMileage");
                }
            }
        }
        private double _endMileage;

        public double X1
        {
            get { return _x1; }
            set
            {
                if (value != _x1)
                {
                    _x1 = value;
                    OnPropertyChanged("X1");
                    OnPropertyChanged("SlopeTableGradeLineStartPoint");
                }
            }
        }
        private double _x1;

        public double X2
        {
            get { return _x2; }
            set
            {
                if (value != _x2)
                {
                    _x2 = value;
                    OnPropertyChanged("X2");
                    OnPropertyChanged("SlopeTableRightTop");
                    OnPropertyChanged("SlopeTableRightBottom");
                    OnPropertyChanged("SlopeTableGradeLineEndPoint");
                }
            }
        }
        private double _x2;

        public double Y1
        {
            get { return ValueConverter.VerticalValue(BeginAltitude * Scale.Vertical); }
        }

        public double Y2
        {
            get { return ValueConverter.VerticalValue(EndAltitude * Scale.Vertical); }
        }

        public void UpdateScale()
        {
            X1 = _beginMileage * Scale.Horizontal;
            X2 = _endMileage * Scale.Horizontal;
            OnPropertyChanged("Y1");
            OnPropertyChanged("Y2");
            OnPropertyChanged("Width");
        }

        /// <summary>
        /// 坡度表、高程表的宽度。
        /// </summary>
        public double Width
        {
            get { return _length * Scale.Horizontal; }
        }

        public double SlopeTableTop
        {
            get { return _slopeTableTop; }
            set
            {
                if (value != _slopeTableTop)
                {
                    _slopeTableTop = value;
                    OnPropertyChanged("SlopeTableTop");
                    OnPropertyChanged("SlopeTableLeftTop");
                    OnPropertyChanged("SlopeTableLeftCenter");
                    OnPropertyChanged("SlopeTableRightTop");
                    OnPropertyChanged("SlopeTableRightCenter");
                    OnPropertyChanged("SlopeTableGradeLineStartPoint");
                    OnPropertyChanged("SlopeTableGradeLineEndPoint");
                }
            }
        }
        private double _slopeTableTop;

        public double SlopeTableBottom
        {
            get { return _slopeTableBottom; }
            set
            {
                if (value != _slopeTableBottom)
                {
                    _slopeTableBottom = value;
                    OnPropertyChanged("SlopeTableBottom");
                    OnPropertyChanged("SlopeTableLeftBottom");
                    OnPropertyChanged("SlopeTableLeftCenter");
                    OnPropertyChanged("SlopeTableRightBottom");
                    OnPropertyChanged("SlopeTableRightCenter");
                    OnPropertyChanged("SlopeTableGradeLineStartPoint");
                    OnPropertyChanged("SlopeTableGradeLineEndPoint");
                }
            }
        }
        private double _slopeTableBottom;

        [XmlIgnore]
        public Point SlopeTableLeftTop
        {
            get { return new Point(_x1, _slopeTableTop); }
        }

        [XmlIgnore]
        public Point SlopeTableLeftBottom
        {
            get { return new Point(_x1, _slopeTableBottom); }
        }

        public Point SlopeTableRightTop
        {
            get { return new Point(_x2, _slopeTableTop); }
        }

        public Point SlopeTableRightBottom
        {
            get { return new Point(_x2, _slopeTableBottom); }
        }

        public Point SlopeTableLeftCenter
        {
            get
            {
                return new Point(_x1, 0.5 * (_slopeTableTop + _slopeTableBottom));
            }
        }

        public Point SlopeTableRightCenter
        {
            get
            {
                return new Point(_x2, 0.5 * (_slopeTableTop + _slopeTableBottom));
            }
        }

        public Point SlopeTableGradeLineStartPoint
        {
            get
            {
                switch (StaticClass.Grade.Direction(_grade))
                {
                    case 1: return SlopeTableLeftTop;
                    case -1: return SlopeTableLeftBottom;
                    default: return SlopeTableLeftCenter;
                }
            }
        }

        public Point SlopeTableGradeLineEndPoint
        {
            get
            {
                switch (StaticClass.Grade.Direction(_grade))
                {
                    case -1: return SlopeTableRightTop;
                    case 1: return SlopeTableRightBottom;
                    default: return SlopeTableRightCenter;
                }
            }
        }

        public HorizontalAlignment SlopeTableGradeLabelHorizontalAlignment
        {
            get
            {
                switch (StaticClass.Grade.Direction(_grade))
                {
                    case 1: return HorizontalAlignment.Right;
                    case 0: return HorizontalAlignment.Center;
                    default: return HorizontalAlignment.Left;
                }
            }
        }

        public HorizontalAlignment SlopeTableLengthLabelHorizontalAlignment
        {
            get
            {
                switch (StaticClass.Grade.Direction(_grade))
                {
                    case 1: return HorizontalAlignment.Left;
                    case 0: return HorizontalAlignment.Center;
                    default: return HorizontalAlignment.Right;
                }
            }
        }

        public SlopeViewModel()
        {

        }

        public SlopeViewModel(double length, double grade, double beginMileage, double endMileage, double beginAltitude, double endAltitude) : base() 
        { 
            _length = length; 
            _grade = grade; 
            _beginMileage = beginMileage; 
            _endMileage = endMileage;
            _beginAltitude = beginAltitude;
            _endAltitude = endAltitude;
            _x1 = _beginMileage * Scale.Horizontal;
            _x2 = _endMileage * Scale.Horizontal;
        }

        public double? GetAltitude(double position)
        {
            if (position < BeginMileage || position > EndMileage) return null;
            return Math.Round(_beginAltitude - _grade * (position - _beginMileage), 3, MidpointRounding.AwayFromZero);
        }
    }
}
