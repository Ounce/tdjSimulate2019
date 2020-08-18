using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace tdjClassLibrary.Profile
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

        public double? BeginAltitude
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
        private double? _beginAltitude;

        public double? EndAltitude
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
        private double? _endAltitude;

        public double? BeginMileage
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
        private double? _beginMileage;

        public double? EndMileage
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
        private double? _endMileage;

        public Point Point { get; set; }

        public SlopeViewModel()
        {

        }
    }
}
