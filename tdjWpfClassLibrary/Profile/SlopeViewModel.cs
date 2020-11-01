using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;
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
                    /*
                    GradeLabel.Width = _length * Scale.Horizontal;
                    LengthLabel.Width = GradeLabel.Width;
                    LengthLabel.Content = _length;
                    */
                    OnPropertyChanged("Length");
                    OnPropertyChanged("Width");
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
                    /*
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
                    */
                    OnPropertyChanged("Grade");
                    OnPropertyChanged("SlopeTableGradeLineStartPoint");
                    OnPropertyChanged("SlopeTableGradeLineEndPoint");
                    OnPropertyChanged("SlopeTableLengthLabelHorizontalAlignment");
                    OnPropertyChanged("SlopeTableGradeLabelHorizontalAlignment");
                }
            }
        }
        private double _grade;

        public double GradeShowValue
        {
            get { return Grade * Option.GradeUnit; }
        }

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

        public double EndAltitude
        {
            get { return _endAltitude; }
            set
            {
                if (value != _endAltitude)
                {
                    _endAltitude = value;
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
                    //double poistion = _beginMileage * Scale.Horizontal;
                    X1 = _beginMileage * Scale.Horizontal;
                    /*
                    SlopeTableItem.X1 = poistion;
                    GradeLine.X1 = poistion;
                    BeginLine.X1 = BeginLine.X2 = poistion;
                    Canvas.SetLeft(GradeLabel, poistion);
                    Canvas.SetLeft(LengthLabel, poistion);
                    */
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
                    //double position = _endMileage * Scale.Horizontal;
                    X2 = _endMileage * Scale.Horizontal; 
                    /*
                    SlopeTableItem.X2 = position;
                    GradeLine.X2 = position;
                    EndLine.X1 = EndLine.X2 = position;
                    */
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
                    OnPropertyChanged("SlopeTableLeftTop");
                    OnPropertyChanged("SlopeTableLeftCenter");
                    OnPropertyChanged("SlopeTableLeftBottom");
                    OnPropertyChanged("SlopeTableGradeLineStartPoint");
                    OnPropertyChanged("SlopeTableGradeLineEndPoint");
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
                    OnPropertyChanged("SlopeTableRightCenter");
                    OnPropertyChanged("SlopeTableRightBottom");
                    OnPropertyChanged("SlopeTableGradeLineStartPoint");
                    OnPropertyChanged("SlopeTableGradeLineEndPoint");
                }
            }
        }
        private double _x2;

        public double Y1
        {
            get { return  - BeginAltitude * Scale.Vertical; }
        }

        public double Y2
        {
            get { return -EndAltitude * Scale.Vertical; }
        }

        /// <summary>
        /// 坡度表、高程表的宽度。
        /// </summary>
        public double Width
        {
            get { return _length * Scale.Horizontal; }
        }

        /*
        public SlopeTableItem SlopeTableItem
        {
            get { return _slopeTableItem; }
            set
            {
                if (value != _slopeTableItem)
                {
                    _slopeTableItem = value;
                    OnPropertyChanged("SlopeTableItem");
                }
            }
        }
        private SlopeTableItem _slopeTableItem;

        public double GradeLineY1
        {
            get { return _gradeLineY1; }
            set
            {
                if (value != _gradeLineY1)
                {
                    _gradeLineY1 = value;
                    OnPropertyChanged("GradeLineY1");
                }
            }
        }
        private double _gradeLineY1;

        public double GradeLineY2
        {
            get { return _gradeLineY2; }
            set
            {
                if (value != _gradeLineY2)
                {
                    _gradeLineY2 = value;
                    OnPropertyChanged("GradeLineY2");
                }
            }
        }
        private double _gradeLineY2;

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
        */

        public double SlopeTableTop
        {
            get { return _slopeTableTop; }
            set
            {
                if (value != _slopeTableTop)
                {
                    _slopeTableTop = value;
                    //BeginLine.Y1 = EndLine.Y1 = _slopeTableTop;
                    //SlopeTableBottom = _slopeTableHeight + _slopeTableTop;
                    //SetGradeLineY();
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
                    //BeginLine.Y2 = EndLine.Y2 = _slopeTableBottom;
                    //SetGradeLineY();
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

        public Point SlopeTableLeftTop
        {
            get { return new Point(_x1, _slopeTableTop); }
        }

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
        private Point _slopeTableRightBottom;

        public Point SlopeTableLeftCenter
        {
            get
            {
                return new Point(_x1, 0.5 * (_slopeTableTop + _slopeTableBottom));
            }
        }
        private Point _slopeTableLeftCenter;

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
                switch (tdjWpfClassLibrary.Profile.Grade.Direction(_grade))
                {
                    case 1: return SlopeTableLeftTop;
                    case -1: return SlopeTableLeftBottom;
                    case 0: return SlopeTableLeftCenter;
                }
                return new Point(0, 0);
            }
        }

        public Point SlopeTableGradeLineEndPoint
        {
            get
            {
                switch (tdjWpfClassLibrary.Profile.Grade.Direction(_grade))
                {
                    case -1: return SlopeTableRightTop;
                    case 1: return SlopeTableRightBottom;
                    case 0: return SlopeTableRightCenter;
                }
                return new Point(0, 0);
            }
        }

        public HorizontalAlignment SlopeTableGradeLabelHorizontalAlignment
        {
            get
            {
                switch (tdjWpfClassLibrary.Profile.Grade.Direction(_grade))
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
                switch (tdjWpfClassLibrary.Profile.Grade.Direction(_grade))
                {
                    case 1: return HorizontalAlignment.Left;
                    case 0: return HorizontalAlignment.Center;
                    default: return HorizontalAlignment.Right;
                }
            }
        }

        public SlopeViewModel()
        {
            /*
            SlopeTableItem = new SlopeTableItem();
            GradeLabel = new Label();
            LengthLabel = new Label();
            BeginLine = new Line();
            EndLine = new Line();
            GradeLine = new Line();
            GradeLabel.VerticalContentAlignment = VerticalAlignment.Center;
            LengthLabel.VerticalContentAlignment = VerticalAlignment.Center;
            BeginLine.StrokeThickness = EndLine.StrokeThickness = GradeLine.StrokeThickness = 4;
            BeginLine.Stroke = EndLine.Stroke = GradeLine.Stroke = Brushes.Blue;
            SlopeTableTop = 288;
            SlopeTableBottom = 388;
            */
        }

        /*
        public void SetGradeLineY()
        {
            if (_grade > 0.00001)
            {
                GradeLineY1 = _slopeTableTop;
                GradeLineY2 = _slopeTableBottom;
            }
            else if (_grade < -0.00001)
            {
                GradeLineY1 = _slopeTableBottom;
                GradeLineY2 = _slopeTableTop;
            }
            else
            {
                GradeLineY1 = GradeLineY2 = 0.5 * (_slopeTableTop + _slopeTableBottom);
            }
        }
        */
    }
}
