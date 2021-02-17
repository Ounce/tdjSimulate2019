using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
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
        public HorizontalAlignment HorizontalAlignment
        {
            set 
            {
                if (value != _horizontalAlignment)
                {
                    _horizontalAlignment = value;
                    PolylineOriginPoint.SetX(_horizontalAlignment, canvasWidth, Length);
                    OnPropertyChanged("HorizontalAlignment");
                }
            }
        }
        private HorizontalAlignment _horizontalAlignment;

        public VerticalAlignment PolylineVerticalAlignment
        {
            set 
            { 
                if (value != _verticalAlignment)
                {
                    _verticalAlignment = value;
                    PolylineOriginPoint.SetY(_verticalAlignment, canvasHeight, _maxAltitude, _minAltitude);
                    OnPropertyChanged("VerticalAlignment");
                }
            }
        }
        private VerticalAlignment _verticalAlignment;

        private double canvasHeight, canvasWidth;

        public PolylineOriginPoint PolylineOriginPoint = new PolylineOriginPoint(0, 0);

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


        public ProfileViewModelOption ProfileOption;

        public double SlopeTableBorderWidth
        {
            get { return ProfileOption.SlopeTableBorderWidth; }
            set
            {
                if (value != ProfileOption.SlopeTableBorderWidth)
                {
                    ProfileOption.SlopeTableBorderWidth = value;
                    // Todo 添加SlopeTableBorderWidtgh对GradeLine的影响。
                }
            }
        }
    
        public double SlopeTableTop
        {
            get { return _slopeTableTop; }
            set
            {
                if(value != _slopeTableTop)
                {
                    _slopeTableTop = value;
                    foreach (SlopeViewModel s in Slopes)
                    {
                        s.SlopeTableTop = _slopeTableTop;
                    }
                    OnPropertyChanged("SlopeTableTop");
                }
            }
        }
        private double _slopeTableTop;

        public double SlopeTableHeight
        {
            get { return _slopeTableHeight; }
            set
            {
                if (value != _slopeTableHeight)
                {
                    _slopeTableHeight = value;
                    SlopeTableBottom = _slopeTableTop + _slopeTableHeight;
                    OnPropertyChanged("SlopeTableHeight");
                }
            }
        }
        private double _slopeTableHeight;

        public double SlopeTableBottom
        {
            get { return _slopeTableBottom; }
            set
            {
                if (value != _slopeTableBottom)
                {
                    _slopeTableBottom = value;
                    foreach (SlopeViewModel s in Slopes)
                    {
                        s.SlopeTableBottom = _slopeTableBottom;
                    }
                    OnPropertyChanged("SlopeTableBottom");
                }
            }
        }
        private double _slopeTableBottom;

        public System.Windows.Shapes.Rectangle SlopeTableRectange;

        public ProfileViewModel()
        {
            _maxAltitude = StaticClass.Altitude.InitMax;
            _minAltitude = StaticClass.Altitude.InitMin;
            ProfileOption = new ProfileViewModelOption();
            _horizontalAlignment = HorizontalAlignment.Center;
            _verticalAlignment = VerticalAlignment.Center;
            GradeUnit = 1000;
            ProfileOption = new ProfileViewModelOption();
            Slopes = new ObservableCollection<SlopeViewModel>();
            Slopes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SlopesCollectionChanged);
            this.PropertyChanged += ProfilePropertyChanged;
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
                    UpdateMaxMinAltitude(Slopes[e.NewStartingIndex].BeginAltitude);
                    UpdateMaxMinAltitude(Slopes[e.NewStartingIndex].EndAltitude);
                    SetSlopeTable(Slopes[e.NewStartingIndex]);
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
                case "BeginAltitude":
                    UpdateMaxMinAltitude(((SlopeViewModel)sender).BeginAltitude);
                    break;
                case "EndMileage":
                    p = GetPosition(sender);
                    if (p < 0 || p > Slopes.Count - 2) break;
                    Slopes[p + 1].BeginMileage = Slopes[p].EndMileage;
                    break;
                case "EndAltitude":
                    UpdateMaxMinAltitude(((SlopeViewModel)sender).EndAltitude);
                    p = GetPosition(sender);
                    if (p < 0 || p > Slopes.Count - 2) break;
                    Slopes[p + 1].BeginAltitude = Slopes[p].EndAltitude;
                    break;
            }
        }

        private void ProfilePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SlopeTableOption":
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
            PolylineOriginPoint.SetX(_horizontalAlignment, canvasWidth, Length);
            PolylineOriginPoint.SetY(_verticalAlignment, canvasHeight, _maxAltitude, _minAltitude);
        }

        /// <summary>
        /// 为Slope设置用于绘制SlopeTable所需必要参数。
        /// </summary>
        /// <param name="slope"></param>
        public void SetSlopeTable(SlopeViewModel slope)
        {
            slope.SlopeTableTop = _slopeTableTop;
            slope.SlopeTableBottom = _slopeTableBottom;
        }

        /// <summary>
        /// 将里程和高程换算成X， Y；
        /// </summary>
        /// <param name="mileage"></param>
        /// <param name="altitude"></param>
        /// <returns></returns>
        private Point GetPoint(double mileage, double altitude)
        {
            return new Point(mileage * Scale.Horizontal, GetPointY(altitude));
        }

        private double GetPointY(double altitude)
        {
            return -altitude * Scale.Vertical;
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
            if (_maxAltitude != max)
                SetMaxAltitude(max);
            if (_minAltitude != min)
                SetMinAltitude(min);
            return;
        }

        /// <summary>
        /// 使用value更新MaxAltitude或MinAltitude.
        /// </summary>
        /// <param name="value"></param>
        public void UpdateMaxMinAltitude(double value)
        {
            if (value > _maxAltitude)
            {
                SetMaxAltitude(value);
                //return;
            }
            else if (value < _minAltitude)
            {
                SetMinAltitude(value);
                //return;
            }
            else
                UpdateMaxMinAltitude();
        }

        private void SetMaxAltitude(double altitude)
        {
            _maxAltitude = altitude;
            OnPropertyChanged("MaxAltitude");
        }

        private void SetMinAltitude(double altitude)
        {
            _minAltitude = altitude;
            OnPropertyChanged("MinAltitude");
        }

        public void UpdateMileage()
        {
            double m = 0;
            foreach (var s in Slopes)
            {
                s.BeginMileage = m;
                m += s.Length;
                s.EndMileage = m;//如果删除这句，程序变慢。
            }
        }

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
                //SetSlopeTable(slope);

                //已经在SlopesCollectionChanged中通过调用SetSlopeTable设置。
                //slope.SlopeTableTop = SlopeTableTop;
                //slope.SlopeTableBottom = SlopeTableBottom;
                slope.BeginMileage = m;
                slope.Length = Convert.ToDouble(((XmlElement)xmlNode).GetAttribute("Length"));
                slope.Grade = Convert.ToDouble(((XmlElement)xmlNode).GetAttribute("Grade")) / GradeUnit;
                slope.BeginAltitude = Convert.ToDouble(((XmlElement)xmlNode).GetAttribute("BeginAltitude"));
                //                slope.EndAltitude = slope.BeginAltitude - slope.Length * slope.Grade / ProfileDrawing.GradeUnit;
                m += slope.Length;
                //slope.EndMileage = m;
                Slopes.Add(slope);
            }
            OnPropertyChanged("Count");
            // Slopes.Add会改变profile.FixAltitudePosition。
        }

        
        #endregion
    }


}
