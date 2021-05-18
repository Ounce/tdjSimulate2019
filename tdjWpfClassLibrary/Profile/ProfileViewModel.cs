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
using System.Xml.Serialization;
using tdjWpfClassLibrary.Draw;
using Point = System.Windows.Point;


namespace tdjWpfClassLibrary.Profile
{
    /// <summary>
    /// 纵断面显示模型。
    /// 内置Slopes。
    /// 提供Polyline和Points。
    /// 需要HorizontalScale和VertialScale。
    /// </summary>
    [Serializable]
    public class ProfileViewModel : NotifyPropertyChanged, IGraphPosition
    {
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }

        [XmlAttribute("Title")]
        public string Title { get; set; }

        [XmlIgnore]
        public HorizontalAlignment HorizontalAlignment
        {
            get => _horizontalAlignment;
            set 
            {
                if (value != _horizontalAlignment)
                {
                    _horizontalAlignment = value;
                    OnPropertyChanged("HorizontalAlignment");
                }
            }
        }
        private HorizontalAlignment _horizontalAlignment;

        [XmlIgnore]
        public VerticalAlignment VerticalAlignment
        {
            get => _verticalAlignment;
            set 
            { 
                if (value != _verticalAlignment)
                {
                    _verticalAlignment = value;
                    OnPropertyChanged("VerticalAlignment");
                }
            }
        }
        private VerticalAlignment _verticalAlignment;

        [XmlIgnore]
        public double CanvasActualHeight { get; set; }
        public double CanvasActualWidth { get; set; }

        [XmlIgnore]
        public Point LeftTop { get; set; } = new Point(0, 0);

        public ObservableCollection<SlopeViewModel> Slopes;

        [XmlIgnore]
        public SlopeViewModel this[int index]
        {
            get { return Slopes[index]; }
            set { Slopes[index] = value; }
        }

        /// <summary>
        /// 坡度单位。1000 = ‰；100 = % 。
        /// </summary>
        [XmlAttribute("GradeUnit")]
        public double GradeUnit { get; set; }

        [XmlIgnore]
        public int Count
        {
            get { return Slopes.Count; }
        }

        [XmlIgnore]
        public bool Updated
        {
            get { return _updated; }
            set
            {
                _updated = true;
                OnPropertyChanged("Updated");
            }
        }
        private bool _updated = true;

        /// <summary>
        /// 原始Altitude位置。未设置时此值为-1。
        /// 读取Excel数据时，以第一个非-999高程为FixAltitudePosition。
        /// </summary>
        [XmlAttribute("FixAltitudePosition")]
        public int FixAltitudePosition { get; set; }

        /// <summary>
        /// 原始Altitude。如果原始值为BeginAltitude，则此值为true；如果为EndAltitude时为false。
        /// </summary>
        [XmlAttribute("FixBeginOrEndAltitude")]
        public bool FixBeginOrEndAltitude { get; set; }

        public double BeginAltitude;

        [XmlIgnore]
        public double HumpMileage 
        { 
            get => _humpMileage; 
            set
            {
                if (value != _humpMileage)
                {
                    _humpMileage = value;
                    OnPropertyChanged("HumpMileage");
                    OnPropertyChanged("HumpHeight");
                }
            }
        }
        private double _humpMileage;

        [XmlIgnore]
        public double HumpHeightCalculatePointMileage 
        { 
            get => _humpHeightCalculatePointMileage;
            set
            {
                if (value != _humpHeightCalculatePointMileage)
                {
                    _humpHeightCalculatePointMileage = value;
                    OnPropertyChanged("HumpHeightCalculatePointMileage");
                    OnPropertyChanged("HumpHeight");
                }
            }
        }
        private double _humpHeightCalculatePointMileage;

        [XmlIgnore]
        public double? HumpHeight { get { return GetAltitude(HumpMileage) - GetAltitude(HumpHeightCalculatePointMileage); } }

        /// <summary>
        /// 纵断面全长。
        /// </summary>
        [XmlIgnore]
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
        /// 最大高程。由Slopes各项变化通过SlopePropertyChanged调用UpdateMaxMinAltitude计算。
        /// </summary>
        [XmlIgnore]
        public double MaxAltitude
        {
            get { return _maxAltitude; }
        }
        public double _maxAltitude;

        /// <summary>
        /// 最小高程。由Slopes各项变化通过SlopePropertyChanged调用UpdateMaxMinAltitude计算。
        /// </summary>
        [XmlIgnore]
        public double MinAltitude
        {
            get { return _minAltitude; }
        }
        public double _minAltitude;

        [XmlIgnore]
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

        [XmlIgnore]
        public ProfileViewModelOption ProfileOption;

        [XmlIgnore]
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

        [XmlIgnore]
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

        [XmlIgnore]
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

        [XmlIgnore]
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

        [XmlIgnore]
        public System.Windows.Shapes.Rectangle SlopeTableRectange;

        public ProfileViewModel()
        {
            _maxAltitude = double.MinValue;
            _minAltitude = double.MaxValue;
            ProfileOption = new ProfileViewModelOption();
            _horizontalAlignment = HorizontalAlignment.Center;
            _verticalAlignment = VerticalAlignment.Center;
            GradeUnit = 1000;
            ProfileOption = new ProfileViewModelOption();
            Slopes = new ObservableCollection<SlopeViewModel>();
            Slopes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SlopesCollectionChanged);
            //this.PropertyChanged += ProfilePropertyChanged;
        }

        public void RemovePropertyChanged()
        {
            //Slopes.CollectionChanged -= new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SlopesCollectionChanged);
            foreach (SlopeViewModel slope in Slopes)
            {
                slope.PropertyChanged -= SlopePropertyChanged;
            }
        }

        public void AppendPropertyChanged()
        {
            //Slopes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SlopesCollectionChanged);
            foreach (SlopeViewModel slope in Slopes)
            {
                slope.PropertyChanged += SlopePropertyChanged;
            }
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
                    if (e.NewStartingIndex > 0)
                    {
                        Slopes[e.NewStartingIndex].BeginMileage = Slopes[e.NewStartingIndex - 1].EndMileage;
                    }
                    if (e.NewStartingIndex < Slopes.Count - 1)
                    {
                        Slopes[e.NewStartingIndex + 1].BeginMileage = Slopes[e.NewStartingIndex].EndMileage;
                    }
                    if (FixAltitudePosition >= e.NewStartingIndex) FixAltitudePosition++;
                    if (e.NewStartingIndex < FixAltitudePosition)
                    {
                        if (e.NewStartingIndex < Slopes.Count - 1)
                            Slopes[e.NewStartingIndex].EndAltitude = Slopes[e.NewStartingIndex + 1].BeginAltitude;
                    }
                    else
                    {
                        if (e.NewStartingIndex > 0)
                            Slopes[e.NewStartingIndex].BeginAltitude = Slopes[e.NewStartingIndex - 1].EndAltitude;
                    }
                    UpdateMaxMinAltitude(Slopes[e.NewStartingIndex].BeginAltitude);
                    UpdateMaxMinAltitude(Slopes[e.NewStartingIndex].EndAltitude);
                    SetSlopeTable(Slopes[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex == 0)
                    {
                        Slopes[0].BeginMileage = 0;
                    }
                    else
                    {
                        Slopes[e.OldStartingIndex].BeginMileage = Slopes[e.OldStartingIndex - 1].EndMileage;
                    }
                    if (FixAltitudePosition > e.OldStartingIndex) FixAltitudePosition--;
                    if (FixAltitudePosition < e.OldStartingIndex)
                    {
                        if (e.OldStartingIndex < Slopes.Count && e.OldStartingIndex > 0)
                            Slopes[e.OldStartingIndex].BeginAltitude = Slopes[e.OldStartingIndex - 1].EndAltitude;
                    }
                    else
                    {
                        if (e.OldStartingIndex < Slopes.Count - 1)
                            Slopes[e.OldStartingIndex].EndAltitude = Slopes[e.OldStartingIndex + 1].BeginAltitude;
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    UpdateMaxMinAltitude();
                    break;
            }
        }

        /// <summary>
        /// Slope属性改变处理函数。检查并更新MaxAltitude和MinAltitude。Mileage改变时，更新其他Slope的Mileage。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlopePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Updated用于触发更新坐标轴，放在此处会过于频繁更新，改在UpdateMaxMinAltitude函数中。
            int p;
            switch (e.PropertyName)
            {
                case "BeginAltitude":
                    p = GetPosition(sender);
                    if (p < 1) break;
                    Slopes[p - 1].EndAltitude = ((SlopeViewModel)sender).BeginAltitude;
                    UpdateMaxMinAltitude();
                    //Updated = true;
                    break;
                case "EndMileage":
                    p = GetPosition(sender);
                    if (p < 0 || p > Slopes.Count - 2) break;
                    Slopes[p + 1].BeginMileage = Slopes[p].EndMileage;
                    break;
                case "EndAltitude":
                    p = GetPosition(sender);
                    if (p < 0 || p > Slopes.Count - 2) break;
                    Slopes[p + 1].BeginAltitude = Slopes[p].EndAltitude;
                    UpdateMaxMinAltitude();
                    //Updated = true;
                    break;
                case "Grade":
                case "Length":
                    p = GetPosition(sender);
                    if (p == FixAltitudePosition)
                    {
                        if (FixBeginOrEndAltitude)
                            Slopes[p].SetEndAltitudeByBeginAltitude();
                        else
                            Slopes[p].SetBeginAltitudeByEndAltitude();
                    }
                    else if (p < FixAltitudePosition)
                        Slopes[p].SetBeginAltitudeByEndAltitude();
                    else
                        Slopes[p].SetEndAltitudeByBeginAltitude();
                    UpdateMaxMinAltitude();
                    //Updated = true;
                    break;
            }
            //Updated = true;
        }

        private void ProfilePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SlopeTableOption":
                    break;
            }
        }

        private void SetLeftTop()
        {
            IGraphPosition gp = new ProfileViewModel();
            LeftTop = gp.SetLeftTop(HorizontalAlignment, VerticalAlignment, CanvasActualWidth, CanvasActualHeight, 0, Length * Scale.Horizontal, _maxAltitude * Scale.Vertical, _minAltitude * Scale.Vertical);
        }

        /// <summary>
        /// 按照height和width设置比例后设置Polyline的Points。
        /// 按照Profile的长度、高差计算比例。
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public void SetPolylineFullSize(double height, double width)
        {
            CanvasActualHeight = height;
            CanvasActualWidth = width;
            UpdateMaxMinAltitude();
            Scale.SetScale(height, width, MaxAltitude, MinAltitude, Length);
            SetLeftTop();
        }

        public void UpdateScale()
        {
            foreach (var i in Slopes)
            {
                i.UpdateScale();
            }
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

        public double? GetAltitude(double position)
        {
            double? r = null;
            foreach (SlopeViewModel s in Slopes)
            {
                if (position > s.BeginMileage && position <= s.EndMileage)
                    return s.GetAltitude(position);
            }
            return r;
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
            if (Slopes == null || Slopes.Count == 0) return;
            min = max = Slopes[0].BeginAltitude;
            foreach (SlopeViewModel s in Slopes)
            {
                if (s.EndAltitude > max)
                    max = s.EndAltitude;
                if (s.EndAltitude < min)
                    min = s.EndAltitude;
            }
            if (_maxAltitude != max)
            {
                SetMaxAltitude(max);
                Updated = true;
            }
            if (_minAltitude != min)
            {
                SetMinAltitude(min);
                Updated = true;
            }
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
            double l, g, a = double.MinValue, e;
            Slopes.Clear();

            if (xmlElement.GetAttribute("GradeUnit") == null)
                GradeUnit = 1000;
            else
                GradeUnit = Convert.ToDouble(xmlElement.GetAttribute("GradeUnit"));
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                l = Convert.ToDouble(((XmlElement)xmlNode).GetAttribute("Length"));
                g = Convert.ToDouble(((XmlElement)xmlNode).GetAttribute("Grade")) / GradeUnit;
                if (m == 0)
                {
                    a = Convert.ToDouble(((XmlElement)xmlNode).GetAttribute("BeginAltitude"));
                    BeginAltitude = a;
                }
                e = a - g * l;
                SlopeViewModel slope = new SlopeViewModel(l, g, m, m + l, a, e);
                Slopes.Add(slope);
                a = e;
                m += l;
            }
            /*
            if (Slopes.Count > 0)
                Slopes[0].BeginAltitude = a;*/
            //避免读取数据时修改FixAltitudePosition。
            FixAltitudePosition = Convert.ToInt32(xmlElement.GetAttribute("FixAltitudePosition"));
            FixBeginOrEndAltitude = Convert.ToBoolean(xmlElement.GetAttribute("FixBeginOrEndAltitude"));
            string v = xmlElement.GetAttribute("HumpHeightBeginMileage");
            if (String.IsNullOrEmpty(v))
                HumpMileage = 0;
            else 
                HumpMileage = Convert.ToDouble(v);
            v = xmlElement.GetAttribute("HumpHeightEndMileage");
            if (String.IsNullOrEmpty(v))
                HumpHeightCalculatePointMileage = 0;
            else
                HumpHeightCalculatePointMileage = Convert.ToDouble(v);
            //OnPropertyChanged("Count");
            // Slopes.Add会改变profile.FixAltitudePosition。
        }

        public void WriteXML(XmlElement xmlElement)
        {
            xmlElement.SetAttribute("GradeUnit", GradeUnit.ToString());
            xmlElement.SetAttribute("FixAltitudePosition", FixAltitudePosition.ToString());
            xmlElement.SetAttribute("FixBeginOrEndAltitude", FixBeginOrEndAltitude.ToString());
            xmlElement.SetAttribute("BeginAltitude", BeginAltitude.ToString());
            xmlElement.SetAttribute("HumpHeightBeginMileage", HumpMileage.ToString());
            xmlElement.SetAttribute("HumpHeightEndMileage", HumpHeightCalculatePointMileage.ToString());
            foreach (SlopeViewModel slope in Slopes)
            {
                XmlElement slopeElement = xmlElement.OwnerDocument.CreateElement("Slope");
                slopeElement.SetAttribute("Length", slope.Length.ToString());
                slopeElement.SetAttribute("Grade", slope.Grade.ToString());
                slopeElement.SetAttribute("BeginAltitude", slope.BeginAltitude.ToString());
                xmlElement.AppendChild(slopeElement);
            }
        }

        #endregion
    }


}
