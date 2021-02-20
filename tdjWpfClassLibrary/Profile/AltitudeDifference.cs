using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace tdjWpfClassLibrary.Profile
{
    public class AltitudeDifference : NotifyPropertyChanged
    {
        public double HorizontalScale
        {
            get
            {
                return _horizontalScale;
            }
            set
            {
                if (value != _horizontalScale)
                {
                    _horizontalScale = value;
                    OnPropertyChanged("HorizontalScale");
                }
            }
        }
        private double _horizontalScale;

        public double? DesignAltitude
        {
            get
            {
                return _designAltitude;
            }
            set
            {
                if (value != _designAltitude)
                {
                    _designAltitude = value;
                    OnPropertyChanged("DesignAltitude");
                }
            }
        }
        private double? _designAltitude;

        public double? ExistAltitude
        {
            get
            {
                return _existAltitude;
            }
            set
            {
                if (value != _existAltitude)
                {
                    _existAltitude = value;
                    OnPropertyChanged("ExistAltitude");
                }
            }
        }
        private double? _existAltitude;

        public double? Difference
        {
            get
            {
                if (_designAltitude == null || _existAltitude == null)
                    return null;
                return _designAltitude - _existAltitude;
            }
        }

        public double Mileage 
        {
            get { return _mileage; }
            set
            {
                if (value != _mileage)
                {
                    _mileage = value;
                    Position = _mileage * Scale.Horizontal;
                    OnPropertyChanged("Mileage");
                    OnPropertyChanged("Position");
                }
            }
        }
        private double _mileage;

        public double Position { get; set; }

        public AltitudeDifference()
        {
        }
    }

    /// <summary>
    /// 既有纵断面与设计纵断面差值，及显示。
    /// </summary>
    public class AltitudeDifferences
    {
        public AltitudeDifference this[int index]
        {
            get { return Items[index]; }
            set { Items[index] = value; }
        }
        public double HorizontalScale
        {
            get { return _horizontalScale; }
            set
            {
                if (value != _horizontalScale)
                {
                    _horizontalScale = value;
                    UpdateScale();
                }
            }
        }
        private double _horizontalScale;

        public ObservableCollection<AltitudeDifference> Items { get; set; }
        public ProfileViewModel DesignProfile { get; set; }
        public ProfileViewModel ExistProfile { get; set; }

        public AltitudeDifferences()
        {
            Items = new ObservableCollection<AltitudeDifference>();
        }

        /// <summary>
        /// 给DesignProfile和ExistProfile附加事件处理函数。
        /// </summary>
        public void AssignEvent()
        {
            if (DesignProfile == null || ExistProfile == null) return;
            DesignProfile.Slopes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SlopesCollectionChanged);
            foreach (SlopeViewModel s in DesignProfile.Slopes)
            {
                s.PropertyChanged += SlopePropertyChanged;
            }
            ExistProfile.Slopes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SlopesCollectionChanged);
            foreach (SlopeViewModel s in ExistProfile.Slopes)
            {
                s.PropertyChanged += SlopePropertyChanged;
            }
        }

        /// <summary>
        /// Slopes改变时，处理函数。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlopesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Update();
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
                case "BeginAltitude":
                case "EndMileage":
                case "EndAltitude":
                    Update();
                    break;
            }
        }

        /// <summary>
        /// 清除所有AltitudeLabel，然后重新添加。
        /// </summary>
        public void Update()
        {
            Items.Clear();
            List<double> dlist = new List<double>();
            foreach (SlopeViewModel s in DesignProfile.Slopes)
                dlist.Add(s.EndMileage);
            List<double> elist = new List<double>();
            foreach (SlopeViewModel s in ExistProfile.Slopes)
                elist.Add(s.EndMileage);
            List<double> Result = dlist.Union(elist).ToList<double>();          //剔除重复项 
            foreach (double m in Result)
            {
                AltitudeDifference a = new AltitudeDifference();
                a.Mileage = m;
                a.DesignAltitude = DesignProfile.GetAltitude(a.Mileage);
                a.ExistAltitude = ExistProfile.GetAltitude(a.Mileage);
                Items.Add(a);
            }
        }

        /// <summary>
        /// HorizontalScale变化时改变数据表相应数据的位置。
        /// </summary>
        public void UpdateScale()
        {
            foreach (var item in Items)
            {

            }
        }
    }
}
