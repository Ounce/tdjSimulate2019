﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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
                    EndAltitude = _beginAltitude + _grade * _length;
                    GradeLabel.Width = _length * Scale.Horizontal;
                    LengthLabel.Width = GradeLabel.Width;
                    LengthLabel.Content = _length;
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
                    EndAltitude = _beginAltitude + _grade * _length;
                    GradeLabel.Content = _grade * Option.GradeUnit;
                    if (_grade < 0.01 && _grade > -0.01)
                    {
                        GradeLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                        LengthLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                    }
                    else if (_grade > 0)
                    {
                        GradeLabel.HorizontalContentAlignment = HorizontalAlignment.Left;
                        LengthLabel.HorizontalContentAlignment = HorizontalAlignment.Right;
                    }
                    else
                    {
                        GradeLabel.HorizontalContentAlignment = HorizontalAlignment.Right;
                        LengthLabel.HorizontalContentAlignment = HorizontalAlignment.Left;
                    }
                    SetGradeLineY();
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
                    EndAltitude = _beginAltitude - _grade * _length;
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
                    EndMileage = _beginMileage + _length;
                    double poistion = _beginMileage * Scale.Horizontal;
                    GradeLine.X1 = poistion;
                    BeginLine.X1 = BeginLine.X2 = poistion;
                    Canvas.SetLeft(GradeLabel, poistion);
                    Canvas.SetLeft(LengthLabel, poistion);
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
                    double position = _endMileage * Scale.Horizontal;
                    GradeLine.X2 = position;
                    EndLine.X1 = EndLine.X2 = position;
                    OnPropertyChanged("EndMileage");
                }
            }
        }
        private double _endMileage;

        /// <summary>
        /// 坡段表中的坡度线
        /// </summary>
        public Line GradeLine { get; set; }

        /// <summary>
        /// 坡段表中的起点分隔线
        /// </summary>
        public Line BeginLine { get; set; }

        /// <summary>
        /// 坡段表中的终点分割线
        /// </summary>
        public Line EndLine { get; set; }

        /// <summary>
        /// 坡段表中的坡度标签
        /// </summary>
        public Label GradeLabel { get; set; }   

        /// <summary>
        /// 坡段表中的长度标签
        /// </summary>
        public Label LengthLabel { get; set; }

        public double SlopeTableTop
        {
            set
            {
                if (value != _slopeTableTop)
                {
                    _slopeTableTop = value;
                    BeginLine.Y1 = EndLine.Y1 = _slopeTableTop;
                    //SlopeTableBottom = _slopeTableHeight + _slopeTableTop;
                    SetGradeLineY();
                    OnPropertyChanged("SlopeTableTop");
                }
            }
        }
        private double _slopeTableTop;
        
        // 在 SlopeViewModel 类中，不考虑Height，及其与其他参数的相互关联，这个问题在ProfileViewModel类中考虑。
        /*
        public double SlopeTableHeight
        {
            get { return _slopeTableHeight; }
            set
            {
                if (value != _slopeTableHeight)
                {
                    _slopeTableHeight = value;
                    SlopeTableBottom = BeginLine.Y1 + _slopeTableHeight;
                    OnPropertyChanged("SlopeTableHeight");
                }
            }
        }
        private double _slopeTableHeight;
        */

        public double SlopeTableBottom
        {
            get { return _slopeTableBottom; }
            set
            {
                if (value != _slopeTableBottom)
                {
                    _slopeTableBottom = value;
                    BeginLine.Y2 = EndLine.Y2 = _slopeTableBottom;
                    SetGradeLineY();
                    OnPropertyChanged("SlopeTableBottom");
                }
            }
        }
        private double _slopeTableBottom;

        public SlopeViewModel()
        {
            GradeLabel = new Label();
            LengthLabel = new Label();
            BeginLine = new Line();
            EndLine = new Line();
            GradeLine = new Line();
            GradeLabel.VerticalContentAlignment = VerticalAlignment.Center;
            LengthLabel.VerticalContentAlignment = VerticalAlignment.Center;
            BeginLine.StrokeThickness = EndLine.StrokeThickness = GradeLine.StrokeThickness = 1;
            BeginLine.Stroke = EndLine.Stroke = GradeLine.Stroke = Brushes.Blue;
        }

        private void SetGradeLineY()
        {
            if (_grade > 0.01)
            {
                GradeLine.Y1 = _slopeTableTop;
                GradeLine.Y2 = _slopeTableBottom;
            }
            else if (_grade < -0.01)
            {
                GradeLine.Y1 = _slopeTableBottom;
                GradeLine.Y2 = _slopeTableTop;
            }
            else
            {
                GradeLine.Y1 = GradeLine.Y2 = 0.5 * (_slopeTableTop + _slopeTableBottom);
            }
        }

    }
}
