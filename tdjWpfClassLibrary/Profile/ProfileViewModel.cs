using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using Point = System.Windows.Point;


namespace tdjWpfClassLibrary.Profile
{
    /// <summary>
    /// 纵断面显示模型。
    /// 内置Slopes。
    /// 提供Polyline和Points。
    /// 需要HorizontalScale和VertialScale。
    /// </summary>
    public class ProfileViewModel : NotifyPropertyChanged
    {
        /// <summary>
        /// 将界面中的控件赋值给这个Polyline后，修改这个Polyline则可同时更新界面控件。
        /// </summary>
        public Polyline Polyline { get; set; }

        /// <summary>
        /// Polyline的Points。该类动态改变这个列表。可将该列表赋值给Polyline的Points。
        /// </summary>
        public PointCollection PolylinePoints
        {
            get { return Polyline.Points; }
        }

        public HorizontalAlignment HorizontalAlignment
        {
            set 
            {
                if (value != _horizontalAlignment)
                {
                    SetHorizontalAlignment();
                    OnPropertyChanged("HorizontalAlignment");
                }
            }
            
        }
        private HorizontalAlignment _horizontalAlignment;

        public VerticalAlignment VerticalAlignment
        {
            set 
            { 
                if (value != _verticalAlignment)
                {
                    SetVerticalAlignment();
                    OnPropertyChanged("VerticalAlignment");
                }
            }
        }
        private VerticalAlignment _verticalAlignment;

        private double canvasHeight, canvasWidth;

        public Point OriginPoint = new Point(0, 0);

        public ObservableCollection<SlopeViewModel> Slopes;

        /// <summary>
        /// 坡度单位。1000 = ‰；100 = % 。
        /// </summary>
        public double GradeUnit { get; set; }

        public int Count
        {
            get { return Slopes.Count; }
        }

        /// <summary>
        /// 原始Altitude位置。未设置时此值为-1。
        /// 读取Excel数据时，以第一个非-999高程为FixAltitudePosition。
        /// </summary>
        public int FixAltitudePosition { get; set; }

        /// <summary>
        /// 原始Altitude。如果原始值为BeginAltitude，则此值为true；如果为EndAltitude时为false。
        /// </summary>
        public bool FixBeginOrEndAltitude { get; set; }

        /// <summary>
        /// 纵断面全长。
        /// </summary>
        public double Length
        {
            get
            {
                double l = 0;
                foreach (var s in Slopes)
                {
                    l += s.Length;
                }
                return l;
            }
        }

        /// <summary>
        /// 最大高程。
        /// </summary>
        public double MaxAltitude
        {
            get { return _maxAltitude; }
        }
        public double _maxAltitude;

        /// <summary>
        /// 最小高程。
        /// </summary>
        public double MinAltitude
        {
            get { return _minAltitude; }
        }
        public double _minAltitude;

        public Point FirstPoint
        {
            get { return firstPoint; }
            set
            {
                if (value != firstPoint)
                {
                    firstPoint = value;
                    OnPropertyChanged("FirstPoint");
                }
            }
        }
        private Point firstPoint;

        public Scale Scale;

        public ProfileViewModel()
        {
            GradeUnit = 1000;
            Polyline = new Polyline();
            Slopes = new ObservableCollection<SlopeViewModel>();
            Slopes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SlopesCollectionChanged);
        }

        /// <summary>
        /// Slopes改变时，处理函数。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlopesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    Slopes[e.NewStartingIndex].PropertyChanged += SlopePropertyChanged;
                    if (Polyline.Points.Count == 0)
                        Polyline.Points.Add(new Point(Slopes[0].BeginMileage * Scale.Horizontal, Slopes[0].BeginAltitude * Scale.Vertical));
                    Polyline.Points.Insert(e.NewStartingIndex + 1, new Point(Slopes[e.NewStartingIndex].EndMileage * Scale.Horizontal, Slopes[e.NewStartingIndex].EndAltitude * Scale.Vertical));
                    break;
            }
        }

        /// <summary>
        /// Slope属性改变处理函数。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlopePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            int p;
            switch (e.PropertyName)
            {
                case "BeginMileage":
                    p = GetPosition(sender);
                    if (p == 0)
                        PolylinePoints[p] = new Point(Slopes[p].BeginMileage * Scale.Horizontal, PolylinePoints[p].Y);
                    break;
                case "BeginAltitude":
                    p = GetPosition(sender);
                    if (p == 0)
                        PolylinePoints[p] = new Point(PolylinePoints[0].X, Slopes[p].BeginAltitude * Scale.Vertical);
                    break;
                case "EndMileage":
                    p = GetPosition(sender);
                    if (p < 0) break;
                    PolylinePoints[p] = new Point(Slopes[p].EndMileage * Scale.Horizontal, PolylinePoints[p].Y);
                    break;
                case "EndAltitude":
                    p = GetPosition(sender);
                    if (p < 0) break;
                    PolylinePoints[p] = new Point(PolylinePoints[p].X, Slopes[p].EndAltitude * Scale.Vertical);
                    break;
            }
        }

        /// <summary>
        /// 按照height和width设置比例后设置Polyline的Points。
        /// 按照Profile的长度、高差计算比例。
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public void SetPolylineFullSize(double height, double width)
        {
            canvasHeight = height;
            canvasWidth = width;
            UpdateMaxMinAltitude();
            Scale.SetScale(height, width, MaxAltitude, MinAltitude, Length);
            UpdatePoints();
            SetHorizontalAlignment();
            SetVerticalAlignment();
        }

        private void SetHorizontalAlignment()
        {
            switch (_horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    OriginPoint.X = 0;
                    break;
                case HorizontalAlignment.Center:
                    OriginPoint.X = (canvasWidth - Length * Scale.Horizontal) * 0.5;
                    break;
                case HorizontalAlignment.Right:
                    OriginPoint.X = canvasWidth - Length * Scale.Horizontal;
                    break;
            }
        }

        private void SetVerticalAlignment()
        {
            switch (_verticalAlignment)
            {
                case VerticalAlignment.Top:
                    OriginPoint.Y = - MaxAltitude * Scale.Vertical;
                    break;
                case VerticalAlignment.Center:
                    OriginPoint.Y = -(MaxAltitude - MinAltitude) * Scale.Vertical;
                    break;
            }
        }

        /// <summary>
        /// 以现有参数更新Points中各点的坐标。
        /// </summary>
        private void UpdatePoints()
        {
            if (PolylinePoints.Count < 1) return;
            PolylinePoints[0] = GetPoint(Slopes[0].BeginMileage, Slopes[0].BeginAltitude);
            for (int i = 0; i < Slopes.Count; i++)
            {
                PolylinePoints[i + 1] = GetPoint(Slopes[i].EndMileage, Slopes[i].EndAltitude);
            }
        }

        /// <summary>
        /// 将里程和高程换算成X， Y；
        /// </summary>
        /// <param name="mileage"></param>
        /// <param name="altitude"></param>
        /// <returns></returns>
        private Point GetPoint(double mileage, double altitude)
        {
            return new Point(mileage * Scale.Horizontal, altitude * Scale.Vertical);
        }

        /// <summary>
        /// 折算altitude对应的Y轴坐标。
        /// </summary>
        /// <param name="altitude">折算高程</param>
        /// <returns>高程Y轴坐标</returns>
        public double GetY(double altitude)
        {
            return 0;
          //  return (TopAltitude - altitude) * VerticalScale;
        }

        /// <summary>
        /// 获得sender代表的Slope在Slopes中的位置。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private int GetPosition(object sender)
        {

            for (int i = 0; i < Slopes.Count; i++)
            {
                if (Slopes[i].Equals(sender))
                {
                    return i;
                }
            }
            return -1;
        }


        #region 数据计算方法

        /// <summary>
        /// 计算最大和最小高程。
        /// </summary>
        /// <returns></returns>
        public void UpdateMaxMinAltitude()
        {
            double min, max;
            min = max = Slopes[0].BeginAltitude;
            foreach (SlopeViewModel s in Slopes)
            {
                if (s.EndAltitude > max)
                    max = s.EndAltitude;
                if (s.EndAltitude < min)
                    min = s.EndAltitude;
            }
            _maxAltitude = max;
            _minAltitude = min;
            return;
        }

        public void UpdateMileage()
        {
            double m = 0;
            foreach (var s in Slopes)
            {
                s.BeginMileage = m;
                m += s.Length;
                s.EndMileage = m;
            }
        }

        /// <summary>
        /// 设置水平、垂直比例。并更新Slope的相关参数。
        /// </summary>
        /// <param name="horizontalScale"></param>
        /// <param name="verticalScale"></param>
        public void SetHorizontalVerticalScale(double horizontalScale, double verticalScale)
        {
            Scale.Horizontal = horizontalScale;
            Scale.Vertical = verticalScale;
            UpdateHorizontalVerticalScale();
        }

        /// <summary>
        /// 水平、垂直比例改变后应调用此函数，更新有关属性。
        /// 更新：Slopes的BeginPoint、EndPoint。
        /// </summary>
        public void UpdateHorizontalVerticalScale()
        {
            PolylinePoints[0] = new Point(Slopes[0].BeginMileage * Scale.Horizontal, Slopes[0].BeginAltitude * Scale.Vertical);
            for(int i = 0; i < Slopes.Count; i++)
            {
                PolylinePoints[i + 1] = new Point(Slopes[i].EndMileage * Scale.Horizontal, Slopes[i].EndAltitude * Scale.Vertical);
            }
        }
        #endregion

        #region Files IO

        /// <summary>
        /// 读取XML格式的数据。
        /// </summary>
        /// <param name="xmlElement">XML包含Profile数据的节点</param>
        public void ReadXML(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            double m = 0;
            Slopes.Clear();
            FixAltitudePosition = Convert.ToInt32(xmlElement.GetAttribute("FixAltitudePosition"));
            FixBeginOrEndAltitude = Convert.ToBoolean(xmlElement.GetAttribute("FixBeginOrEndAltitude"));
            if (xmlElement.GetAttribute("GradeUnit") == null)
                GradeUnit = 1000;
            else
                GradeUnit = Convert.ToDouble(xmlElement.GetAttribute("GradeUnit"));
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                SlopeViewModel slope = new SlopeViewModel();
                slope.BeginMileage = m;
                slope.Length = Convert.ToDouble(((XmlElement)xmlNode).GetAttribute("Length"));
                slope.Grade = Convert.ToDouble(((XmlElement)xmlNode).GetAttribute("Grade")) / GradeUnit;
                slope.BeginAltitude = Convert.ToDouble(((XmlElement)xmlNode).GetAttribute("BeginAltitude"));
                //                slope.EndAltitude = slope.BeginAltitude - slope.Length * slope.Grade / ProfileDrawing.GradeUnit;
                m += slope.Length;
                slope.EndMileage = m;
                Slopes.Add(slope);
            }
            OnPropertyChanged("Count");
            // Slopes.Add会改变profile.FixAltitudePosition。
        }

        
        #endregion
    }


}
