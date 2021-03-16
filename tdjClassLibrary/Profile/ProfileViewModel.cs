using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Resolvers;

namespace tdjClassLibrary.Profile
{
    public class ProfileViewModel : NotifyPropertyChanged
    {
        // 将界面中的控件赋值给这个Polyline后，修改这个Polyline则可同时更新界面控件。
        public Polyline Polyline { get; set; }

       // public PointCollection points { get; set; }

        public ObservableCollection<SlopeViewModel> Slopes;

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

        public ProfileViewModel()
        {
            Slopes = new ObservableCollection<SlopeViewModel>();
            GradeUnit = 1000;
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
            switch (e.PropertyName)
            {
                case "EndAltitude":
                    break;
            }
        }

        /// <summary>
        /// 计算最大和最小高程。
        /// </summary>
        /// <returns></returns>
        public void SetMaxMinAltitude()
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
            MaxAltitude = max;
            MinAltitude = min;
            return;
        }
    
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
                Slopes.Add(slope);
                slope.BeginMileage = m;
                slope.Length = Convert.ToDouble(((XmlElement)xmlNode).GetAttribute("Length"));
                slope.Grade = Convert.ToDouble(((XmlElement)xmlNode).GetAttribute("Grade")) / GradeUnit;
                slope.BeginAltitude = Convert.ToDouble(((XmlElement)xmlNode).GetAttribute("BeginAltitude"));
                //                slope.EndAltitude = slope.BeginAltitude - slope.Length * slope.Grade / ProfileDrawing.GradeUnit;
                m += slope.Length;
                //slope.EndMileage = m;
            }
            // Slopes.Add会改变profile.FixAltitudePosition。
            
        }
    }


}
